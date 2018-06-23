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

namespace OrdinaryExamplesAppSingleThread
{
  public static class Program
  {
    #region public members

    public static void Main(string[] args)
    {
      log4net.Repository.ILoggerRepository logRepository = log4net.LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
      log4net.Config.XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

      Setup();

      IApplication application = configuration.Get<IApplication>();
      application.Run(args);
    }

    #endregion

    #region private mermbers

    private static readonly IKernel configuration = Core.AppContext.Configuration;
    private static readonly log4net.ILog log =
      log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private static void Setup()
    {
      log.Info("Setup");

      configuration.Load<AppNinjectModule>();

      ExampleSetProvider exampleSetProvider = configuration.Get<ExampleSetProvider>();

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
