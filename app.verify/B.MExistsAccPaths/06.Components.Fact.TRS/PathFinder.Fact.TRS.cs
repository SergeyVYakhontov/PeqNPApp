﻿////////////////////////////////////////////////////////////////////////////////////////////////////

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
  public class PathFinderFactTRS
  {
    #region public members

    public void Run()
    {
      log.Info("Finding shortest path");

      CopyResult();

      meapContext.AcceptingNodes.Clear();
      meapContext.UnusedNodes.Clear();
    }

    #endregion

    #region Ctors

    public PathFinderFactTRS(MEAPContext meapContext)
    {
      this.meapContext = meapContext;
    }

    #endregion

    #region private members

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private readonly MEAPContext meapContext;

    private bool pathExists = false;
    private bool pathFound = false;
    private List<long> tConsistPath = new List<long>();
    private int[] output = Array.Empty<int>();

    private void ExtractTConsistSeq()
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
            uId, meapContext.TArbitrarySeqGraph.IdToInfoMap[uId].CompStep))
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

        int currentTapeSym = tmInstance.TapeSymbol(meapContext.MEAPSharedContext.Input, (int)compStep.kappaTape);

        Ensure.That(currentTapeSym).Is(compStep.s);

        TMInstance.MoveToNextConfiguration(
          new StateSymbolDirectionTriple()
          {
            State = compStep.qNext,
            Symbol = compStep.sNext,
            Direction = compStep.m,
            Shift = compStep.Shift
          },
          tmInstance);

        if (tmInstance.IsInFinalState())
        {
          break;
        }
      }

      output = meapContext.MEAPSharedContext.MNP.GetOutput(
        tmInstance, meapContext.mu, meapContext.MEAPSharedContext.Input.Length);
    }

    private void CopyResult()
    {
      meapContext.PathExists = pathExists;
      meapContext.PathFound = pathFound;
      meapContext.TConsistPath = new List<long>(tConsistPath);
      meapContext.Output = AppHelper.CreateArrayCopy(output);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////