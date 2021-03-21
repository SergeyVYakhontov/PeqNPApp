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

namespace VerifyResults
{
  public class LCS_MEAP_Runnable : MEAP_Runnable
  {
    #region public members

    public override bool Decide(int[] input)
    {
      tm = new LCS_NDTM();
      tm.Setup();

      meap = new LCS_MEAP(tm);

      return meap.Decide(input);
    }

    public override int[] Compute(int[] input)
    {
      return meap.Compute(input);
    }

    public override bool RunCheckAlgorithm => false;
    public override bool CompareOutputs => true;
    public override bool ComputationFinished => true;

    #endregion

    #region private members

    private LCS_MEAP meap = default!;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
