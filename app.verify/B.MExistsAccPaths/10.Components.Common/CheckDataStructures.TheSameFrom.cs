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

  public class CheckDataStructuresTheSameFrom : CheckDataStructures
  {
    #region public members

    public override void CheckTASGNodesHaveTheSameSymbol(MEAPContext meapContext)
    {
      log.Info("CheckTASGNodesHaveTheSameSymbolFrom");

      foreach (KeyValuePair<long, DAGNode> itemPair in meapContext.TArbSeqCFG.NodeEnumeration)
      {
        long vNodeId = itemPair.Key;

        if (vNodeId == meapContext.TArbSeqCFG.GetSinkNodeId())
        {
          continue;
        }

        DAGNode vNode = itemPair.Value;
        ComputationStep vCompStep = meapContext.TArbSeqCFG.IdToNodeInfoMap[vNodeId].CompStep;

        bool firstEdge = true;
        int sFrom = OneTapeTuringMachine.blankSymbol;

        foreach (DAGEdge e in vNode.InEdges)
        {
          long uNodeId = e.FromNode.Id;
          ComputationStep uCompStep = meapContext.TArbSeqCFG.IdToNodeInfoMap[uNodeId].CompStep;

          if (uNodeId == meapContext.TArbSeqCFG.GetSourceNodeId())
          {
            continue;
          }

          if (firstEdge)
          {
            sFrom = uCompStep.sNext;
            firstEdge = false;
          }
          else
          {
            Ensure.That(uCompStep.sNext).Is(sFrom);
          }
        }
      }
    }

    public override void CheckNCGNodesHaveTheSameSymbol(MEAPContext meapContext)
    {
      log.Info("CheckNCGNodesHaveTheSameSymbolFrom");

      foreach (KeyValuePair<long, FwdBkwdNCommsGraphPair> ncgItemPair in
        meapContext.muToNestedCommsGraphPair)
      {
        NCGraphType fwdNestedCommsGraph = ncgItemPair.Value.FwdNestedCommsGraph;

        foreach (KeyValuePair<long, DAGNode> ncgNodeItemPair in fwdNestedCommsGraph.NodeEnumeration)
        {
          long uNCGNodeId = ncgNodeItemPair.Key;
          DAGNode uNCGNode = ncgNodeItemPair.Value;

          if (!meapContext.Commodities.TryGetValue(uNCGNodeId, out Commodity uComm))
          {
            continue;
          }

          long uNodeId = uComm.sNodeId;
          ComputationStep uCompStep = meapContext.TArbSeqCFG.IdToNodeInfoMap[uNodeId].CompStep;

          if (uNodeId == meapContext.TArbSeqCFG.GetSourceNodeId())
          {
            continue;
          }

          bool firstEdge = true;
          int sFrom = OneTapeTuringMachine.blankSymbol;

          foreach (DAGEdge e in uNCGNode.OutEdges)
          {
            long vNCGNodeId = e.FromNode.Id;
            Commodity vComm = meapContext.Commodities[vNCGNodeId];
            long vNodeId = vComm.sNodeId;
            ComputationStep vCompStep = meapContext.TArbSeqCFG.IdToNodeInfoMap[vNodeId].CompStep;

            if (firstEdge)
            {
              sFrom = vCompStep.sNext;
              firstEdge = false;
            }
            else
            {
              Ensure.That(vCompStep.sNext).Is(sFrom);
            }
          }
        }
      }
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
