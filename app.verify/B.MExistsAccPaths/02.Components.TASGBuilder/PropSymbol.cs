////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public readonly record struct PropSymbol :
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

    public override int GetHashCode() => Symbol;

    public override string ToString() => $"({Variable}, {Symbol})";

    public int CompareTo(PropSymbol other)
    {
      return propSymbolComparer.Compare(this, other);
    }

    public static bool operator <(in PropSymbol left, in PropSymbol right)
    {
      return left.CompareTo(right) < 0;
    }

    public static bool operator <=(in PropSymbol left, in PropSymbol right)
    {
      return left.CompareTo(right) <= 0;
    }

    public static bool operator >(in PropSymbol left, in PropSymbol right)
    {
      return left.CompareTo(right) > 0;
    }

    public static bool operator >=(in PropSymbol left, in PropSymbol right)
    {
      return left.CompareTo(right) >= 0;
    }

    #endregion

    #region private members

    private static readonly PropSymbolComparer propSymbolComparer = new();

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
