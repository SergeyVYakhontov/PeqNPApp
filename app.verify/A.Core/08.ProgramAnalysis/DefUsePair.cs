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
  public class DefUsePair
  {
    #region Ctors

    public DefUsePair(long variable, long defNode, long useNode)
    {
      this.Variable = variable;

      this.DefNode = defNode;
      this.UseNode = useNode;
    }

    #endregion

    #region public members

    public long Variable { get; }

    public long DefNode { get; }
    public long UseNode { get; }

    public override string ToString()
    {
      return "(" + Variable + "," + DefNode + "," + UseNode + ")";
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
