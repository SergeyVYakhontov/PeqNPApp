////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public partial class BitVector
  {
    public void BitwiseSubtract(IBitVector u)
    {
      throw new InvalidOperationException();
    }

    public void BitwiseAnd(IBitVector u)
    {
      throw new InvalidOperationException();
    }

    public void BitwiseOr(IBitVector u)
    {
      throw new InvalidOperationException();
    }

    public IEnumerable<ulong> GetBit1List()
    {
      LinkedList<ulong> itemList = new();

      for (ulong i = 0; i < Size; i++)
      {
        if (GetItem(i) == 1)
        {
          itemList.AddLast(i);
        }
      }

      return itemList;
    }
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
