////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ninject;
using Core;
using EnsureThat;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  using ReachGraphType = TypedDAG<JNodesReachGraphNodeInfo, JNodesReachGraphEdgeInfo>;

  public class LRJointNodesReachGraphPair
  {
    #region Ctors

    public LRJointNodesReachGraphPair()
    {
      LeftJointNodesReachGraph = new ReachGraphType(nameof(LeftJointNodesReachGraph));
      RightJointNodesReachGraph = new ReachGraphType(nameof(RightJointNodesReachGraph));
    }

    #endregion

    #region public members

    public ReachGraphType LeftJointNodesReachGraph { get; }
    public ReachGraphType RightJointNodesReachGraph { get; }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
