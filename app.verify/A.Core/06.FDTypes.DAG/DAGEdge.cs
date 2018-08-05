////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Ninject;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public class DAGEdge : IObjectWithId
  {
    #region Ctors

    public DAGEdge(long id, DAGNode fromNode, DAGNode toNode)
    {
      this.Id = id;

      this.FromNode = fromNode;
      this.ToNode = toNode;
    }

    #endregion

    #region public members

    public long Id { get; set; }

    public DAGNode FromNode { get; }
    public DAGNode ToNode { get; }

    public override string ToString()
    {
      return "(" + FromNode.Id + "," + ToNode.Id + ")";
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
