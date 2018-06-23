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
  public class LAP_CheckAlgorithm : ICheckAlgorithm
  {
    #region public members

    public bool CheckDecide(int[] input, bool result)
    {
      return true;
    }

    public bool CheckCompute(int[] input, int[] output)
    {
      return true;
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
