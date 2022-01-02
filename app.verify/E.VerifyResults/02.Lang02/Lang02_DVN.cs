////////////////////////////////////////////////////////////////////////////////////////////////////

namespace VerifyResults
{
  public class Lang2_DVN : IAlgorithm
  {
    #region public members

    public bool Decide(int[] input)
    {
      long inputLength = input.Length;

      if (inputLength < 2)
      {
        return false;
      }

      for (long i = 0; i < inputLength-1; i++)
      {
        if (input[i] == input[i + 1])
        {
          return true;
        }
      }

      return false;
    }

    public int[] Compute(int[] input)
    {
      return Array.Empty<int>();
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
