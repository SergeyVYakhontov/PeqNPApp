////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Runtime.CompilerServices;
using EnsureThat;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public partial class RationalNumber
  {
    #region public members

    public static void Normalize(ref long p, ref long q)
    {
      Ensure.That(q, "Divisor").IsNot(0);

      if (p == 0)
      {
        q = 1;

        return;
      }

      if (q < 0)
      {
        p = -p;
        q = -q;
      }

      if (q == 1)
      {
        return;
      }

      long pAbs = Math.Abs(p);

      if ((pAbs == 1) && (q == 1))
      {
        return;
      }

      long gcd = AppHelper.GCD(pAbs, q);

      p = p / gcd;
      q = q / gcd;
    }

    public void Assign(RationalNumber r)
    {
      p = r.p;
      q = r.q;
    }

    public void Assign0()
    {
      p = 0;
      q = 1;
    }

    public bool IsEqualsTo(RationalNumber r)
    {
      return ((p * r.q) == (q * r.p));
    }

    public bool IsEqualsTo0()
    {
      return (p == 0);
    }

    public void Add(RationalNumber r)
    {
      if (r.p == 0)
      {
        return;
      }

      if (p == 0)
      {
        p = r.p;
        q = r.q;
        
        return;
      }

      p = p * r.q + q * r.p;
      q = q * r.q;

      Normalize();
    }

    public void Subtr(RationalNumber r)
    {
      if (r.p == 0)
      {
        return;
      }

      if (p == 0)
      {
        p = -r.p;
        q = r.q;

        return;
      }

      p = p * r.q - q * r.p;
      q = q * r.q;

      Normalize();
    }

    public void Mult(RationalNumber r)
    {
      if (r.p == 0)
      {
        p = 0;
        q = 1;
        
        return;
      }

      if (p == 0)
      {
        return;
      }

      p = p * r.p;
      q = q * r.q;

      Normalize();
    }

    public void Div(RationalNumber r)
    {
      Ensure.That(r.IsEqualsTo(Const_0)).IsFalse();

      if (p == 0)
      {
        return;
      }

      p = p * r.q;
      q = q * r.p;

      Normalize();
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
