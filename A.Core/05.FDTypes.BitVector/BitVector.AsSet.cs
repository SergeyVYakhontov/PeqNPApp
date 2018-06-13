////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public partial class BitVectorAsSet : IBitVector, IBitVectorOperations
  {
    #region Ctors

    public BitVectorAsSet() { }

    public BitVectorAsSet(ulong Size) { }

    public BitVectorAsSet(BitVectorAsSet s)
    {
      bits = new SortedSet<ulong>(s.bits);
    }

    public BitVectorAsSet(SortedSet<ulong> s)
    {
      bits = new SortedSet<ulong>(s);
    }

    #endregion

    #region public members

    public ulong Size => (ulong)bits.Count;

    public void SetItem(ulong Index, byte To)
    {
      if(To == 0)
      {
        bits.Remove(Index);
      }
      else
      {
        bits.Add(Index);
      }
    }

    public byte GetItem(ulong Index)
    {
      return (bits.Contains(Index) ? (byte)1 : (byte)0);
    }

    public byte this[ulong i]
    {
      get { return GetItem(i); }
      set { SetItem(i, value); }
    }

    public ulong ItemCount()
    {
      throw new NotImplementedException();
    }

    public bool IsEmpty()
    {
      return !bits.Any();
    }

    public override bool Equals(Object other)
    {
      BitVectorAsSet v = (BitVectorAsSet)other;

      return bits.SetEquals(v.bits);
    }

    public override int GetHashCode()
    {
      return bits.Count;
    }

    public override string ToString()
    {
      StringBuilder repr = new StringBuilder();

      foreach(ulong e in bits)
      {
        repr.Append(ItemRepr(e));
      }

      return repr.ToString();
    }

    #endregion

    #region private members

    private readonly SortedSet<ulong> bits = new SortedSet<ulong>();

    private string ItemRepr(ulong Index)
    {
      return (GetItem(Index) == 1 ? "1" : "0");
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
