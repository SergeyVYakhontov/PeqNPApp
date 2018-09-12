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
  public sealed class U_CPLTM_Delta_Integral_03_Tests : U_CPLTM_Delta_Tests_Base, IDisposable
  {
    #region public members

    public void Dispose()
    {
      ResetNinjectKernel();
    }

    [Fact]
    public void T01_Integral_03_Mult0_Test()
    {
      int[] input = new int[] {  1, 0, 0 }.Reverse().ToArray();
      Setup(input.Length);

      MTExtDefinitions.v2.IF_NDTM tm = new MTExtDefinitions.v2.IF_NDTM(input.Length);
      tm.Setup();

      TMInstance tmInstance = new TMInstance(tm, input);
      tm.PrepareTapeFwd(input, tmInstance);

      DetermStepsEmulator determStepsEmulator = new DetermStepsEmulator(tm.Delta, tmInstance);
      determStepsEmulator.SetupConfiguration(1, tm.qStart);

      uint stepsNum = ((uint)frameLength * 52) + 4;
      determStepsEmulator.DoStepN(stepsNum, acceptingIndexMap03);

      int expectedCellIndex = frameEnd4 - 1;

      Assert.True(tmInstance.CellIndex() == expectedCellIndex);
      Assert.True(tmInstance.State() == (uint)MTExtDefinitions.v2.IF_NDTM.MultiplyStates.MoveToDelimeter0_0II);
      Assert.True(tmInstance.TapeSymbol(expectedCellIndex) == MTExtDefinitions.v2.IF_NDTM.markD0);

      Assert.True(tmInstance.TapeSymbol(frameStart1) == MTExtDefinitions.v2.IF_NDTM.delimiter1);
      Assert.True(tmInstance.TapeSymbol(frameStart2) == MTExtDefinitions.v2.IF_NDTM.delimiter2);
      Assert.True(tmInstance.TapeSymbol(frameStart3) == MTExtDefinitions.v2.IF_NDTM.delimiter3);
      Assert.True(tmInstance.TapeSymbol(frameEnd4) == MTExtDefinitions.v2.IF_NDTM.delimiter4);
    }

    [Fact]
    public void T02_Integral_03_Mult0_Rewind_Test()
    {
      int[] input = new int[] { 1, 0, 0 }.Reverse().ToArray();
      Setup(input.Length);

      MTExtDefinitions.v2.IF_NDTM tm = new MTExtDefinitions.v2.IF_NDTM(input.Length);
      tm.Setup();

      TMInstance tmInstance = new TMInstance(tm, input);
      tm.PrepareTapeFwd(input, tmInstance);

      DetermStepsEmulator determStepsEmulator = new DetermStepsEmulator(tm.Delta, tmInstance);
      determStepsEmulator.SetupConfiguration(1, tm.qStart);

      uint stepsNum = ((uint)frameLength * 68) + 4;
      determStepsEmulator.DoStepN(stepsNum, acceptingIndexMap03);

      int expectedCellIndex = frameStart1 + 3;

      Assert.True(tmInstance.CellIndex() == expectedCellIndex);
      Assert.True(tmInstance.State() == (uint)MTExtDefinitions.v2.IF_NDTM.MultiplyStates.MoveToDelimeter2_1I);
      Assert.True(tmInstance.TapeSymbol(expectedCellIndex) == OneTapeTuringMachine.blankSymbol);

      Assert.True(tmInstance.TapeSymbol(frameStart1) == MTExtDefinitions.v2.IF_NDTM.delimiter1);
      Assert.True(tmInstance.TapeSymbol(frameStart2) == MTExtDefinitions.v2.IF_NDTM.delimiter2);
      Assert.True(tmInstance.TapeSymbol(frameStart3) == MTExtDefinitions.v2.IF_NDTM.delimiter3);
      Assert.True(tmInstance.TapeSymbol(frameEnd4) == MTExtDefinitions.v2.IF_NDTM.delimiter4);
    }

    [Fact]
    public void T03_Integral_03_Mult1_Test()
    {
      int[] input = new int[] { 1, 0, 0 }.Reverse().ToArray();
      Setup(input.Length);

      MTExtDefinitions.v2.IF_NDTM tm = new MTExtDefinitions.v2.IF_NDTM(input.Length);
      tm.Setup();

      TMInstance tmInstance = new TMInstance(tm, input);
      tm.PrepareTapeFwd(input, tmInstance);

      DetermStepsEmulator determStepsEmulator = new DetermStepsEmulator(tm.Delta, tmInstance);
      determStepsEmulator.SetupConfiguration(1, tm.qStart);

      uint stepsNum = ((uint)frameLength * 126) + 3;
      determStepsEmulator.DoStepN(stepsNum, acceptingIndexMap03);

      int expectedCellIndex = frameStart1 + 4;

      Assert.True(tmInstance.CellIndex() == expectedCellIndex);
      Assert.True(tmInstance.State() == (uint)MTExtDefinitions.v2.IF_NDTM.MultiplyStates.MoveToDelimeter2_0I);
      Assert.True(tmInstance.TapeSymbol(expectedCellIndex) == OneTapeTuringMachine.blankSymbol);

      Assert.True(tmInstance.TapeSymbol(frameStart1) == MTExtDefinitions.v2.IF_NDTM.delimiter1);
      Assert.True(tmInstance.TapeSymbol(frameStart2) == MTExtDefinitions.v2.IF_NDTM.delimiter2);
      Assert.True(tmInstance.TapeSymbol(frameStart3) == MTExtDefinitions.v2.IF_NDTM.delimiter3);
      Assert.True(tmInstance.TapeSymbol(frameEnd4) == MTExtDefinitions.v2.IF_NDTM.delimiter4);
    }

    [Fact]
    public void T04_Integral_03_MultToCompare_Test()
    {
      int[] input = new int[] { 1, 0, 0 }.Reverse().ToArray();
      Setup(input.Length);

      MTExtDefinitions.v2.IF_NDTM tm = new MTExtDefinitions.v2.IF_NDTM(input.Length);
      tm.Setup();

      TMInstance tmInstance = new TMInstance(tm, input);
      tm.PrepareTapeFwd(input, tmInstance);

      DetermStepsEmulator determStepsEmulator = new DetermStepsEmulator(tm.Delta, tmInstance);
      determStepsEmulator.SetupConfiguration(1, tm.qStart);

      uint stepsNum = ((uint)frameLength * 304) + 4;
      determStepsEmulator.DoStepN(stepsNum, acceptingIndexMap03);

      int expectedCellIndex = frameStart2 - 1;

      Assert.True(tmInstance.CellIndex() == expectedCellIndex);
      Assert.True(tmInstance.State() == (uint)MTExtDefinitions.v2.IF_NDTM.CompareStates.StartComparing);
      Assert.True(tmInstance.TapeSymbol(expectedCellIndex) == MTExtDefinitions.v2.IF_NDTM.markB0);

      Assert.True(tmInstance.TapeSymbol(frameStart1) == MTExtDefinitions.v2.IF_NDTM.delimiter1);
      Assert.True(tmInstance.TapeSymbol(frameStart2) == MTExtDefinitions.v2.IF_NDTM.delimiter2);
      Assert.True(tmInstance.TapeSymbol(frameStart3) == MTExtDefinitions.v2.IF_NDTM.delimiter3);
      Assert.True(tmInstance.TapeSymbol(frameEnd4) == MTExtDefinitions.v2.IF_NDTM.delimiter4);
    }

    [Fact]
    public void T05_Integral_03_Accepting_Test()
    {
      int[] input = new int[] { 1, 0, 0 }.Reverse().ToArray();
      Setup(input.Length);

      MTExtDefinitions.v2.IF_NDTM tm = new MTExtDefinitions.v2.IF_NDTM(input.Length);
      tm.Setup();

      TMInstance tmInstance = new TMInstance(tm, input);
      tm.PrepareTapeFwd(input, tmInstance);

      DetermStepsEmulator determStepsEmulator = new DetermStepsEmulator(tm.Delta, tmInstance);
      determStepsEmulator.SetupConfiguration(1, tm.qStart);

      uint stepsNum = ((uint)frameLength * 336) + 4;
      determStepsEmulator.DoStepN(stepsNum, acceptingIndexMap03);

      const int expectedCellIndex = 5;

      Assert.True(tmInstance.CellIndex() == expectedCellIndex);
      Assert.True(tmInstance.State() == MTExtDefinitions.v2.IF_NDTM.acceptingState);
      Assert.True(tmInstance.TapeSymbol(expectedCellIndex) == OneTapeTuringMachine.blankSymbol);

      Assert.True(tmInstance.TapeSymbol(frameStart1) == MTExtDefinitions.v2.IF_NDTM.delimiter1);
      Assert.True(tmInstance.TapeSymbol(frameStart2) == MTExtDefinitions.v2.IF_NDTM.delimiter2);
      Assert.True(tmInstance.TapeSymbol(frameStart3) == MTExtDefinitions.v2.IF_NDTM.delimiter3);
      Assert.True(tmInstance.TapeSymbol(frameEnd4) == MTExtDefinitions.v2.IF_NDTM.delimiter4);
    }

    [Fact]
    public void T06_Integral_03_Rejecting_Test()
    {
      int[] input = new int[] { 1, 0, 0 }.Reverse().ToArray();
      Setup(input.Length);

      MTExtDefinitions.v2.IF_NDTM tm = new MTExtDefinitions.v2.IF_NDTM(input.Length);
      tm.Setup();

      TMInstance tmInstance = new TMInstance(tm, input);
      tm.PrepareTapeFwd(input, tmInstance);

      DetermStepsEmulator determStepsEmulator = new DetermStepsEmulator(tm.Delta, tmInstance);
      determStepsEmulator.SetupConfiguration(1, tm.qStart);

      uint stepsNum = ((uint)frameLength * 320) + 4;
      determStepsEmulator.DoStepN(stepsNum, rejectingIndexMap03);

      int expectedCellIndex = frameStart3 + 3;

      Assert.True(tmInstance.CellIndex() == expectedCellIndex);
      Assert.True(tmInstance.State() == MTExtDefinitions.v2.IF_NDTM.rejectingState);
      Assert.True(tmInstance.TapeSymbol(expectedCellIndex) == 1);

      Assert.True(tmInstance.TapeSymbol(frameStart1) == MTExtDefinitions.v2.IF_NDTM.delimiter1);
      Assert.True(tmInstance.TapeSymbol(frameStart2) == MTExtDefinitions.v2.IF_NDTM.delimiter2);
      Assert.True(tmInstance.TapeSymbol(frameStart3) == MTExtDefinitions.v2.IF_NDTM.delimiter3);
      Assert.True(tmInstance.TapeSymbol(frameEnd4) == MTExtDefinitions.v2.IF_NDTM.delimiter4);
    }

    [Fact]
    public void T07_Integral_06_Accepting_Test()
    {
      int[] input = new int[] { 1, 1, 0 }.Reverse().ToArray();
      Setup(input.Length);

      MTExtDefinitions.v2.IF_NDTM tm = new MTExtDefinitions.v2.IF_NDTM(input.Length);
      tm.Setup();

      TMInstance tmInstance = new TMInstance(tm, input);
      tm.PrepareTapeFwd(input, tmInstance);

      DetermStepsEmulator determStepsEmulator = new DetermStepsEmulator(tm.Delta, tmInstance);
      determStepsEmulator.SetupConfiguration(1, tm.qStart);

      uint stepsNum = ((uint)frameLength * 336) + 4;
      determStepsEmulator.DoStepN(stepsNum, acceptingIndexMap06);

      const int expectedCellIndex = 5;

      Assert.True(tmInstance.CellIndex() == expectedCellIndex);
      Assert.True(tmInstance.State() == MTExtDefinitions.v2.IF_NDTM.acceptingState);
      Assert.True(tmInstance.TapeSymbol(expectedCellIndex) == OneTapeTuringMachine.blankSymbol);

      Assert.True(tmInstance.TapeSymbol(frameStart1) == MTExtDefinitions.v2.IF_NDTM.delimiter1);
      Assert.True(tmInstance.TapeSymbol(frameStart2) == MTExtDefinitions.v2.IF_NDTM.delimiter2);
      Assert.True(tmInstance.TapeSymbol(frameStart3) == MTExtDefinitions.v2.IF_NDTM.delimiter3);
      Assert.True(tmInstance.TapeSymbol(frameEnd4) == MTExtDefinitions.v2.IF_NDTM.delimiter4);
    }

    #endregion

    #region private members

    private static readonly IReadOnlyDictionary<int, byte> acceptingIndexMap03 =
      new Dictionary<int, byte>
      {
        [6] = 0,
        [7] = 1,
        [8] = 2,

        [11] = 0,

        [12] = 0,
        [13] = 1,
        [14] = 2
      };

    private static readonly IReadOnlyDictionary<int, byte> rejectingIndexMap03 =
      new Dictionary<int, byte>
      {
        [6] = 1,
        [7] = 1,
        [8] = 2,

        [11] = 0,

        [12] = 0,
        [13] = 1,
        [14] = 2
      };

    private static readonly IReadOnlyDictionary<int, byte> acceptingIndexMap06 =
      new Dictionary<int, byte>
      {
        [6] = 1,
        [7] = 1,
        [8] = 2,

        [11] = 0,

        [12] = 0,
        [13] = 1,
        [14] = 2
      };

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
