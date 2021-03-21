////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public class RowSubtractorSparceMThreads : ITPLCollectionItem
  {
    #region Ctors

    public RowSubtractorSparceMThreads() { }

    public RowSubtractorSparceMThreads(
      SparseMatrix A,
      long rowIndex,
      long m,
      long n,
      long p,
      SortedDictionary<long, RationalNumber> Mk,
      RationalNumber Mkp)
    {
      this.A = A;
      this.rowIndex = rowIndex;
      this.m = m;
      this.n = n;
      this.p = p;
      this.Mk = Mk;
      this.Mkp = Mkp;
    }

    #endregion

    #region public members

    public SparseMatrix A { get; set; } = default!;

    public long rowIndex { get; set; }

    public long m { get; set; }
    public long n { get; set; }
    public long p { get; set; }

    public SortedDictionary<long, RationalNumber> Mk { get; set; } = new();
    public RationalNumber Mkp { get; set; } = default!;

    public bool Done { get; }

    public void Run()
    {
      RationalNumber t = new(0, 1);
      RationalNumber Mip = A.Get(rowIndex, p);

      if (Mip.IsEqualsTo0())
      {
        return;
      }

      SortedDictionary<long, RationalNumber> Mi = A.GetRow(rowIndex);

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

        lock (A)
        {
          RationalNumber Mij = SparseMatrix.Get(Mi, j);
          Mij.Subtr(t);
          SparseMatrix.Set(Mi, j, Mij);
        }
      }

      lock (A)
      {
        Mip.Assign0();
        A.Set(rowIndex, p, Mip);
      }
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
