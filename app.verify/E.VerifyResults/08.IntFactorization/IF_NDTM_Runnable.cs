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
  public class IF_NDTM_Runnable : TM_Runnable
  {
    #region public members

    public override bool Decide(int[] input)
    {
      tm = new IF_NDTM_A(input.Length);
      tm.Setup();

      tm.Run(input);

      return tm.Accepted;
    }

    public override int[] Compute(int[] input)
    {
      return tm.GetAcceptingInstanceOutput(input);
    }

    public override bool CompareOutputs => false;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
