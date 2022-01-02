////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public class BitVectorAsSetProvider : IBitVectorProvider
  {
    #region public members

    public IBitVector CreateVector(ulong size)
    {
      return new BitVectorAsSet(size);
    }

    public IBitVector CreateVector(IBitVector v)
    {
      return new BitVectorAsSet((BitVectorAsSet)v);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
