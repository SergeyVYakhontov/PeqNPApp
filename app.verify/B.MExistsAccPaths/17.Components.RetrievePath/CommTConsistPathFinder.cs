////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public class CommTConsistPathFinder : TConsistPathFinder
  {
    #region Ctors

    public CommTConsistPathFinder(
      MEAPContext meapContext,
      TapeSegContext tapeSegContext,
      LinEquationContext linEquationContext)
      : base(meapContext, tapeSegContext, linEquationContext)
    { }

    #endregion

    #region public members

    public override void FindTConsistPath()
    {
      tapeSegContext.TapeSegTConsistPath = new List<long>();
      tapeSegContext.TapeSegPathFound = false;

      if (tapeSegContext.KSetZetaSubset.Count == 1)
      {
        if (FindPathInCaseSingleZeta())
        {
          tapeSegContext.TapeSegPathExists = true;
          tapeSegContext.TapeSegPathFound = true;

          log.Info("Found path in case of single zeta");

          return;
        }
      }

      List<long> partialTConsistPath = tapeSegContext.PartialTConsistPath;

      while (true)
      {
        if (!SelectNode(partialTConsistPath, out long initNodeId))
        {
          log.Info("Path not found (select node)");

          return;
        }

        DAGNode initNode = meapContext.TArbSeqCFG.NodeEnumeration[initNodeId];
        processedNodes.Add(initNodeId);

        (List<long> tConsistPath, bool KPathFound) = DAG.FindPath_Greedy(
          initNode,
          meapContext.TArbSeqCFG.t,
          GraphDirection.Forward,
          u => !tapeSegContext.TArbSeqCFGUnusedNodes.Contains(u.Id),
          d => NodeInChain(d.ToNode),
          u => processedNodes.Add(u.Id),
          (_) => { });

        if (KPathFound)
        {
          tapeSegContext.TapeSegTConsistPath.AddRange(partialTConsistPath);
          tapeSegContext.TapeSegTConsistPath.AddRange(tConsistPath);

          tapeSegContext.TapeSegPathExists = true;
          tapeSegContext.TapeSegPathFound = true;

          return;
        }
      }
    }

    #endregion

    #region private members

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType);

    private readonly SortedSet<long> processedNodes = new();

    private bool FindPathInCaseSingleZeta()
    {
      GraphTConZetaBuilder gtczBuilder = new(meapContext);
      TypedDAG<GTCZNodeInfo, StdEdgeInfo> gtcz = gtczBuilder.Run(tapeSegContext.KSetZetaSubset.First().Value);

      (List<long> tConsistPath, bool KPathFound) = DAG.FindPath_Greedy(
         gtcz.s,
        gtcz.t,
        GraphDirection.Forward,
        u => !tapeSegContext.TArbSeqCFGUnusedNodes.Contains(u.Id),
        _ => true,
        _ => { },
        _ => { });

      tapeSegContext.TapeSegTConsistPath = new List<long>(tConsistPath);
      tapeSegContext.TapeSegPathFound = KPathFound;

      if (!KPathFound)
      {
        log.Info("Not found path in case of single zeta");
      }

      return KPathFound;
    }

    private long InNodesCount(DAGNode u)
    {
      return u.InEdges.Count(e => !tapeSegContext.TArbSeqCFGUnusedNodes.Contains(e.FromNode.Id));
    }

    private long OutNodesCount(DAGNode u)
    {
      return u.OutEdges.Count(e => !tapeSegContext.TArbSeqCFGUnusedNodes.Contains(e.ToNode.Id));
    }

    private bool SelectNode(List<long> partialTConsistPath, out long selectedNode)
    {
      SortedDictionary<long, DAGNode> nodeEnumeration = meapContext.TArbSeqCFG.NodeEnumeration;
      DAGNode headNode = nodeEnumeration[partialTConsistPath.Last()];

      foreach (DAGEdge e in headNode.OutEdges)
      {
        DAGNode vNode = e.ToNode;

        if (!processedNodes.Contains(vNode.Id))
        {
          if (meapContext.TArbSeqCFG.IsSinkNode(vNode.Id) ||
              ((InNodesCount(vNode) == 1) && (OutNodesCount(vNode) == 1)))
          {
            selectedNode = vNode.Id;

            return true;
          }
        }
      }

      selectedNode = -1;

      return false;
    }

    private bool NodeInChain(DAGNode u)
    {
      long uNodeId = u.Id;

      if (meapContext.TArbSeqCFG.IsSourceNode(uNodeId))
      {
        return true;
      }

      if (meapContext.TArbSeqCFG.IsSinkNode(uNodeId))
      {
        return true;
      }

      if (processedNodes.Contains(u.Id))
      {
        return false;
      }

      SortedDictionary<long, DAGNode> nodeEnumeration = meapContext.TArbSeqCFG.NodeEnumeration;
      DAGNode uNode = nodeEnumeration[uNodeId];

      return (InNodesCount(uNode) == 1) && (OutNodesCount(uNode) == 1);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
