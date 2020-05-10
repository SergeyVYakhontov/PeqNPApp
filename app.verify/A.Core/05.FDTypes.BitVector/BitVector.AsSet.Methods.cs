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
    public void BitwiseSubtract(IBitVector u)
    {
      BitVectorAsSet v = u as BitVectorAsSet;
      Ensure.That(v).IsNotNull();

      foreach (ulong b in v!.bits)
      {
        bits.Remove(b);
      }
    }

    public void BitwiseOr(IBitVector u)
    {
      BitVectorAsSet v = u as BitVectorAsSet;
      Ensure.That(v).IsNotNull();

      foreach (ulong b in v!.bits)
      {
        bits.Add(b);
      }
    }

    public void BitwiseAnd(IBitVector u)
    {
      throw new InvalidOperationException();
    }

    public IEnumerable<ulong> GetBit1List()
    {
      return bits.ToList();
    }
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
