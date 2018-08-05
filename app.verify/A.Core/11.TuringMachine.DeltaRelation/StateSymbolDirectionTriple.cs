////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public class StateSymbolDirectionTriple
  {
    #region Ctors

    public StateSymbolDirectionTriple() { }

    public StateSymbolDirectionTriple(
      uint state,
      int symbol,
      TMDirection direction,
      long shift = 1)
    {
      this.State = state;
      this.Symbol = symbol;
      this.Direction = direction;
      this.Shift = shift;
    }

    #endregion

    #region public members

    public uint State { get; }
    public int Symbol { get; }
    public TMDirection Direction { get; }
    public long Shift { get; } = 1;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
