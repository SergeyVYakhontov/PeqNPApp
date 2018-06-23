////////////////////////////////////////////////////////////////////////////////////////////////////

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
  public class IF_MEAP : IAlgorithm
  {
    #region public members

    public IF_MEAP(OneTapeTuringMachine tm)
    {
      this.tm = tm;
    }

    public bool Decide(int[] input)
    {
      MExistsAcceptingPathCtorArgs mExistsAcceptingPathCtorArgs =
        new MExistsAcceptingPathCtorArgs
        {
          tMachine = tm
        };

      mExistAcceptingPaths = configuration.Get<IMExistsAcceptingPath>(
        new Ninject.Parameters.ConstructorArgument(
          nameof(mExistsAcceptingPathCtorArgs),
          mExistsAcceptingPathCtorArgs));
      mExistAcceptingPaths.Determine(input, out pathExists, out output);

      return pathExists;
    }

    public int[] Compute(int[] input)
    {
      return output;
    }

    #endregion

    #region private members

    private static readonly IKernel configuration = Core.AppContext.Configuration;

    private readonly OneTapeTuringMachine tm;
    private IMExistsAcceptingPath mExistAcceptingPaths;

    private bool pathExists;
    private int[] output;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
