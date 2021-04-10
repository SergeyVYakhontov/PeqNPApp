﻿////////////////////////////////////////////////////////////////////////////////////////////////////

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
  public static class F_03_IF_NDTM_Comms_Tests
  {
    #region public members

    public static void RunTests()
    {
      ExampleSetA_Test();
    }

    #endregion

    #region private members

    private static void ResetNinjectKernel()
    {
      Core.AppContext.UnloadConfigurationModule();
    }

    private static void ExampleSetA_Test()
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

      Console.WriteLine($"Passed: {MethodBase.GetCurrentMethod()}");
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////