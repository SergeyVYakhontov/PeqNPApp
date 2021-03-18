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
  public sealed class F_04_IF_NDTM_CPLTM_Tests : IDisposable
  {
    #region Ctors

    static F_04_IF_NDTM_CPLTM_Tests()
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
    public void ExampleSetA_Test()
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
