////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ninject;
using EnsureThat;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public class ComputeKStepDUPairs
  {
    #region Ctors

    public ComputeKStepDUPairs(
      MEAPContext meapContext,
      Tuple<long, long, long> kSteps)
    {
      this.meapContext = meapContext;
      this.kSteps = kSteps;
      this.CPLTMInfo = meapContext.MEAPSharedContext.CPLTMInfo;
      this.nodeVLevels = meapContext.MEAPSharedContext.NodeLevelInfo.NodeVLevels;
    }

    #endregion

    #region public members

    public void Run()
    {
      TypedDAG<TASGNodeInfo, StdEdgeInfo> cfg = meapContext.TArbSeqCFG;
      SortedDictionary<long, TASGNodeInfo> idToInfoMap = meapContext.TArbSeqCFG.IdToNodeInfoMap;

      long kStepA = kSteps.Item1;
      long kStepB = kSteps.Item2;
      long kStepC = kSteps.Item3;

      foreach (long nodeId in nodeVLevels[kStepB])
      {
        currDUPairs.Add(new KeyValuePair<long, long>(nodeId, nodeId));
      }

      for(long i = kStepB; i >= (kStepA + 1); i--)
      {
        foreach(KeyValuePair<long, long> duPair in currDUPairs)
        {
          long uNodeId = duPair.Key;
          long vNodeId = duPair.Value;

          DAGNode uNode = cfg.GetNode(uNodeId);
          DAGNode vNode = cfg.GetNode(vNodeId);

          foreach(DAGEdge uEdge in uNode.InEdges)
          {
            foreach (DAGEdge vEdge in vNode.OutEdges)
            {
              long defNodeId = uEdge.FromNode.Id;
              long useNodeId = vEdge.ToNode.Id;

              ComputationStep defCompStep = idToInfoMap[defNodeId].CompStep;
              ComputationStep useCompStep = idToInfoMap[useNodeId].CompStep;

              if((defCompStep.kappaTape == useCompStep.kappaTape) &&
                 (defCompStep.sNext == useCompStep.s))
              {
                nextDUPairs.Add(new KeyValuePair<long, long>(defNodeId, useNodeId));
              }
            }
          }
        }

        currDUPairs.Clear();
        nextDUPairs.ForEach(t => currDUPairs.Add(t));

        foreach(KeyValuePair<long, long> duPair in currDUPairs)
        {
          long defNodeId = duPair.Key;
          long useNodeId = duPair.Value;
          ComputationStep defCompStep = idToInfoMap[defNodeId].CompStep;

          meapContext.TConsistPairSet.Add(
            new CompStepNodePair(
              variable: defCompStep.kappaTape,
              uNode: duPair.Key,
              vNode: duPair.Value
            ));

          meapContext.TConsistPairCount++;
        }
      }
    }

    #endregion

    #region private members

    private readonly MEAPContext meapContext;
    private readonly Tuple<long, long, long> kSteps;
    private readonly ICPLTMInfo CPLTMInfo;
    private readonly SortedDictionary<long, SortedSet<long>> nodeVLevels;

    private readonly SortedSet<KeyValuePair<long, long>> currDUPairs = new(new KeyValueComparer());
    private readonly SortedSet<KeyValuePair<long, long>> nextDUPairs = new(new KeyValueComparer());

    #endregion
  }
}
