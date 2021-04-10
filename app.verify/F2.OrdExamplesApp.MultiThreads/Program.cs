////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Core;
using Ninject;
using ExistsAcceptingPath;
using VerifyResults;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace OrdinaryExamplesAppSlotsMThreads
{
  public static class Program
  {
    #region public members

    public static void Main(string[] args)
    {
      log4net.Repository.ILoggerRepository logRepository = log4net.LogManager.GetRepository(
        System.Reflection.Assembly.GetEntryAssembly());
      log4net.Config.XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

      Setup();

      IReadOnlyKernel configuration = Core.AppContext.GetConfiguration();
      IApplication application = configuration.Get<IApplication>();

      application.Run(args);

      if (!args.Contains("test"))
      {
        Console.ReadKey();
      }
    }

    #endregion

    #region private members

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType);

    private static void Setup()
    {
      log.Info("Setup");

      Core.AppContext.LoadConfigurationModule<AppNinjectModule>();

      IReadOnlyKernel configuration = Core.AppContext.GetConfiguration();
      IExampleSetProvider exampleSetProvider = configuration.Get<IExampleSetProvider>();

      exampleSetProvider.ExampleSets.Add(exampleSetProvider.Lang01_ExampleSet);
      exampleSetProvider.ExampleSets.Add(exampleSetProvider.Lang02_ExampleSet);

      exampleSetProvider.ExampleSets.Add(exampleSetProvider.SAP_ExampleSet);
      exampleSetProvider.ExampleSets.Add(exampleSetProvider.LAP_ExampleSet);

      exampleSetProvider.ExampleSets.Add(exampleSetProvider.UPAPMNE_ExampleSet);
      exampleSetProvider.ExampleSets.Add(exampleSetProvider.UPAPAE_ExampleSet);
      exampleSetProvider.ExampleSets.Add(exampleSetProvider.UPLDR_ExampleSet);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
