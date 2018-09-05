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
  public sealed class U_CPLTM_Delta_03Multiply_05_Tests : U_CPLTM_Delta_Tests_Base, IDisposable
  {
    #region public members

    public void Dispose()
    {
      ResetNinjectKernel();
    }

    [Fact]
    public void T01_Multiply_05_Blank_Test()
    {
      int[] input = new int[] { 1, 0, 1, 0 }.Reverse().ToArray();
      Setup(input.Length);

      MTExtDefinitions.v2.IF_NDTM tm = new MTExtDefinitions.v2.IF_NDTM(input.Length);
      tm.Setup();

      TMInstance tmInstance = new TMInstance(tm, input);
      tm.PrepareTapeFwd(input, tmInstance);

      DetermStepsEmulator determStepsEmulator = new DetermStepsEmulator(tm.Delta, tmInstance);
      determStepsEmulator.SetupConfiguration(
        frameStart1 + 1,
        (uint)MTExtDefinitions.v2.IF_NDTM.MultiplyStates.MultReady);

      tmInstance.SetTapeSymbol(frameStart1 + 1, 1);
      tmInstance.SetTapeSymbol(frameStart1 + 2, 1);
      tmInstance.SetTapeSymbol(frameStart1 + 3, 0);
      tmInstance.SetTapeSymbol(frameStart1 + 4, 1);

      tmInstance.SetTapeSymbol(frameStart2 + 1, MTExtDefinitions.v2.IF_NDTM.markC0);
      tmInstance.SetTapeSymbol(frameStart2 + 2, MTExtDefinitions.v2.IF_NDTM.markC1);
      tmInstance.SetTapeSymbol(frameStart2 + 3, OneTapeTuringMachine.blankSymbol);

      uint stepsNum = (uint)frameLength + 4;
      determStepsEmulator.DoStepN(stepsNum);

      int expectedCellIndex = frameStart2 + 4;

      Assert.True(tmInstance.CellIndex() == expectedCellIndex);
      Assert.True(tmInstance.State() == (uint)MTExtDefinitions.v2.IF_NDTM.MultiplyStates.MoveToDelimeter3_Bit0_1II);

      Assert.True(tmInstance.TapeSymbol(frameStart1 + 1) == MTExtDefinitions.v2.IF_NDTM.markB1);
      Assert.True(tmInstance.TapeSymbol(frameStart2 + 3) == MTExtDefinitions.v2.IF_NDTM.markC0);

      Assert.True(tmInstance.TapeSymbol(frameStart1) == MTExtDefinitions.v2.IF_NDTM.delimiter1);
      Assert.True(tmInstance.TapeSymbol(frameStart2) == MTExtDefinitions.v2.IF_NDTM.delimiter2);
      Assert.True(tmInstance.TapeSymbol(frameStart3) == MTExtDefinitions.v2.IF_NDTM.delimiter3);
      Assert.True(tmInstance.TapeSymbol(frameEnd4) == MTExtDefinitions.v2.IF_NDTM.delimiter4);
    }

    [Fact]
    public void T02_Multiply_05_Bit0_Test()
    {
      int[] input = new int[] { 1, 0, 1, 0 }.Reverse().ToArray();
      Setup(input.Length);

      MTExtDefinitions.v2.IF_NDTM tm = new MTExtDefinitions.v2.IF_NDTM(input.Length);
      tm.Setup();

      TMInstance tmInstance = new TMInstance(tm, input);
      tm.PrepareTapeFwd(input, tmInstance);

      DetermStepsEmulator determStepsEmulator = new DetermStepsEmulator(tm.Delta, tmInstance);
      determStepsEmulator.SetupConfiguration(
        frameStart1 + 1,
        (uint)MTExtDefinitions.v2.IF_NDTM.MultiplyStates.MultReady);

      tmInstance.SetTapeSymbol(frameStart1 + 1, 1);
      tmInstance.SetTapeSymbol(frameStart1 + 2, 1);
      tmInstance.SetTapeSymbol(frameStart1 + 3, 0);
      tmInstance.SetTapeSymbol(frameStart1 + 4, 1);

      tmInstance.SetTapeSymbol(frameStart2 + 1, MTExtDefinitions.v2.IF_NDTM.markC0);
      tmInstance.SetTapeSymbol(frameStart2 + 2, MTExtDefinitions.v2.IF_NDTM.markC1);
      tmInstance.SetTapeSymbol(frameStart2 + 3, 0);

      uint stepsNum = (uint)frameLength + 4;
      determStepsEmulator.DoStepN(stepsNum);

      int expectedCellIndex = frameStart2 + 4;

      Assert.True(tmInstance.CellIndex() == expectedCellIndex);
      Assert.True(tmInstance.State() == (uint)MTExtDefinitions.v2.IF_NDTM.MultiplyStates.MoveToDelimeter3_Bit0_1II);

      Assert.True(tmInstance.TapeSymbol(frameStart1 + 1) == MTExtDefinitions.v2.IF_NDTM.markB1);
      Assert.True(tmInstance.TapeSymbol(frameStart2 + 3) == MTExtDefinitions.v2.IF_NDTM.markC0);

      Assert.True(tmInstance.TapeSymbol(frameStart1) == MTExtDefinitions.v2.IF_NDTM.delimiter1);
      Assert.True(tmInstance.TapeSymbol(frameStart2) == MTExtDefinitions.v2.IF_NDTM.delimiter2);
      Assert.True(tmInstance.TapeSymbol(frameStart3) == MTExtDefinitions.v2.IF_NDTM.delimiter3);
      Assert.True(tmInstance.TapeSymbol(frameEnd4) == MTExtDefinitions.v2.IF_NDTM.delimiter4);
    }

    [Fact]
    public void T03_Multiply_05_Bit1_Test()
    {
      int[] input = new int[] { 1, 0, 1, 0 }.Reverse().ToArray();
      Setup(input.Length);

      MTExtDefinitions.v2.IF_NDTM tm = new MTExtDefinitions.v2.IF_NDTM(input.Length);
      tm.Setup();

      TMInstance tmInstance = new TMInstance(tm, input);
      tm.PrepareTapeFwd(input, tmInstance);

      DetermStepsEmulator determStepsEmulator = new DetermStepsEmulator(tm.Delta, tmInstance);
      determStepsEmulator.SetupConfiguration(
        frameStart1 + 1,
        (uint)MTExtDefinitions.v2.IF_NDTM.MultiplyStates.MultReady);

      tmInstance.SetTapeSymbol(frameStart1 + 1, 1);
      tmInstance.SetTapeSymbol(frameStart1 + 2, 1);
      tmInstance.SetTapeSymbol(frameStart1 + 3, 0);
      tmInstance.SetTapeSymbol(frameStart1 + 4, 1);

      tmInstance.SetTapeSymbol(frameStart2 + 1, MTExtDefinitions.v2.IF_NDTM.markC0);
      tmInstance.SetTapeSymbol(frameStart2 + 2, MTExtDefinitions.v2.IF_NDTM.markC1);
      tmInstance.SetTapeSymbol(frameStart2 + 3, 1);

      uint stepsNum = (uint)frameLength + 4;
      determStepsEmulator.DoStepN(stepsNum);

      int expectedCellIndex = frameStart2 + 4;

      Assert.True(tmInstance.CellIndex() == expectedCellIndex);
      Assert.True(tmInstance.State() == (uint)MTExtDefinitions.v2.IF_NDTM.MultiplyStates.MoveToDelimeter3_Bit1_1II);

      Assert.True(tmInstance.TapeSymbol(frameStart1 + 1) == MTExtDefinitions.v2.IF_NDTM.markB1);
      Assert.True(tmInstance.TapeSymbol(frameStart2 + 3) == MTExtDefinitions.v2.IF_NDTM.markC1);

      Assert.True(tmInstance.TapeSymbol(frameStart1) == MTExtDefinitions.v2.IF_NDTM.delimiter1);
      Assert.True(tmInstance.TapeSymbol(frameStart2) == MTExtDefinitions.v2.IF_NDTM.delimiter2);
      Assert.True(tmInstance.TapeSymbol(frameStart3) == MTExtDefinitions.v2.IF_NDTM.delimiter3);
      Assert.True(tmInstance.TapeSymbol(frameEnd4) == MTExtDefinitions.v2.IF_NDTM.delimiter4);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
