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
  public class MExistsAcceptingPathSlotsMThreads : IMExistsAcceptingPath
  {
    #region Ctors

    public MExistsAcceptingPathSlotsMThreads(MExistsAcceptingPathCtorArgs mExistsAcceptingPathCtorArgs)
    {
      this.tMachine = mExistsAcceptingPathCtorArgs.tMachine;
    }

    #endregion

    #region public members

    public void Determine(int[] input, out bool result, out int[] output)
    {
      MEAPSharedContext MEAPSharedContext = new MEAPSharedContext();

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
      long currentMu = (long)debugOptions.muStart;

      TASGBuilderSlotsMThreads tasgBuilder = new TASGBuilderSlotsMThreads();

      MEAPSharedContext.TASGBuilder = tasgBuilder;
      tasgBuilder.MEAPSharedContext = MEAPSharedContext;

      tasgBuilder.Init();

      while (true)
      {
        List<DeterminePathRunner> determinePathRunners = new List<DeterminePathRunner>();
        long baseMu = currentMu;

        for (long i = 0; i < determinePathRunnersCount; i++)
        {
          DeterminePathRunnerCtorArgs determinePathRunnerCtorArgs =
            new DeterminePathRunnerCtorArgs
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

        TPLCollectionRunner<DeterminePathRunner> determinePathRunnerSet =
          new TPLCollectionRunner<DeterminePathRunner>(
            determinePathRunners,
            determinePathRunnersCount,
            WaitMethod.WaitAll,
            itemsArray => itemsArray.FirstOrDefault(s => s.Done));
        determinePathRunnerSet.Run();

        if (determinePathRunnerSet.Done)
        {
          result = determinePathRunnerSet.CurrentItem.Result;
          output = determinePathRunnerSet.CurrentItem.Output;

          return;
        }
      }
    }

    #endregion

    #region private members

    private static readonly IKernel configuration = Core.AppContext.Configuration;

    private OneTapeTuringMachine tMachine;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
