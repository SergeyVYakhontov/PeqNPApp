////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public class LinEquationsMatrix
  {
    #region Ctors

    public LinEquationsMatrix()
    {
      this.VarsCount = 0;
      this.EquationsCount = 0;

      this.Equations = new SortedDictionary<long, SortedDictionary<long, RationalNumber>>();
      this.VectorB = new SortedDictionary<long, KeyValuePair<EquationKind, RationalNumber>>();

      this.ObjFuncVars = new SortedSet<long>();
    }

    #endregion

    #region public members

    public long VarsCount { get; private set; }
    public long EquationsCount { get; private set; }

    public SortedDictionary<long, SortedDictionary<long, RationalNumber>> Equations { get; }
    public SortedDictionary<long, KeyValuePair<EquationKind, RationalNumber>> VectorB { get; }

    public SortedSet<long> ObjFuncVars { get; }

    public long AddVariable()
    {
      return VarsCount++;
    }

    public long AddEquation(
      SortedDictionary<long, RationalNumber> coeffs,
      EquationKind equationKind,
      RationalNumber b)
    {
      Equations[EquationsCount] = coeffs;
      VectorB[EquationsCount] = new KeyValuePair<EquationKind, RationalNumber>(equationKind, b);

      return EquationsCount++;
    }

    public void RemoveEquation(long equation)
    {
      Equations.Remove(equation);
      VectorB.Remove(equation);
    }

    public String GetLinEqSetQuery()
    {
      int[] varsArray = new int[VarsCount];

      SortedDictionary<long, RationalNumber>[] eqsSetArray = AppHelper.SortedDictionaryToArray(Equations, EquationsCount);
      String[] equationsArray = eqsSetArray.Select(e =>
        "{" + String.Join(",", AppHelper.SortedDictionaryToArray(e, VarsCount)
          .Select(r => (r != null ? r.ToString() : "0"))) +
        "}").ToArray();
      String equationsStr = String.Join(",", equationsArray);

      String[] vectorBStr = new String[EquationsCount];
      String[] bVectorArray = AppHelper.SortedDictionaryToArray(VectorB, EquationsCount).Select(e =>
        e.Value.ToString()).ToArray();

      String bVectorStr = String.Join(",", bVectorArray);

      return
        "A={" + equationsStr + "};" +
        "b={" + bVectorStr + "};";
    }

    public RationalNumber[][] GetLinEqSetMatrixOrd()
    {
      long m = EquationsCount;
      long n = VarsCount;

      RationalNumber[][] A = AppHelper.CreateAmnMatrix<RationalNumber>(m, n + 1);
      RationalNumber[][] A_local = A;

      for (int i = 0; i < m; i++)
      {
        for (int j = 0; j < n + 1; j++)
        {
          A[i][j] = new RationalNumber(0, 1);
        }
      }

      Equations.ForEach(r1 => r1.Value.ForEach(
        r2 =>
        {
          A_local[r1.Key][r2.Key] = new RationalNumber(r2.Value);
        }));

      VectorB.ForEach(r =>
      {
        A_local[r.Key][n] = new RationalNumber(r.Value.Value);
      });

      return A;
    }

    public (long[][], long[][]) GetLinEqSetMatrixFast()
    {
      long m = EquationsCount;
      long n = VarsCount;

      long[][] A_p = AppHelper.CreateAmnMatrix<long>(m, n + 1);
      long[][] A_q = AppHelper.CreateAmnMatrix<long>(m, n + 1);

      for (int i = 0; i < m; i++)
      {
        for (int j = 0; j < n + 1; j++)
        {
          A_p[i][j] = 0;
          A_q[i][j] = 1;
        }
      }

      Equations.ForEach(r1 => r1.Value.ForEach(
        r2 =>{
          A_p[r1.Key][r2.Key] = r2.Value.ToLong();
          A_q[r1.Key][r2.Key] = 1;
        }));

      VectorB.ForEach(r =>
        {
          A_p[r.Key][n] = r.Value.Value.ToLong();
          A_q[r.Key][n] = 1;
        });

        return (A_p, A_q);
    }

    public SparseMatrix GetLinEqSetSparseMatrix()
    {
      long m = EquationsCount;
      long n = VarsCount;

      SparseMatrix A = new(m, n + 1);

      Equations.ForEach(r1 => r1.Value.ForEach(
        r2 =>
        {
          A.Set(r1.Key, r2.Key, new RationalNumber(r2.Value));
        }));

      VectorB.ForEach(r =>
      {
        A.Set(r.Key, n, new RationalNumber(r.Value.Value));
      });

      return A;
    }

    public String GetLinProgQuery()
    {
      int[] varsArray = new int[VarsCount];

      for (long i = 0; i < VarsCount; i++)
      {
        if (ObjFuncVars.Contains(i))
        {
          varsArray[i] = 1;
        }
      }

      String minimizeStr = "{" + String.Join(",", varsArray) + "}";

      SortedDictionary<long, RationalNumber>[] eqsSetArray =
        AppHelper.SortedDictionaryToArray(Equations, EquationsCount);
      String[] equationsArray = eqsSetArray.Select(e =>
        "{" + String.Join(",", AppHelper.SortedDictionaryToArray(e, VarsCount)
          .Select(r => (r!=null ? r.ToString() : "0"))) +
        "}").ToArray();
      String equationsStr = String.Join(",", equationsArray);

      String[] bVectorArray = AppHelper.SortedDictionaryToArray(VectorB, EquationsCount).Select(e =>
        "{" + e.Value + "," + GetEquationKindRepr(e.Key) + "}").ToArray();
      String bVectorStr = String.Join(",", bVectorArray);

      return minimizeStr + ",{" + equationsStr + "},{" + bVectorStr + "}";
    }

    #endregion

    #region private members

    private static String GetEquationKindRepr(EquationKind equationKind)
    {
      return equationKind switch
      {
        EquationKind.LessThan => "-1",
        EquationKind.Equal => "0",
        EquationKind.GreaterThan => "1",
        _ => string.Empty,
      };
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
