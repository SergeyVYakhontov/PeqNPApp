////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;
using ExistsAcceptingPath;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace VerifyResults
{
  public static partial class AppHelper
  {
    #region public members

    public static readonly Random RandNumber = new Random(0);

    public static int[] ProduceRandomArray(int alphabetSize, int length)
    {
      int[] result = new int[length];

      for (int i=0; i<length; i++)
      {
        result[i] = RandNumber.Next(0, alphabetSize);
      }

      return result;
    }

    public static int[] ProduceRandomBinArray(int length)
    {
      return ProduceRandomArray(2, length);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
