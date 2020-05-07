////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ninject;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public class DeterminePathRunnerFactCPLTM : DeterminePathRunner
  {
    #region Ctors

    public DeterminePathRunnerFactCPLTM(DeterminePathRunnerCtorArgs determinePathRunnerCtorArgs)
      : base(determinePathRunnerCtorArgs)
    {
      this.configuration = Core.AppContext.GetConfiguration();
    }

    #endregion

    #region public members

    public override void Run()
    {
      meapContext = new MEAPContext
      {
        mu = currentMu
      };

      meapContext.TapeSegContext = new TapeSegContext();
      meapContext.MEAPSharedContext = MEAPSharedContext;

      log.DebugFormat("CurrentMu = {0}", currentMu);

      IMeapCurrentStep currentStep = configuration.Get<IMeapCurrentStep>(
        new Ninject.Parameters.ConstructorArgument(nameof(meapContext), meapContext));

      currentStep.Run(meapContext.MEAPSharedContext.MNP.F);
      bool gammaF = meapContext.PathExists;

      if (gammaF)
      {
        if (!meapContext.PathFound)
        {
          currentStep.RetrievePath();
        }

        Result = meapContext.PathExists;
        Output = meapContext.Output;

        Done = true;
      }

      if (Done)
      {
        lock (meapContext.MEAPSharedContext)
        {
          meapContext.MEAPSharedContext.DeterminePathRunnerDoneMu = currentMu;
          meapContext.MEAPSharedContext.CancellationTokenSource.Cancel();
        }
      }
    }

    #endregion

    #region private members

    private readonly IReadOnlyKernel configuration;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
