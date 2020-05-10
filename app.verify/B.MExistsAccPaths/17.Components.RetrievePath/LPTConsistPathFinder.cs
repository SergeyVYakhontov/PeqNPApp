////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Ninject;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public class LPTConsistPathFinder : TConsistPathFinder
  {
    #region Ctors

    public LPTConsistPathFinder(
      MEAPContext meapContext,
      TapeSegContext tapeSegContext,
      LinEquationContext linEquationContext)
      : base(meapContext, tapeSegContext, linEquationContext)
    {
      this.configuration = Core.AppContext.GetConfiguration();
    }

    #endregion

    #region public members

    public override void FindTConsistPath()
    {
      log.Debug("Finding tconsist path ...");

      TraceKZetaSet();
      TraceLPSolution();

      tapeSegContext.TapeSegTConsistPath = new List<long>();
      tapeSegContext.TapeSegPathFound = false;

      List<long> partialTConsistPath = tapeSegContext.PartialTConsistPath;
      long headNodeId = partialTConsistPath.Last();

      DAG.FindPath_Greedy(
        meapContext.TArbSeqCFG.NodeEnumeration[headNodeId],
        meapContext.TArbSeqCFG.t,
        GraphDirection.Forward,
        IsIntegralFlowNode,
        (_) => true,
        (_) => { },
        (_) => { },
        out tConsistPath, out integralKPathFound);

      partialTConsistPath.AddRange(tConsistPath.Skip(1));

      if (integralKPathFound)
      {
        tapeSegContext.TapeSegTConsistPath = new List<long>(partialTConsistPath);
        tapeSegContext.TapeSegPathFound = true;
      }
      else
      {
        log.DebugFormat("Integral KPath not found");

        long tailNodeId = partialTConsistPath.Last();
        DAGNode tailNode = meapContext.TArbSeqCFG.NodeEnumeration[tailNodeId];

        tapeSegContext.TConsistPathNodes = tailNode.OutEdges.Where(
          e => IsPositiveFlowNode(e.ToNode)).Select(e => e.ToNode.Id).ToList();

        tapeSegContext.TapeSegPathFound = false;
      }
    }

    #endregion

    #region private members

    private readonly IReadOnlyKernel configuration;

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType);

    private List<long> tConsistPath;
    private bool integralKPathFound;

    private void TraceKZetaSet()
    {
      log.Debug("TraceKZetaSet");

      foreach (KeyValuePair<long, SortedSet<long>> idCommList in tapeSegContext.KSetZetaSubset)
      {
        log.DebugFormat("zeta = {0}: ", idCommList.Key);

        foreach (long commodityId in idCommList.Value)
        {
          log.Debug(commodityId + " ");
        }
      }
    }

    private void TraceLPSolution()
    {
      log.Debug("TraceLPSolution");

      DAGEquationsSet eqsSet = linEquationContext.TArbSeqCFGLinProgEqsSet;

      foreach (DAGNode u in meapContext.TArbSeqCFG.Nodes)
      {
        long uNodeId = u.Id;

        if (tapeSegContext.TArbSeqCFGUnusedNodes.Contains(uNodeId))
        {
          continue;
        }

        double nodeValue = tapeSegContext.TCPELinProgSolution[eqsSet.NodeToVar[u.Id]];

        log.DebugFormat("{0}:{1} ", uNodeId, nodeValue);
      }
    }

    private bool IsIntegralFlowNode(DAGNode u)
    {
      DAGEquationsSet eqsSet = linEquationContext.TArbSeqCFGLinProgEqsSet;

      if (tapeSegContext.TArbSeqCFGUnusedNodes.Contains(u.Id))
      {
        return false;
      }

      ICommonOptions commonOptions = configuration.Get<ICommonOptions>();

      return (tapeSegContext.TCPELinProgSolution[eqsSet.NodeToVar[u.Id]] >
        (1.0 - commonOptions.LinProgEpsilon));
    }

    private bool IsPositiveFlowNode(DAGNode u)
    {
      DAGEquationsSet eqsSet = linEquationContext.TArbSeqCFGLinProgEqsSet;

      if (tapeSegContext.TArbSeqCFGUnusedNodes.Contains(u.Id))
      {
        return false;
      }

      ICommonOptions commonOptions = configuration.Get<ICommonOptions>();

      return (tapeSegContext.TCPELinProgSolution[eqsSet.NodeToVar[u.Id]] >
        commonOptions.LinProgEpsilon);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
