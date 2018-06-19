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
  public readonly struct PropSymbol
  {
    #region Ctors

    public PropSymbol(long variable, int symbol)
    {
      this.Variable = variable;
      this.Symbol = symbol;
    }

    #endregion

    #region public members

    public long Variable { get; }
    public int Symbol { get; }

    public override string ToString()
    {
      return "(" + Variable + "," + Symbol + ")";
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////

