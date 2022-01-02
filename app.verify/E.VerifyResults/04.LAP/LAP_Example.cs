////////////////////////////////////////////////////////////////////////////////////////////////////

namespace VerifyResults
{
  public class LAP_Example : IExample
  {
    #region public members

    public string Name { get; set; } = string.Empty;

    public int[] Input { get; set; } = Array.Empty<int>();

    public IList<IRunnable> GetRunnables()
    {
      return new List<IRunnable>
        {
          new LAP_DVN_Runnable(),
          new LAP_NDTM_Runnable(),
          new LAP_MEAP_Runnable()
        };
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
