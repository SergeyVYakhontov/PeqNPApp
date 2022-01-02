////////////////////////////////////////////////////////////////////////////////////////////////////

namespace VerifyResults
{
  public abstract class VN_Runnable : IRunnable
  {
    #region public members

    public string Kind => "VN";

    public virtual bool RunCheckAlgorithm => false;
    public abstract bool ComputationFinished { get; }
    public abstract bool CompareOutputs { get; }

    public abstract bool Decide(int[] input);
    public abstract int[] Compute(int[] input);

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
