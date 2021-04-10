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
  public static class F_01_Application_Tests
  {
    #region public members

    public static void RunTests()
    {
      T01_OrdinaryExamplesAppSlotsMThreads_Test();
      T02_OrdinaryExamplesAppSlotsMThreads_Test();
      T03_IntegerFactExamplesAppComms_Test();
      T04_IntegerFactExamplesAppCPLTM_Test();
    }

    #endregion

    #region private members

    private static void ResetNinjectKernel()
    {
      Core.AppContext.UnloadConfigurationModule();
    }

    private static void T01_OrdinaryExamplesAppSlotsMThreads_Test()
    {
      OrdinaryExamplesAppSingleThread.Program.Main(new string[] { "test" });

      IReadOnlyKernel configuration = Core.AppContext.GetConfiguration();
      AppStatistics appStatistics = configuration.Get<AppStatistics>();

      Ensure.That(appStatistics.ThereWereErrors).IsFalse();
      ResetNinjectKernel();

      Console.WriteLine($"Passed: {MethodBase.GetCurrentMethod()}");
    }

    private static void T02_OrdinaryExamplesAppSlotsMThreads_Test()
    {
      OrdinaryExamplesAppSlotsMThreads.Program.Main(new string[] { "test" });

      IReadOnlyKernel configuration = Core.AppContext.GetConfiguration();
      AppStatistics appStatistics = configuration.Get<AppStatistics>();

      Ensure.That(appStatistics.ThereWereErrors).IsFalse();
      ResetNinjectKernel();

      Console.WriteLine($"Passed: {MethodBase.GetCurrentMethod()}");
    }

    private static void T03_IntegerFactExamplesAppComms_Test()
    {
      IntegerFactExamplesAppComms.Program.Main(new string[] { "test" });

      IReadOnlyKernel configuration = Core.AppContext.GetConfiguration();
      AppStatistics appStatistics = configuration.Get<AppStatistics>();

      Ensure.That(appStatistics.ThereWereErrors).IsFalse();
      ResetNinjectKernel();

      Console.WriteLine($"Passed: {MethodBase.GetCurrentMethod()}");
    }

    private static void T04_IntegerFactExamplesAppCPLTM_Test()
    {
      IntegerFactExamplesAppCPLTM.Program.Main(new string[] { "test" });

      IReadOnlyKernel configuration = Core.AppContext.GetConfiguration();
      AppStatistics appStatistics = configuration.Get<AppStatistics>();

      Ensure.That(appStatistics.ThereWereErrors).IsFalse();
      ResetNinjectKernel();

      Console.WriteLine($"Passed: {MethodBase.GetCurrentMethod()}");
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
