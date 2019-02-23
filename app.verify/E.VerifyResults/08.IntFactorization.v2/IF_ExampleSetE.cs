////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;
using ExistsAcceptingPath;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace VerifyResults.v2
{
  public class IF_ExampleSetE : IF_ExampleSets
  {
    #region public members

    public override List<IExample> GetSmallExamples()
    {
      List<IExample> smallExamples =
        new List<IExample>
        {
          new IF_Example
          {
            Name = "E01",
            // 4834854734712912934856756745239
            Input = new int[]
            {
              1,1,1,1,0,1,0,0,0,0,0,1,1,0,0,1,0,0,0,0,
              1,0,0,0,0,1,1,0,0,1,1,1,0,1,1,1,1,1,0,1,
              1,0,0,0,0,0,1,1,0,1,1,1,0,0,1,1,1,0,0,1,
              0,1,0,0,1,1,0,1,0,0,0,0,0,1,0,0,1,0,1,1,
              1,0,1,1,1,0,1,1,0,1,0,1,0,0,0,0,0,1,0,1,1,1
            }.Reverse().ToArray()
          }
        };

      return smallExamples;
    }

    public override List<IExample> GetLargeExamples()
    {
      List<IExample> largeExamples =
        new List<IExample>
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
