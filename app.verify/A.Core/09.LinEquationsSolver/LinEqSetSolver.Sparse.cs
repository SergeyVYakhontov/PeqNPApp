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
  public class LinEqSetSolverSparse
  {
    #region Ctors

    public LinEqSetSolverSparse(SparseMatrix A)
    {
      this.A = A;

      m = A.m;
      n = A.n - 1;
    }

    #endregion

    #region public members

    public bool IfSolutionExists()
    {
      RunGaussElimination();

      for (long i = m - 1; i >= 0; i--)
      {
        RationalNumber lastColumnCoeff = A.Get(i, lastColumnIndex);
        RationalNumber bVectorElem = A.Get(i, bVectorIndex());

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

    private readonly SparseMatrix A;
    private readonly long m, n;

    private long lastColumnIndex => n - 1;
    private long bVectorIndex() => n;

    private void RunGaussElimination()
    {
      long s = 0;
      RationalNumber t = new RationalNumber(0, 1);

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
            if (!(A.Get(i, k + s).IsEqualsTo0()))
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

        A.SwapRows(k, i_max);

        long p = k + s;

        SortedDictionary<long, RationalNumber> Mk = A.GetRow(k);
        RationalNumber Mkp = SparseMatrix.Get(Mk, p);

        for (long i = k + 1; i < m; i++)
        {
          RationalNumber Mip = A.Get(i, p);

          if (Mip.IsEqualsTo0())
          {
            continue;
          }

          SortedDictionary<long, RationalNumber> Mi = A.GetRow(i);

          for (long j = p + 1; j < n + 1; j++)
          {
            RationalNumber Mkj = SparseMatrix.Get(Mk, j);

            if (Mkj.IsEqualsTo0())
            {
              continue;
            }

            t.Assign(Mip);
            t.Div(Mkp);
            t.Mult(Mkj);

            RationalNumber Mij = SparseMatrix.Get(Mi, j);
            Mij.Subtr(t);
            SparseMatrix.Set(Mi, j, Mij);
          }

          Mip.Assign0();
          A.Set(i, p, Mip);
        }
      }
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
