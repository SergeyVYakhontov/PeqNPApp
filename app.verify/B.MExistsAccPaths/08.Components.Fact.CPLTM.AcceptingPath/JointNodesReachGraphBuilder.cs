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
      FwdBkwdNCommsGraphPair fwdBkwdNCommsGraphPair)
    {
      this.meapContext = meapContext;
      this.CPLTMInfo = meapContext.MEAPSharedContext.CPLTMInfo;
      this.kStep = kStep;
      this.fwdKStepSequence = CPLTMInfo.FwdCommsKStepSequence(kStep).ToArray();
      this.fwdBkwdNCommsGraphPair = fwdBkwdNCommsGraphPair;
      this.bkwdKStepSequence = CPLTMInfo.BkwdCommsKStepSequence(kStep).ToArray();

      this.LRJointNodesReachGraphPair = new LRJointNodesReachGraphPair();
    }

    #endregion

    #region public members

    public LRJointNodesReachGraphPair LRJointNodesReachGraphPair { get; }

    public void Run()
    {
      log.Info($"Creating JointNodesReachGraphs at kStep = {kStep}");

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
    private readonly long[] fwdKStepSequence;
    private readonly long[] bkwdKStepSequence;
    private readonly FwdBkwdNCommsGraphPair fwdBkwdNCommsGraphPair;

    private void ConstructLeftGraph()
    {
      NCGraphType fwdNCommsGraph = fwdBkwdNCommsGraphPair.FwdNestedCommsGraph;
      ReachGraphType leftJointNodesReachGraph = LRJointNodesReachGraphPair.LeftJointNodesReachGraph;

      SortedDictionary<long, SortedSet<long>> nodeVLevels =
        meapContext.MEAPSharedContext.NodeLevelInfo.NodeVLevels;
      long fwdKStepLastIndex = fwdKStepSequence.Length - 1;

      for (long i = (fwdKStepLastIndex - 1); i >= 1; i--)
      {
        foreach (long uId in nodeVLevels[fwdKStepSequence[i]])
        {
          if (!meapContext.TArbSeqCFG.HasNode(uId))
          {
            continue;
          }

          DAGNode uNode = meapContext.TArbSeqCFG.GetNode(uId);

          if (!fwdBkwdNCommsGraphPair.FwdCFGNodeToNCGNodesMap.ContainsKey(uId))
          {
            continue;
          }

          List<long> ncgNodes = fwdBkwdNCommsGraphPair.FwdCFGNodeToNCGNodesMap[uId];
        }
      }
    }

    private void ConstructRightGraph()
    {
      ReachGraphType rightJointNodesReachGraph = LRJointNodesReachGraphPair.RightJointNodesReachGraph;
      ReachGraphType leftJointNodesReachGraph = LRJointNodesReachGraphPair.LeftJointNodesReachGraph;

      SortedDictionary<long, SortedSet<long>> nodeVLevels =
        meapContext.MEAPSharedContext.NodeLevelInfo.NodeVLevels;
      long bkwdKStepLastIndex = bkwdKStepSequence.Length - 1;

      for (long i = 1; i <= (bkwdKStepLastIndex - 1); i++)
      {
        foreach (long uId in nodeVLevels[bkwdKStepSequence[i]])
        {
          if (!meapContext.TArbSeqCFG.HasNode(uId))
          {
            continue;
          }

          DAGNode uNode = meapContext.TArbSeqCFG.GetNode(uId);

          if (!fwdBkwdNCommsGraphPair.BkwdCFGNodeToNCGNodesMap.ContainsKey(uId))
          {
            continue;
          }

          List<long> ncgNodes = fwdBkwdNCommsGraphPair.BkwdCFGNodeToNCGNodesMap[uId];
        }
      }
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
