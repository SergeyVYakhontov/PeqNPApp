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
  using NCGraphType = TypedDAG<NestedCommsGraphNodeInfo, StdEdgeInfo>;
  using ReachGraphType = TypedDAG<JNodesReachGraphNodeInfo, JNodesReachGraphEdgeInfo>;

  public class LeftJNodesReachGraphBuilder
  {
    #region Ctors

    public LeftJNodesReachGraphBuilder(
      MEAPContext meapContext,
      long kStep,
      FwdBkwdNCommsGraphPair leftFwdBkwdNCommsGraphPair,
      LRJointNodesReachGraphPair lrJointNodesReachGraphPair)
    {
      this.meapContext = meapContext;
      this.CPLTMInfo = meapContext.MEAPSharedContext.CPLTMInfo;
      this.kStep = kStep;
      this.leftFwdBkwdNCommsGraphPair = leftFwdBkwdNCommsGraphPair;
      this.lrJointNodesReachGraphPair = lrJointNodesReachGraphPair;
    }

    #endregion

    #region public members

    public void Run()
    {
      log.Info($"Creating LeftJNodesReachGraphs at kStep = {kStep}");

      leftFwdKStepSequence = CPLTMInfo.FwdCommsKStepSequence(kStep).ToArray();
      leftBkwdKStepSequence = CPLTMInfo.BkwdCommsKStepSequence(kStep).ToArray();

      ConstructLeftGraph();
    }

    #endregion

    #region private members

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private readonly MEAPContext meapContext;
    private readonly ICPLTMInfo CPLTMInfo;
    private readonly long kStep;
    private long[] leftFwdKStepSequence;
    private long[] leftBkwdKStepSequence;
    private readonly FwdBkwdNCommsGraphPair leftFwdBkwdNCommsGraphPair;
    private readonly LRJointNodesReachGraphPair lrJointNodesReachGraphPair;

    private void ConstructLeftGraph()
    {
      NCGraphType leftBkwdNCommsGraph = leftFwdBkwdNCommsGraphPair.BkwdNestedCommsGraph;

      ReachGraphType leftJointNodesReachGraph = lrJointNodesReachGraphPair.LeftJointNodesReachGraph;

      SortedDictionary<long, SortedSet<long>> nodeVLevels =
        meapContext.MEAPSharedContext.NodeLevelInfo.NodeVLevels;
      long bkwdKStepLastIndex = leftBkwdKStepSequence.LastIndex();

      for (long i = (bkwdKStepLastIndex - 1); i >= 0; i--)
      {
        foreach (long uId in nodeVLevels[leftBkwdKStepSequence[i]])
        {
          if (!meapContext.TArbSeqCFG.HasNode(uId))
          {
            continue;
          }

          DAGNode uNode = meapContext.TArbSeqCFG.GetNode(uId);

          if (!leftFwdBkwdNCommsGraphPair.BkwdCFGNodeToNCGNodesMap.ContainsKey(uId))
          {
            continue;
          }

          List<long> ncgNodes = leftFwdBkwdNCommsGraphPair.BkwdCFGNodeToNCGNodesMap[uId];
          NCommsGraphJointNode ncgJointNode = meapContext.CfgNodeIdToNCGJointNode[uId];
        }
      }
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
