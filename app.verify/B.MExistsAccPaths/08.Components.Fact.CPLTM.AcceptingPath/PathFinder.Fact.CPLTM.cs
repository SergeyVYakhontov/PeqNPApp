////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EnsureThat;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public class PathFinderFactCPLTM
  {
    #region public members

    public void Run()
    {
      log.Info("Finding shortest path");

      //DetermineTConsistPath();
      //ExtractTConsistSeq()

      CopyResult();

      meapContext.AcceptingNodes.Clear();
      meapContext.UnusedNodes.Clear();
    }

    #endregion

    #region Ctors

    public PathFinderFactCPLTM(MEAPContext meapContext)
    {
      this.meapContext = meapContext;
    }

    #endregion

    #region private members

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private readonly MEAPContext meapContext;

    //private bool pathExist;
    //private bool pathFound;
    private readonly List<long> tConsistPath = new List<long>();
    private readonly int[] output = Array.Empty<int>();

    /*private void ExtractTConsistSeq()
    {
      log.Info("MExistsAcceptingPath.Compute: path");

      tConsistPath.ForEach(s =>
      {
        log.InfoFormat("node = {0}", s.ToString());
      });

      long prevNode = tConsistPath[0];

      foreach (long currentNode in tConsistPath.Skip(1))
      {
        DAGEdge e = meapContext.TArbSeqCFG.NodePairEnumeration[new KeyValuePair<long, long>(prevNode, currentNode)];

        prevNode = currentNode;
      }

      List<KeyValuePair<long, ComputationStep>> pathCompSteps =
        tConsistPath.Select(uId =>
          new KeyValuePair<long, ComputationStep>(
            uId, meapContext.TArbitrarySeqGraph.IdToNodeInfoMap[uId].CompStep))
              .ToList();

      pathCompSteps.ForEach(s =>
      {
        log.InfoFormat(
          "id = {0}, comp.step = {1}", s.Key.ToString(), s.Value.ToString());
      });

      TMInstance tmInstance = new TMInstance(
        meapContext.MEAPSharedContext.MNP,
        meapContext.MEAPSharedContext.Input);

      meapContext.MEAPSharedContext.MNP.PrepareTapeFwd(
        meapContext.MEAPSharedContext.Input,
        tmInstance);

      foreach (KeyValuePair<long, ComputationStep> compStepP in pathCompSteps)
      {
        ComputationStep compStep = compStepP.Value;

        int currentTapeSym = tmInstance.TapeSymbol((int)compStep.kappaTape);

        Ensure.That(currentTapeSym).Is(compStep.s);

        TMInstance.MoveToNextConfiguration(
          new StateSymbolDirectionTriple
            (
              state: compStep.qNext,
              symbol: compStep.sNext,
              direction: compStep.m,
              shift: compStep.Shift
            ),
          tmInstance);

        if (tmInstance.IsInFinalState())
        {
          break;
        }
      }

      output = meapContext.MEAPSharedContext.MNP.GetOutput(
        tmInstance,
        meapContext.mu,
        (uint)meapContext.MEAPSharedContext.Input.Length);
    }*/

    private void CopyResult()
    {
      //meapContext.PathExists = pathExist;
      //meapContext.PathFound = pathFound;
      meapContext.TConsistPath = new List<long>(tConsistPath);
      meapContext.Output = AppHelper.CreateArrayCopy(output);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
