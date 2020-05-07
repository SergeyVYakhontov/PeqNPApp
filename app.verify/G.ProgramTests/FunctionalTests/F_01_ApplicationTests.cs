////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using Ninject;
using Xunit;
using Core;
using ExistsAcceptingPath;
using VerifyResults;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace FunctionalTests
{
  [TestCaseOrderer("ProgramTests.AlphabeticalTestOrderer", "G.ProgramTests")]
  public sealed class F_01_Application_Tests : IDisposable
  {
    #region public members

    public void Dispose()
    {
      ResetNinjectKernel();
    }

    [Fact]
    public void T01_OrdinaryExamplesAppSlotsMThreads_Test()
    {
      OrdinaryExamplesAppSingleThread.Program.Main(new string[] { "test" });

      IReadOnlyKernel configuration = Core.AppContext.GetConfiguration();
      AppStatistics appStatistics = configuration.Get<AppStatistics>();

      Assert.False(appStatistics.ThereWereErrors);
    }

    [Fact]
    public void T02_OrdinaryExamplesAppSlotsMThreads_Test()
    {
      OrdinaryExamplesAppSlotsMThreads.Program.Main(new string[] { "test" });

      IReadOnlyKernel configuration = Core.AppContext.GetConfiguration();
      AppStatistics appStatistics = configuration.Get<AppStatistics>();

      Assert.False(appStatistics.ThereWereErrors);
    }

    [Fact]
    public void T03_IntegerFactExamplesAppComms_Test()
    {
      IntegerFactExamplesAppComms.Program.Main(new string[] { "test" });

      IReadOnlyKernel configuration = Core.AppContext.GetConfiguration();
      AppStatistics appStatistics = configuration.Get<AppStatistics>();

      Assert.False(appStatistics.ThereWereErrors);
    }

    [Fact]
    public void T04_IntegerFactExamplesAppCPLTM_Test()
    {
      IntegerFactExamplesAppCPLTM.Program.Main(new string[] { "test" });

      IReadOnlyKernel configuration = Core.AppContext.GetConfiguration();
      AppStatistics appStatistics = configuration.Get<AppStatistics>();

      Assert.False(appStatistics.ThereWereErrors);
    }

    #endregion

    #region private members

    private void ResetNinjectKernel()
    {
      Core.AppContext.UnloadConfigurationModule();
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
