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
  public class IF_ExampleSetB : IF_ExampleSets
  {
    #region public members

    public override List<IExample> GetSmallExamples()
    {
      List<IExample> smallExamples = new()
        {
          new IF_Example
          {
            Name = "E01",
            // 6 = 3 * 2
            Input = new int[] {1, 1, 0}.Reverse().ToArray()
          }
        };

      return smallExamples;
    }

    public override List<IExample> GetLargeExamples()
    {
      return new List<IExample>();
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
