////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;
using ExistsAcceptingPath;
using MTExtDefinitions;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace VerifyResults.v2
{
  public class IF_Example : IExample
  {
    #region public members

    public string Name { get; set; }

    public int[] Input { get; set; }

    public IList<IRunnable> GetRunnables()
    {
      return new List<IRunnable>
        {
          new IF_DVN_Runnable(),
          //new IF_NDTM_Runnable(AppTracer),
          new IF_MEAP_Runnable()
        };
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
