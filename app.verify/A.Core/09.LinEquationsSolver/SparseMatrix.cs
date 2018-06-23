////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public class SparseMatrix
  {
    #region Ctors

    public SparseMatrix(long m, long n)
    {
      this.m = m;
      this.n = n;

      cells = new SortedDictionary<long, RationalNumber>[m];

      for (long i = 0; i < m; i++)
      {
        cells[i] = new SortedDictionary<long, RationalNumber>();
      }
    }

    #endregion

    #region public members

    public long m { get; }
    public long n { get; }

    public SortedDictionary<long, RationalNumber> GetRow(long i)
    {
      return cells[i];
    }

    public void Set(long i, long j, RationalNumber v)
    {
      if (v.IsEqualsTo0())
      {
        cells[i].Remove(j);
        return;
      }

      cells[i][j] = v;
    }

    public RationalNumber Get(long i, long j)
    {
      SortedDictionary<long, RationalNumber> row = cells[i];

      if (!row.TryGetValue(j, out RationalNumber v))
      {
        return new RationalNumber(0, 1);
      }

      return v;
    }

    public static RationalNumber Get(SortedDictionary<long, RationalNumber> row, long j)
    {
      if (!row.TryGetValue(j, out RationalNumber v))
      {
        return new RationalNumber(0, 1);
      }

      return v;
    }

    public static void Set(SortedDictionary<long, RationalNumber> row, long j, RationalNumber v)
    {
      if (v.IsEqualsTo0())
      {
        row.Remove(j);

        return;
      }

      row[j] = v;
    }

    public void SwapRows(long i, long j)
    {
      SortedDictionary<long, RationalNumber> t = cells[i];

      cells[i] = cells[j];
      cells[j] = t;
    }

    #endregion

    #region private members

    private readonly SortedDictionary<long, RationalNumber>[] cells;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
