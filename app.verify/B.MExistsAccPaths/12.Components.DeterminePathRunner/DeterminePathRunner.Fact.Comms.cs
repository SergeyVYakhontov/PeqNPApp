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
  public class DeterminePathRunnerFactComms : DeterminePathRunner
  {
    #region Ctors

    public DeterminePathRunnerFactComms(
      DeterminePathRunnerCtorArgs determinePathRunnerCtorArgs)
      : base(determinePathRunnerCtorArgs) {}

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

      if ((!Done) && (!meapContext.MEAPSharedContext.MNP.AcceptingPathAlwaysExists))
      {
        currentStep.Run(meapContext.MEAPSharedContext.MNP.QAny);
        bool gammaAny = meapContext.PathExists;

        if (!gammaAny)
        {
          Result = false;
          Output = Array.Empty<int>();

          Done = true;
        }
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
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
