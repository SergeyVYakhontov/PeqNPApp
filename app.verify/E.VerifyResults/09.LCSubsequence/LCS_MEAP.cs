﻿////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;
using Ninject;
using ExistsAcceptingPath;
using MTExtDefinitions;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace VerifyResults
{
  public class LCS_MEAP : IAlgorithm
  {
    #region public members

    public LCS_MEAP(OneTapeTuringMachine tm)
    {
      this.configuration = Core.AppContext.GetConfiguration();

      this.tm = tm;
    }

    public bool Decide(int[] input)
    {
      MExistsAcceptingPathCtorArgs mExistsAcceptingPathCtorArgs = new() { tMachine = tm };

      mExistAcceptingPaths = configuration.Get<IMExistsAcceptingPath>(
        new Ninject.Parameters.ConstructorArgument(
          nameof(mExistsAcceptingPathCtorArgs),
          mExistsAcceptingPathCtorArgs));
      (pathExists, output) = mExistAcceptingPaths.Determine(input);

      return pathExists;
    }

    public int[] Compute(int[] input)
    {
      return output;
    }

    #endregion

    #region private members

    private readonly IReadOnlyKernel configuration;

    private readonly OneTapeTuringMachine tm = default!;
    private IMExistsAcceptingPath mExistAcceptingPaths = default!;

    private bool pathExists;
    private int[] output = Array.Empty<int>();

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
