////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.IO;
using System.Reflection;
using Ninject;
using EnsureThat;
using Core;
using ExistsAcceptingPath;
using VerifyResults;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace FunctionalTests
{
  public static class F_02_MEAPTests
  {
    #region public members

    public static void RunTests()
    {
      T01_SlotsMThreads_Lang01_Test();
      T02_SlotsMThreads_Lang02_Test();
      T03_SlotsMThreads_SAP_Test();
      T04_SlotsMThreads_LAP_Test();
      T05_SlotsMThreads_UPAPMNE_Test();
      T06_SlotsMThreads_UPAPAE_Test();
      T07_SlotsMThreads_UPLDR_Test();
      T08_IF_Comms_Test();
      T09_IF_CPLTM_Test();
    }

    #endregion

    #region private members

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType);

    private static void ResetNinjectKernel()
    {
      Core.AppContext.UnloadConfigurationModule();
    }

    private static void T01_SlotsMThreads_Lang01_Test()
    {
      Core.AppContext.LoadConfigurationModule<OrdinaryExamplesAppSlotsMThreads.AppNinjectModule>();

      IReadOnlyKernel configuration = Core.AppContext.GetConfiguration();
      IExampleSetProvider exampleSetProvider = configuration.Get<IExampleSetProvider>();

      exampleSetProvider.ExampleSets.Add(exampleSetProvider.Lang01_ExampleSet);

      IApplication application = configuration.Get<IApplication>();
      application.Run(new string[] { "test" });

      AppStatistics appStatistics = configuration.Get<AppStatistics>();

      Ensure.That(appStatistics.ThereWereErrors).IsFalse();
      ResetNinjectKernel();

      log.InfoFormat(
        "Passed: {0}.{1}",
        MethodBase.GetCurrentMethod()!.DeclaringType,
        MethodBase.GetCurrentMethod());
    }

    private static void T02_SlotsMThreads_Lang02_Test()
    {
      Core.AppContext.LoadConfigurationModule<OrdinaryExamplesAppSlotsMThreads.AppNinjectModule>();

      IReadOnlyKernel configuration = Core.AppContext.GetConfiguration();
      IExampleSetProvider exampleSetProvider = configuration.Get<IExampleSetProvider>();

      exampleSetProvider.ExampleSets.Add(exampleSetProvider.Lang02_ExampleSet);

      IApplication application = configuration.Get<IApplication>();
      application.Run(new string[] { "test" });

      AppStatistics appStatistics = configuration.Get<AppStatistics>();

      Ensure.That(appStatistics.ThereWereErrors).IsFalse();
      ResetNinjectKernel();

      log.InfoFormat(
        "Passed: {0}.{1}",
        MethodBase.GetCurrentMethod()!.DeclaringType,
        MethodBase.GetCurrentMethod());
    }

    private static void T03_SlotsMThreads_SAP_Test()
    {
      Core.AppContext.LoadConfigurationModule<OrdinaryExamplesAppSlotsMThreads.AppNinjectModule>();

      IReadOnlyKernel configuration = Core.AppContext.GetConfiguration();
      IExampleSetProvider exampleSetProvider = configuration.Get<IExampleSetProvider>();

      exampleSetProvider.ExampleSets.Add(exampleSetProvider.SAP_ExampleSet);

      IApplication application = configuration.Get<IApplication>();
      application.Run(new string[] { "test" });

      AppStatistics appStatistics = configuration.Get<AppStatistics>();

      Ensure.That(appStatistics.ThereWereErrors).IsFalse();
      ResetNinjectKernel();

      log.InfoFormat(
        "Passed: {0}.{1}",
        MethodBase.GetCurrentMethod()!.DeclaringType,
        MethodBase.GetCurrentMethod());
    }

    private static void T04_SlotsMThreads_LAP_Test()
    {
      Core.AppContext.LoadConfigurationModule<OrdinaryExamplesAppSlotsMThreads.AppNinjectModule>();

      IReadOnlyKernel configuration = Core.AppContext.GetConfiguration();
      IExampleSetProvider exampleSetProvider = configuration.Get<IExampleSetProvider>();

      exampleSetProvider.ExampleSets.Add(exampleSetProvider.LAP_ExampleSet);

      IApplication application = configuration.Get<IApplication>();
      application.Run(new string[] { "test" });

      AppStatistics appStatistics = configuration.Get<AppStatistics>();

      Ensure.That(appStatistics.ThereWereErrors).IsFalse();
      ResetNinjectKernel();

      log.InfoFormat(
        "Passed: {0}.{1}",
        MethodBase.GetCurrentMethod()!.DeclaringType,
        MethodBase.GetCurrentMethod());
    }

    private static void T05_SlotsMThreads_UPAPMNE_Test()
    {
      Core.AppContext.LoadConfigurationModule<OrdinaryExamplesAppSlotsMThreads.AppNinjectModule>();

      IReadOnlyKernel configuration = Core.AppContext.GetConfiguration();
      IExampleSetProvider exampleSetProvider = configuration.Get<IExampleSetProvider>();

      exampleSetProvider.ExampleSets.Add(exampleSetProvider.UPAPMNE_ExampleSet);

      IApplication application = configuration.Get<IApplication>();
      application.Run(new string[] { "test" });

      AppStatistics appStatistics = configuration.Get<AppStatistics>();

      Ensure.That(appStatistics.ThereWereErrors).IsFalse();
      ResetNinjectKernel();

      log.InfoFormat(
        "Passed: {0}.{1}",
        MethodBase.GetCurrentMethod()!.DeclaringType,
        MethodBase.GetCurrentMethod());
    }

    private static void T06_SlotsMThreads_UPAPAE_Test()
    {
      Core.AppContext.LoadConfigurationModule<OrdinaryExamplesAppSlotsMThreads.AppNinjectModule>();

      IReadOnlyKernel configuration = Core.AppContext.GetConfiguration();
      IExampleSetProvider exampleSetProvider = configuration.Get<IExampleSetProvider>();

      exampleSetProvider.ExampleSets.Add(exampleSetProvider.UPAPAE_ExampleSet);

      IApplication application = configuration.Get<IApplication>();
      application.Run(new string[] { "test" });

      AppStatistics appStatistics = configuration.Get<AppStatistics>();

      Ensure.That(appStatistics.ThereWereErrors).IsFalse();
      ResetNinjectKernel();

      log.InfoFormat(
        "Passed: {0}.{1}",
        MethodBase.GetCurrentMethod()!.DeclaringType,
        MethodBase.GetCurrentMethod());
    }

    private static void T07_SlotsMThreads_UPLDR_Test()
    {
      Core.AppContext.LoadConfigurationModule<OrdinaryExamplesAppSlotsMThreads.AppNinjectModule>();

      IReadOnlyKernel configuration = Core.AppContext.GetConfiguration();
      IExampleSetProvider exampleSetProvider = configuration.Get<IExampleSetProvider>();

      exampleSetProvider.ExampleSets.Add(exampleSetProvider.UPLDR_ExampleSet);

      IApplication application = configuration.Get<IApplication>();
      application.Run(new string[] { "test" });

      AppStatistics appStatistics = configuration.Get<AppStatistics>();

      Ensure.That(appStatistics.ThereWereErrors).IsFalse();
      ResetNinjectKernel();

      log.InfoFormat(
        "Passed: {0}.{1}",
        MethodBase.GetCurrentMethod()!.DeclaringType,
        MethodBase.GetCurrentMethod());
    }

    private static void T08_IF_Comms_Test()
    {
      Core.AppContext.LoadConfigurationModule<IntegerFactExamplesAppComms.AppNinjectModule>();

      IReadOnlyKernel configuration = Core.AppContext.GetConfiguration();
      IExampleSetProvider exampleSetProvider = configuration.Get<IExampleSetProvider>();

      exampleSetProvider.ExampleSets.Add(exampleSetProvider.IF_ExampleSetA);

      IApplication application = configuration.Get<IApplication>();
      application.Run(new string[] { "test" });

      AppStatistics appStatistics = configuration.Get<AppStatistics>();

      Ensure.That(appStatistics.ThereWereErrors).IsFalse();
      ResetNinjectKernel();

      log.InfoFormat(
        "Passed: {0}.{1}",
        MethodBase.GetCurrentMethod()!.DeclaringType,
        MethodBase.GetCurrentMethod());
    }

    private static void T09_IF_CPLTM_Test()
    {
      Core.AppContext.LoadConfigurationModule<IntegerFactExamplesAppCPLTM.AppNinjectModule>();

      IReadOnlyKernel configuration = Core.AppContext.GetConfiguration();
      IExampleSetProvider exampleSetProvider = configuration.Get<IExampleSetProvider>();

      exampleSetProvider.ExampleSets.Add(exampleSetProvider.IF_ExampleSetA);

      IApplication application = configuration.Get<IApplication>();
      application.Run(new string[] { "test" });

      AppStatistics appStatistics = configuration.Get<AppStatistics>();

      Ensure.That(appStatistics.ThereWereErrors).IsFalse();
      ResetNinjectKernel();

      log.InfoFormat(
        "Passed: {0}.{1}",
        MethodBase.GetCurrentMethod()!.DeclaringType,
        MethodBase.GetCurrentMethod());
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
