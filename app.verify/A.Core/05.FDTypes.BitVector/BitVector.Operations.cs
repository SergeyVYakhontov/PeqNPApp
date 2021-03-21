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
    public IBitVector BitwiseSubtract(IBitVector x, IBitVector y)
    {
      BitVector v1 = (BitVector)x;
      BitVector v2 = (BitVector)y;

      Ensure.That(v1).IsNotNull();
      Ensure.That(v2).IsNotNull();

      Ensure.That(v1!.Size).Is(v2!.Size);

      BitVector result = new(v1.Size);

      for (ulong i = 0; i < (ulong)v1.items.Length; i++)
      {
        result.items[i] = v1.items[i] & (~v2.items[i]);
      }

      return result;
    }

    public IBitVector BitwiseAnd(IBitVector x, IBitVector y)
    {
      BitVector v1 = x as BitVector;
      BitVector v2 = y as BitVector;

      Ensure.That(v1).IsNotNull();
      Ensure.That(v2).IsNotNull();

      Ensure.That(x.Size).Is(v2!.Size);

      BitVector result = new(v1!.Size);

      for (ulong i = 0; i < (ulong)v1.items.Length; i++)
      {
        result.items[i] = v1.items[i] & v2.items[i];
      }

      return result;
    }

    public IBitVector BitwiseOr(IBitVector x, IBitVector y)
    {
      BitVector v1 = x as BitVector;
      BitVector v2 = y as BitVector;

      Ensure.That(v1).IsNotNull();
      Ensure.That(v2).IsNotNull();

      Ensure.That(v1!.Size).Is(v2!.Size);

      BitVector result = new(v1.Size);

      for (ulong i = 0; i < (ulong)v1.items.Length; i++)
      {
        result.items[i] = v1.items[i] | v2.items[i];
      }

      return result;
    }
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
