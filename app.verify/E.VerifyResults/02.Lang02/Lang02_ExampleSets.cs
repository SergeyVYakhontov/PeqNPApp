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
  public class Lang02_ExampleSets : ExampleSet
  {
    #region public members

    public override string Name { get; set; }

    public override List<IExample> GetSmallExamples()
    {
      return new List<IExample>
        {
          new Lang2_Example
            {
              Name = "E1",
              Input = new int[] {1, 0, 0, 1, 1}
            },
          new Lang2_Example
            {
              Name = "E2",
              Input = new int[] {1, 0, 1}
            },
          new Lang2_Example
            {
              Name = "E3",
              Input = new int[] {1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0}
            },
          new Lang2_Example
             {
               Name = "E4",
               Input = new int[]
               {
                 1, 0, 1, 0, 1, 0, 1, 0, 1, 0,
                 1, 0, 1, 0, 1, 0, 1, 0, 1, 0,
                 1, 0, 1, 1, 1, 0, 1, 0, 1, 0, 1, 1
               }
             },
          new Lang2_Example
             {
               Name = "E5",
               Input = new int[]
               {
                 1, 0, 1, 0, 1, 0, 1, 0, 1, 0,
                 1, 0, 1, 0, 1, 0, 1, 0, 1, 0,
                 1, 0, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0,
                 1, 0, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0
               }
             },
          new Lang2_Example
             {
               Name = "E6",
               Input = new int[]
               {
                 1, 0, 1, 0, 1, 0, 1, 0, 1, 0,
                 1, 0, 1, 0, 1, 0, 1, 0, 1, 0,
                 1, 0, 1, 0, 1, 0, 1, 0, 1, 0,
                 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0
               }
             }
        };
    }

    public override List<IExample> GetLargeExamples()
    {
      return new List<IExample>
        {
          new Lang2_Example
             {
               Name = "E7",
               Input = new int[]
               {
                 1, 0, 1, 0, 1, 0, 1, 0, 1, 0,
                 1, 0, 1, 0, 1, 0, 1, 0, 1, 0,
                 1, 0, 1, 0, 1, 0, 1, 0, 1, 0,
                 1, 0, 1, 0, 1, 0, 1, 0, 1, 0,
                 1, 0, 1, 0, 1, 0, 1, 0, 1, 0,
                 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1 
               }
             }
        };
    }

    public override List<IExample> GetRandomExamples(
      int count, int inputLength)
    {
      List<IExample> examples = new List<IExample>();

      for (int i = 0; i < count; i++)
      {
        IExample example = new Lang2_Example
          {
            Name = "RE" + i,
            Input = AppHelper.ProduceRandomBinArray(inputLength)
          };

        int putDigitPair = AppHelper.RandNumber.Next(0, 2);
        int placeToPut = AppHelper.RandNumber.Next(0, inputLength - 1);
        int digitToPut = AppHelper.RandNumber.Next(0, 2);

        if (putDigitPair == 1)
        {
          example.Input[placeToPut] = digitToPut;
          example.Input[placeToPut + 1] = digitToPut;
        }

        examples.Add(example);
      }

      return examples;
    }

    public override ICheckAlgorithm GetCheckAlgorithm()
    {
      return new Lang2_CheckAlgorithm();
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
