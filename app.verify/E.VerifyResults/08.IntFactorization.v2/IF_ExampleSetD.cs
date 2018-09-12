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
      return new List<IExample>();
    }

    public override List<IExample> GetLargeExamples()
    {
      List<IExample> largeExamples =
        new List<IExample>()
        {
          new IF_Example()
          {
            Name = "E05",
            Input = new int[] {1,0,0,0,0,0,0,0,0,0,0}.Reverse().ToArray() // 1024
          },
          new IF_Example()
          {
            Name = "E06",
            Input = new int[] {1,0,0,1,1,1,0,0,0,1,0,0,0,0,0,0}.Reverse().ToArray() // 40 000
          },
          new IF_Example()
          {
            Name = "E07",
            Input = new int[] {1, 0, 0, 1}.Reverse().ToArray() // 9 = 3 * 3
          },
          new IF_Example()
          {
            Name = "E08",
            Input = new int[] {1, 1, 0, 0, 1, 0}.Reverse().ToArray() // 50
          },
          new IF_Example()
          {
            Name = "E09",
            Input = new int[] {1, 0, 1, 0, 0, 0, 0}.Reverse().ToArray() // 80
          },
          new IF_Example()
          {
            Name = "E10",
            Input = new int[] {1, 0, 1, 0, 0, 0, 1}.Reverse().ToArray() // 81
          },
          new IF_Example()
          {
            Name = "E11",
            Input = new int[] {1, 1, 1, 0, 0, 0, 0, 1}.Reverse().ToArray() // 225
          },
          new IF_Example()
          {
            Name = "E12",
            Input = new int[] {1,0,0,1,1,1,0,0,0,1,0,0,0,0}.Reverse().ToArray() // 10 0000
          },
          new IF_Example()
          {
            Name = "E13",
            Input = new int[] {1,0,0,1,1,1,0,0,0,1,0,0,0,0,0,0}.Reverse().ToArray() // 40 000
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
