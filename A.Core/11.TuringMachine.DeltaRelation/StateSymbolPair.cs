////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public struct StateSymbolPair : IEquatable<StateSymbolPair>
  {
    #region Ctors

    public StateSymbolPair(int state, int symbol)
    {
      this.State = state;
      this.Symbol = symbol;
    }

    #endregion

    #region public members

    public int State { get; set; }
    public int Symbol { get; set; }

    public override int GetHashCode()
    {
      return State;
    }

    public bool Equals(StateSymbolPair other)
    {
      return (State == other.State) && (Symbol == other.Symbol);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
