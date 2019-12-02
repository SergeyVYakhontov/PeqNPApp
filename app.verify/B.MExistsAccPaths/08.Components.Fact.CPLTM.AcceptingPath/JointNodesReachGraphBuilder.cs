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
  public class JointNodesReachGraphBuilder
  {
    #region Ctors

    public JointNodesReachGraphBuilder(LRJointNodesReachGraphPair lrJointNodesReachGraphPair)
    {
      this.LRJointNodesReachGraphPair = lrJointNodesReachGraphPair;
    }

    #endregion

    #region public members

    public LRJointNodesReachGraphPair LRJointNodesReachGraphPair { get; }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
