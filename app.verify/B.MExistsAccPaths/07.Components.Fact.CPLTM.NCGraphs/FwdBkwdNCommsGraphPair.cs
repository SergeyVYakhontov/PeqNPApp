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

  public class FwdBkwdNCommsGraphPair
  {
    #region Ctors

    public FwdBkwdNCommsGraphPair()
    {
      FwdNestedCommsGraph = new NCGraphType(nameof(FwdNestedCommsGraph));
      BkwdNestedCommsGraph = new NCGraphType(nameof(BkwdNestedCommsGraph));

      FwdCFGNodeToNCGNodesMap = new SortedDictionary<long, List<long>>();
      BkwdCFGNodeToNCGNodesMap = new SortedDictionary<long, List<long>>();

      FwdNCGEdgeToCFGEdgeMap = new SortedDictionary<long, long>();
      BkwdNCGEdgeToCFGEdgeMap = new SortedDictionary<long, long>();
    }

    #endregion

    #region public members

    public NCGraphType FwdNestedCommsGraph { get; }
    public NCGraphType BkwdNestedCommsGraph { get; }

    public SortedDictionary<long, List<long>> FwdCFGNodeToNCGNodesMap { get; }
    public SortedDictionary<long, List<long>> BkwdCFGNodeToNCGNodesMap { get; }

    public SortedDictionary<long, long> FwdNCGEdgeToCFGEdgeMap { get; }
    public SortedDictionary<long, long> BkwdNCGEdgeToCFGEdgeMap { get; }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////

