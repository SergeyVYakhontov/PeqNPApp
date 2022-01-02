////////////////////////////////////////////////////////////////////////////////////////////////////

namespace VerifyResults
{
  public class SAP_NDTM_Runnable : TM_Runnable
  {
    #region public members

    public override bool Decide(int[] input)
    {
      tm = new SAP_NDTM();
      tm.Setup();

      tm.Run(input);

      return tm.Accepted;
    }

    public override int[] Compute(int[] input)
    {
      return tm.GetAcceptingInstanceOutput(input);
    }

    public override bool CompareOutputs => true;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
