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
  public class LinEqSetSolver
  {
    #region Ctors

    public LinEqSetSolver(RationalNumber[][] A)
    {
      this.A = A;

      m = A.GetUpperBound(0) + 1;
      n = A[0].GetUpperBound(0);
    }

    #endregion

    #region public members

    public bool IfSolutionExists()
    {
      RunGaussElimination();

      for (long i = m - 1; i >= 0; i--)
      {
        RationalNumber lastColumnCoeff = A[i][lastColumnIndex];
        RationalNumber bVectorElem = A[i][GetbVectorIndex()];

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

    private readonly RationalNumber[][] A;
    private readonly long m, n;

    private long lastColumnIndex => n - 1;

    private long GetbVectorIndex() => n;

    private void RunGaussElimination()
    {
      long s = 0;
      RationalNumber t = new(0, 1);

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
            if (!A[i][k + s].IsEqualsTo0())
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

        (A[k], A[i_max]) = (A[i_max], A[k]);

        long p = k + s;

        RationalNumber[] Mk = A[k];
        RationalNumber Mkp = Mk[p];

        for (long i = k + 1; i < m; i++)
        {
          RationalNumber Mip = A[i][p];

          if (Mip.IsEqualsTo0())
          {
            continue;
          }

          RationalNumber[] Mi = A[i];

          for (long j = p + 1; j < n + 1; j++)
          {
            RationalNumber Mkj = Mk[j];

            if (Mkj.IsEqualsTo0())
            {
              continue;
            }

            t.Assign(Mip);
            t.Div(Mkp);
            t.Mult(Mkj);
            t.Subtr(Mi[j]);

            Mi[j] = new RationalNumber(t);
          }

          Mip.Assign0();
        }
      }
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
