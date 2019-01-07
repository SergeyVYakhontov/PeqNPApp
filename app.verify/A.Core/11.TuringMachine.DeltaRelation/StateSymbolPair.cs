////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public readonly struct StateSymbolPair : IEquatable<StateSymbolPair>
  {
    #region Ctors

    public StateSymbolPair(uint state, int symbol)
    {
      this.State = state;
      this.Symbol = symbol;
    }

    #endregion

    #region public members

    public uint State { get; }
    public int Symbol { get; }

    public override bool Equals(object obj)
    {
      StateSymbolPair toCompare = (StateSymbolPair)obj;

      return this == toCompare;
    }

    public override int GetHashCode() => unchecked((int)State);

    public bool Equals(StateSymbolPair other)
    {
      return this == other;
    }

    public static bool operator == (in StateSymbolPair x, in StateSymbolPair y)
    {
      return (x.State == y.State) && (x.Symbol == y.Symbol);
    }

    public static bool operator !=(in StateSymbolPair x, in StateSymbolPair y)
    {
      return !(x == y);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
