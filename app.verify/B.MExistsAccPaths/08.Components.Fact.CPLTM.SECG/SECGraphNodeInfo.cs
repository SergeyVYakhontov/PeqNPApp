////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public readonly record struct SECGraphNodeInfo : IObjectWithId
  {
    #region Ctors

    public SECGraphNodeInfo(long id)
    {
      this.Id = id;
    }

    #endregion

    #region public members

    public long Id { get; }

    public override int GetHashCode() => unchecked((int)Id);

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
