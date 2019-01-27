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

      FwdCFGNodeToNCGNodesMap = new SortedDictionary<long, List<long>>();
      BkwdCFGNodeToNCGNodesMap = new SortedDictionary<long, List<long>>();

      FwdNCGEdgeToCFGEdgeMap = new SortedDictionary<long, long>();
      BkwdNCGEdgeToCFGEdgeMap = new SortedDictionary<long, long>();
    }

    #endregion

    #region public members

    public TypedDAG<NestedCommsGraphNodeInfo, StdEdgeInfo> FwdNestedCommsGraph { get; }
    public TypedDAG<NestedCommsGraphNodeInfo, StdEdgeInfo> BkwdNestedCommsGraph { get; }

    public SortedDictionary<long, List<long>> FwdCFGNodeToNCGNodesMap { get; }
    public SortedDictionary<long, List<long>> BkwdCFGNodeToNCGNodesMap { get; }

    public SortedDictionary<long, long> FwdNCGEdgeToCFGEdgeMap { get; }
    public SortedDictionary<long, long> BkwdNCGEdgeToCFGEdgeMap { get; }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////

