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

namespace ProgramTests
{
  [TestCaseOrderer("ProgramTests.AlphabeticalTestOrderer", "G.ProgramTests")]
  public class MEAPTests : IDisposable
  {
    #region Ctors

    static MEAPTests()
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
      configuration.Load<OrdinaryExamplesAppSlotsMThreads.AppNinjectModule>();

      ExampleSetProvider exampleSetProvider = configuration.Get<ExampleSetProvider>();
      exampleSetProvider.ExampleSets.Add(exampleSetProvider.Lang01_ExampleSet);

      IApplication application = configuration.Get<IApplication>();
      application.Run(new string[] { "test" });

      AppStatistics appStatistics = configuration.Get<AppStatistics>();

      Assert.False(appStatistics.ThereWereErrors);
    }

    [Fact]
    public void T02_SlotsMThreads_Lang02_Test()
    {
      configuration.Load<OrdinaryExamplesAppSlotsMThreads.AppNinjectModule>();

      ExampleSetProvider exampleSetProvider = configuration.Get<ExampleSetProvider>();
      exampleSetProvider.ExampleSets.Add(exampleSetProvider.Lang02_ExampleSet);

      IApplication application = configuration.Get<IApplication>();
      application.Run(new string[] { "test" });

      AppStatistics appStatistics = configuration.Get<AppStatistics>();

      Assert.False(appStatistics.ThereWereErrors);
    }

    [Fact]
    public void T03_SlotsMThreads_SAP_Test()
    {
      configuration.Load<OrdinaryExamplesAppSlotsMThreads.AppNinjectModule>();

      ExampleSetProvider exampleSetProvider = configuration.Get<ExampleSetProvider>();
      exampleSetProvider.ExampleSets.Add(exampleSetProvider.SAP_ExampleSet);

      IApplication application = configuration.Get<IApplication>();
      application.Run(new string[] { "test" });

      AppStatistics appStatistics = configuration.Get<AppStatistics>();

      Assert.False(appStatistics.ThereWereErrors);
    }

    [Fact]
    public void T04_SlotsMThreads_LAP_Test()
    {
      configuration.Load<OrdinaryExamplesAppSlotsMThreads.AppNinjectModule>();

      ExampleSetProvider exampleSetProvider = configuration.Get<ExampleSetProvider>();
      exampleSetProvider.ExampleSets.Add(exampleSetProvider.LAP_ExampleSet);

      IApplication application = configuration.Get<IApplication>();
      application.Run(new string[] { "test" });

      AppStatistics appStatistics = configuration.Get<AppStatistics>();

      Assert.False(appStatistics.ThereWereErrors);
    }

    [Fact]
    public void T05_SlotsMThreads_UPAPMNE_Test()
    {
      configuration.Load<OrdinaryExamplesAppSlotsMThreads.AppNinjectModule>();

      ExampleSetProvider exampleSetProvider = configuration.Get<ExampleSetProvider>();
      exampleSetProvider.ExampleSets.Add(exampleSetProvider.UPAPMNE_ExampleSet);

      IApplication application = configuration.Get<IApplication>();
      application.Run(new string[] { "test" });

      AppStatistics appStatistics = configuration.Get<AppStatistics>();

      Assert.False(appStatistics.ThereWereErrors);
    }

    [Fact]
    public void T06_SlotsMThreads_UPAPAE_Test()
    {
      configuration.Load<OrdinaryExamplesAppSlotsMThreads.AppNinjectModule>();

      ExampleSetProvider exampleSetProvider = configuration.Get<ExampleSetProvider>();
      exampleSetProvider.ExampleSets.Add(exampleSetProvider.UPAPAE_ExampleSet);

      IApplication application = configuration.Get<IApplication>();
      application.Run(new string[] { "test" });

      AppStatistics appStatistics = configuration.Get<AppStatistics>();

      Assert.False(appStatistics.ThereWereErrors);
    }

    [Fact]
    public void T07_SlotsMThreads_UPLDR_Test()
    {
      configuration.Load<OrdinaryExamplesAppSlotsMThreads.AppNinjectModule>();

      ExampleSetProvider exampleSetProvider = configuration.Get<ExampleSetProvider>();
      exampleSetProvider.ExampleSets.Add(exampleSetProvider.UPLDR_ExampleSet);

      IApplication application = configuration.Get<IApplication>();
      application.Run(new string[] { "test" });

      AppStatistics appStatistics = configuration.Get<AppStatistics>();

      Assert.False(appStatistics.ThereWereErrors);
    }

    [Fact]
    public void T08_IF_Comms_Test()
    {
      configuration.Load<IntegerFactExamplesAppComms.AppNinjectModule>();

      ExampleSetProvider exampleSetProvider = configuration.Get<ExampleSetProvider>();
      exampleSetProvider.ExampleSets.Add(exampleSetProvider.IF_ExampleSetA);

      IApplication application = configuration.Get<IApplication>();
      application.Run(new string[] { "test" });

      AppStatistics appStatistics = configuration.Get<AppStatistics>();

      Assert.False(appStatistics.ThereWereErrors);
    }

    [Fact]
    public void T09_IF_TRS_Test()
    {
      configuration.Load<IntegerFactExamplesAppTRS.AppNinjectModule>();

      ExampleSetProvider exampleSetProvider = configuration.Get<ExampleSetProvider>();
      exampleSetProvider.ExampleSets.Add(exampleSetProvider.IF_ExampleSetA);

      IApplication application = configuration.Get<IApplication>();
      application.Run(new string[] { "test" });

      AppStatistics appStatistics = configuration.Get<AppStatistics>();

      Assert.False(appStatistics.ThereWereErrors);
    }

    #endregion

    #region private members

    private static readonly IKernel configuration = Core.AppContext.Configuration;
    private static readonly log4net.ILog log =
      log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private static void ResetNinjectKernel()
    {
      List<Ninject.Modules.INinjectModule> modules = configuration.GetModules().ToList();
      modules.ForEach(m => configuration.Unload(m.Name));
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
