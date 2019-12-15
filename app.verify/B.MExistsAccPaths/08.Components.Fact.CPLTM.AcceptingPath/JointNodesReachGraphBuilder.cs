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

    public JointNodesReachGraphBuilder(MEAPContext meapContext)
    {
      this.meapContext = meapContext;
      this.CPLTMInfo = meapContext.MEAPSharedContext.CPLTMInfo;
    }

    #endregion

    #region public members

    public void Setup()
    {
      meapContext.muToLRJointNodesReachGraphPair = new SortedDictionary<long, LRJointNodesReachGraphPair>();
    }

    public void Run()
    {
      log.Info("ComputeJointNodesReachGraphsSet");

      KeyValuePair<long, FwdBkwdNCommsGraphPair>[] nestedCommsGraphPair =
        meapContext.muToNestedCommsGraphPair.ToArray();

      for (int i = 0; i <= (nestedCommsGraphPair.Length - 2); i++)
      {
        long mu = nestedCommsGraphPair[i].Key;

        LRJointNodesReachGraphPair lrRJointNodesReachGraphPair = new LRJointNodesReachGraphPair();
        meapContext.muToLRJointNodesReachGraphPair[mu] = lrRJointNodesReachGraphPair;

        FwdBkwdNCommsGraphPair leftFwdBkwdNCommsGraphPair = nestedCommsGraphPair[i].Value;
        FwdBkwdNCommsGraphPair rightFwdBkwdNCommsGraphPair = nestedCommsGraphPair[i + 1].Value;

        LeftJNodesReachGraphBuilder leftJNodesReachGraphBuilder =
          new LeftJNodesReachGraphBuilder(
            meapContext,
            mu,
            leftFwdBkwdNCommsGraphPair,
            lrRJointNodesReachGraphPair);

        leftJNodesReachGraphBuilder.Run();

        RightJNodesReachGraphBuilder rightJNodesReachGraphBuilder =
          new RightJNodesReachGraphBuilder(
            meapContext,
            mu,
            leftFwdBkwdNCommsGraphPair,
            rightFwdBkwdNCommsGraphPair,
            lrRJointNodesReachGraphPair);

        rightJNodesReachGraphBuilder.Run();
      }
    }

    #endregion

    #region private members

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private readonly MEAPContext meapContext;
    private readonly ICPLTMInfo CPLTMInfo;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
