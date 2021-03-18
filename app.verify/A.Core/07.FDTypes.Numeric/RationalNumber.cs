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
  public partial class RationalNumber
  {
    #region Ctors

    public RationalNumber(long p, long q)
    {
      this.p = p;
      this.q = q;

      Normalize();
    }

    public RationalNumber(RationalNumber r)
    {
      this.p = r.p;
      this.q = r.q;
    }

    #endregion

    #region public members

    public long p { get; private set; }
    public long q { get; private set; }

    public static RationalNumber Const_Neg1 { get; } = new RationalNumber(-1, 1);
    public static RationalNumber Const_0 { get; } = new RationalNumber(0, 1);
    public static RationalNumber Const_1 { get; } = new RationalNumber(1, 1);

    public long ToLong()
    {
      Ensure.That(q).Is(1);

      return p;
    }

    public override string ToString()
    {
      if (IsEqualsToASPair(Const_Neg1))
      {
        return "-1";
      }
      else if (IsEqualsToASPair(Const_0))
      {
        return "0";
      }
      else if (IsEqualsToASPair(Const_1))
      {
        return "1";
      }
      else
      {
        return $"({p}/{q})";
      }
    }

    #endregion

    #region private mebers

    private bool IsEqualsToASPair(RationalNumber r)
    {
      return ((p == r.p) && (q == r.q));
    }

    private void Normalize()
    {
      (p, q) = RationalNumber.Normalize(p, q);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
