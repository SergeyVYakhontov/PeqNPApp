////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public readonly record struct CompStepNodePair :
    IEquatable<CompStepNodePair>,
    IComparable<CompStepNodePair>
  {
    #region Ctors

    public CompStepNodePair(long variable, long uNode, long vNode)
    {
      this.Variable = variable;
      this.uNode = uNode;
      this.vNode = vNode;
    }

    #endregion

    #region public members

    public long Variable { get; }

    public long uNode { get; }

    public long vNode { get; }

    public override int GetHashCode() => unchecked((int)Variable);

    public override string ToString() => $"({Variable}, {uNode}, {vNode})";

    public int CompareTo(CompStepNodePair other)
    {
      return compStepNodePairComparer.Compare(this, other);
    }

    public static bool operator <(in CompStepNodePair left, in CompStepNodePair right)
    {
      return left.CompareTo(right) < 0;
    }

    public static bool operator <=(in CompStepNodePair left, in CompStepNodePair right)
    {
      return left.CompareTo(right) <= 0;
    }

    public static bool operator >(in CompStepNodePair left, in CompStepNodePair right)
    {
      return left.CompareTo(right) > 0;
    }

    public static bool operator >=(in CompStepNodePair left, in CompStepNodePair right)
    {
      return left.CompareTo(right) >= 0;
    }

    #endregion

    #region private members

    private static readonly CompStepNodePairComparer compStepNodePairComparer = new();

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
