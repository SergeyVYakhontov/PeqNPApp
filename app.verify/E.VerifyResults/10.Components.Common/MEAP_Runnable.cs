////////////////////////////////////////////////////////////////////////////////////////////////////

namespace VerifyResults
{
  public abstract class MEAP_Runnable : IRunnable
  {
    #region public members

    public string Kind => "MEAP";

    public abstract bool RunCheckAlgorithm { get; }
    public abstract bool ComputationFinished { get; }
    public abstract bool CompareOutputs { get; }

    public abstract bool Decide(int[] input);
    public abstract int[] Compute(int[] input);

    #endregion

    #region privatre members

    protected OneTapeTuringMachine tm = default!;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
