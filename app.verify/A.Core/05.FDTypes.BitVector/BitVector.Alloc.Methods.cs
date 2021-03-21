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
  public partial class BitVectorAlloc
  {
    public void BitwiseSubtract(IBitVector u)
    {
      BitVectorAlloc v = (BitVectorAlloc)u;

      Ensure.That(v).IsNotNull();
      Ensure.That(Size).Is(v.Size);

      if (!allocated && !v.allocated)
      {
        return;
      }

      if (allocated && !v.allocated)
      {
        return;
      }

      if (!allocated && v.allocated)
      {
        return;
      }

      bool isEmpty = true;

      unsafe
      {
        fixed (UInt64* items_ptr = items)
        {
          fixed (UInt64* v_items_ptr = v.items)
          {
            UInt64* items_last = items_ptr + items.Length;
            UInt64* v2items_last = v_items_ptr + v.items.Length;

            UInt64* ptr = items_ptr;
            UInt64* v_ptr = v_items_ptr;

            for (; ptr < items_last;)
            {
              *ptr &= (~(*v_ptr));

              if ((*ptr) != 0)
              {
                isEmpty = false;
              }

              ptr++;
              v_ptr++;
            }
          }
        }
      }

      if (isEmpty)
      {
        Deallocate();
      }
    }

    public void BitwiseAnd(IBitVector u)
    {
      BitVectorAlloc v = (BitVectorAlloc)u;

      Ensure.That(v).IsNotNull();
      Ensure.That(Size).Is(v.Size);

      if (!allocated && !v.allocated)
      {
        return;
      }

      if (allocated && !v.allocated)
      {
        Deallocate();
        return;
      }

      if (!allocated && v.allocated)
      {
        return;
      }

      bool isEmpty = true;

      unsafe
      {
        fixed (UInt64* items_ptr = items)
        {
          fixed (UInt64* v_items_ptr = v.items)
          {
            UInt64* items_last = items_ptr + items.Length;
            UInt64* v2items_last = v_items_ptr + v.items.Length;

            UInt64* ptr = items_ptr;
            UInt64* v_ptr = v_items_ptr;

            for (; ptr < items_last;)
            {
              (*ptr) &= (*v_ptr);

              if ((*ptr) != 0)
              {
                isEmpty = false;
              }

              ptr++;
              v_ptr++;
            }
          }
        }
      }

      if (isEmpty)
      {
        Deallocate();
      }
    }

    public void BitwiseOr(IBitVector u)
    {
      BitVectorAlloc v = (BitVectorAlloc)u;

      Ensure.That(v).IsNotNull();
      Ensure.That(Size).Is(v.Size);

      if (!allocated && !v.allocated)
      {
        return;
      }

      if (allocated && !v.allocated)
      {
        return;
      }
      else if (!allocated && v.allocated)
      {
        Allocate();
      }

      bool isEmpty = true;

      unsafe
      {
        fixed (UInt64* items_ptr = items)
        {
          fixed (UInt64* v_items_ptr = v.items)
          {
            UInt64* items_last = items_ptr + items.Length;
            UInt64* v2items_last = v_items_ptr + v.items.Length;

            UInt64* ptr = items_ptr;
            UInt64* v_ptr = v_items_ptr;

            for (; ptr < items_last;)
            {
              (*ptr) |= (*v_ptr);

              if ((*ptr) != 0)
              {
                isEmpty = false;
              }

              ptr++;
              v_ptr++;
            }
          }
        }
      }

      if (isEmpty)
      {
        Deallocate();
      }
    }

    public IEnumerable<ulong> GetBit1List()
    {
      LinkedList<ulong> itemList = new();

      if (!allocated)
      {
        return itemList;
      }

      unsafe
      {
        ulong currentItemNumber = 0;

        fixed (UInt64* items_ptr = items)
        {
          UInt64* items_last = items_ptr + items.Length;
          UInt64* ptr = items_ptr;

          for (; ptr < items_last; ptr++)
          {
            byte currentBit = 0;
            for (; currentBit < wordSize; )
            {
              if (((*ptr) & (((UInt64)1) << currentBit)) != 0)
              {
                itemList.AddLast(currentItemNumber);
              }

              currentBit++;
              currentItemNumber++;

              if (currentItemNumber == Size)
              {
                break;
              }
            }

            if (currentItemNumber == Size)
            {
              break;
            }
          }
        }
      }

      return itemList;
    }
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
