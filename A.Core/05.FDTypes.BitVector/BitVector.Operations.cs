////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using EnsureThat;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public partial class BitVector
  {
    public IBitVector BitwiseSubtract(IBitVector u1, IBitVector u2)
    {
      BitVector v1 = u1 as BitVector;
      BitVector v2 = u2 as BitVector;

      Ensure.That(v1.Size).Is(v2.Size);

      BitVector result = new BitVector(v1.Size);

      for (ulong i = 0; i < (ulong)v1.items.Length; i++)
      {
        result.items[i] = v1.items[i] & (~v2.items[i]);
      }

      return result;
    }

    public IBitVector BitwiseAnd(IBitVector u1, IBitVector u2)
    {
      BitVector v1 = u1 as BitVector;
      BitVector v2 = u2 as BitVector;

      Ensure.That(u1.Size).Is(v2.Size);

      BitVector result = new BitVector(v1.Size);

      for (ulong i = 0; i < (ulong)v1.items.Length; i++)
      {
        result.items[i] = v1.items[i] & v2.items[i];
      }

      return result;
    }

    public IBitVector BitwiseOr(IBitVector u1, IBitVector u2)
    {
      BitVector v1 = u1 as BitVector;
      BitVector v2 = u2 as BitVector;

      Ensure.That(v1.Size).Is(v2.Size);

      BitVector result = new BitVector(v1.Size);

      for (ulong i = 0; i < (ulong)v1.items.Length; i++)
      {
        result.items[i] = v1.items[i] | v2.items[i];
      }

      return result;
    }
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
