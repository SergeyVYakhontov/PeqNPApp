////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;
using ExistsAcceptingPath;
using MTDefinitions;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace VerifyResults
{
  public class UPAPAE_Example : IExample
  {
    #region public members

    public string Name { get; set; } = string.Empty;

    public int[] Input { get; set; } = Array.Empty<int>();

    public IList<IRunnable> GetRunnables()
    {
      return new List<IRunnable>
        {
          new UPAPAE_DVN_Runnable(),
          new UPAPAE_NDTM_Runnable(),
          new UPAPAE_MEAP_Runnable()
        };
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
