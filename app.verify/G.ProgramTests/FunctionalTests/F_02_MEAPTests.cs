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
using Xunit;
using Core;
using ExistsAcceptingPath;
using VerifyResults;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace FunctionalTests
{
  [TestCaseOrderer("ProgramTests.AlphabeticalTestOrderer", "G.ProgramTests")]
  public sealed class F_02_MEAPTests : IDisposable
  {
    #region Ctors

    static F_02_MEAPTests()
    {
      log4net.Repository.ILoggerRepository logRepository = log4net.LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
      log4net.Config.XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
    }

    #endregion

    #region public members

    public void Dispose()
    {
      ResetNinjectKernel();
    }

    [Fact]
    public void T01_SlotsMThreads_Lang01_Test()
    {
      Core.AppContext.LoadConfigurationModule<OrdinaryExamplesAppSlotsMThreads.AppNinjectModule>();

      IReadOnlyKernel configuration = Core.AppContext.GetConfiguration();
      IExampleSetProvider exampleSetProvider = configuration.Get<IExampleSetProvider>();

      exampleSetProvider.ExampleSets.Add(exampleSetProvider.Lang01_ExampleSet);

      IApplication application = configuration.Get<IApplication>();
      application.Run(new string[] { "test" });

      AppStatistics appStatistics = configuration.Get<AppStatistics>();

      Assert.False(appStatistics.ThereWereErrors);
    }

    [Fact]
    public void T02_SlotsMThreads_Lang02_Test()
    {
      Core.AppContext.LoadConfigurationModule<OrdinaryExamplesAppSlotsMThreads.AppNinjectModule>();

      IReadOnlyKernel configuration = Core.AppContext.GetConfiguration();
      IExampleSetProvider exampleSetProvider = configuration.Get<IExampleSetProvider>();

      exampleSetProvider.ExampleSets.Add(exampleSetProvider.Lang02_ExampleSet);

      IApplication application = configuration.Get<IApplication>();
      application.Run(new string[] { "test" });

      AppStatistics appStatistics = configuration.Get<AppStatistics>();

      Assert.False(appStatistics.ThereWereErrors);
    }

    [Fact]
    public void T03_SlotsMThreads_SAP_Test()
    {
      Core.AppContext.LoadConfigurationModule<OrdinaryExamplesAppSlotsMThreads.AppNinjectModule>();

      IReadOnlyKernel configuration = Core.AppContext.GetConfiguration();
      IExampleSetProvider exampleSetProvider = configuration.Get<IExampleSetProvider>();

      exampleSetProvider.ExampleSets.Add(exampleSetProvider.SAP_ExampleSet);

      IApplication application = configuration.Get<IApplication>();
      application.Run(new string[] { "test" });

      AppStatistics appStatistics = configuration.Get<AppStatistics>();

      Assert.False(appStatistics.ThereWereErrors);
    }

    [Fact]
    public void T04_SlotsMThreads_LAP_Test()
    {
      Core.AppContext.LoadConfigurationModule<OrdinaryExamplesAppSlotsMThreads.AppNinjectModule>();

      IReadOnlyKernel configuration = Core.AppContext.GetConfiguration();
      IExampleSetProvider exampleSetProvider = configuration.Get<IExampleSetProvider>();

      exampleSetProvider.ExampleSets.Add(exampleSetProvider.LAP_ExampleSet);

      IApplication application = configuration.Get<IApplication>();
      application.Run(new string[] { "test" });

      AppStatistics appStatistics = configuration.Get<AppStatistics>();

      Assert.False(appStatistics.ThereWereErrors);
    }

    [Fact]
    public void T05_SlotsMThreads_UPAPMNE_Test()
    {
      Core.AppContext.LoadConfigurationModule<OrdinaryExamplesAppSlotsMThreads.AppNinjectModule>();

      IReadOnlyKernel configuration = Core.AppContext.GetConfiguration();
      IExampleSetProvider exampleSetProvider = configuration.Get<IExampleSetProvider>();

      exampleSetProvider.ExampleSets.Add(exampleSetProvider.UPAPMNE_ExampleSet);

      IApplication application = configuration.Get<IApplication>();
      application.Run(new string[] { "test" });

      AppStatistics appStatistics = configuration.Get<AppStatistics>();

      Assert.False(appStatistics.ThereWereErrors);
    }

    [Fact]
    public void T06_SlotsMThreads_UPAPAE_Test()
    {
      Core.AppContext.LoadConfigurationModule<OrdinaryExamplesAppSlotsMThreads.AppNinjectModule>();

      IReadOnlyKernel configuration = Core.AppContext.GetConfiguration();
      IExampleSetProvider exampleSetProvider = configuration.Get<IExampleSetProvider>();

      exampleSetProvider.ExampleSets.Add(exampleSetProvider.UPAPAE_ExampleSet);

      IApplication application = configuration.Get<IApplication>();
      application.Run(new string[] { "test" });

      AppStatistics appStatistics = configuration.Get<AppStatistics>();

      Assert.False(appStatistics.ThereWereErrors);
    }

    [Fact]
    public void T07_SlotsMThreads_UPLDR_Test()
    {
      Core.AppContext.LoadConfigurationModule<OrdinaryExamplesAppSlotsMThreads.AppNinjectModule>();

      IReadOnlyKernel configuration = Core.AppContext.GetConfiguration();
      IExampleSetProvider exampleSetProvider = configuration.Get<IExampleSetProvider>();

      exampleSetProvider.ExampleSets.Add(exampleSetProvider.UPLDR_ExampleSet);

      IApplication application = configuration.Get<IApplication>();
      application.Run(new string[] { "test" });

      AppStatistics appStatistics = configuration.Get<AppStatistics>();

      Assert.False(appStatistics.ThereWereErrors);
    }

    [Fact]
    public void T08_IF_Comms_Test()
    {
      Core.AppContext.LoadConfigurationModule<IntegerFactExamplesAppComms.AppNinjectModule>();

      IReadOnlyKernel configuration = Core.AppContext.GetConfiguration();
      IExampleSetProvider exampleSetProvider = configuration.Get<IExampleSetProvider>();

      exampleSetProvider.ExampleSets.Add(exampleSetProvider.IF_ExampleSetA);

      IApplication application = configuration.Get<IApplication>();
      application.Run(new string[] { "test" });

      AppStatistics appStatistics = configuration.Get<AppStatistics>();

      Assert.False(appStatistics.ThereWereErrors);
    }

    [Fact]
    public void T09_IF_CPLTM_Test()
    {
      Core.AppContext.LoadConfigurationModule<IntegerFactExamplesAppCPLTM.AppNinjectModule>();

      IReadOnlyKernel configuration = Core.AppContext.GetConfiguration();
      IExampleSetProvider exampleSetProvider = configuration.Get<IExampleSetProvider>();

      exampleSetProvider.ExampleSets.Add(exampleSetProvider.IF_ExampleSetA);

      IApplication application = configuration.Get<IApplication>();
      application.Run(new string[] { "test" });

      AppStatistics appStatistics = configuration.Get<AppStatistics>();

      Assert.False(appStatistics.ThereWereErrors);
    }

    #endregion

    #region private members

    private static void ResetNinjectKernel()
    {
      Core.AppContext.UnloadConfigurationModule();
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
