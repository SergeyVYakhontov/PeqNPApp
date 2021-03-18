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
  public sealed class U_CPLTM_Delta_02GenNumber1_Tests : U_CPLTM_Delta_Tests_Base, IDisposable
  {
    #region public members

    public void Dispose()
    {
      ResetNinjectKernel();
    }

    [Fact]
    public void T01_GenNumber1_Test()
    {
      int[] input = new int[] { 1, 0, 1, 0 }.Reverse().ToArray();
      Setup(input.Length);

      MTExtDefinitions.v2.IF_NDTM tm = new(input.Length);
      tm.Setup();

      TMInstance tmInstance = new(tm, input);
      tm.PrepareTapeFwd(input, tmInstance);

      DetermStepsEmulator determStepsEmulator = new(tm.Delta, tmInstance);
      determStepsEmulator.SetupConfiguration(
        frameStart1 + 1,
        (uint)MTExtDefinitions.v2.IF_NDTM.GenNumber1States.GenBitA);

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

      int expectedCellIndex = frameStart2 + 1;

      Assert.True(tmInstance.CellIndex() == expectedCellIndex);
      Assert.True(tmInstance.State() == (uint)MTExtDefinitions.v2.IF_NDTM.GenNumber2States.GenBitA);
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
