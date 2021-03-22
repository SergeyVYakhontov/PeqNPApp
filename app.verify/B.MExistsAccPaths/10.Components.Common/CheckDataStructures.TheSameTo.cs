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

  public class CheckDataStructuresTheSameTo : CheckDataStructures
  {
    #region public members

    public override void CheckTASGNodesHaveTheSameSymbol(MEAPContext meapContext)
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

    public override void CheckNCGNodesHaveTheSameSymbol(MEAPContext meapContext)
    {
      log.Info("CheckNCGNodesHaveTheSameSymbolFrom");

      foreach (KeyValuePair<long, FwdBkwdNCommsGraphPair> ncgItemPair in
        meapContext.muToNestedCommsGraphPair)
      {
        NCGraphType bkwdNestedCommsGraph = ncgItemPair.Value.BkwdNestedCommsGraph;

        foreach (KeyValuePair<long, DAGNode> ncgNodeItemPair in bkwdNestedCommsGraph.NodeEnumeration)
        {
          long uNCGNodeId = ncgNodeItemPair.Key;
          DAGNode uNCGNode = ncgNodeItemPair.Value;

          if (!meapContext.Commodities.TryGetValue(uNCGNodeId, out Commodity? uComm))
          {
            continue;
          }

          long uNodeId = uComm.tNodeId;
          ComputationStep uCompStep = meapContext.TArbSeqCFG.IdToNodeInfoMap[uNodeId].CompStep;

          bool firstEdge = true;
          int sTo = OneTapeTuringMachine.blankSymbol;

          foreach (DAGEdge e in uNCGNode.InEdges)
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

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
