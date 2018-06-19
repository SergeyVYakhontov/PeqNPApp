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
  public partial class BitVectorAlloc : IBitVector, IBitVectorOperations
  {
    #region Ctors

    public BitVectorAlloc() { }

    public BitVectorAlloc(ulong Size)
    {
      Ensure.That(Size).IsNot<ulong>(0);

      this.Size = Size;
      wordCount = (this.Size + wordSize - 1) / wordSize;
    }

    public BitVectorAlloc(BitVectorAlloc v)
    {
      Size = v.Size;
      wordCount = v.wordCount;

      if (v.allocated)
      {
        Allocate();
        v.items.CopyTo(items, 0);
      }
    }

    #endregion

    #region public members

    public ulong Size { get; }

    public void SetItem(ulong Index, byte To)
    {
      Ensure.That((To == 0) || (To == 1)).IsTrue();
      Ensure.That(Index <= (Size - 1)).IsTrue();

      ulong w_i = WordIndex(Index);
      byte b_i = BitIndex(Index);

      if (To == 0)
      {
        if(!allocated)
        {
          return;
        }

        items[w_i] &= ~(((UInt64)1) << b_i);
      }
      else
      {
        Allocate();
        items[w_i] |= (((UInt64)1) << b_i);
      }
    }

    public byte GetItem(ulong Index)
    {
      if ((Index < 0) || (Index > Size - 1))
      {
        return 0;
      }

      if(!allocated)
      {
        return 0;
      }

      ulong w_i = WordIndex(Index);
      byte b_i = BitIndex(Index);

      UInt64 w = items[w_i];

      if ((w & (((UInt64)1) << b_i)) == 0)
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
      if (!allocated)
      {
        return 0;
      }

      ulong count = 0;

      for (ulong i = 0; i < Size; i++)
      {
        if (GetItem(i) == 1)
        {
          count++;
        }
      }

      return count;
    }

    public bool IsEmpty()
    {
      if (!allocated)
      {
        return true;
      }

      return items.All(p => p == 0);
    }

    public override bool Equals(Object other)
    {
      BitVectorAlloc v = (BitVectorAlloc)other;

      Ensure.That(Size).Is(v.Size);

      if (!allocated && !v.allocated)
      {
        return true;
      }

      if(!allocated && v.allocated)
      {
        return v.items.All(p => p == 0);
      }

      if (allocated && !v.allocated)
      {
        return items.All(p => p == 0);
      }

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

    private bool allocated = false;
    private readonly ulong wordCount;
    private UInt64[] items;

    private void InitBits()
    {
      for (ulong i = 0; i < wordCount; i++)
      {
        items[i] = 0;
      }
    }

    private void Allocate()
    {
      if (allocated)
      {
        return;
      }

      items = new UInt64[wordCount];
      InitBits();

      allocated = true;
    }

    private void Deallocate()
    {
      if (!allocated)
      {
        return;
      }

      items = null;
      allocated = false;
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
