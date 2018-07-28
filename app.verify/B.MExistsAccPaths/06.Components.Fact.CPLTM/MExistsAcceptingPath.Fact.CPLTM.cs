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
  public class MExistsAcceptingPathFactTRS : IMExistsAcceptingPath
  {
    #region Ctors

    public MExistsAcceptingPathFactTRS(MExistsAcceptingPathCtorArgs mExistsAcceptingPathCtorArgs)
    {
      this.tMachine = mExistsAcceptingPathCtorArgs.tMachine;
    }

    #endregion

    #region public members

    public void Determine(int[] input, out bool result, out int[] output)
    {
      MEAPSharedContext MEAPSharedContext = new MEAPSharedContext
      {
        MNP = tMachine,
        Input = input
      };

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

      TASGBuilderFactTRS tasgBuilder = new TASGBuilderFactTRS();
      MEAPSharedContext.TASGBuilder = tasgBuilder;
      tasgBuilder.Init(MEAPSharedContext);

      while (currentMu <= debugOptions.FactTRS_muUpperBound)
      {
        List<DeterminePathRunner> determinePathRunners = new List<DeterminePathRunner>();

        for (long i = 0; i < determinePathRunnersCount; i++)
        {
          DeterminePathRunnerCtorArgs determinePathRunnerCtorArgs =
            new DeterminePathRunnerCtorArgs
              {
                tMachine = tMachine,
                input = input,
                currentMu = currentMu,
                MEAPSharedContext = MEAPSharedContext
              };

          currentMu++;

          DeterminePathRunner determinePathRunner =
            configuration.Get<DeterminePathRunner>(
              new Ninject.Parameters.ConstructorArgument(
                nameof(determinePathRunnerCtorArgs),
                determinePathRunnerCtorArgs));

          determinePathRunners.Add(determinePathRunner);
        }

        TPLCollectionRunner<DeterminePathRunner> determinePathRunnerSet =
          new TPLCollectionRunner<DeterminePathRunner>(
            determinePathRunners,
            determinePathRunnersCount,
            WaitMethod.WaitAll,
            itemsArray => Array.Find(itemsArray, s => s.Done));

        determinePathRunnerSet.Run();

        if (determinePathRunnerSet.Done)
        {
          result = determinePathRunnerSet.CurrentItem.Result;
          output = determinePathRunnerSet.CurrentItem.Output;

          break;
        }
      }

      result = true;
      output = Array.Empty<int>();
    }

    #endregion

    #region private members

    private static readonly IKernel configuration = Core.AppContext.Configuration;

    private readonly OneTapeTuringMachine tMachine;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////