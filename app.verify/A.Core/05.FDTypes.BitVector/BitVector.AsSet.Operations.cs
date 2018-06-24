////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public partial class BitVectorAsSet
  {
    public IBitVector BitwiseSubtract(IBitVector x, IBitVector y)
    {
      BitVectorAsSet v1 = x as BitVectorAsSet;
      BitVectorAsSet v2 = y as BitVectorAsSet;

      SortedSet<ulong> zBits = new SortedSet<ulong>(v1.bits.Except(v2.bits));

      return new BitVectorAsSet(zBits);
    }

    public IBitVector BitwiseAnd(IBitVector x, IBitVector y)
    {
      BitVectorAsSet v1 = x as BitVectorAsSet;
      BitVectorAsSet v2 = y as BitVectorAsSet;

      SortedSet<ulong> zBits = new SortedSet<ulong>(v1.bits.Intersect(v2.bits));

      return new BitVectorAsSet(zBits);
    }

    public IBitVector BitwiseOr(IBitVector x, IBitVector y)
    {
      BitVectorAsSet v1 = x as BitVectorAsSet;
      BitVectorAsSet v2 = y as BitVectorAsSet;

      SortedSet<ulong> zBits = new SortedSet<ulong>(v1.bits.Union(v2.bits));

      return new BitVectorAsSet(zBits);
    }
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
