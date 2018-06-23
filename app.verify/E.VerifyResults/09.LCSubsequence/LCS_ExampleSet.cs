////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;
using ExistsAcceptingPath;
using MTExtDefinitions;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace VerifyResults
{
  public class LCS_ExampleSet : ExampleSet
  {
    #region public members

    public override string Name { get; set; }

    public override List<IExample> GetSmallExamples()
    {
      return new List<IExample>()
        {
          new LCS_Example()
          {
            Name = "E1",
            Input = new int[1] {0}
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
      return new List<IExample>()
        {
          new LCS_Example()
            {
              Name = "Random example",
              Input = new int[1]{0}
            }
        };
    }

    public override ICheckAlgorithm GetCheckAlgorithm()
    {
      return new LCS_CheckAlgorithm();
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
