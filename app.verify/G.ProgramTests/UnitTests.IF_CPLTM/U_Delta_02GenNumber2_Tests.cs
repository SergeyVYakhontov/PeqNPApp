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
  public sealed class U_CPLTM_Delta_02GenNumber2_Tests : IDisposable
  {
    #region Ctors

    static U_CPLTM_Delta_02GenNumber2_Tests()
    {
      log4net.Repository.ILoggerRepository logRepository = log4net.LogManager.GetRepository(
        System.Reflection.Assembly.GetEntryAssembly());
      log4net.Config.XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
    }

    #endregion

    #region public members

    public void Dispose()
    {
      ResetNinjectKernel();
    }

    [Fact]
    public void T01_GenNumber2_Delimiter3_Test()
    {
      int[] input = new int[] { 1, 0, 1, 0 }.Reverse().ToArray();
      Setup(input.Length);

      MTExtDefinitions.v2.IF_NDTM tm = new MTExtDefinitions.v2.IF_NDTM(input.Length);
      tm.Setup();

      TMInstance tmInstance = new TMInstance(tm, input);
      tm.PrepareTapeFwd(input, tmInstance);

      DetermStepsEmulator determStepsEmulator = new DetermStepsEmulator(tm.Delta, tmInstance);
      determStepsEmulator.SetupConfiguration(
        frameStart2 + 1,
        (uint)MTExtDefinitions.v2.IF_NDTM.GenNumber2States.GenBitA);

      Dictionary<int, byte> indexMap = new Dictionary<int, byte>
      {
        [0] = 0,
        [1] = 1,
        [2] = 0,
        [3] = 1,
        [4] = 2,
        [5] = 0,
        [6] = 0
      };

      const uint stepsNum = 7;
      determStepsEmulator.DoStepN(stepsNum, indexMap);

      int expectedCellIndex = frameStart3 + 1;

      Assert.True(tmInstance.CellIndex() == expectedCellIndex);
      Assert.True(tmInstance.State() == (uint)MTExtDefinitions.v2.IF_NDTM.GenNumber2States.MoveToDelimiter4);
      Assert.True(tmInstance.TapeSymbol(expectedCellIndex) == OneTapeTuringMachine.blankSymbol);

      Assert.True(tmInstance.TapeSymbol(frameStart1) == MTExtDefinitions.v2.IF_NDTM.delimiter1);
      Assert.True(tmInstance.TapeSymbol(frameStart2) == MTExtDefinitions.v2.IF_NDTM.delimiter2);
      Assert.True(tmInstance.TapeSymbol(frameStart3) == MTExtDefinitions.v2.IF_NDTM.delimiter3);
      Assert.True(tmInstance.TapeSymbol(frameEnd4) == MTExtDefinitions.v2.IF_NDTM.delimiter4);
    }

    [Fact]
    public void T01_GenNumber2_Delimiter4_Test()
    {
      int[] input = new int[] { 1, 0, 1, 0 }.Reverse().ToArray();
      Setup(input.Length);

      MTExtDefinitions.v2.IF_NDTM tm = new MTExtDefinitions.v2.IF_NDTM(input.Length);
      tm.Setup();

      TMInstance tmInstance = new TMInstance(tm, input);
      tm.PrepareTapeFwd(input, tmInstance);

      DetermStepsEmulator determStepsEmulator = new DetermStepsEmulator(tm.Delta, tmInstance);
      determStepsEmulator.SetupConfiguration(
        frameStart3 + 1,
        (uint)MTExtDefinitions.v2.IF_NDTM.GenNumber2States.MoveToDelimiter4);

      const uint stepsNum = 7;
      determStepsEmulator.DoStepN(stepsNum);

      int expectedCellIndex = frameEnd4 - 1;

      Assert.True(tmInstance.CellIndex() == expectedCellIndex);
      Assert.True(tmInstance.State() == (uint)MTExtDefinitions.v2.IF_NDTM.GenNumber2States.MoveToDelimiter0);
      Assert.True(tmInstance.TapeSymbol(expectedCellIndex) == OneTapeTuringMachine.blankSymbol);

      Assert.True(tmInstance.TapeSymbol(frameStart1) == MTExtDefinitions.v2.IF_NDTM.delimiter1);
      Assert.True(tmInstance.TapeSymbol(frameStart2) == MTExtDefinitions.v2.IF_NDTM.delimiter2);
      Assert.True(tmInstance.TapeSymbol(frameStart3) == MTExtDefinitions.v2.IF_NDTM.delimiter3);
      Assert.True(tmInstance.TapeSymbol(frameEnd4) == MTExtDefinitions.v2.IF_NDTM.delimiter4);
    }

    [Fact]
    public void T01_GenNumber2_Delimiter0_Test()
    {
      int[] input = new int[] { 1, 0, 1, 0 }.Reverse().ToArray();
      Setup(input.Length);

      MTExtDefinitions.v2.IF_NDTM tm = new MTExtDefinitions.v2.IF_NDTM(input.Length);
      tm.Setup();

      TMInstance tmInstance = new TMInstance(tm, input);
      tm.PrepareTapeFwd(input, tmInstance);

      tmInstance.SetTapeSymbol(frameStart1 + 1, 0);
      tmInstance.SetTapeSymbol(frameStart1 + 2, 1);

      tmInstance.SetTapeSymbol(frameStart2 + 1, 0);
      tmInstance.SetTapeSymbol(frameStart2 + 2, 1);

      DetermStepsEmulator determStepsEmulator = new DetermStepsEmulator(tm.Delta, tmInstance);
      determStepsEmulator.SetupConfiguration(
        frameEnd4 - 1,
        (uint)MTExtDefinitions.v2.IF_NDTM.GenNumber2States.MoveToDelimiter0);

      const uint stepsNum = 28;
      determStepsEmulator.DoStepN(stepsNum);

      const int expectedCellIndex = 1;

      Assert.True(tmInstance.CellIndex() == expectedCellIndex);
      Assert.True(tmInstance.State() == (uint)MTExtDefinitions.v2.IF_NDTM.GenNumber2States.MoveToDelimiter1);
      Assert.True(tmInstance.TapeSymbol(expectedCellIndex) == 0);

      Assert.True(tmInstance.TapeSymbol(frameStart1) == MTExtDefinitions.v2.IF_NDTM.delimiter1);
      Assert.True(tmInstance.TapeSymbol(frameStart2) == MTExtDefinitions.v2.IF_NDTM.delimiter2);
      Assert.True(tmInstance.TapeSymbol(frameStart3) == MTExtDefinitions.v2.IF_NDTM.delimiter3);
      Assert.True(tmInstance.TapeSymbol(frameEnd4) == MTExtDefinitions.v2.IF_NDTM.delimiter4);
    }

    [Fact]
    public void T01_GenNumber2_Delimiter1_Test()
    {
      int[] input = new int[] { 1, 0, 1, 0 }.Reverse().ToArray();
      Setup(input.Length);

      MTExtDefinitions.v2.IF_NDTM tm = new MTExtDefinitions.v2.IF_NDTM(input.Length);
      tm.Setup();

      TMInstance tmInstance = new TMInstance(tm, input);
      tm.PrepareTapeFwd(input, tmInstance);

      tmInstance.SetTapeSymbol(frameStart1 + 1, 0);
      tmInstance.SetTapeSymbol(frameStart1 + 2, 1);

      DetermStepsEmulator determStepsEmulator = new DetermStepsEmulator(tm.Delta, tmInstance);
      determStepsEmulator.SetupConfiguration(
        1,
        (uint)MTExtDefinitions.v2.IF_NDTM.GenNumber2States.MoveToDelimiter1);

      const uint stepsNum = 7;
      determStepsEmulator.DoStepN(stepsNum);

      const int expectedCellIndex = 8;

      Assert.True(tmInstance.CellIndex() == expectedCellIndex);
      Assert.True(tmInstance.State() == (uint)MTExtDefinitions.v2.IF_NDTM.MultiplyStates.MultReady);
      Assert.True(tmInstance.TapeSymbol(expectedCellIndex) == 0);

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
