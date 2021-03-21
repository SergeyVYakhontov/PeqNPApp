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
  public class LCS_DVN_Runnable : VN_Runnable
  {
    #region public members

    public override bool Decide(int[] input)
    {
      dvn = new LCS_DVN();

      return dvn.Decide(input);
    }

    public override int[] Compute(int[] input)
    {
      return dvn.Compute(input);
    }

    public override bool CompareOutputs => false;
    public override bool ComputationFinished => true;

    #endregion

    #region private members

    private LCS_DVN dvn = default!;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
