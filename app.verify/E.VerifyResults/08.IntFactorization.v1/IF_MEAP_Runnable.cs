////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Ninject;
using Core;
using ExistsAcceptingPath;
using MTExtDefinitions.v1;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace VerifyResults.v1
{
  public class IF_MEAP_Runnable : MEAP_Runnable
  {
    #region public members

    public override bool Decide(int[] input)
    {
      tm = new IF_NDTM(input.Length);
      tm.Setup();

      meap = new IF_MEAP(tm);

      return meap.Decide(input);
    }

    public override int[] Compute(int[] input)
    {
      return meap.Compute(input);
    }

    public override bool RunCheckAlgorithm
    {
      get
      {
        IDebugOptions debugOptions = configuration.Get<IDebugOptions>();

        return debugOptions.IntFact_RunCheckAlgorithm;
      }
    }

    public override bool ComputationFinished => true;
    public override bool CompareOutputs => false;

    #endregion

    #region private members

    private static readonly IKernel configuration = Core.AppContext.Configuration;

    private IF_MEAP meap;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
