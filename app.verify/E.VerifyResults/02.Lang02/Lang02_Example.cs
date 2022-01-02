////////////////////////////////////////////////////////////////////////////////////////////////////

namespace VerifyResults
{
  public class Lang2_Example : IExample
  {
    #region public members

    public string Name { get; set; } = string.Empty;

    public int[] Input { get; set; } = Array.Empty<int>();

    public IList<IRunnable> GetRunnables()
    {
      return new List<IRunnable>
        {
          new Lang2_DVN_Runnable(),
          new Lang2_NDTM_Runnable(),
          new Lang2_MEAP_Runnable()
        };
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
