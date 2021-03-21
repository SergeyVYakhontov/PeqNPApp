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
  public class Lang01_ExampleSets : ExampleSet
  {
    #region public members

    public override string Name { get; set; } = string.Empty;

    public override List<IExample> GetSmallExamples()
    {
      return new List<IExample>
        {
          new Lang01_Example
            {
              Name = "E1",
              Input = new int[] {1, 1, 1}
            },
          new Lang01_Example
            {
              Name = "E2",
              Input = new int[] {1, 0, 0}
            },
          new Lang01_Example
            {
              Name = "E3",
              Input = new int[] {1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1}
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
        IExample example = new Lang01_Example
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
      return new Lang01_CheckAlgorithm();
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
