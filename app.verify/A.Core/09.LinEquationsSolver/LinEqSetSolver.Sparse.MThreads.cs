﻿////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Ninject;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public class LinEqSetSolverSparseMThreads
  {
    #region Ctors

    public LinEqSetSolverSparseMThreads(SparseMatrix A)
    {
      this.configuration = Core.AppContext.GetConfiguration();

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

    private readonly IReadOnlyKernel configuration;

    private readonly SparseMatrix A;
    private readonly long m, n;

    private long lastColumnIndex => n - 1;
    private long bVectorIndex() => n;

    private static void SelectRows(
      long[] rowsToProcess,
      long from,
      long count,
      long[] rowsToSubtract,
      out long rowsToSubtractCount)
    {
      rowsToSubtractCount = 0;

      for (long i = from; (i < rowsToProcess.Length) && (rowsToSubtractCount < count); i++)
      {
        rowsToSubtract[rowsToSubtractCount] = rowsToProcess[i];
        rowsToSubtractCount++;
      }
    }

    private void RunGaussElimination()
    {
      ITPLOptions tplOptions = configuration.Get<ITPLOptions>();
      uint gaussElimRunnersCount = tplOptions.GaussElimRunnersCount;

      long s = 0;
      long[] rowsToSubtract = new long[gaussElimRunnersCount];

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

        long rowsToProcessCount = m - (k + 1);
        long[] rowsToProcess = new long[m];

        for (long w = 0; w < rowsToProcessCount; w++)
        {
          long d = w + (k + 1);
          rowsToProcess[d] = d;
        }

        long from = k + 1;

        while (true)
        {
          SelectRows(
            rowsToProcess,
            from,
            gaussElimRunnersCount,
            rowsToSubtract,
            out long rowsToSubtractCount);

          if (rowsToSubtractCount == 0)
          {
            break;
          }

          Task[] tasks = new Task[rowsToSubtractCount];
          List<RowSubtractorSparceMThreads> rowSubtractors = new();

          for (int i = 0; i < rowsToSubtractCount; i++)
          {
            long row = rowsToSubtract[i];
            RowSubtractorSparceMThreads rowSubtractor = new();

            rowSubtractor.A = A;
            rowSubtractor.rowIndex = row;
            rowSubtractor.m = m;
            rowSubtractor.n = n;
            rowSubtractor.p = p;
            rowSubtractor.Mk = Mk;
            rowSubtractor.Mkp = Mkp;

            rowSubtractors.Add(rowSubtractor);
          }

          TPLCollectionRunner<RowSubtractorSparceMThreads> rowSubtractorsRunner = new
            (
              rowSubtractors,
              gaussElimRunnersCount,
              WaitMethod.WaitAll,
              _ => default!
            );
          rowSubtractorsRunner.Run();

          from += rowsToSubtract.Length;
        }
      }
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
