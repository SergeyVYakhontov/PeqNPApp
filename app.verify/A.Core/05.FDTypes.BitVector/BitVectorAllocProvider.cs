////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public class BitVectorAllocProvider : IBitVectorProvider
  {
    #region public members

    public IBitVector CreateVector(ulong size)
    {
      return new BitVectorAlloc(size);
    }

    public IBitVector CreateVector(IBitVector v)
    {
      return new BitVectorAlloc((BitVectorAlloc)v);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
