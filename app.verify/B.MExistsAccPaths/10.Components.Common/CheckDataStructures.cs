////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using EnsureThat;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  using NCGraphType = TypedDAG<NestedCommsGraphNodeInfo, StdEdgeInfo>;

  public static class CheckDataStructures
  {
    #region public members

    public static void CheckTASGHasNoBackAndCrossEdges(DAG dag)
    {
      log.Info("CheckTASGHasNoBackAndCrossEdges");

      SortedDictionary<long, SortedSet<long>> VLevelSets =
        new SortedDictionary<long,SortedSet<long>>();

      DAG.DFS(
        dag.s,
        GraphDirection.Forward,
        (u, level) =>
        {
          AppHelper.TakeValueByKey(VLevelSets, level,
            () => new SortedSet<long>()).Add(u.Id);
        },
        (_, __) => { }
        );

      DAG.ClassifyDAGEdges(
        dag,
        VLevelSets,
        out SortedSet<long> backEdges,
        out SortedSet<long> crossEdges);

      Ensure.That(backEdges.Any()).IsFalse();
      Ensure.That(crossEdges.Any()).IsFalse();
    }

    public static void CheckTASGNodesHaveTheSameSymbolFrom(
      MEAPContext meapContext)
    {
      log.Info("CheckTASGNodesHaveTheSameSymbolFrom");

      foreach (KeyValuePair<long, DAGNode> itemPair in meapContext.TArbSeqCFG.NodeEnumeration)
      {
        long uNodeId = itemPair.Key;

        if (uNodeId == meapContext.TArbSeqCFG.GetSourceNodeId())
        {
          continue;
        }

        DAGNode uNode = itemPair.Value;
        ComputationStep uCompStep = meapContext.TArbSeqCFG.IdToNodeInfoMap[uNodeId].CompStep;

        bool firstEdge = true;
        int sTo = OneTapeTuringMachine.blankSymbol;

        foreach (DAGEdge e in uNode.OutEdges)
        {
          long vNodeId = e.ToNode.Id;
          ComputationStep vCompStep = meapContext.TArbSeqCFG.IdToNodeInfoMap[vNodeId].CompStep;

          if (vNodeId == meapContext.TArbSeqCFG.GetSinkNodeId())
          {
            continue;
          }

          if (firstEdge)
          {
            sTo = vCompStep.s;
            firstEdge = false;
          }
          else
          {
            Ensure.That(vCompStep.s).Is(sTo);
          }
        }
      }
    }

    public static void CheckNCGNodesHaveTheSameSymbolFrom(
      MEAPContext meapContext)
    {
      log.Info("CheckNCGNodesHaveTheSameSymbolFrom");

      foreach (KeyValuePair<long, FwdBkwdNCommsGraphPair> ncgItemPair in
        meapContext.muToNestedCommsGraphPair)
      {
        NCGraphType bkwdNestedCommsGraph = ncgItemPair.Value.BkwdNestedCommsGraph;

        foreach(KeyValuePair<long, DAGNode> ncgNodeItemPair in bkwdNestedCommsGraph.NodeEnumeration)
        {
          long uNCGNodeId = ncgNodeItemPair.Key;
          DAGNode ncgNode = ncgNodeItemPair.Value;

          if(!meapContext.Commodities.TryGetValue(uNCGNodeId, out Commodity uComm))
          {
            continue;
          }

          long uNodeId = uComm.tNodeId;
          ComputationStep uCompStep = meapContext.TArbSeqCFG.IdToNodeInfoMap[uNodeId].CompStep;

          bool firstEdge = true;
          int sTo = OneTapeTuringMachine.blankSymbol;

          foreach (DAGEdge e in ncgNode.InEdges)
          {
            long vNCGNodeId = e.FromNode.Id;
            Commodity vComm = meapContext.Commodities[vNCGNodeId];
            long vNodeId = vComm.tNodeId;
            ComputationStep vCompStep = meapContext.TArbSeqCFG.IdToNodeInfoMap[vNodeId].CompStep;

            if (vNodeId == meapContext.TArbSeqCFG.GetSinkNodeId())
            {
              continue;
            }

            if (firstEdge)
            {
              sTo = vCompStep.s;
              firstEdge = false;
            }
            else
            {
              Ensure.That(vCompStep.s).Is(sTo);
            }
          }
        }
      }
    }

    public static void CheckCommoditiesHaveNoSingleNodes(
      MEAPContext meapContext)
    {
      log.Info("CheckCommoditiesHaveNoSingleNodes");

      foreach (Commodity commodity in meapContext.Commodities.Values)
      {
        Ensure.That(IsGraphHasSingleNode(commodity.Gi)).IsFalse();
      }
    }

    #endregion

    #region private members

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private static bool IsGraphHasSingleNode(DAG dag)
    {
      return dag.GetInnerNodeIds().Any(uNodeId =>
      {
        DAGNode uNode = dag.NodeEnumeration[uNodeId];

        return ((!uNode.InEdges.Any()) || (!uNode.OutEdges.Any()));
      });
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
