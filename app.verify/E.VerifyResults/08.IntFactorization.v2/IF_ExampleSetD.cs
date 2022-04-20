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
          Input = new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 }.Reverse().ToArray()
        }
      };

      return smallExamples;
    }

    public override List<IExample> GetLargeExamples()
    {
      return new();
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
