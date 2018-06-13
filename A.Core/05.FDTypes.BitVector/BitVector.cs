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
  public partial class BitVector : IBitVector, IBitVectorOperations
  {
    #region Ctors

    public BitVector() { }

    public BitVector(ulong Size)
    {
      Ensure.That(Size).IsNot<ulong>(0);

      this.Size = Size;
      wordCount = (this.Size + wordSize - 1) / wordSize;
      items = new UInt64[wordCount];

      InitBits();
    }

    public BitVector(BitVector v)
    {
      Size = v.Size;
      wordCount = v.wordCount;
      items = new UInt64[wordCount];

      v.items.CopyTo(items, 0);
    }

    #endregion

    #region public members

    public ulong Size { get; }

    public void SetItem(ulong Index, byte To)
    {
      Ensure.That((To == 0) || (To == 1)).IsTrue();
      Ensure.That((Index < 0) || (Index > Size - 1)).IsFalse();

      ulong w_i = WordIndex(Index);
      byte b_i = BitIndex(Index);

      if (To == 0)
      {
        items[w_i] &= ~((UInt64)1 << b_i);
      }
      else
      {
        items[w_i] |= ((UInt64)1 << b_i);
      }
    }

    public byte GetItem(ulong Index)
    {
      if ((Index < 0) || (Index > Size - 1))
      {
        return 0;
      }

      ulong w_i = WordIndex(Index);
      byte b_i = BitIndex(Index);

      UInt64 w = items[w_i];

      if ((w & ((UInt64)1 << b_i)) == 0)
      {
        return 0;
      }
      else
      {
        return 1;
      }
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
      throw new NotImplementedException();
    }

    public override bool Equals(Object other)
    {
      BitVector v = (BitVector)other;

      Ensure.That(Size).Is(v.Size);

      return Enumerable.SequenceEqual(items, v.items);
    }

    public override int GetHashCode()
    {
      return (int)Size;
    }

    public override string ToString()
    {
      StringBuilder repr = new StringBuilder();

      for (ulong i = 0; i < Size; i++)
      {
        repr.Append(ItemRepr(i));
      }

      return repr.ToString();
    }

    #endregion

    #region private members

    private const byte wordSize = sizeof(UInt64);
    private readonly ulong wordCount;
    private readonly UInt64[] items;

    private void InitBits()
    {
      for (ulong i = 0; i < wordCount; i++)
      {
        items[i] = 0;
      }
    }

    private ulong WordIndex(ulong Index)
    {
      return (Index / wordSize);
    }

    private byte BitIndex(ulong Index)
    {
      return (byte)(Index % wordSize);
    }

    private string ItemRepr(ulong Index)
    {
      return (GetItem(Index) == 1 ? "1" : "0");
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
