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
  public class MExistsAcceptingPathFactCPLTM : IMExistsAcceptingPath
  {
    #region Ctors

    public MExistsAcceptingPathFactCPLTM(MExistsAcceptingPathCtorArgs mExistsAcceptingPathCtorArgs)
    {
      this.configuration = Core.AppContext.GetConfiguration();

      this.tMachine = mExistsAcceptingPathCtorArgs.tMachine;
    }

    #endregion

    #region public members

    public (bool, int[]) Determine(int[] input)
    {
      int inputLength = input.Length;

      ICPLTMInfo cpltmInfo = configuration.Get<ICPLTMInfo>(
        new Ninject.Parameters.ConstructorArgument(
          nameof(inputLength),
          input.Length));
      IDebugOptions debugOptions = configuration.Get<IDebugOptions>();

      cpltmInfo.ComputeSequences();

      uint maxMu = cpltmInfo.PathLength;
      ulong currentMu = debugOptions.muStart;

      log.InfoFormat($"path length: {cpltmInfo.PathLength}");

      MEAPSharedContext MEAPSharedContext = new()
      {
        MNP = tMachine,
        Input = input,
        CPLTMInfo = cpltmInfo
      };

      MEAPSharedContext.InitInstance = new TMInstance(
        MEAPSharedContext.MNP,
        MEAPSharedContext.Input);

      MEAPSharedContext.MNP.PrepareTapeFwd(
        MEAPSharedContext.Input,
        MEAPSharedContext.InitInstance);

      MEAPSharedContext.CancellationTokenSource = new CancellationTokenSource();
      MEAPSharedContext.CancellationToken = MEAPSharedContext.CancellationTokenSource.Token;

      MEAPSharedContext.NodeLevelInfo = new NodeLevelInfo();

      ITASGBuilder tasgBuilder = configuration.Get<ITASGBuilder>();

      MEAPSharedContext.TASGBuilder = tasgBuilder;
      tasgBuilder.MEAPSharedContext = MEAPSharedContext;

      tasgBuilder.Init();

      ITPLOptions tplOptions = configuration.Get<ITPLOptions>();
      uint determinePathRunnersCount = tplOptions.DeterminePathRunnersCount;

      bool result;
      int[] output;

      while (currentMu <= maxMu)
      {
        List<DeterminePathRunner> determinePathRunners = new();

        for (long i = 0; i < determinePathRunnersCount; i++)
        {
          DeterminePathRunnerCtorArgs determinePathRunnerCtorArgs = new()
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
          result = determinePathRunnerSet.CurrentItem.Result;
          output = determinePathRunnerSet.CurrentItem.Output;

          break;
        }
      }

      result = true;
      output = Array.Empty<int>();

      return (result, output);
    }

    #endregion

    #region private members

    private readonly IReadOnlyKernel configuration;

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType);

    private readonly OneTapeTuringMachine tMachine;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
