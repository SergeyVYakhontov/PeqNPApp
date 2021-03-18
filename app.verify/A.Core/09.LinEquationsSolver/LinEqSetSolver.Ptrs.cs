////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Runtime.CompilerServices;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public class LinEqSetSolverPtrs
  {
    #region Ctors

    public LinEqSetSolverPtrs(long[][] A_p, long[][] A_q)
    {
      this.A_p = A_p;
      this.A_q = A_q;

      m = A_p.GetUpperBound(0) + 1;
      n = A_p[0].GetUpperBound(0);
    }

    #endregion

    #region public members

    public bool IfSolutionExists()
    {
      RunGaussEliminationFast();

      for (long i = m - 1; i >= 0; i--)
      {
        RationalNumber lastColumnCoeff = new(A_p[i][lastColumnIndex], A_q[i][lastColumnIndex]);
        RationalNumber bVectorElem = new(A_p[i][bVectorIndex()], A_q[i][bVectorIndex()]);

        if (!lastColumnCoeff.IsEqualsTo0())
        {
          break;
        }

        if (lastColumnCoeff.IsEqualsTo0() && !bVectorElem.IsEqualsTo0())
        {
          return false;
        }
      }

      return true;
    }

    #endregion

    #region private members

    private readonly long[][] A_p;
    private readonly long[][] A_q;
    private readonly long m, n;

    private long lastColumnIndex => n - 1;

    private long bVectorIndex() => n;

    private void RunGaussEliminationFast()
    {
      long s = 0;

      for (long k = 0; k < m; k++)
      {
        long i_max = -1;

        while (true)
        {
          if (k + s == n)
          {
            return;
          }

          for (long i = k; i < m; i++)
          {
            if (A_p[i][k + s] != 0)
            {
              i_max = i;
            }
          }

          if (i_max == -1)
          {
            s++;
          }
          else
          {
            break;
          }
        }

        (A_p[k], A_p[i_max]) = (A_p[i_max], A_p[k]);
        (A_q[k], A_q[i_max]) = (A_q[i_max], A_q[k]);

        long[] Mkp = A_p[k];
        long[] Mkq = A_q[k];

        long p = k + s;
        long Mkp_p = Mkp[p];
        long Mkp_q = Mkq[p];

        for (long i = k + 1; i < m; i++)
        {
          long Mip_p = A_p[i][p];
          long Mip_q = A_q[i][p];

          if (Mip_p == 0)
          {
            continue;
          }

          unsafe
          {
            fixed (long* Mk_p_fptr = A_p[k])
            {
              fixed (long* Mk_q_fptr = A_q[k])
              {
                fixed (long* Mi_p_fptr = A_p[i])
                {
                  fixed (long* Mi_q_fptr = A_q[i])
                  {
                    long* Mk_p_ptr = Mk_p_fptr + (p + 1);
                    long* Mk_q_ptr = Mk_q_fptr + (p + 1);

                    long* Mi_p_ptr = Mi_p_fptr + (p + 1);
                    long* Mi_q_ptr = Mi_q_fptr + (p + 1);

                    for (long j = p + 1; j < n + 1; j++)
                    {
                      long Mkj_p = *Mk_p_ptr;
                      long Mkj_q = *Mk_q_ptr;

                      long Mij_p = *Mi_p_ptr;
                      long Mij_q = *Mi_q_ptr;

#pragma warning disable CA1508
                      if (Mkj_p == 0)
#pragma warning restore CA1508
                      {
                        Mk_p_ptr++;
                        Mk_q_ptr++;

                        Mi_p_ptr++;
                        Mi_q_ptr++;

                        continue;
                      }

                      long t_p = Mip_p;
                      long t_q = Mip_q;

                      t_p *= Mkp_q;
                      t_q *= Mkp_p;

                      t_p *= Mkj_p;
                      t_q *= Mkj_q;

                      t_p = (t_p * Mij_q) - (Mij_p * t_q);
                      t_q = Mij_q * t_q;

                      (t_p, t_q) = RationalNumber.Normalize(t_p, t_q);

                      *Mi_p_ptr = t_p;
                      *Mi_q_ptr = t_q;

                      Mk_p_ptr++;
                      Mk_q_ptr++;

                      Mi_p_ptr++;
                      Mi_q_ptr++;
                    }
                  }
                }
              }
            }
          }

          A_p[i][p] = 0;
          A_q[i][p] = 1;
        }
      }
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
