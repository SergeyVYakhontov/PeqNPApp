////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public interface IBitVector
  {
    ulong Size { get; }

    void SetItem(ulong Index, byte To);
    byte GetItem(ulong Index);

    byte this[ulong i] { get; set; }

    ulong ItemCount();
    bool IsEmpty();

    IBitVector BitwiseSubtract(IBitVector v1, IBitVector v2);
    IBitVector BitwiseAnd(IBitVector v1, IBitVector v2);
    IBitVector BitwiseOr(IBitVector v1, IBitVector v2);

    void BitwiseSubtract(IBitVector v);
    void BitwiseAnd(IBitVector v);
    void BitwiseOr(IBitVector v);

    IEnumerable<ulong> GetBit1List();
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
