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
  public class LRJointNodesReachGraphPair
  {
    #region Ctors

    public LRJointNodesReachGraphPair()
    {
      LeftJointNodesReachGraph = new TypedDAG<JNodesReachGraphNodeInfo, JNodesReachGraphEdgeInfo>(nameof(LeftJointNodesReachGraph));
      RightJointNodesReachGraph = new TypedDAG<JNodesReachGraphNodeInfo, JNodesReachGraphEdgeInfo>(nameof(RightJointNodesReachGraph));
    }

    #endregion

    #region public members

    public TypedDAG<JNodesReachGraphNodeInfo, JNodesReachGraphEdgeInfo> LeftJointNodesReachGraph { get; }
    public TypedDAG<JNodesReachGraphNodeInfo, JNodesReachGraphEdgeInfo> RightJointNodesReachGraph { get; }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
