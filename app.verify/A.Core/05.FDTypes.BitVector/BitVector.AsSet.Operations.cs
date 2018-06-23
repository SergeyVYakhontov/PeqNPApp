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
    public IBitVector BitwiseSubtract(IBitVector u1, IBitVector u2)
    {
      BitVectorAsSet v1 = u1 as BitVectorAsSet;
      BitVectorAsSet v2 = u2 as BitVectorAsSet;

      SortedSet<ulong> bits = new SortedSet<ulong>(v1.bits.Except(v2.bits));

      return new BitVectorAsSet(bits);
    }

    public IBitVector BitwiseAnd(IBitVector u1, IBitVector u2)
    {
      BitVectorAsSet v1 = u1 as BitVectorAsSet;
      BitVectorAsSet v2 = u2 as BitVectorAsSet;

      SortedSet<ulong> bits = new SortedSet<ulong>(v1.bits.Intersect(v2.bits));

      return new BitVectorAsSet(bits);
    }

    public IBitVector BitwiseOr(IBitVector u1, IBitVector u2)
    {
      BitVectorAsSet v1 = u1 as BitVectorAsSet;
      BitVectorAsSet v2 = u2 as BitVectorAsSet;

      SortedSet<ulong> bits = new SortedSet<ulong>(v1.bits.Union(v2.bits));

      return new BitVectorAsSet(bits);
    }
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
