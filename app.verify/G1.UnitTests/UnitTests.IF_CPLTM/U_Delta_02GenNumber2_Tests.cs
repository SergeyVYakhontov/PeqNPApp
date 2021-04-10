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
  public sealed class U_CPLTM_Delta_02GenNumber2_Tests : U_CPLTM_Delta_Tests_Base, IDisposable
  {
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

      MTExtDefinitions.v2.IF_NDTM tm = new(input.Length);
      tm.Setup();

      TMInstance tmInstance = new(tm, input);
      tm.PrepareTapeFwd(input, tmInstance);

      DetermStepsEmulator determStepsEmulator = new(tm.Delta, tmInstance);
      determStepsEmulator.SetupConfiguration(
        frameStart2 + 1,
        (uint)MTExtDefinitions.v2.IF_NDTM.GenNumber2States.GenBitA);

      Dictionary<int, byte> indexMap = new()
      {
        [1] = 0,
        [2] = 1,
        [3] = 0,
        [4] = 1,
        [5] = 2,
        [6] = 0,
        [7] = 0
      };

      uint stepsNum = (uint)frameLength + 1;
      determStepsEmulator.DoStepsN(stepsNum, indexMap);

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
    public void T02_GenNumber2_Delimiter4_Test()
    {
      int[] input = new int[] { 1, 0, 1, 0 }.Reverse().ToArray();
      Setup(input.Length);

      MTExtDefinitions.v2.IF_NDTM tm = new(input.Length);
      tm.Setup();

      TMInstance tmInstance = new(tm, input);
      tm.PrepareTapeFwd(input, tmInstance);

      DetermStepsEmulator determStepsEmulator = new(tm.Delta, tmInstance);
      determStepsEmulator.SetupConfiguration(
        frameStart3 + 1,
        (uint)MTExtDefinitions.v2.IF_NDTM.GenNumber2States.MoveToDelimiter4);

      uint stepsNum = (uint)frameLength + 1;
      determStepsEmulator.DoStepsN(stepsNum);

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
    public void T03_GenNumber2_Delimiter0_Test()
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

      DetermStepsEmulator determStepsEmulator = new(tm.Delta, tmInstance);
      determStepsEmulator.SetupConfiguration(
        frameEnd4 - 1,
        (uint)MTExtDefinitions.v2.IF_NDTM.GenNumber2States.MoveToDelimiter0);

      uint stepsNum = ((uint)frameLength * 4) + 4;
      determStepsEmulator.DoStepsN(stepsNum);

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
    public void T04_GenNumber2_Delimiter1_Test()
    {
      int[] input = new int[] { 1, 0, 1, 0 }.Reverse().ToArray();
      Setup(input.Length);

      MTExtDefinitions.v2.IF_NDTM tm = new(input.Length);
      tm.Setup();

      TMInstance tmInstance = new(tm, input);
      tm.PrepareTapeFwd(input, tmInstance);

      tmInstance.SetTapeSymbol(frameStart1 + 1, 0);
      tmInstance.SetTapeSymbol(frameStart1 + 2, 1);

      DetermStepsEmulator determStepsEmulator = new(tm.Delta, tmInstance);
      determStepsEmulator.SetupConfiguration(
        1,
        (uint)MTExtDefinitions.v2.IF_NDTM.GenNumber2States.MoveToDelimiter1);

      uint stepsNum = (uint)frameLength + 1;
      determStepsEmulator.DoStepsN(stepsNum);

      int expectedCellIndex = frameLength + 2;

      Assert.True(tmInstance.CellIndex() == expectedCellIndex);
      Assert.True(tmInstance.State() == (uint)MTExtDefinitions.v2.IF_NDTM.MultiplyStates.MultReady);
      Assert.True(tmInstance.TapeSymbol(expectedCellIndex) == 0);

      Assert.True(tmInstance.TapeSymbol(frameStart1) == MTExtDefinitions.v2.IF_NDTM.delimiter1);
      Assert.True(tmInstance.TapeSymbol(frameStart2) == MTExtDefinitions.v2.IF_NDTM.delimiter2);
      Assert.True(tmInstance.TapeSymbol(frameStart3) == MTExtDefinitions.v2.IF_NDTM.delimiter3);
      Assert.True(tmInstance.TapeSymbol(frameEnd4) == MTExtDefinitions.v2.IF_NDTM.delimiter4);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
