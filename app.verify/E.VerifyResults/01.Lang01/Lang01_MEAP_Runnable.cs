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
  public class Lang01_MEAP_Runnable : MEAP_Runnable
  {
    #region public members

    public override bool Decide(int[] input)
    {
      tm = new Lang01_DTM();
      tm.Setup();

      meap = new Lang01_MEAP(tm);

      return meap.Decide(input);
    }

    public override int[] Compute(int[] input)
    {
      return meap.Compute(input);
    }

    public override bool RunCheckAlgorithm => false;
    public override bool ComputationFinished => true;
    public override bool CompareOutputs => true;

    #endregion

    #region private members

    private Lang01_MEAP meap;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
