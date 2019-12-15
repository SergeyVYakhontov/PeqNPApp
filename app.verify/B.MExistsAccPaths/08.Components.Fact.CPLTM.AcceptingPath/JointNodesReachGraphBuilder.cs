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

  public class JointNodesReachGraphBuilder
  {
    #region Ctors

    public JointNodesReachGraphBuilder(
      MEAPContext meapContext,
      long kStep,
      FwdBkwdNCommsGraphPair leftFwdBkwdNCommsGraphPair,
      FwdBkwdNCommsGraphPair rightFwdBkwdNCommsGraphPair)
    {
      this.meapContext = meapContext;
      this.CPLTMInfo = meapContext.MEAPSharedContext.CPLTMInfo;
      this.kStep = kStep;
      this.leftFwdBkwdNCommsGraphPair = leftFwdBkwdNCommsGraphPair;
      this.rightFwdBkwdNCommsGraphPair = rightFwdBkwdNCommsGraphPair;

      this.LRJointNodesReachGraphPair = new LRJointNodesReachGraphPair();
    }

    #endregion

    #region public members

    public LRJointNodesReachGraphPair LRJointNodesReachGraphPair { get; }

    public void Run()
    {
      log.Info($"Creating JointNodesReachGraphs at kStep = {kStep}");

      leftFwdKStepSequence = CPLTMInfo.FwdCommsKStepSequence(kStep).ToArray();
      leftBkwdKStepSequence = CPLTMInfo.BkwdCommsKStepSequence(kStep).ToArray();

      rightFwdKStepSequence = CPLTMInfo.FwdCommsKStepSequence(kStep + 1).ToArray();
      rightBkwdKStepSequence = CPLTMInfo.BkwdCommsKStepSequence(kStep + 1).ToArray();

      log.Info("Creating LeftJointNodesReachGraph");
      ConstructLeftGraph();

      log.Info("Creating RightJointNodesReachGraph");
      ConstructRightGraph();
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
    private long[] rightFwdKStepSequence;
    private long[] rightBkwdKStepSequence;
    private readonly FwdBkwdNCommsGraphPair leftFwdBkwdNCommsGraphPair;
    private readonly FwdBkwdNCommsGraphPair rightFwdBkwdNCommsGraphPair;

    private void ConstructLeftGraph()
    {
      NCGraphType leftBkwdNCommsGraph = leftFwdBkwdNCommsGraphPair.BkwdNestedCommsGraph;

      ReachGraphType leftJointNodesReachGraph = LRJointNodesReachGraphPair.LeftJointNodesReachGraph;
      ReachGraphType rightJointNodesReachGraph = LRJointNodesReachGraphPair.RightJointNodesReachGraph;

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
        }
      }
    }

    private void ConstructRightGraph()
    {
      NCGraphType rightBkwdNCommsGraph = leftFwdBkwdNCommsGraphPair.FwdNestedCommsGraph;

      ReachGraphType leftJointNodesReachGraph = LRJointNodesReachGraphPair.LeftJointNodesReachGraph;
      ReachGraphType rightJointNodesReachGraph = LRJointNodesReachGraphPair.RightJointNodesReachGraph;

      SortedDictionary<long, SortedSet<long>> nodeVLevels =
        meapContext.MEAPSharedContext.NodeLevelInfo.NodeVLevels;
      long fwdKStepLastIndex = rightFwdKStepSequence.LastIndex();

      for (long i = 0; i <= (fwdKStepLastIndex - 1); i++)
      {
        foreach (long uId in nodeVLevels[rightFwdKStepSequence[i]])
        {
          if (!meapContext.TArbSeqCFG.HasNode(uId))
          {
            continue;
          }

          DAGNode uNode = meapContext.TArbSeqCFG.GetNode(uId);

          if (!rightFwdBkwdNCommsGraphPair.FwdCFGNodeToNCGNodesMap.ContainsKey(uId))
          {
            continue;
          }

          List<long> ncgNodes = rightFwdBkwdNCommsGraphPair.FwdCFGNodeToNCGNodesMap[uId];
        }
      }
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
