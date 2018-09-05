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

namespace ProgramTests
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

      AppStatistics appStatistics = configuration.Get<AppStatistics>();
      Assert.False(appStatistics.ThereWereErrors);
    }

    [Fact]
    public void T02_OrdinaryExamplesAppSlotsMThreads_Test()
    {
      OrdinaryExamplesAppSlotsMThreads.Program.Main(new string[] { "test" });

      AppStatistics appStatistics = configuration.Get<AppStatistics>();
      Assert.False(appStatistics.ThereWereErrors);
    }

    [Fact]
    public void T03_IntegerFactExamplesAppComms_Test()
    {
      IntegerFactExamplesAppComms.Program.Main(new string[] { "test" });

      AppStatistics appStatistics = configuration.Get<AppStatistics>();
      Assert.False(appStatistics.ThereWereErrors);
    }

    [Fact]
    public void T04_IntegerFactExamplesAppCPLTM_Test()
    {
      IntegerFactExamplesAppCPLTM.Program.Main(new string[] { "test" });

      AppStatistics appStatistics = configuration.Get<AppStatistics>();
      Assert.False(appStatistics.ThereWereErrors);
    }

    #endregion

    #region private members

    private readonly IKernel configuration = Core.AppContext.Configuration;

    private void ResetNinjectKernel()
    {
      List<Ninject.Modules.INinjectModule> modules = configuration.GetModules().ToList();
      modules.ForEach(m => configuration.Unload(m.Name));
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
