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

    public SortedDictionary<long, NodeType> IdToInfoMap { get; private set; }

    public void CreateIdToInfoMap()
    {
      IdToInfoMap = new SortedDictionary<long, NodeType>();

      Nodes.ForEach(u => { IdToInfoMap[u.Id] = new NodeType(); });
    }

    public void CopyIdToInfoMap(SortedDictionary<long, NodeType> from)
    {
      IdToInfoMap = new SortedDictionary<long, NodeType>(from);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
