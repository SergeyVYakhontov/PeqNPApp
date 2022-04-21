////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public readonly record struct NestedCommsGraphNodeInfo :
    IEquatable<NestedCommsGraphNodeInfo>,
    IObjectWithId
  {
    #region Ctors

    public NestedCommsGraphNodeInfo(long id)
    {
      this.Id = id;
    }

    #endregion

    #region public members

    public long Id { get; }

    public override int GetHashCode() => unchecked((int)Id);

    public bool Equals(NestedCommsGraphNodeInfo other) => this == other;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////

