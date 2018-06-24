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

    void SetItem(ulong index, byte to);
    byte GetItem(ulong index);

    byte this[ulong i] { get; set; }

    ulong ItemCount();
    bool IsEmpty();

    IBitVector BitwiseSubtract(IBitVector x, IBitVector y);
    IBitVector BitwiseAnd(IBitVector x, IBitVector y);
    IBitVector BitwiseOr(IBitVector x, IBitVector y);

    void BitwiseSubtract(IBitVector u);
    void BitwiseAnd(IBitVector u);
    void BitwiseOr(IBitVector u);

    IEnumerable<ulong> GetBit1List();
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
