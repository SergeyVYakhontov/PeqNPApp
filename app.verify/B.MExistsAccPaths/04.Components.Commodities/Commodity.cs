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
  public class Commodity : IObjectWithId
  {
    #region Ctors

    public Commodity(String name, long id, long variable, DAG Gi)
    {
      this.Name = name;
      this.Id = id;
      this.Variable = variable;
      this.Gi = Gi;
    }

    #endregion

    #region public members

    public long Id { get; private set; }
    public string Name { get; private set; }
    
    public long Variable { get; private set; }
    public long sNodeId { get; set; }
    public long tNodeId { get; set; }

    public DAG Gi { get; set; }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
