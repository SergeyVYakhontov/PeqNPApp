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
      VerifyKTapeSequence();

      ComputeKTapeLRSubseq();
    }

    public uint PathLength => (uint)kTapeSequence.Count;

    public List<int> KTapeLRSubseq() => kTapeLRSubseq;

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

    private readonly List<int> kTapeSequence = new List<int>();
    private readonly List<int> kTapeLRSubseq = new List<int>();

    private void Setup()
    {
      input = new int[inputLength];

      frameLength = IF_NDTM.FrameLength(inputLength);
      frameStart1 = IF_NDTM.FrameStart1(inputLength);
      frameStart2 = IF_NDTM.FrameStart2(inputLength);
      frameStart3 = IF_NDTM.FrameStart3(inputLength);
      frameEnd4 = IF_NDTM.FrameEnd4(inputLength);

      tm = new IF_NDTM(inputLength);
      tm.Setup();

      tmInstance = new TMInstance(tm, input);
      tm.PrepareTapeFwd(input, tmInstance);

      DetermStepsEmulator determStepsEmulator = new DetermStepsEmulator(tm.Delta, tmInstance);
      determStepsEmulator.SetupConfiguration(0, IF_NDTM.qStartState);
    }

    public void ComputeKTapeSequence()
    {
      for (int i = (frameStart1 + 1); i <= (frameStart2 - 1); i++)
      {
        indexMap[i] = 0;
      }

      for (int i = (frameStart2 + 1); i <= (frameStart3 - 1); i++)
      {
        indexMap[i] = 0;
      }

      for (int i = 0; i < 500; i++)
      {
        kTapeSequence.Add(i);
      }
    }

    public void ComputeKTapeLRSubseq()
    {
    }

    private void VerifyKTapeSequence()
    {
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
