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
  public sealed class U_CPLTM_Delta_03Multiply_12_14_III_Tests : U_CPLTM_Delta_Tests_Base, IDisposable
  {
    #region public members

    public void Dispose()
    {
      ResetNinjectKernel();
    }

    [Fact]
    public void T01_Multiply_10_14_III_Delimiter4_D2D3_Test()
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
        (uint)MTExtDefinitions.v2.IF_NDTM.MultiplyStates.MoveToDelimeter4_III);

      tmInstance.SetTapeSymbol(frameStart3 + 1, MTExtDefinitions.v2.IF_NDTM.markD2);
      tmInstance.SetTapeSymbol(frameStart3 + 2, MTExtDefinitions.v2.IF_NDTM.markD3);
      tmInstance.SetTapeSymbol(frameStart3 + 3, 0);
      tmInstance.SetTapeSymbol(frameStart3 + 4, 1);

      uint stepsNum = (uint)frameLength + 1;
      determStepsEmulator.DoStepN(stepsNum);

      int expectedCellIndex = frameEnd4 - 1;

      Assert.True(tmInstance.CellIndex() == expectedCellIndex);
      Assert.True(tmInstance.State() == (uint)MTExtDefinitions.v2.IF_NDTM.MultiplyStates.MoveToDelimeter0_IV);
      Assert.True(tmInstance.TapeSymbol(expectedCellIndex) == OneTapeTuringMachine.blankSymbol);

      Assert.True(tmInstance.TapeSymbol(frameStart3 + 1) == MTExtDefinitions.v2.IF_NDTM.markD0);
      Assert.True(tmInstance.TapeSymbol(frameStart3 + 2) == MTExtDefinitions.v2.IF_NDTM.markD1);

      Assert.True(tmInstance.TapeSymbol(frameStart1) == MTExtDefinitions.v2.IF_NDTM.delimiter1);
      Assert.True(tmInstance.TapeSymbol(frameStart2) == MTExtDefinitions.v2.IF_NDTM.delimiter2);
      Assert.True(tmInstance.TapeSymbol(frameStart3) == MTExtDefinitions.v2.IF_NDTM.delimiter3);
      Assert.True(tmInstance.TapeSymbol(frameEnd4) == MTExtDefinitions.v2.IF_NDTM.delimiter4);
    }

    [Fact]
    public void T01_Multiply_10_14_III_Delimiter4_D0D1_Test()
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
        (uint)MTExtDefinitions.v2.IF_NDTM.MultiplyStates.MoveToDelimeter4_III);

      tmInstance.SetTapeSymbol(frameStart3 + 1, MTExtDefinitions.v2.IF_NDTM.markD0);
      tmInstance.SetTapeSymbol(frameStart3 + 2, MTExtDefinitions.v2.IF_NDTM.markD1);
      tmInstance.SetTapeSymbol(frameStart3 + 3, 0);
      tmInstance.SetTapeSymbol(frameStart3 + 4, 1);

      uint stepsNum = (uint)frameLength + 1;
      determStepsEmulator.DoStepN(stepsNum);

      int expectedCellIndex = frameEnd4 - 1;

      Assert.True(tmInstance.CellIndex() == expectedCellIndex);
      Assert.True(tmInstance.State() == (uint)MTExtDefinitions.v2.IF_NDTM.MultiplyStates.MoveToDelimeter0_IV);
      Assert.True(tmInstance.TapeSymbol(expectedCellIndex) == OneTapeTuringMachine.blankSymbol);

      Assert.True(tmInstance.TapeSymbol(frameStart3 + 1) == MTExtDefinitions.v2.IF_NDTM.markD0);
      Assert.True(tmInstance.TapeSymbol(frameStart3 + 2) == MTExtDefinitions.v2.IF_NDTM.markD1);

      Assert.True(tmInstance.TapeSymbol(frameStart1) == MTExtDefinitions.v2.IF_NDTM.delimiter1);
      Assert.True(tmInstance.TapeSymbol(frameStart2) == MTExtDefinitions.v2.IF_NDTM.delimiter2);
      Assert.True(tmInstance.TapeSymbol(frameStart3) == MTExtDefinitions.v2.IF_NDTM.delimiter3);
      Assert.True(tmInstance.TapeSymbol(frameEnd4) == MTExtDefinitions.v2.IF_NDTM.delimiter4);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
