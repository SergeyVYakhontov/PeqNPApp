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

namespace IntegerFactExamplesAppCPLTM
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
      Bind<IMeapCurrentStep>().ToConstructor<MEAPCurrentStepFactCPLTM>(
        arg => new MEAPCurrentStepFactCPLTM(arg.Inject<MEAPContext>()));
      Bind<DeterminePathRunner>().ToConstructor<DeterminePathRunnerFactCPLTM>(
        arg => new DeterminePathRunnerFactCPLTM(arg.Inject<DeterminePathRunnerCtorArgs>()));
      Bind<IMExistsAcceptingPath>().ToConstructor<MExistsAcceptingPathFactCPLTM>(
        arg => new MExistsAcceptingPathFactCPLTM(arg.Inject<MExistsAcceptingPathCtorArgs>()));

      Bind<ExampleSetProvider>().To<ExampleSetProvider>().InSingletonScope();

      Bind<IVerificator>().To<IntegerFactVerificator>();
      Bind<IApplication>().To<IntegerFactApplication>();
    }
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
