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
      List<IExample> smallExamples =
        new List<IExample>()
        {
          new IF_Example()
          {
            Name = "E03",
            Input = new int[] {1, 0, 1, 0, 0, 0, 1}.Reverse().ToArray() // 81
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
