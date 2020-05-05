////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Ninject;
using Core;
using ExistsAcceptingPath;
using VerifyResults;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace OrdinaryExamplesAppSlotsMThreads
{
  public class AppNinjectModule : Ninject.Modules.NinjectModule
  {
    public override void Load()
    {
      Bind<AppStatistics>().To<AppStatistics>().InSingletonScope();
      Bind<MathKernelConnector>().To<MathKernelConnector>().InSingletonScope();

      Bind<ICommonOptions>().To<CommonOptions>().InSingletonScope();
      Bind<ITPLOptions>().To<TPLOptions>().InSingletonScope();
      Bind<IDebugOptions>().To<DebugOptions>().InSingletonScope();

      Bind<IBitVectorProvider>().To<BitVectorAllocProvider>()
        .InSingletonScope();
      Bind<IBitVectorOperations>().To<BitVectorAlloc>()
        .InSingletonScope();

      Bind<ILinEqsAlgorithmProvider>().To<LinEqsAlgorithmProvider>()
        .InSingletonScope();
      Bind<ICheckDataStructures>().To<CheckDataStructures>()
        .InSingletonScope();
      Bind<ITASGBuilder>().ToConstructor<TASGBuilderSlotsMThreads>(
        _ => new TASGBuilderSlotsMThreads());
      Bind<IMeapCurrentStep>().ToConstructor<MEAPCurrentStepSlotsMultiThreads>(
        arg => new MEAPCurrentStepSlotsMultiThreads(arg.Inject<MEAPContext>()));
      Bind<DeterminePathRunner>().ToConstructor<DeterminePathRunnerSlotsMThreads>(
        arg => new DeterminePathRunnerSlotsMThreads(arg.Inject<DeterminePathRunnerCtorArgs>()));
      Bind<IMExistsAcceptingPath>().ToConstructor<MExistsAcceptingPathSlotsMThreads>(
        arg => new MExistsAcceptingPathSlotsMThreads(arg.Inject<MExistsAcceptingPathCtorArgs>()));

      Bind<IExampleSetProvider>().To<VerifyResults.v1.ExampleSetProvider>().InSingletonScope();

      Bind<IVerificator>().To<OrdinaryVerificator>();
      Bind<IApplication>().To<OrdinaryApplication>();
    }
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
