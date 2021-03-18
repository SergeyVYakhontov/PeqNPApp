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
  public class IF_ExampleSetD : IF_ExampleSets
  {
    #region public members

    public override List<IExample> GetSmallExamples()
    {
      List<IExample> smallExamples = new()
        {
          new IF_Example
          {
            Name = "E01",
            // 512
            Input = new int[] {1,0,0,0,0,0,0,0,0,0}.Reverse().ToArray()
          }
        };

      return smallExamples;
    }

    public override List<IExample> GetLargeExamples()
    {
      /* List<IExample> largeExamples =
        new List<IExample>
        { 
          new IF_Example
          {
            Name = "E01",
            // 512
            Input = new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 }.Reverse().ToArray()
          },
          new IF_Example
          {
            Name = "E02",
            // 1024
            Input = new int[] {1,0,0,0,0,0,0,0,0,0,0}.Reverse().ToArray()
          },
          new IF_Example
          {
            Name = "E03",
            // 9 = 3 * 3
            Input = new int[] {1, 0, 0, 1}.Reverse().ToArray()
          },
          new IF_Example
          {
            Name = "E04",
            // 50
            Input = new int[] {1, 1, 0, 0, 1, 0}.Reverse().ToArray()
          },
          new IF_Example
          {
            Name = "E05",
            // 80
            Input = new int[] {1, 0, 1, 0, 0, 0, 0}.Reverse().ToArray()
          },
          new IF_Example
          {
            Name = "E06",
            // 81
            Input = new int[] {1, 0, 1, 0, 0, 0, 1}.Reverse().ToArray()
          },
          new IF_Example
          {
            Name = "E07",
            // 225
            Input = new int[] {1, 1, 1, 0, 0, 0, 0, 1}.Reverse().ToArray()
          },
          new IF_Example
          {
            Name = "E08",
            // 10 0000
            Input = new int[] {1,0,0,1,1,1,0,0,0,1,0,0,0,0}.Reverse().ToArray()
          },
          new IF_Example
          {
            Name = "E09",
            // 40 000
            Input = new int[] {1,0,0,1,1,1,0,0,0,1,0,0,0,0,0,0}.Reverse().ToArray()
          }
        };

      return largeExamples; */

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
