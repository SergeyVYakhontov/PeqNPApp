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
  public class FunctionalTests : IDisposable
  {
    #region public members

    [Fact]
    public void T01_OrdinaryExamplesAppSlotsMThreadsTest()
    {
      OrdinaryExamplesAppSingleThread.Program.Main(new string[] { "test" });

      AppStatistics appStatistics = configuration.Get<AppStatistics>();
      Assert.False(appStatistics.ThereWereErrors);
    }

    [Fact]
    public void T02_OrdinaryExamplesAppSlotsMThreadsTest()
    {
      OrdinaryExamplesAppSlotsMThreads.Program.Main(new string[] { "test" });

      AppStatistics appStatistics = configuration.Get<AppStatistics>();
      Assert.False(appStatistics.ThereWereErrors);
    }

    [Fact]
    public void T03_IntegerFactExamplesAppCommsTest()
    {
      IntegerFactExamplesAppComms.Program.Main(new string[] { "test" });

      AppStatistics appStatistics = configuration.Get<AppStatistics>();
      Assert.False(appStatistics.ThereWereErrors);
    }

    [Fact]
    public void T04_IntegerFactExamplesAppTRSTest()
    {
      IntegerFactExamplesAppTRS.Program.Main(new string[] { "test" });

      AppStatistics appStatistics = configuration.Get<AppStatistics>();
      Assert.False(appStatistics.ThereWereErrors);
    }

    public void Dispose()
    {
      ResetNinjectKernel();
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
