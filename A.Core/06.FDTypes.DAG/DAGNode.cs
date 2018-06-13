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
  public class DAGNode : IObjectWithId
  {
    #region Ctors

    public DAGNode(long id)
    {
      this.Id = id;

      this.InEdges = new List<DAGEdge>();
      this.OutEdges = new List<DAGEdge>();
    }

    #endregion

    #region public members

    public long Id { get; private set; }

    public List<DAGEdge> InEdges { get; private set; }
    public List<DAGEdge> OutEdges { get; private set; }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
