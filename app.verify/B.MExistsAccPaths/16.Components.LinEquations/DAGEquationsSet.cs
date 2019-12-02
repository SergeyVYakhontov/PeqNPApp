////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public abstract class DAGEquationsSet
  {
    #region Ctors

    protected DAGEquationsSet(DAG graph)
    {
      this.Graph = graph;

      this.FVars = new List<long>();
      this.HVars = new List<long>();
      this.Equations = new List<long>();

      this.NodeToVar = new SortedDictionary<long, long>();
      this.EdgeToVar = new SortedDictionary<long, long>();
    }

    #endregion

    #region public members

    public DAG Graph { get; }

    public List<long> FVars { get; }
    public List<long> HVars { get; }
    public List<long> Equations { get; }

    public long sVar { get; set; }
    public long tVar { get; set; }

    public SortedDictionary<long, long> NodeToVar { get; }
    public SortedDictionary<long, long> EdgeToVar { get; }

    public abstract long AddVar();
    public abstract long AddVarForNode(long nodeId);
    public abstract long AddVarForEdge(long edgeId);

    public void AddEquation(long equation)
    {
      Equations.Add(equation);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
