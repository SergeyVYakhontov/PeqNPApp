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
  public class IF_ExampleSetDebug : IF_ExampleSets
  {
    #region public members

    public override List<IExample> GetSmallExamples()
    {
      List<IExample> exampleForDebug = new()
        {
          new IF_Example
          {
            Name = "E04",
            Input = new int[] {0, 1, 1, -1, -1, 2, 1, 1, -1, -1, 2, 0, 1, -1, -1, 2}
          }
        };

      return exampleForDebug;
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
