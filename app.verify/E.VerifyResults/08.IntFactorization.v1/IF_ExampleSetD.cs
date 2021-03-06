﻿////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;
using ExistsAcceptingPath;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace VerifyResults.v1
{
  public class IF_ExampleSetD : IF_ExampleSets
  {
    #region public members

    public override List<IExample> GetSmallExamples()
    {
      return new List<IExample>();
    }

    public override List<IExample> GetLargeExamples()
    {
      List<IExample> largeExamples = new()
        {
          new IF_Example
          {
            Name = "E01",
            // 10 0000
            Input = new int[] { 1, 0, 0, 1, 1, 1, 0, 0, 0, 1, 0, 0, 0, 0 }.Reverse().ToArray()
          }
        };

      return largeExamples;
    }

    public override List<IExample> GetRandomExamples(
      int count,
      int inputLength)
    {
      return new List<IExample>();
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
