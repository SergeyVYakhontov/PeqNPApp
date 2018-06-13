////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;
using ExistsAcceptingPath;
using MTDefinitions;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace VerifyResults
{
  public class Lang2_DVN : IAlgorithm
  {
    #region public members

    public bool Decide(int[] input)
    {
      long inputLength = input.Length;

      if (inputLength < 2)
      {
        return false;
      }

      for (long i = 0; i < inputLength-1; i++)
      {
        if (input[i] == input[i + 1])
        {
          return true;
        }
      }

      return false;
    }

    public int[] Compute(int[] input)
    {
      return new int[] {};
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
