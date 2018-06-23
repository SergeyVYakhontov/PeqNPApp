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

    public override bool Equals(object obj)
    {
      return
        Variable == ((PropSymbol)obj).Variable &&
        Symbol == ((PropSymbol)obj).Symbol;
    }

    public override int GetHashCode() => Symbol;

    public static bool operator ==(PropSymbol left, PropSymbol right)
    {
      return
        left.Variable == right.Variable &&
        left.Symbol == right.Symbol;
    }

    public static bool operator !=(PropSymbol left, PropSymbol right)
      => !(left == right);

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////

