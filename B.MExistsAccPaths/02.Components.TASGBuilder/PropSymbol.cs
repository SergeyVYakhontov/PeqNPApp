////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using EnsureThat;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public readonly struct PropSymbol :
    IEquatable<PropSymbol>,
    IComparable<PropSymbol>
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

    public override bool Equals(object obj)
    {
      Ensure.That(obj).IsNotNull();

      PropSymbol other = (PropSymbol)obj;

      return this == other;
    }

    public override int GetHashCode() => Symbol;

    public override string ToString() => $"({Variable}, {Symbol})";

    public bool Equals(PropSymbol other) => this == other;

    public int CompareTo(PropSymbol other)
    {
      return propSymbolComparer.Compare(this, other);
    }

    public static bool operator ==(PropSymbol left, PropSymbol right)
    {
      return
        (left.Variable == right.Variable) &&
        (left.Symbol == right.Symbol);
    }

    public static bool operator !=(PropSymbol left, PropSymbol right)
      => !(left == right);

    public static bool operator <(PropSymbol left, PropSymbol right)
    {
      return left.CompareTo(right) < 0;
    }

    public static bool operator <=(PropSymbol left, PropSymbol right)
    {
      return left.CompareTo(right) <= 0;
    }

    public static bool operator >(PropSymbol left, PropSymbol right)
    {
      return left.CompareTo(right) > 0;
    }

    public static bool operator >=(PropSymbol left, PropSymbol right)
    {
      return left.CompareTo(right) >= 0;
    }

    #endregion

    #region private members

    private static readonly PropSymbolComparer propSymbolComparer =
      new PropSymbolComparer();

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////

