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
  public class Lang01_DVN_Runnable : VN_Runnable
  {
    #region public members

    public override bool Decide(int[] input)
    {
      dvn = new Lang01_DVN();

      return dvn.Decide(input);
    }

    public override int[] Compute(int[] input)
    {
      return dvn.Compute(input);
    }

    public override bool ComputationFinished => true;
    public override bool CompareOutputs => true;

    #endregion

    #region private members

    private Lang01_DVN dvn = default!;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
