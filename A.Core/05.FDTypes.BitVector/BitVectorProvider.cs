using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

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
      return new BitVector(v as BitVector);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
