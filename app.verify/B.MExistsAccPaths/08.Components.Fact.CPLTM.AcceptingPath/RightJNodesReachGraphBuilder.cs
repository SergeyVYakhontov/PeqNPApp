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

  public class RightJNodesReachGraphBuilder
  {
    #region Ctors

    public RightJNodesReachGraphBuilder(
      MEAPContext meapContext,
      long kStep,
      FwdBkwdNCommsGraphPair leftFwdBkwdNCommsGraphPair,
      FwdBkwdNCommsGraphPair rightFwdBkwdNCommsGraphPair,
      LRJointNodesReachGraphPair lrJointNodesReachGraphPair)
    {
      this.meapContext = meapContext;
      this.CPLTMInfo = meapContext.MEAPSharedContext.CPLTMInfo;
      this.kStep = kStep;
      this.leftFwdBkwdNCommsGraphPair = leftFwdBkwdNCommsGraphPair;
      this.rightFwdBkwdNCommsGraphPair = rightFwdBkwdNCommsGraphPair;
      this.lrJointNodesReachGraphPair = lrJointNodesReachGraphPair;
    }

    #endregion

    #region public members

    public void Run()
    {
      log.Info($"Creating RightJNodesReachGraphs at kStep = {kStep}");

      rightFwdKStepSequence = CPLTMInfo.FwdCommsKStepSequence(kStep + 1).ToArray();
      rightBkwdKStepSequence = CPLTMInfo.BkwdCommsKStepSequence(kStep + 1).ToArray();

      ConstructRightGraph();
    }

    #endregion

    #region private members

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private readonly MEAPContext meapContext;
    private readonly ICPLTMInfo CPLTMInfo;
    private readonly long kStep;
    private long[] rightFwdKStepSequence;
    private long[] rightBkwdKStepSequence;
    private readonly FwdBkwdNCommsGraphPair leftFwdBkwdNCommsGraphPair;
    private readonly FwdBkwdNCommsGraphPair rightFwdBkwdNCommsGraphPair;
    private readonly LRJointNodesReachGraphPair lrJointNodesReachGraphPair;

    private void ConstructRightGraph()
    {
      NCGraphType rightBkwdNCommsGraph = leftFwdBkwdNCommsGraphPair.FwdNestedCommsGraph;

      ReachGraphType leftJointNodesReachGraph = lrJointNodesReachGraphPair.LeftJointNodesReachGraph;
      ReachGraphType rightJointNodesReachGraph = lrJointNodesReachGraphPair.RightJointNodesReachGraph;

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
