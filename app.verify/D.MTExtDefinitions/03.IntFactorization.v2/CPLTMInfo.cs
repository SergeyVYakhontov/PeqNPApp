////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using EnsureThat;
using Core;
using ExistsAcceptingPath;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace MTExtDefinitions.v2
{
  public class CPLTMInfo : ICPLTMInfo
  {
    #region Ctors

    public CPLTMInfo(int inputLength)
    {
      this.inputLength = inputLength;
    }

    #endregion

    #region public members

    public void ComputeSequences()
    {
      Setup();

      ComputeKTapeSequence();
      ComputeKTapeLRSubseq();
    }

    public uint PathLength => (uint)kTapeSequence.Count;

    public List<long> KTapeLRSubseq() => kTapeLRSubseq;

    #endregion

    #region private members

    private readonly int inputLength;
    private int[] input;

    private int frameLength;
    private int frameStart1;
    private int frameStart2;
    private int frameStart3;
    private int frameEnd4;

    private IF_NDTM tm;
    private TMInstance tmInstance;

    private readonly Dictionary<int, byte> indexMap = new Dictionary<int, byte>();
    private DetermStepsEmulator determStepsEmulator;

    private readonly List<long> kTapeSequence = new List<long>();
    private readonly List<long> kTapeLRSubseq = new List<long>();
    private readonly SortedDictionary<int, long> cellIndexes = new SortedDictionary<int, long>();

    private void Setup()
    {
      input = new int[inputLength];
      input[0] = 1;

      frameLength = IF_NDTM.FrameLength(inputLength);
      frameStart1 = IF_NDTM.FrameStart1(inputLength);
      frameStart2 = IF_NDTM.FrameStart2(inputLength);
      frameStart3 = IF_NDTM.FrameStart3(inputLength);
      frameEnd4 = IF_NDTM.FrameEnd4(inputLength);

      tm = new IF_NDTM(inputLength);
      tm.Setup();

      tmInstance = new TMInstance(tm, input);
      tm.PrepareTapeFwd(input, tmInstance);

      determStepsEmulator = new DetermStepsEmulator(tm.Delta, tmInstance);
      determStepsEmulator.SetupConfiguration(1, IF_NDTM.qStartState);
    }

    private void ComputeKTapeSequence()
    {
      indexMap[frameStart1] = 1;

      for (int i = (frameStart1 + 1); i <= (frameStart2 - 3); i++)
      {
        indexMap[i] = 0;
      }

      indexMap[frameStart2 - 2] = 2;
      indexMap[frameStart2 - 1] = 0;

      indexMap[frameStart2] = 1;

      for (int i = (frameStart2 + 1) ; i <= (frameStart3 - 3); i++)
      {
        indexMap[i] = 0;
      }

      indexMap[frameStart3 - 2] = 2;
      indexMap[frameStart3 - 1] = 0;

      determStepsEmulator.DoStepsWhile(
        indexMap,
        () => tmInstance.State() != IF_NDTM.acceptingState,
        (int stepNumber) =>
        {
          kTapeSequence.Add(stepNumber);
          cellIndexes[stepNumber] = tmInstance.CellIndex();
        });
    }

    private void ComputeKTapeLRSubseq()
    {
      const int leftmostCellIndex = 0;
      int rightmostCellIndex = frameEnd4;

      bool L = false;
      bool R = true;

      long currCellIndex = 0;
      long prevCellIndex = 0;

      foreach (KeyValuePair<int, long> stepCellPair in cellIndexes)
      {
        currCellIndex = stepCellPair.Value;

        if (R && (currCellIndex == (prevCellIndex - 1)))
        {
          Ensure.That(prevCellIndex).Is(rightmostCellIndex);

          kTapeLRSubseq.Add(stepCellPair.Key + 1);

          L = true;
          R = false;
        }
        else if (L && (currCellIndex == (prevCellIndex + 1)))
        {
          Ensure.That(prevCellIndex).Is(leftmostCellIndex);

          kTapeLRSubseq.Add(stepCellPair.Key + 1);

          L = false;
          R = true;
        }

        prevCellIndex = currCellIndex;
      }

      kTapeLRSubseq.Add(PathLength);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////