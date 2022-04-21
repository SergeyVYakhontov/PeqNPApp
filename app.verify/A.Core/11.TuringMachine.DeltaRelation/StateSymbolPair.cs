////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public readonly record struct StateSymbolPair : IEquatable<StateSymbolPair>
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

    public override int GetHashCode() => unchecked((int)State);

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
