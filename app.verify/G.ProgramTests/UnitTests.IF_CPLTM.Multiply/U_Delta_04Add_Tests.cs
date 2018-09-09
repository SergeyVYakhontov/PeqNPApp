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

namespace UnitTests
{
  [TestCaseOrderer("ProgramTests.AlphabeticalTestOrderer", "G.ProgramTests")]
  public sealed class U_CPLTM_Delta_04Add_Tests : U_CPLTM_Delta_Tests_Base, IDisposable
  {
    #region public members

    public void Dispose()
    {
      ResetNinjectKernel();
    }

    [Fact]
    public void T01_Add_0b_Test()
    {
      int[] input = new int[] { 1, 0, 1, 0 }.Reverse().ToArray();
      Setup(input.Length);

      MTExtDefinitions.v2.IF_NDTM tm = new MTExtDefinitions.v2.IF_NDTM(input.Length);
      tm.Setup();

      TMInstance tmInstance = new TMInstance(tm, input);
      tm.PrepareTapeFwd(input, tmInstance);

      DetermStepsEmulator determStepsEmulator = new DetermStepsEmulator(tm.Delta, tmInstance);
      determStepsEmulator.SetupConfiguration(
        frameStart2 + 3,
        (uint)MTExtDefinitions.v2.IF_NDTM.MultiplyStates.MoveToDelimeter3_Bit1_1II);

      tmInstance.SetTapeSymbol(frameStart2 + 1, MTExtDefinitions.v2.IF_NDTM.markC0);
      tmInstance.SetTapeSymbol(frameStart2 + 2, MTExtDefinitions.v2.IF_NDTM.markC1);
      tmInstance.SetTapeSymbol(frameStart2 + 3, 1);

      tmInstance.SetTapeSymbol(frameStart3 + 1, OneTapeTuringMachine.blankSymbol);

      uint stepsNum = (uint)frameLength * 2;
      determStepsEmulator.DoStepN(stepsNum);

      int expectedCellIndex = frameEnd4 - 1;

      Assert.True(tmInstance.CellIndex() == expectedCellIndex);
      Assert.True(tmInstance.State() == (uint)MTExtDefinitions.v2.IF_NDTM.MultiplyStates.MoveToDelimeter0_Bit1_1II);
      Assert.True(tmInstance.TapeSymbol(expectedCellIndex) == OneTapeTuringMachine.blankSymbol);

      Assert.True(tmInstance.TapeSymbol(frameStart3 + 1) == MTExtDefinitions.v2.IF_NDTM.markD1);
      Assert.True(tmInstance.TapeSymbol(frameStart3 + 2) == OneTapeTuringMachine.blankSymbol);
      Assert.True(tmInstance.TapeSymbol(frameStart3 + 3) == OneTapeTuringMachine.blankSymbol);

      Assert.True(tmInstance.TapeSymbol(frameStart1) == MTExtDefinitions.v2.IF_NDTM.delimiter1);
      Assert.True(tmInstance.TapeSymbol(frameStart2) == MTExtDefinitions.v2.IF_NDTM.delimiter2);
      Assert.True(tmInstance.TapeSymbol(frameStart3) == MTExtDefinitions.v2.IF_NDTM.delimiter3);
      Assert.True(tmInstance.TapeSymbol(frameEnd4) == MTExtDefinitions.v2.IF_NDTM.delimiter4);
    }

    [Fact]
    public void T02_Add_1b_Test()
    {
      int[] input = new int[] { 1, 0, 1, 0 }.Reverse().ToArray();
      Setup(input.Length);

      MTExtDefinitions.v2.IF_NDTM tm = new MTExtDefinitions.v2.IF_NDTM(input.Length);
      tm.Setup();

      TMInstance tmInstance = new TMInstance(tm, input);
      tm.PrepareTapeFwd(input, tmInstance);

      DetermStepsEmulator determStepsEmulator = new DetermStepsEmulator(tm.Delta, tmInstance);
      determStepsEmulator.SetupConfiguration(
        frameStart2 + 3,
        (uint)MTExtDefinitions.v2.IF_NDTM.MultiplyStates.MoveToDelimeter3_Bit1_1II);

      tmInstance.SetTapeSymbol(frameStart2 + 1, MTExtDefinitions.v2.IF_NDTM.markC0);
      tmInstance.SetTapeSymbol(frameStart2 + 2, MTExtDefinitions.v2.IF_NDTM.markC1);
      tmInstance.SetTapeSymbol(frameStart2 + 3, 1);

      tmInstance.SetTapeSymbol(frameStart3 + 1, 1);

      uint stepsNum = (uint)frameLength * 2;
      determStepsEmulator.DoStepN(stepsNum);

      int expectedCellIndex = frameEnd4 - 1;

      Assert.True(tmInstance.CellIndex() == expectedCellIndex);
      Assert.True(tmInstance.State() == (uint)MTExtDefinitions.v2.IF_NDTM.MultiplyStates.MoveToDelimeter0_Bit1_1II);
      Assert.True(tmInstance.TapeSymbol(expectedCellIndex) == OneTapeTuringMachine.blankSymbol);

      Assert.True(tmInstance.TapeSymbol(frameStart3 + 1) == MTExtDefinitions.v2.IF_NDTM.markD0);
      Assert.True(tmInstance.TapeSymbol(frameStart3 + 2) == 1);
      Assert.True(tmInstance.TapeSymbol(frameStart3 + 3) == OneTapeTuringMachine.blankSymbol);

      Assert.True(tmInstance.TapeSymbol(frameStart1) == MTExtDefinitions.v2.IF_NDTM.delimiter1);
      Assert.True(tmInstance.TapeSymbol(frameStart2) == MTExtDefinitions.v2.IF_NDTM.delimiter2);
      Assert.True(tmInstance.TapeSymbol(frameStart3) == MTExtDefinitions.v2.IF_NDTM.delimiter3);
      Assert.True(tmInstance.TapeSymbol(frameEnd4) == MTExtDefinitions.v2.IF_NDTM.delimiter4);
    }

    [Fact]
    public void T03_Add_00_Test()
    {
      int[] input = new int[] { 1, 0, 1, 0 }.Reverse().ToArray();
      Setup(input.Length);

      MTExtDefinitions.v2.IF_NDTM tm = new MTExtDefinitions.v2.IF_NDTM(input.Length);
      tm.Setup();

      TMInstance tmInstance = new TMInstance(tm, input);
      tm.PrepareTapeFwd(input, tmInstance);

      DetermStepsEmulator determStepsEmulator = new DetermStepsEmulator(tm.Delta, tmInstance);
      determStepsEmulator.SetupConfiguration(
        frameStart2 + 3,
        (uint)MTExtDefinitions.v2.IF_NDTM.MultiplyStates.MoveToDelimeter3_Bit1_1II);

      tmInstance.SetTapeSymbol(frameStart2 + 1, MTExtDefinitions.v2.IF_NDTM.markC0);
      tmInstance.SetTapeSymbol(frameStart2 + 2, MTExtDefinitions.v2.IF_NDTM.markC1);
      tmInstance.SetTapeSymbol(frameStart2 + 3, 1);

      tmInstance.SetTapeSymbol(frameStart3 + 1, 0);
      tmInstance.SetTapeSymbol(frameStart3 + 2, 0);

      uint stepsNum = (uint)frameLength * 2;
      determStepsEmulator.DoStepN(stepsNum);

      int expectedCellIndex = frameEnd4 - 1;

      Assert.True(tmInstance.CellIndex() == expectedCellIndex);
      Assert.True(tmInstance.State() == (uint)MTExtDefinitions.v2.IF_NDTM.MultiplyStates.MoveToDelimeter0_Bit1_1II);
      Assert.True(tmInstance.TapeSymbol(expectedCellIndex) == OneTapeTuringMachine.blankSymbol);

      Assert.True(tmInstance.TapeSymbol(frameStart3 + 1) == MTExtDefinitions.v2.IF_NDTM.markD1);
      Assert.True(tmInstance.TapeSymbol(frameStart3 + 2) == 0);
      Assert.True(tmInstance.TapeSymbol(frameStart3 + 3) == OneTapeTuringMachine.blankSymbol);

      Assert.True(tmInstance.TapeSymbol(frameStart1) == MTExtDefinitions.v2.IF_NDTM.delimiter1);
      Assert.True(tmInstance.TapeSymbol(frameStart2) == MTExtDefinitions.v2.IF_NDTM.delimiter2);
      Assert.True(tmInstance.TapeSymbol(frameStart3) == MTExtDefinitions.v2.IF_NDTM.delimiter3);
      Assert.True(tmInstance.TapeSymbol(frameEnd4) == MTExtDefinitions.v2.IF_NDTM.delimiter4);
    }

    [Fact]
    public void T04_Add_10_Test()
    {
      int[] input = new int[] { 1, 0, 1, 0 }.Reverse().ToArray();
      Setup(input.Length);

      MTExtDefinitions.v2.IF_NDTM tm = new MTExtDefinitions.v2.IF_NDTM(input.Length);
      tm.Setup();

      TMInstance tmInstance = new TMInstance(tm, input);
      tm.PrepareTapeFwd(input, tmInstance);

      DetermStepsEmulator determStepsEmulator = new DetermStepsEmulator(tm.Delta, tmInstance);
      determStepsEmulator.SetupConfiguration(
        frameStart2 + 3,
        (uint)MTExtDefinitions.v2.IF_NDTM.MultiplyStates.MoveToDelimeter3_Bit1_1II);

      tmInstance.SetTapeSymbol(frameStart2 + 1, MTExtDefinitions.v2.IF_NDTM.markC0);
      tmInstance.SetTapeSymbol(frameStart2 + 2, MTExtDefinitions.v2.IF_NDTM.markC1);
      tmInstance.SetTapeSymbol(frameStart2 + 3, 1);

      tmInstance.SetTapeSymbol(frameStart3 + 1, 1);
      tmInstance.SetTapeSymbol(frameStart3 + 2, 0);

      uint stepsNum = (uint)frameLength * 2;
      determStepsEmulator.DoStepN(stepsNum);

      int expectedCellIndex = frameEnd4 - 1;

      Assert.True(tmInstance.CellIndex() == expectedCellIndex);
      Assert.True(tmInstance.State() == (uint)MTExtDefinitions.v2.IF_NDTM.MultiplyStates.MoveToDelimeter0_Bit1_1II);
      Assert.True(tmInstance.TapeSymbol(expectedCellIndex) == OneTapeTuringMachine.blankSymbol);

      Assert.True(tmInstance.TapeSymbol(frameStart3 + 1) == MTExtDefinitions.v2.IF_NDTM.markD0);
      Assert.True(tmInstance.TapeSymbol(frameStart3 + 2) == 1);
      Assert.True(tmInstance.TapeSymbol(frameStart3 + 3) == OneTapeTuringMachine.blankSymbol);

      Assert.True(tmInstance.TapeSymbol(frameStart1) == MTExtDefinitions.v2.IF_NDTM.delimiter1);
      Assert.True(tmInstance.TapeSymbol(frameStart2) == MTExtDefinitions.v2.IF_NDTM.delimiter2);
      Assert.True(tmInstance.TapeSymbol(frameStart3) == MTExtDefinitions.v2.IF_NDTM.delimiter3);
      Assert.True(tmInstance.TapeSymbol(frameEnd4) == MTExtDefinitions.v2.IF_NDTM.delimiter4);
    }

    [Fact]
    public void T05_Add_01_Test()
    {
      int[] input = new int[] { 1, 0, 1, 0 }.Reverse().ToArray();
      Setup(input.Length);

      MTExtDefinitions.v2.IF_NDTM tm = new MTExtDefinitions.v2.IF_NDTM(input.Length);
      tm.Setup();

      TMInstance tmInstance = new TMInstance(tm, input);
      tm.PrepareTapeFwd(input, tmInstance);

      DetermStepsEmulator determStepsEmulator = new DetermStepsEmulator(tm.Delta, tmInstance);
      determStepsEmulator.SetupConfiguration(
        frameStart2 + 3,
        (uint)MTExtDefinitions.v2.IF_NDTM.MultiplyStates.MoveToDelimeter3_Bit1_1II);

      tmInstance.SetTapeSymbol(frameStart2 + 1, MTExtDefinitions.v2.IF_NDTM.markC0);
      tmInstance.SetTapeSymbol(frameStart2 + 2, MTExtDefinitions.v2.IF_NDTM.markC1);
      tmInstance.SetTapeSymbol(frameStart2 + 3, 1);

      tmInstance.SetTapeSymbol(frameStart3 + 1, 0);
      tmInstance.SetTapeSymbol(frameStart3 + 2, 1);

      uint stepsNum = (uint)frameLength * 2;
      determStepsEmulator.DoStepN(stepsNum);

      int expectedCellIndex = frameEnd4 - 1;

      Assert.True(tmInstance.CellIndex() == expectedCellIndex);
      Assert.True(tmInstance.State() == (uint)MTExtDefinitions.v2.IF_NDTM.MultiplyStates.MoveToDelimeter0_Bit1_1II);
      Assert.True(tmInstance.TapeSymbol(expectedCellIndex) == OneTapeTuringMachine.blankSymbol);

      Assert.True(tmInstance.TapeSymbol(frameStart3 + 1) == MTExtDefinitions.v2.IF_NDTM.markD1);
      Assert.True(tmInstance.TapeSymbol(frameStart3 + 2) == 1);
      Assert.True(tmInstance.TapeSymbol(frameStart3 + 3) == OneTapeTuringMachine.blankSymbol);

      Assert.True(tmInstance.TapeSymbol(frameStart1) == MTExtDefinitions.v2.IF_NDTM.delimiter1);
      Assert.True(tmInstance.TapeSymbol(frameStart2) == MTExtDefinitions.v2.IF_NDTM.delimiter2);
      Assert.True(tmInstance.TapeSymbol(frameStart3) == MTExtDefinitions.v2.IF_NDTM.delimiter3);
      Assert.True(tmInstance.TapeSymbol(frameEnd4) == MTExtDefinitions.v2.IF_NDTM.delimiter4);
    }

    [Fact]
    public void T06_Add_11_Test()
    {
      int[] input = new int[] { 1, 0, 1, 0 }.Reverse().ToArray();
      Setup(input.Length);

      MTExtDefinitions.v2.IF_NDTM tm = new MTExtDefinitions.v2.IF_NDTM(input.Length);
      tm.Setup();

      TMInstance tmInstance = new TMInstance(tm, input);
      tm.PrepareTapeFwd(input, tmInstance);

      DetermStepsEmulator determStepsEmulator = new DetermStepsEmulator(tm.Delta, tmInstance);
      determStepsEmulator.SetupConfiguration(
        frameStart2 + 3,
        (uint)MTExtDefinitions.v2.IF_NDTM.MultiplyStates.MoveToDelimeter3_Bit1_1II);

      tmInstance.SetTapeSymbol(frameStart2 + 1, MTExtDefinitions.v2.IF_NDTM.markC0);
      tmInstance.SetTapeSymbol(frameStart2 + 2, MTExtDefinitions.v2.IF_NDTM.markC1);
      tmInstance.SetTapeSymbol(frameStart2 + 3, 1);

      tmInstance.SetTapeSymbol(frameStart3 + 1, 1);
      tmInstance.SetTapeSymbol(frameStart3 + 2, 1);

      uint stepsNum = (uint)frameLength * 2;
      determStepsEmulator.DoStepN(stepsNum);

      int expectedCellIndex = frameEnd4 - 1;

      Assert.True(tmInstance.CellIndex() == expectedCellIndex);
      Assert.True(tmInstance.State() == (uint)MTExtDefinitions.v2.IF_NDTM.MultiplyStates.MoveToDelimeter0_Bit1_1II);
      Assert.True(tmInstance.TapeSymbol(expectedCellIndex) == OneTapeTuringMachine.blankSymbol);

      Assert.True(tmInstance.TapeSymbol(frameStart3 + 1) == MTExtDefinitions.v2.IF_NDTM.markD0);
      Assert.True(tmInstance.TapeSymbol(frameStart3 + 2) == 0);
      Assert.True(tmInstance.TapeSymbol(frameStart3 + 3) == 1);
      Assert.True(tmInstance.TapeSymbol(frameStart3 + 4) == OneTapeTuringMachine.blankSymbol);

      Assert.True(tmInstance.TapeSymbol(frameStart1) == MTExtDefinitions.v2.IF_NDTM.delimiter1);
      Assert.True(tmInstance.TapeSymbol(frameStart2) == MTExtDefinitions.v2.IF_NDTM.delimiter2);
      Assert.True(tmInstance.TapeSymbol(frameStart3) == MTExtDefinitions.v2.IF_NDTM.delimiter3);
      Assert.True(tmInstance.TapeSymbol(frameEnd4) == MTExtDefinitions.v2.IF_NDTM.delimiter4);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////