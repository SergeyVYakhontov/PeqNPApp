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
  public class IF_DVN_Runnable: VN_Runnable
  {
    #region private members

    private IF_DVN dvn;

    #endregion

    #region public members

    public override bool Decide(int[] input)
    {
      dvn = new IF_DVN();

      return dvn.Decide(input);
    }

    public override int[] Compute(int[] input)
    {
      return dvn.Compute(input);
    }

    public override bool ComputationFinished => true;
    public override bool CompareOutputs => false;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
