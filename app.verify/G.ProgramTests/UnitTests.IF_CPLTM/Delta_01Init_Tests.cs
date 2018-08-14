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
using MTExtDefinitions;
using VerifyResults;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ProgramTests
{
  [TestCaseOrderer("ProgramTests.AlphabeticalTestOrderer", "G.ProgramTests")]
  public sealed class Delta_05Init_Tests : IDisposable
  {
    #region Ctors

    static Delta_05Init_Tests()
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
    public void T01_Init_Test()
    {
      int[] input = new int[] { 1, 0, 1, 0 }.Reverse().ToArray();
      Setup(input.Length);

      MTExtDefinitions.v2.IF_NDTM tm = new MTExtDefinitions.v2.IF_NDTM(input.Length);
      tm.Setup();

      TMInstance tmInstance = new TMInstance(tm, input);
      tm.PrepareTapeFwd(input, tmInstance);

      DetermStepsEmulator determStepsEmulator = new DetermStepsEmulator(tm.Delta, tmInstance);
      determStepsEmulator.SetupConfiguration(
        1,
        MTExtDefinitions.v2.IF_NDTM.qStartState);

      determStepsEmulator.DoStepN(7);

      Assert.True(tmInstance.CellIndex() == 8);
      Assert.True(tmInstance.State() == (uint)MTExtDefinitions.v2.IF_NDTM.GenNumber1States.GenBitA);
      Assert.True(tmInstance.TapeSymbol(8) == OneTapeTuringMachine.blankSymbol);

      Assert.True(tmInstance.TapeSymbol(frameStart1) == MTExtDefinitions.v2.IF_NDTM.delimiter1);
      Assert.True(tmInstance.TapeSymbol(frameStart2) == MTExtDefinitions.v2.IF_NDTM.delimiter2);
      Assert.True(tmInstance.TapeSymbol(frameStart3) == MTExtDefinitions.v2.IF_NDTM.delimiter3);
      Assert.True(tmInstance.TapeSymbol(frameEnd4) == MTExtDefinitions.v2.IF_NDTM.delimiter4);
    }

    #endregion

    #region private members

    private readonly IKernel configuration = Core.AppContext.Configuration;

    private int frameLength;
    private int frameStart1;
    private int frameStart2;
    private int frameStart3;
    private int frameEnd4;

    private void Setup(int inputLength)
    {
      configuration.Load<IntegerFactExamplesAppCPLTM.AppNinjectModule>();

      frameLength = MTExtDefinitions.v2.IF_NDTM.FrameLength(inputLength);
      frameStart1 = MTExtDefinitions.v2.IF_NDTM.FrameStart1(inputLength);
      frameStart2 = MTExtDefinitions.v2.IF_NDTM.FrameStart2(inputLength);
      frameStart3 = MTExtDefinitions.v2.IF_NDTM.FrameStart3(inputLength);
      frameEnd4 = MTExtDefinitions.v2.IF_NDTM.FrameEnd4(inputLength);
    }

    private void ResetNinjectKernel()
    {
      List<Ninject.Modules.INinjectModule> modules = configuration.GetModules().ToList();
      modules.ForEach(m => configuration.Unload(m.Name));
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
