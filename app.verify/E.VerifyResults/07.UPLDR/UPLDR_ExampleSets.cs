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
  public class UPLDR_ExampleSets : ExampleSet
  {
    #region public members

    public override string Name { get; set; } = string.Empty;

    public override List<IExample> GetSmallExamples()
    {
      return new List<IExample>
        {
          new UPLDR_Example
            {
              Name = "E1",
              Input = new int[] {1, 2, 4}
            },
          new UPLDR_Example
            {
              Name = "E2",
              Input = new int[] {3, 0, 4}
            },
          new UPLDR_Example
            {
              Name = "E3",
              Input = new int[]
              {
                2, 0, 3, 1, 4
              }
            }
        };
    }

    public override List<IExample> GetLargeExamples()
    {
      return new List<IExample>();
    }

    public override List<IExample> GetRandomExamples(
      int count, int inputLength)
    {
      List<IExample> examples = new();

      for (int i = 0; i < count; i++)
      {
        IExample example = new UPAPAE_Example
          {
            Name = "RE" + i,
            Input = AppHelper.ProduceRandomBinArray(inputLength)
          };

        examples.Add(example);
      }

      return examples;
    }

    public override ICheckAlgorithm GetCheckAlgorithm()
    {
      return new UPLDR_CheckAlgorithm();
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
