////////////////////////////////////////////////////////////////////////////////////////////////////

namespace VerifyResults
{
  public class UPAPMNE_DVN : IAlgorithm
  {
    #region public members

    public bool Decide(int[] input)
    {
      return (input.Last() == 2);
    }

    public int[] Compute(int[] input)
    {
      return Array.Empty<int>();
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
