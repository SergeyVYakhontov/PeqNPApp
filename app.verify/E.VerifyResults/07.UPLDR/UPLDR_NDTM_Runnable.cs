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
  public class UPLDR_NDTM_Runnable : TM_Runnable
  {
    #region public members

    public override bool Decide(int[] input)
    {
      tm = new UPLDR_NDTM();
      tm.Setup();

      tm.Run(input);

      return tm.Accepted;
    }

    public override int[] Compute(int[] input)
    {
      return tm.GetAcceptingInstanceOutput(input);
    }

    public override bool CompareOutputs => true;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////

