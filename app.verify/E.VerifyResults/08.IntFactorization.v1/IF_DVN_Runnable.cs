////////////////////////////////////////////////////////////////////////////////////////////////////

namespace VerifyResults.v1
{
  public class IF_DVN_Runnable: VN_Runnable
  {
    #region private members

    private IF_DVN dvn = default!;

    #endregion

    #region public members

    public override bool Decide(int[] input)
    {
      dvn = new IF_DVN();

      return dvn.Decide(input);
    }

    public override int[] Compute(int[] input)
    {
      return dvn.Compute(input);
    }

    public override bool ComputationFinished => true;
    public override bool CompareOutputs => false;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
