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
      log.Info("ComputeSequences: setup");
      Setup();

      log.Info("ComputeSequences: ComputeKTapeSequence");
      ComputeKTapeSequence();

      log.Info("ComputeSequences: ComputeKTapeLRSubseq");
      ComputeKTapeLRSubseq();
    }

    public uint PathLength => (uint)kTapeSequence.Count;
    public SortedDictionary<int, long> CellIndexes() => cellIndexes;
    public List<long> KTapeLRSubseq() => kTapeLRSubseq;
    public int LRSubseqSegLength => frameEnd4;

    public IEnumerable<long> FwdCommsKStepSequence(long startKStep)
    {
      long from = startKStep;
      long to = (startKStep + LRSubseqSegLength - 1);

      List<long> fwdCommsKStepSequence = new List<long>();

      for (long i = from; i <= to; i++)
      {
        fwdCommsKStepSequence.Add(i);
      }

      Ensure.That(fwdCommsKStepSequence.Count % 2).Is(0);

      return fwdCommsKStepSequence;
    }

    public IEnumerable<long> BkwdCommsKStepSequence(long startKStep)
    {
      long from = (startKStep + (LRSubseqSegLength * 2));
      long to = (startKStep + LRSubseqSegLength + 1);

      List<long> bkwdCommsKStepSequence = new List<long>();

      for (long i = from; i >= to; i--)
      {
        bkwdCommsKStepSequence.Add(i);
      }

      Ensure.That(bkwdCommsKStepSequence.Count % 2).Is(0);

      return bkwdCommsKStepSequence;
    }

    #endregion

    #region private members

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
      indexMap[frameStart1 + 1] = 1;

      for (int i = (frameStart1 + 2); i <= (frameStart2 - 2); i++)
      {
        indexMap[i] = 0;
      }

      indexMap[frameStart2 - 1] = 2;
      indexMap[frameStart2] = 0;

      indexMap[frameStart2 + 1] = 1;

      for (int i = (frameStart2 + 2) ; i <= (frameStart3 - 2); i++)
      {
        indexMap[i] = 0;
      }

      indexMap[frameStart3 - 1] = 2;
      indexMap[frameStart3] = 0;

      cellIndexes[0] = 0;

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

      kTapeLRSubseq.Add(0);

      foreach (KeyValuePair<int, long> stepCellPair in cellIndexes.Skip(1))
      {
        currCellIndex = stepCellPair.Value;

        if (R && (currCellIndex == (prevCellIndex - 1)))
        {
          Ensure.That(prevCellIndex).Is(rightmostCellIndex);

          kTapeLRSubseq.Add(stepCellPair.Key - 1);

          L = true;
          R = false;
        }
        else if (L && (currCellIndex == (prevCellIndex + 1)))
        {
          Ensure.That(prevCellIndex).Is(leftmostCellIndex);

          kTapeLRSubseq.Add(stepCellPair.Key - 1);

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
