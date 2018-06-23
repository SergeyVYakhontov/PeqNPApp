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
      return Enumerable.Repeat(0, length).
        Select(i => RandNumber.Next(0, alphabetSize)).ToArray();
    }

    public static int[] ProduceRandomBinArray(int length)
    {
      return ProduceRandomArray(2, length);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
