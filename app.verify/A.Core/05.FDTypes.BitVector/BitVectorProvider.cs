////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public class BitVectorProvider : IBitVectorProvider
  {
    #region public members

    public IBitVector CreateVector(ulong size)
    {
      return new BitVector(size);
    }

    public IBitVector CreateVector(IBitVector v)
    {
      return new BitVector((BitVector)v);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
