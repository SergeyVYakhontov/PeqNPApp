﻿////////////////////////////////////////////////////////////////////////////////////////////////////

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
  public class MExistsAcceptingPathFactComms : IMExistsAcceptingPath
  {
    #region Ctors

    public MExistsAcceptingPathFactComms(MExistsAcceptingPathCtorArgs mExistsAcceptingPathCtorArgs)
    {
      this.configuration = Core.AppContext.GetConfiguration();

      this.tMachine = mExistsAcceptingPathCtorArgs.tMachine;
    }

    #endregion

    #region public members

    public (bool, int[]) Determine(int[] input)
    {
      MEAPSharedContext MEAPSharedContext = new();

      MEAPSharedContext.MNP = tMachine;
      MEAPSharedContext.Input = input;

      MEAPSharedContext.InitInstance = new TMInstance(
        MEAPSharedContext.MNP,
        MEAPSharedContext.Input);

      MEAPSharedContext.MNP.PrepareTapeFwd(
        MEAPSharedContext.Input,
        MEAPSharedContext.InitInstance);

      MEAPSharedContext.CancellationTokenSource = new CancellationTokenSource();
      MEAPSharedContext.CancellationToken = MEAPSharedContext.CancellationTokenSource.Token;

      ITPLOptions tplOptions = configuration.Get<ITPLOptions>();
      uint determinePathRunnersCount = tplOptions.DeterminePathRunnersCount;

      IDebugOptions debugOptions = configuration.Get<IDebugOptions>();
      ulong currentMu = debugOptions.muStart;

      ITASGBuilder tasgBuilder = configuration.Get<ITASGBuilder>();

      MEAPSharedContext.TASGBuilder = tasgBuilder;
      tasgBuilder.MEAPSharedContext = MEAPSharedContext;

      tasgBuilder.Init();

      while (true)
      {
        List<DeterminePathRunner> determinePathRunners = new();
        ulong baseMu = currentMu;

        for (long i = 0; i < determinePathRunnersCount; i++)
        {
          DeterminePathRunnerCtorArgs determinePathRunnerCtorArgs = new()
            {
                tMachine = tMachine,
                input = input,
                currentMu = currentMu++,
                MEAPSharedContext = MEAPSharedContext
            };

          DeterminePathRunner determinePathRunner = configuration.Get<DeterminePathRunner>(
            new Ninject.Parameters.ConstructorArgument(
              nameof(determinePathRunnerCtorArgs),
              determinePathRunnerCtorArgs));

          determinePathRunners.Add(determinePathRunner);
        }

        TPLCollectionRunner<DeterminePathRunner> determinePathRunnerSet = new
          (
            determinePathRunners,
            determinePathRunnersCount,
            WaitMethod.WaitAll,
            itemsArray => Array.Find(itemsArray, s => s.Done)!
          );
        determinePathRunnerSet.Run();

        if (determinePathRunnerSet.Done)
        {
          bool result = determinePathRunnerSet.CurrentItem.Result;
          int[] output = determinePathRunnerSet.CurrentItem.Output;

          return (result, output);
        }
      }
    }

    #endregion

    #region private members

    private readonly IReadOnlyKernel configuration;

    private readonly OneTapeTuringMachine tMachine;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
