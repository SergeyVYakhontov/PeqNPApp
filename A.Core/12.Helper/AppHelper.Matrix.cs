////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public static partial class AppHelper
  {
    #region public members

    public static ElemType[][] CreateAmnMatrix<ElemType>(long m, long n)
    {
      ElemType[][] A = new ElemType[m][];

      for (uint i = 0; i < m; i++)
      {
        A[i] = new ElemType[n];
      }

      return A;
    }

    public static string GetMatrixRepr(long[][] A)
    {
      long m = A.GetUpperBound(0) + 1;
      long n = A.GetUpperBound(1) + 1;

      string[] rows = new string[m];

      for (uint i = 0; i < m; i++)
      {
        long[] currRow = new long[n];

        for (uint j = 0; j < n; j++)
        {
          currRow[j] = A[i][j];
        }

        rows[i] = "{" + string.Join(",", currRow) + "}";
      }

      string result = "A={" + string.Join(",", rows) + "}";

      return result;
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
