﻿////////////////////////////////////////////////////////////////////////////////////////////////////

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

namespace IntegerFactExamplesAppComms
{
  public class AppNinjectModule : Ninject.Modules.NinjectModule
  {
    public override void Load()
    {
      Bind<AppStatistics>().To<AppStatistics>().InSingletonScope();

      Bind<ICommonOptions>().To<CommonOptions>().InSingletonScope();
      Bind<ITPLOptions>().To<TPLOptions>().InSingletonScope();
      Bind<IDebugOptions>().To<DebugOptions>().InSingletonScope();

      Bind<IBitVectorProvider>().To<BitVectorAllocProvider>()
        .InSingletonScope();
      Bind<IBitVectorOperations>().To<BitVectorAlloc>()
        .InSingletonScope();

      Bind<ILinEqsAlgorithmProvider>().To<LinEqsAlgorithmProvider>()
        .InSingletonScope();
      Bind<IMeapCurrentStep>().ToConstructor<MEAPCurrentStepFactComm>(
        arg => new MEAPCurrentStepFactComm(arg.Inject<MEAPContext>()));
      Bind<DeterminePathRunner>().ToConstructor<DeterminePathRunnerFactComms>(
        arg => new DeterminePathRunnerFactComms(arg.Inject<DeterminePathRunnerCtorArgs>()));
      Bind<IMExistsAcceptingPath>().ToConstructor<MExistsAcceptingPathFactComms>(
        arg => new MExistsAcceptingPathFactComms(arg.Inject<MExistsAcceptingPathCtorArgs>()));

      Bind<ExampleSetProvider>().To<ExampleSetProvider>().InSingletonScope();

      Bind<IVerificator>().To<IntegerFactVerificator>();
      Bind<IApplication>().To<IntegerFactApplication>();
    }
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////