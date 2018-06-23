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
  public class Lang01_Example : IExample
  {
    #region public members

    public string Name { get; set; }

    public int[] Input { get; set; }

    public IList<IRunnable> GetRunnables()
    {
      return new List<IRunnable>()
        {
          new Lang01_DVN_Runnable(),
          new Lang01_DTM_Runnable(),
          new Lang01_MEAP_Runnable()
        };
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
