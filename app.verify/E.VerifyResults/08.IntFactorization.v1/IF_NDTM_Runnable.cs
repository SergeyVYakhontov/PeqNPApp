////////////////////////////////////////////////////////////////////////////////////////////////////

using MTExtDefinitions.v1;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace VerifyResults.v1
{
  public class IF_NDTM_Runnable : TM_Runnable
  {
    #region public members

    public override bool Decide(int[] input)
    {
      tm = new IF_NDTM(input.Length);
      tm.Setup();

      tm.Run(input);

      return tm.Accepted;
    }

    public override int[] Compute(int[] input)
    {
      return tm.GetAcceptingInstanceOutput(input);
    }

    public override bool CompareOutputs => false;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
