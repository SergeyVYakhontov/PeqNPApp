////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public class TypedDAG<NodeType, EdgeType> : DAG where NodeType : new()
  {
    #region Ctors

    public TypedDAG(string name)
      : base(name) { }

    #endregion

    #region public members

    public SortedDictionary<long, NodeType> IdToNodeInfoMap { get; private set; }
    public SortedDictionary<long, EdgeType> IdToEdgeInfoMap { get; }

    public void CopyIdToNodeInfoMap(SortedDictionary<long, NodeType> from)
    {
      IdToNodeInfoMap = new SortedDictionary<long, NodeType>(from);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
