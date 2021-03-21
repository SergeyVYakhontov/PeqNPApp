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
  public partial class BitVectorAsSet
  {
    public IBitVector BitwiseSubtract(IBitVector x, IBitVector y)
    {
      BitVectorAsSet v1 = (BitVectorAsSet)x;
      BitVectorAsSet v2 = (BitVectorAsSet)y;

      Ensure.That(v1).IsNotNull();
      Ensure.That(v2).IsNotNull();

      SortedSet<ulong> zBits = new(v1.bits.Except(v2.bits));

      return new BitVectorAsSet(zBits);
    }

    public IBitVector BitwiseAnd(IBitVector x, IBitVector y)
    {
      BitVectorAsSet v1 = (BitVectorAsSet)x;
      BitVectorAsSet v2 = (BitVectorAsSet)y;

      Ensure.That(v1).IsNotNull();
      Ensure.That(v2).IsNotNull();

      SortedSet<ulong> zBits = new(v1.bits.Intersect(v2.bits));

      return new BitVectorAsSet(zBits);
    }

    public IBitVector BitwiseOr(IBitVector x, IBitVector y)
    {
      BitVectorAsSet v1 = (BitVectorAsSet)x;
      BitVectorAsSet v2 = (BitVectorAsSet)y;

      Ensure.That(v1).IsNotNull();
      Ensure.That(v2).IsNotNull();

      SortedSet<ulong> zBits = new(v1.bits.Union(v2.bits));

      return new BitVectorAsSet(zBits);
    }
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
