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
    public IBitVector BitwiseSubtract(IBitVector x, IBitVector y)
    {
      BitVectorAlloc v1 = x as BitVectorAlloc;
      BitVectorAlloc v2 = y as BitVectorAlloc;

      Ensure.That(v1).IsNotNull();
      Ensure.That(v2).IsNotNull();

      Ensure.That(v1!.Size).Is(v2!.Size);

      BitVectorAlloc result = new BitVectorAlloc(v1.Size);

      if (!v1.allocated && !v2.allocated)
      {
        return result;
      }

      if (v1.allocated && !v2.allocated)
      {
        return new BitVectorAlloc(v1);
      }

      if (!v1.allocated && v2.allocated)
      {
        return result;
      }

      result.Allocate();

      unsafe
      {
        fixed (UInt64* r_items_ptr = result.items)
        {
          fixed (UInt64* v1_items_ptr = v1.items)
          {
            fixed (UInt64* v2_items_ptr = v2.items)
            {
              UInt64* r_items_last = r_items_ptr + result.items.Length;
              UInt64* v1_items_last = v1_items_ptr + v1.items.Length;
              UInt64* v2_items_last = v2_items_ptr + v2.items.Length;

              UInt64* r_ptr = r_items_ptr;
              UInt64* v1_ptr = v1_items_ptr;
              UInt64* v2_ptr = v2_items_ptr;

              for (; v1_ptr < v1_items_last;)
              {
                (*r_ptr) = (*v1_ptr) & (~(*v2_ptr));

                r_ptr++;
                v1_ptr++;
                v2_ptr++;
              }
            }
          }
        }
      }

      if (result.IsEmpty())
      {
        return new BitVectorAlloc(v1.Size);
      }

      return result;
    }

    public IBitVector BitwiseAnd(IBitVector x, IBitVector y)
    {
      BitVectorAlloc v1 = x as BitVectorAlloc;
      BitVectorAlloc v2 = y as BitVectorAlloc;

      Ensure.That(v1).IsNotNull();
      Ensure.That(v2).IsNotNull();

      Ensure.That(v1!.Size).Is(v2!.Size);

      BitVectorAlloc result = new BitVectorAlloc(v1.Size);

      if (!v1.allocated && !v2.allocated)
      {
        return result;
      }

      if (v1.allocated && !v2.allocated)
      {
        return result;
      }

      if (!v1.allocated && v2.allocated)
      {
        return result;
      }

      result.Allocate();

      unsafe
      {
        fixed (UInt64* r_items_ptr = result.items)
        {
          fixed (UInt64* v1_items_ptr = v1.items)
          {
            fixed (UInt64* v2_items_ptr = v2.items)
            {
              UInt64* r_items_last = r_items_ptr + result.items.Length;
              UInt64* v1_items_last = v1_items_ptr + v1.items.Length;
              UInt64* v2_items_last = v2_items_ptr + v2.items.Length;

              UInt64* r_ptr = r_items_ptr;
              UInt64* v1_ptr = v1_items_ptr;
              UInt64* v2_ptr = v2_items_ptr;

              for (; v1_ptr < v1_items_last;)
              {
                (*r_ptr) = (*v1_ptr) & (*v2_ptr);

                r_ptr++;
                v1_ptr++;
                v2_ptr++;
              }
            }
          }
        }
      }

      if (result.IsEmpty())
      {
        return new BitVectorAlloc(v1.Size);
      }

      return result;
    }

    public IBitVector BitwiseOr(IBitVector x, IBitVector y)
    {
      BitVectorAlloc v1 = x as BitVectorAlloc;
      BitVectorAlloc v2 = y as BitVectorAlloc;

      Ensure.That(v1).IsNotNull();
      Ensure.That(v2).IsNotNull();

      Ensure.That(v1!.Size).Is(v2!.Size);

      BitVectorAlloc result = new BitVectorAlloc(v1.Size);

      if (!v1.allocated && !v2.allocated)
      {
        return result;
      }

      if (v1.allocated && !v2.allocated)
      {
        return new BitVectorAlloc(v1);
      }
      else if (!v1.allocated && v2.allocated)
      {
        return new BitVectorAlloc(v2);
      }

      result.Allocate();

      unsafe
      {
        fixed (UInt64* r_items_ptr = result.items)
        {
          fixed (UInt64* v1_items_ptr = v1.items)
          {
            fixed (UInt64* v2_items_ptr = v2.items)
            {
              UInt64* r_items_last = r_items_ptr + result.items.Length;
              UInt64* v1_items_last = v1_items_ptr + v1.items.Length;
              UInt64* v2_items_last = v2_items_ptr + v2.items.Length;

              UInt64* r_ptr = r_items_ptr;
              UInt64* v1_ptr = v1_items_ptr;
              UInt64* v2_ptr = v2_items_ptr;

              for (; v1_ptr < v1_items_last;)
              {
                (*r_ptr) = (*v1_ptr) | (*v2_ptr);

                r_ptr++;
                v1_ptr++;
                v2_ptr++;
              }
            }
          }
        }
      }

      if (result.IsEmpty())
      {
        return new BitVectorAlloc(v1.Size);
      }

      return result;
    }
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
