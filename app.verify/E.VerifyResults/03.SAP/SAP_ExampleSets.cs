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
  public class SAP_ExampleSets : ExampleSet
  {
    #region public members

    public override string Name { get; set; }

    public override List<IExample> GetSmallExamples()
    {
      return new List<IExample>
        {
          new SAP_Example
            {
              Name = "E1",
              Input = new int[] {1, 0}
            },
          new SAP_Example
            {
              Name = "E2",
              Input = new int[] {1, 1}
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
        IExample example = new LAP_Example
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
      return new SAP_CheckAlgorithm();
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
