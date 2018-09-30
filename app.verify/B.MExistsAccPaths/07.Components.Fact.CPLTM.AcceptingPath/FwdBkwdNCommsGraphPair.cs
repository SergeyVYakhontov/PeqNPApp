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
  public class FwdBkwdNCommsGraphPair
  {
    #region Ctors

    public FwdBkwdNCommsGraphPair()
    {
      FwdNestedCommsGraph = new TypedDAG<NestedCommsGraphNodeInfo, StdEdgeInfo>(nameof(FwdNestedCommsGraph));
      BkwdNestedCommsGraph = new TypedDAG<NestedCommsGraphNodeInfo, StdEdgeInfo>(nameof(BkwdNestedCommsGraph));
    }

    #endregion

    #region public members

    public TypedDAG<NestedCommsGraphNodeInfo, StdEdgeInfo> FwdNestedCommsGraph { get; }
    public TypedDAG<NestedCommsGraphNodeInfo, StdEdgeInfo> BkwdNestedCommsGraph { get; }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////

