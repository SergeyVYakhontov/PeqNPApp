﻿////////////////////////////////////////////////////////////////////////////////////////////////////

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
  public sealed class U_CPLTM_Delta_05CompareResults_Tests : U_CPLTM_Delta_Tests_Base, IDisposable
  {
    #region public members

    public void Dispose()
    {
      ResetNinjectKernel();
    }

    [Fact]
    public void T01_CompareResults1_Test()
    {
      int[] input = new int[] { 1, 0, 1, 0 }.Reverse().ToArray();
      Setup(input.Length);

      MTExtDefinitions.v2.IF_NDTM tm = new(input.Length);
      tm.Setup();

      TMInstance tmInstance = new(tm, input);
      tm.PrepareTapeFwd(input, tmInstance);

      tmInstance.SetTapeSymbol(frameStart1 + 1, 0);
      tmInstance.SetTapeSymbol(frameStart1 + 2, 1);
      tmInstance.SetTapeSymbol(frameStart1 + 3, MTExtDefinitions.v2.IF_NDTM.markB0);
      tmInstance.SetTapeSymbol(frameStart1 + 4, MTExtDefinitions.v2.IF_NDTM.markB1);
      tmInstance.SetTapeSymbol(frameStart1 + 5, MTExtDefinitions.v2.IF_NDTM.markB0);
      tmInstance.SetTapeSymbol(frameStart1 + 6, MTExtDefinitions.v2.IF_NDTM.markB1);

      DetermStepsEmulator determStepsEmulator = new(tm.Delta, tmInstance);
      determStepsEmulator.SetupConfiguration(
        frameStart1 + 6,
        (uint)MTExtDefinitions.v2.IF_NDTM.CompareStates.StartComparing);

      uint stepsNum = ((uint)frameLength * 2) + 2;
      determStepsEmulator.DoStepsN(stepsNum);

      const int expectedCellIndex = 1;

      Assert.True(tmInstance.CellIndex() == expectedCellIndex);
      Assert.True(tmInstance.State() == (uint)MTExtDefinitions.v2.IF_NDTM.CompareStates.BitLoopStart);
      Assert.True(tmInstance.TapeSymbol(expectedCellIndex) == 0);

      Assert.True(tmInstance.TapeSymbol(frameStart1 + 3) == 0);
      Assert.True(tmInstance.TapeSymbol(frameStart1 + 4) == 1);

      Assert.True(tmInstance.TapeSymbol(frameStart1) == MTExtDefinitions.v2.IF_NDTM.delimiter1);
      Assert.True(tmInstance.TapeSymbol(frameStart2) == MTExtDefinitions.v2.IF_NDTM.delimiter2);
      Assert.True(tmInstance.TapeSymbol(frameStart3) == MTExtDefinitions.v2.IF_NDTM.delimiter3);
      Assert.True(tmInstance.TapeSymbol(frameEnd4) == MTExtDefinitions.v2.IF_NDTM.delimiter4);
    }

    [Fact]
    public void T02_CompareResults2_Bit0_Success_Test()
    {
      int[] input = new int[] { 1, 0, 1, 0 }.Reverse().ToArray();
      Setup(input.Length);

      MTExtDefinitions.v2.IF_NDTM tm = new(input.Length);
      tm.Setup();

      TMInstance tmInstance = new(tm, input);
      tm.PrepareTapeFwd(input, tmInstance);

      tmInstance.SetTapeSymbol(frameStart1 + 1, 0);
      tmInstance.SetTapeSymbol(frameStart1 + 2, 1);

      tmInstance.SetTapeSymbol(frameStart2 + 1, 0);
      tmInstance.SetTapeSymbol(frameStart2 + 2, 1);

      tmInstance.SetTapeSymbol(frameStart3 + 1, MTExtDefinitions.v2.IF_NDTM.markF0);
      tmInstance.SetTapeSymbol(frameStart3 + 2, MTExtDefinitions.v2.IF_NDTM.markF1);
      tmInstance.SetTapeSymbol(frameStart3 + 3, 0);

      DetermStepsEmulator determStepsEmulator = new(tm.Delta, tmInstance);
      determStepsEmulator.SetupConfiguration(
        1,
        (uint)MTExtDefinitions.v2.IF_NDTM.CompareStates.BitLoopStart);

      uint stepsNum = ((uint)frameLength * 4) + 4;
      determStepsEmulator.DoStepsN(stepsNum);

      int expectedCellIndex = frameEnd4 - 1;

      Assert.True(tmInstance.CellIndex() == expectedCellIndex);
      Assert.True(tmInstance.State() == (uint)MTExtDefinitions.v2.IF_NDTM.CompareStates.MoveToDelimiter0);
      Assert.True(tmInstance.TapeSymbol(expectedCellIndex) == OneTapeTuringMachine.blankSymbol);

      Assert.True(tmInstance.TapeSymbol(1) == MTExtDefinitions.v2.IF_NDTM.markE0);
      Assert.True(tmInstance.TapeSymbol(frameStart3 + 3) == MTExtDefinitions.v2.IF_NDTM.markF0);

      Assert.True(tmInstance.TapeSymbol(frameStart1) == MTExtDefinitions.v2.IF_NDTM.delimiter1);
      Assert.True(tmInstance.TapeSymbol(frameStart2) == MTExtDefinitions.v2.IF_NDTM.delimiter2);
      Assert.True(tmInstance.TapeSymbol(frameStart3) == MTExtDefinitions.v2.IF_NDTM.delimiter3);
      Assert.True(tmInstance.TapeSymbol(frameEnd4) == MTExtDefinitions.v2.IF_NDTM.delimiter4);
    }

    [Fact]
    public void T03_CompareResults2_Bit0_Failure_Test()
    {
      int[] input = new int[] { 1, 0, 1, 0 }.Reverse().ToArray();
      Setup(input.Length);

      MTExtDefinitions.v2.IF_NDTM tm = new(input.Length);
      tm.Setup();

      TMInstance tmInstance = new(tm, input);
      tm.PrepareTapeFwd(input, tmInstance);

      tmInstance.SetTapeSymbol(frameStart1 + 1, 0);
      tmInstance.SetTapeSymbol(frameStart1 + 2, 1);

      tmInstance.SetTapeSymbol(frameStart2 + 1, 0);
      tmInstance.SetTapeSymbol(frameStart2 + 2, 1);

      tmInstance.SetTapeSymbol(frameStart3 + 1, MTExtDefinitions.v2.IF_NDTM.markF0);
      tmInstance.SetTapeSymbol(frameStart3 + 2, MTExtDefinitions.v2.IF_NDTM.markF1);
      tmInstance.SetTapeSymbol(frameStart3 + 3, 1);

      DetermStepsEmulator determStepsEmulator = new(tm.Delta, tmInstance);
      determStepsEmulator.SetupConfiguration(
        1,
        (uint)MTExtDefinitions.v2.IF_NDTM.CompareStates.BitLoopStart);

      uint stepsNum = (uint)frameLength * 4;
      determStepsEmulator.DoStepsN(stepsNum);

      int expectedCellIndex = frameEnd4 - 3;

      Assert.True(tmInstance.CellIndex() == expectedCellIndex);
      Assert.True(tmInstance.State() == MTExtDefinitions.v2.IF_NDTM.rejectingState);
      Assert.True(tmInstance.TapeSymbol(expectedCellIndex) == OneTapeTuringMachine.blankSymbol);

      Assert.True(tmInstance.TapeSymbol(1) == MTExtDefinitions.v2.IF_NDTM.markE0);
      Assert.True(tmInstance.TapeSymbol(frameStart3 + 3) == 1);

      Assert.True(tmInstance.TapeSymbol(frameStart1) == MTExtDefinitions.v2.IF_NDTM.delimiter1);
      Assert.True(tmInstance.TapeSymbol(frameStart2) == MTExtDefinitions.v2.IF_NDTM.delimiter2);
      Assert.True(tmInstance.TapeSymbol(frameStart3) == MTExtDefinitions.v2.IF_NDTM.delimiter3);
      Assert.True(tmInstance.TapeSymbol(frameEnd4) == MTExtDefinitions.v2.IF_NDTM.delimiter4);
    }

    [Fact]
    public void T04_CompareResults3_Bit1_Success_Test()
    {
      int[] input = new int[] { 1, 0, 1, 1 }.Reverse().ToArray();
      Setup(input.Length);

      MTExtDefinitions.v2.IF_NDTM tm = new(input.Length);
      tm.Setup();

      TMInstance tmInstance = new(tm, input);
      tm.PrepareTapeFwd(input, tmInstance);

      tmInstance.SetTapeSymbol(frameStart1 + 1, 0);
      tmInstance.SetTapeSymbol(frameStart1 + 2, 1);

      tmInstance.SetTapeSymbol(frameStart2 + 1, 0);
      tmInstance.SetTapeSymbol(frameStart2 + 2, 1);

      tmInstance.SetTapeSymbol(frameStart3 + 1, MTExtDefinitions.v2.IF_NDTM.markF0);
      tmInstance.SetTapeSymbol(frameStart3 + 2, MTExtDefinitions.v2.IF_NDTM.markF1);
      tmInstance.SetTapeSymbol(frameStart3 + 3, 1);

      DetermStepsEmulator determStepsEmulator = new(tm.Delta, tmInstance);
      determStepsEmulator.SetupConfiguration(
        1,
        (uint)MTExtDefinitions.v2.IF_NDTM.CompareStates.BitLoopStart);

      uint stepsNum = ((uint)frameLength * 4) + 4;
      determStepsEmulator.DoStepsN(stepsNum);

      int expectedCellIndex = frameEnd4 - 1;

      Assert.True(tmInstance.CellIndex() == expectedCellIndex);
      Assert.True(tmInstance.State() == (uint)MTExtDefinitions.v2.IF_NDTM.CompareStates.MoveToDelimiter0);
      Assert.True(tmInstance.TapeSymbol(expectedCellIndex) == OneTapeTuringMachine.blankSymbol);

      Assert.True(tmInstance.TapeSymbol(1) == MTExtDefinitions.v2.IF_NDTM.markE1);
      Assert.True(tmInstance.TapeSymbol(frameStart3 + 3) == MTExtDefinitions.v2.IF_NDTM.markF1);

      Assert.True(tmInstance.TapeSymbol(frameStart1) == MTExtDefinitions.v2.IF_NDTM.delimiter1);
      Assert.True(tmInstance.TapeSymbol(frameStart2) == MTExtDefinitions.v2.IF_NDTM.delimiter2);
      Assert.True(tmInstance.TapeSymbol(frameStart3) == MTExtDefinitions.v2.IF_NDTM.delimiter3);
      Assert.True(tmInstance.TapeSymbol(frameEnd4) == MTExtDefinitions.v2.IF_NDTM.delimiter4);
    }

    [Fact]
    public void T05_CompareResults3_Bit1_Failure_Test()
    {
      int[] input = new int[] { 1, 0, 1, 1 }.Reverse().ToArray();
      Setup(input.Length);

      MTExtDefinitions.v2.IF_NDTM tm = new(input.Length);
      tm.Setup();

      TMInstance tmInstance = new(tm, input);
      tm.PrepareTapeFwd(input, tmInstance);

      tmInstance.SetTapeSymbol(frameStart1 + 1, 0);
      tmInstance.SetTapeSymbol(frameStart1 + 2, 1);

      tmInstance.SetTapeSymbol(frameStart2 + 1, 0);
      tmInstance.SetTapeSymbol(frameStart2 + 2, 1);

      tmInstance.SetTapeSymbol(frameStart3 + 1, MTExtDefinitions.v2.IF_NDTM.markF0);
      tmInstance.SetTapeSymbol(frameStart3 + 2, MTExtDefinitions.v2.IF_NDTM.markF1);
      tmInstance.SetTapeSymbol(frameStart3 + 3, 0);

      DetermStepsEmulator determStepsEmulator = new(tm.Delta, tmInstance);
      determStepsEmulator.SetupConfiguration(
        1,
        (uint)MTExtDefinitions.v2.IF_NDTM.CompareStates.BitLoopStart);

      uint stepsNum = (uint)frameLength * 4;
      determStepsEmulator.DoStepsN(stepsNum);

      int expectedCellIndex = frameEnd4 - 3;

      Assert.True(tmInstance.CellIndex() == expectedCellIndex);
      Assert.True(tmInstance.State() == MTExtDefinitions.v2.IF_NDTM.rejectingState);
      Assert.True(tmInstance.TapeSymbol(expectedCellIndex) == OneTapeTuringMachine.blankSymbol);

      Assert.True(tmInstance.TapeSymbol(1) == MTExtDefinitions.v2.IF_NDTM.markE1);
      Assert.True(tmInstance.TapeSymbol(frameStart3 + 3) == 0);

      Assert.True(tmInstance.TapeSymbol(frameStart1) == MTExtDefinitions.v2.IF_NDTM.delimiter1);
      Assert.True(tmInstance.TapeSymbol(frameStart2) == MTExtDefinitions.v2.IF_NDTM.delimiter2);
      Assert.True(tmInstance.TapeSymbol(frameStart3) == MTExtDefinitions.v2.IF_NDTM.delimiter3);
      Assert.True(tmInstance.TapeSymbol(frameEnd4) == MTExtDefinitions.v2.IF_NDTM.delimiter4);
    }

    [Fact]
    public void T06_CompareResults6_Bit0_SkipE_Test()
    {
      int[] input = new int[] { 1, 0, 1, 0 }.Reverse().ToArray();
      Setup(input.Length);

      MTExtDefinitions.v2.IF_NDTM tm = new(input.Length);
      tm.Setup();

      TMInstance tmInstance = new(tm, input);
      tm.PrepareTapeFwd(input, tmInstance);

      tmInstance.SetTapeSymbol(1, MTExtDefinitions.v2.IF_NDTM.markE0);
      tmInstance.SetTapeSymbol(2, MTExtDefinitions.v2.IF_NDTM.markE1);
      tmInstance.SetTapeSymbol(3, 0);
      tmInstance.SetTapeSymbol(4, 1);

      tmInstance.SetTapeSymbol(frameStart1 + 1, 0);
      tmInstance.SetTapeSymbol(frameStart1 + 2, 1);

      tmInstance.SetTapeSymbol(frameStart2 + 1, 0);
      tmInstance.SetTapeSymbol(frameStart2 + 2, 1);

      tmInstance.SetTapeSymbol(frameStart3 + 1, MTExtDefinitions.v2.IF_NDTM.markF0);
      tmInstance.SetTapeSymbol(frameStart3 + 2, MTExtDefinitions.v2.IF_NDTM.markF1);
      tmInstance.SetTapeSymbol(frameStart3 + 3, 0);

      DetermStepsEmulator determStepsEmulator = new(tm.Delta, tmInstance);
      determStepsEmulator.SetupConfiguration(
        1,
        (uint)MTExtDefinitions.v2.IF_NDTM.CompareStates.SkipE);

      uint stepsNum = ((uint)frameLength * 4) + 4;
      determStepsEmulator.DoStepsN(stepsNum);

      int expectedCellIndex = frameEnd4 - 1;

      Assert.True(tmInstance.CellIndex() == expectedCellIndex);
      Assert.True(tmInstance.State() == (uint)MTExtDefinitions.v2.IF_NDTM.CompareStates.MoveToDelimiter0);
      Assert.True(tmInstance.TapeSymbol(expectedCellIndex) == OneTapeTuringMachine.blankSymbol);

      Assert.True(tmInstance.TapeSymbol(1) == MTExtDefinitions.v2.IF_NDTM.markE0);
      Assert.True(tmInstance.TapeSymbol(2) == MTExtDefinitions.v2.IF_NDTM.markE1);
      Assert.True(tmInstance.TapeSymbol(frameStart3 + 3) == MTExtDefinitions.v2.IF_NDTM.markF0);

      Assert.True(tmInstance.TapeSymbol(frameStart1) == MTExtDefinitions.v2.IF_NDTM.delimiter1);
      Assert.True(tmInstance.TapeSymbol(frameStart2) == MTExtDefinitions.v2.IF_NDTM.delimiter2);
      Assert.True(tmInstance.TapeSymbol(frameStart3) == MTExtDefinitions.v2.IF_NDTM.delimiter3);
      Assert.True(tmInstance.TapeSymbol(frameEnd4) == MTExtDefinitions.v2.IF_NDTM.delimiter4);
    }

    [Fact]
    public void T07_CompareResults6_Bit1_SkipE_Test()
    {
      int[] input = new int[] { 1, 0, 1, 0 }.Reverse().ToArray();
      Setup(input.Length);

      MTExtDefinitions.v2.IF_NDTM tm = new(input.Length);
      tm.Setup();

      TMInstance tmInstance = new(tm, input);
      tm.PrepareTapeFwd(input, tmInstance);

      tmInstance.SetTapeSymbol(1, MTExtDefinitions.v2.IF_NDTM.markE0);
      tmInstance.SetTapeSymbol(2, MTExtDefinitions.v2.IF_NDTM.markE1);
      tmInstance.SetTapeSymbol(3, 1);
      tmInstance.SetTapeSymbol(4, 0);

      tmInstance.SetTapeSymbol(frameStart1 + 1, 0);
      tmInstance.SetTapeSymbol(frameStart1 + 2, 1);

      tmInstance.SetTapeSymbol(frameStart2 + 1, 0);
      tmInstance.SetTapeSymbol(frameStart2 + 2, 1);

      tmInstance.SetTapeSymbol(frameStart3 + 1, MTExtDefinitions.v2.IF_NDTM.markF0);
      tmInstance.SetTapeSymbol(frameStart3 + 2, MTExtDefinitions.v2.IF_NDTM.markF1);
      tmInstance.SetTapeSymbol(frameStart3 + 3, 1);

      DetermStepsEmulator determStepsEmulator = new(tm.Delta, tmInstance);
      determStepsEmulator.SetupConfiguration(
        1,
        (uint)MTExtDefinitions.v2.IF_NDTM.CompareStates.SkipE);

      uint stepsNum = ((uint)frameLength * 4) + 4;
      determStepsEmulator.DoStepsN(stepsNum);

      int expectedCellIndex = frameEnd4 - 1;

      Assert.True(tmInstance.CellIndex() == expectedCellIndex);
      Assert.True(tmInstance.State() == (uint)MTExtDefinitions.v2.IF_NDTM.CompareStates.MoveToDelimiter0);
      Assert.True(tmInstance.TapeSymbol(expectedCellIndex) == OneTapeTuringMachine.blankSymbol);

      Assert.True(tmInstance.TapeSymbol(1) == MTExtDefinitions.v2.IF_NDTM.markE0);
      Assert.True(tmInstance.TapeSymbol(2) == MTExtDefinitions.v2.IF_NDTM.markE1);
      Assert.True(tmInstance.TapeSymbol(frameStart3 + 3) == MTExtDefinitions.v2.IF_NDTM.markF1);

      Assert.True(tmInstance.TapeSymbol(frameStart1) == MTExtDefinitions.v2.IF_NDTM.delimiter1);
      Assert.True(tmInstance.TapeSymbol(frameStart2) == MTExtDefinitions.v2.IF_NDTM.delimiter2);
      Assert.True(tmInstance.TapeSymbol(frameStart3) == MTExtDefinitions.v2.IF_NDTM.delimiter3);
      Assert.True(tmInstance.TapeSymbol(frameEnd4) == MTExtDefinitions.v2.IF_NDTM.delimiter4);
    }

    [Fact]
    public void T08_CompareResults5_Test()
    {
      int[] input = new int[] { 1, 0, 1, 0 }.Reverse().ToArray();
      Setup(input.Length);

      MTExtDefinitions.v2.IF_NDTM tm = new(input.Length);
      tm.Setup();

      TMInstance tmInstance = new(tm, input);
      tm.PrepareTapeFwd(input, tmInstance);

      tmInstance.SetTapeSymbol(frameStart1 + 1, 0);
      tmInstance.SetTapeSymbol(frameStart1 + 2, 1);

      tmInstance.SetTapeSymbol(frameStart2 + 1, 0);
      tmInstance.SetTapeSymbol(frameStart2 + 2, 1);

      tmInstance.SetTapeSymbol(frameStart3 + 1, MTExtDefinitions.v2.IF_NDTM.markF0);
      tmInstance.SetTapeSymbol(frameStart3 + 2, MTExtDefinitions.v2.IF_NDTM.markF1);
      tmInstance.SetTapeSymbol(frameStart3 + 3, 0);

      DetermStepsEmulator determStepsEmulator = new(tm.Delta, tmInstance);
      determStepsEmulator.SetupConfiguration(
        frameEnd4 - 1,
        (uint)MTExtDefinitions.v2.IF_NDTM.CompareStates.MoveToDelimiter0);

      uint stepsNum = ((uint)frameLength * 4) + 4;
      determStepsEmulator.DoStepsN(stepsNum);

      const int expectedCellIndex = 1;

      Assert.True(tmInstance.CellIndex() == expectedCellIndex);
      Assert.True(tmInstance.State() == (uint)MTExtDefinitions.v2.IF_NDTM.CompareStates.SkipE);
      Assert.True(tmInstance.TapeSymbol(expectedCellIndex) == 0);

      Assert.True(tmInstance.TapeSymbol(frameStart1) == MTExtDefinitions.v2.IF_NDTM.delimiter1);
      Assert.True(tmInstance.TapeSymbol(frameStart2) == MTExtDefinitions.v2.IF_NDTM.delimiter2);
      Assert.True(tmInstance.TapeSymbol(frameStart3) == MTExtDefinitions.v2.IF_NDTM.delimiter3);
      Assert.True(tmInstance.TapeSymbol(frameEnd4) == MTExtDefinitions.v2.IF_NDTM.delimiter4);
    }

    [Fact]
    public void T09_CompareResults6_Accept_Test()
    {
      int[] input = new int[] { 1, 0, 1, 0 }.Reverse().ToArray();
      Setup(input.Length);

      MTExtDefinitions.v2.IF_NDTM tm = new(input.Length);
      tm.Setup();

      TMInstance tmInstance = new(tm, input);
      tm.PrepareTapeFwd(input, tmInstance);

      tmInstance.SetTapeSymbol(1, MTExtDefinitions.v2.IF_NDTM.markE0);
      tmInstance.SetTapeSymbol(2, MTExtDefinitions.v2.IF_NDTM.markE1);
      tmInstance.SetTapeSymbol(3, MTExtDefinitions.v2.IF_NDTM.markE0);
      tmInstance.SetTapeSymbol(4, MTExtDefinitions.v2.IF_NDTM.markE1);
      tmInstance.SetTapeSymbol(5, OneTapeTuringMachine.blankSymbol);

      DetermStepsEmulator determStepsEmulator = new(tm.Delta, tmInstance);
      determStepsEmulator.SetupConfiguration(
        1,
        (uint)MTExtDefinitions.v2.IF_NDTM.CompareStates.SkipE);

      uint stepsNum = ((uint)frameLength * 4) + 4;
      determStepsEmulator.DoStepsN(stepsNum);

      int expectedCellIndex = frameEnd4 + 1;

      Assert.True(tmInstance.CellIndex() == expectedCellIndex);
      Assert.True(tmInstance.State() == MTExtDefinitions.v2.IF_NDTM.acceptingState);
      Assert.True(tmInstance.TapeSymbol(expectedCellIndex) == OneTapeTuringMachine.blankSymbol);

      Assert.True(tmInstance.TapeSymbol(1) == MTExtDefinitions.v2.IF_NDTM.markE0);
      Assert.True(tmInstance.TapeSymbol(2) == MTExtDefinitions.v2.IF_NDTM.markE1);
      Assert.True(tmInstance.TapeSymbol(3) == MTExtDefinitions.v2.IF_NDTM.markE0);
      Assert.True(tmInstance.TapeSymbol(4) == MTExtDefinitions.v2.IF_NDTM.markE1);

      Assert.True(tmInstance.TapeSymbol(frameStart1) == MTExtDefinitions.v2.IF_NDTM.delimiter1);
      Assert.True(tmInstance.TapeSymbol(frameStart2) == MTExtDefinitions.v2.IF_NDTM.delimiter2);
      Assert.True(tmInstance.TapeSymbol(frameStart3) == MTExtDefinitions.v2.IF_NDTM.delimiter3);
      Assert.True(tmInstance.TapeSymbol(frameEnd4) == MTExtDefinitions.v2.IF_NDTM.delimiter4);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
