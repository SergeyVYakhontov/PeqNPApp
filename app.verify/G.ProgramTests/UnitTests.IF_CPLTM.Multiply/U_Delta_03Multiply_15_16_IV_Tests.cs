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
  public sealed class U_CPLTM_Delta_03Multiply_15_16_IV_Tests : U_CPLTM_Delta_Tests_Base, IDisposable
  {
    #region public members

    public void Dispose()
    {
      ResetNinjectKernel();
    }

    [Fact]
    public void T01_Multiply_15_16_IV_BProcessed_Test()
    {
      int[] input = new int[] { 1, 0, 1, 0 }.Reverse().ToArray();
      Setup(input.Length);

      MTExtDefinitions.v2.IF_NDTM tm = new(input.Length);
      tm.Setup();

      TMInstance tmInstance = new(tm, input);
      tm.PrepareTapeFwd(input, tmInstance);

      DetermStepsEmulator determStepsEmulator = new(tm.Delta, tmInstance);
      determStepsEmulator.SetupConfiguration(
        frameStart2,
        (uint)MTExtDefinitions.v2.IF_NDTM.MultiplyStates.MoveTo01InB_III);

      tmInstance.SetTapeSymbol(frameStart2 + 1, 0);
      tmInstance.SetTapeSymbol(frameStart2 + 2, 1);

      const uint stepsNum = 1;
      determStepsEmulator.DoStepsN(stepsNum);

      int expectedCellIndex = frameStart2 + 1;

      Assert.True(tmInstance.CellIndex() == expectedCellIndex);
      Assert.True(tmInstance.State() == (uint)MTExtDefinitions.v2.IF_NDTM.MultiplyStates.MoveToDelimeter4_IV);
      Assert.True(tmInstance.TapeSymbol(expectedCellIndex) == 0);

      Assert.True(tmInstance.TapeSymbol(frameStart1) == MTExtDefinitions.v2.IF_NDTM.delimiter1);
      Assert.True(tmInstance.TapeSymbol(frameStart2) == MTExtDefinitions.v2.IF_NDTM.delimiter2);
      Assert.True(tmInstance.TapeSymbol(frameStart3) == MTExtDefinitions.v2.IF_NDTM.delimiter3);
      Assert.True(tmInstance.TapeSymbol(frameEnd4) == MTExtDefinitions.v2.IF_NDTM.delimiter4);
    }

    [Fact]
    public void T02_Multiply_15_16_IV_Delimiter4_D0D1_Test()
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
        (uint)MTExtDefinitions.v2.IF_NDTM.MultiplyStates.MoveToDelimeter4_IV);

      tmInstance.SetTapeSymbol(frameStart2 + 1, 0);
      tmInstance.SetTapeSymbol(frameStart2 + 2, 1);

      tmInstance.SetTapeSymbol(frameStart3 + 1, MTExtDefinitions.v2.IF_NDTM.markD0);
      tmInstance.SetTapeSymbol(frameStart3 + 2, MTExtDefinitions.v2.IF_NDTM.markD1);
      tmInstance.SetTapeSymbol(frameStart3 + 3, 0);
      tmInstance.SetTapeSymbol(frameStart3 + 4, 1);

      uint stepsNum = ((uint)frameLength * 2) + 2;
      determStepsEmulator.DoStepsN(stepsNum);

      int expectedCellIndex = frameEnd4 - 1;

      Assert.True(tmInstance.CellIndex() == expectedCellIndex);
      Assert.True(tmInstance.State() == (uint)MTExtDefinitions.v2.IF_NDTM.MultiplyStates.MoveToDelimeter0_IV);
      Assert.True(tmInstance.TapeSymbol(expectedCellIndex) == OneTapeTuringMachine.blankSymbol);

      Assert.True(tmInstance.TapeSymbol(frameStart3 + 1) == 0);
      Assert.True(tmInstance.TapeSymbol(frameStart3 + 2) == 1);

      Assert.True(tmInstance.TapeSymbol(frameStart1) == MTExtDefinitions.v2.IF_NDTM.delimiter1);
      Assert.True(tmInstance.TapeSymbol(frameStart2) == MTExtDefinitions.v2.IF_NDTM.delimiter2);
      Assert.True(tmInstance.TapeSymbol(frameStart3) == MTExtDefinitions.v2.IF_NDTM.delimiter3);
      Assert.True(tmInstance.TapeSymbol(frameEnd4) == MTExtDefinitions.v2.IF_NDTM.delimiter4);
    }

    [Fact]
    public void T03_Multiply_15_16_IV_Delimiter4_D2D3_Test()
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
        (uint)MTExtDefinitions.v2.IF_NDTM.MultiplyStates.MoveToDelimeter4_IV);

      tmInstance.SetTapeSymbol(frameStart2 + 1, 0);
      tmInstance.SetTapeSymbol(frameStart2 + 2, 1);

      tmInstance.SetTapeSymbol(frameStart3 + 1, MTExtDefinitions.v2.IF_NDTM.markD2);
      tmInstance.SetTapeSymbol(frameStart3 + 2, MTExtDefinitions.v2.IF_NDTM.markD3);
      tmInstance.SetTapeSymbol(frameStart3 + 3, 0);
      tmInstance.SetTapeSymbol(frameStart3 + 4, 1);

      uint stepsNum = ((uint)frameLength * 2) + 2;
      determStepsEmulator.DoStepsN(stepsNum);

      int expectedCellIndex = frameEnd4 - 1;

      Assert.True(tmInstance.CellIndex() == expectedCellIndex);
      Assert.True(tmInstance.State() == (uint)MTExtDefinitions.v2.IF_NDTM.MultiplyStates.MoveToDelimeter0_IV);
      Assert.True(tmInstance.TapeSymbol(expectedCellIndex) == OneTapeTuringMachine.blankSymbol);

      Assert.True(tmInstance.TapeSymbol(frameStart3 + 1) == 0);
      Assert.True(tmInstance.TapeSymbol(frameStart3 + 2) == 1);

      Assert.True(tmInstance.TapeSymbol(frameStart1) == MTExtDefinitions.v2.IF_NDTM.delimiter1);
      Assert.True(tmInstance.TapeSymbol(frameStart2) == MTExtDefinitions.v2.IF_NDTM.delimiter2);
      Assert.True(tmInstance.TapeSymbol(frameStart3) == MTExtDefinitions.v2.IF_NDTM.delimiter3);
      Assert.True(tmInstance.TapeSymbol(frameEnd4) == MTExtDefinitions.v2.IF_NDTM.delimiter4);
    }

    [Fact]
    public void T04_Multiply_15_16_IV_Delimiter0_Test()
    {
      int[] input = new int[] { 1, 0, 1, 0 }.Reverse().ToArray();
      Setup(input.Length);

      MTExtDefinitions.v2.IF_NDTM tm = new(input.Length);
      tm.Setup();

      TMInstance tmInstance = new(tm, input);
      tm.PrepareTapeFwd(input, tmInstance);

      DetermStepsEmulator determStepsEmulator = new(tm.Delta, tmInstance);
      determStepsEmulator.SetupConfiguration(
        frameEnd4 - 1,
        (uint)MTExtDefinitions.v2.IF_NDTM.MultiplyStates.MoveToDelimeter0_IV);

      tmInstance.SetTapeSymbol(frameStart1 + 1, MTExtDefinitions.v2.IF_NDTM.markB0);
      tmInstance.SetTapeSymbol(frameStart1 + 2, MTExtDefinitions.v2.IF_NDTM.markB1);
      tmInstance.SetTapeSymbol(frameStart1 + 3, 0);
      tmInstance.SetTapeSymbol(frameStart1 + 4, 1);

      tmInstance.SetTapeSymbol(frameStart2 + 1, 0);
      tmInstance.SetTapeSymbol(frameStart2 + 2, 1);
      tmInstance.SetTapeSymbol(frameStart2 + 3, 0);
      tmInstance.SetTapeSymbol(frameStart2 + 4, 1);

      tmInstance.SetTapeSymbol(frameStart3 + 1, 0);
      tmInstance.SetTapeSymbol(frameStart3 + 2, 1);
      tmInstance.SetTapeSymbol(frameStart3 + 3, 0);
      tmInstance.SetTapeSymbol(frameStart3 + 4, 1);

      uint stepsNum = ((uint)frameLength * 2) + 2;
      determStepsEmulator.DoStepsN(stepsNum);

      int expectedCellIndex = frameStart2 - 1;

      Assert.True(tmInstance.CellIndex() == expectedCellIndex);
      Assert.True(tmInstance.State() == (uint)MTExtDefinitions.v2.IF_NDTM.CompareStates.StartComparing);
      Assert.True(tmInstance.TapeSymbol(expectedCellIndex) == OneTapeTuringMachine.blankSymbol);

      Assert.True(tmInstance.TapeSymbol(frameStart1) == MTExtDefinitions.v2.IF_NDTM.delimiter1);
      Assert.True(tmInstance.TapeSymbol(frameStart2) == MTExtDefinitions.v2.IF_NDTM.delimiter2);
      Assert.True(tmInstance.TapeSymbol(frameStart3) == MTExtDefinitions.v2.IF_NDTM.delimiter3);
      Assert.True(tmInstance.TapeSymbol(frameEnd4) == MTExtDefinitions.v2.IF_NDTM.delimiter4);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
