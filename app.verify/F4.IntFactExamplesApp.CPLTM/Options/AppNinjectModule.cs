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
using MTExtDefinitions;
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
      Bind<ICheckDataStructures>().To<CheckDataStructuresTheSameFrom>()
        .InSingletonScope();
      Bind<ITASGBuilder>().ToConstructor<TASGBuilderFactCPLTMTheSameFrom>(
        _ => new TASGBuilderFactCPLTMTheSameFrom());
      Bind<IMeapCurrentStep>().ToConstructor<MEAPCurrentStepFactCPLTM>(
        arg => new MEAPCurrentStepFactCPLTM(arg.Inject<MEAPContext>()));
      Bind<DeterminePathRunner>().ToConstructor<DeterminePathRunnerFactCPLTM>(
        arg => new DeterminePathRunnerFactCPLTM(arg.Inject<DeterminePathRunnerCtorArgs>()));
      Bind<IMExistsAcceptingPath>().ToConstructor<MExistsAcceptingPathFactCPLTM>(
        arg => new MExistsAcceptingPathFactCPLTM(arg.Inject<MExistsAcceptingPathCtorArgs>()));

      Bind<ICPLTMInfo>().ToConstructor<MTExtDefinitions.v2.CPLTMInfo>(
        arg => new MTExtDefinitions.v2.CPLTMInfo(arg.Inject<int>()));

      Bind<IExampleSetProvider>().To<VerifyResults.v2.ExampleSetProvider>().InSingletonScope();

      Bind<IVerificator>().To<IntegerFactVerificator>();
      Bind<IApplication>().To<IntegerFactApplication>();
    }
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
