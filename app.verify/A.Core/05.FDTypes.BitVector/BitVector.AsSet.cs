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

    public BitVectorAsSet(ulong size) { }

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

    public void SetItem(ulong index, byte to)
    {
      if(to == 0)
      {
        bits.Remove(index);
      }
      else
      {
        bits.Add(index);
      }
    }

    public byte GetItem(ulong index)
    {
      return bits.Contains(index) ? (byte)1 : (byte)0;
    }

    public byte this[ulong i]
    {
      get { return GetItem(i); }
      set { SetItem(i, value); }
    }

    public ulong ItemCount()
    {
      return (ulong)bits.Count;
    }

    public bool IsEmpty()
    {
      return !bits.Any();
    }

    public override bool Equals(Object obj)
    {
      BitVectorAsSet v = obj as BitVectorAsSet;

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
