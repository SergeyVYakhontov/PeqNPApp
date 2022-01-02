////////////////////////////////////////////////////////////////////////////////////////////////////

namespace VerifyResults
{
  public abstract class TM_Runnable : IRunnable
  {
    #region public members

    public string Kind => "TM";

    public virtual bool RunCheckAlgorithm => false;
    public bool ComputationFinished => tm.ComputationFinished;
    public abstract bool CompareOutputs { get; }

    public abstract bool Decide(int[] input);
    public abstract int[] Compute(int[] input);

    #endregion

    #region private members

    protected OneTapeTuringMachine tm = default!;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
