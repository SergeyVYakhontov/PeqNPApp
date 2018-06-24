////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;
using Ninject;
using ExistsAcceptingPath;

////////////////////////////////////////////////////////////////////////////////////////////////////
// IF_NDTM: Turing machine for integer factorization
////////////////////////////////////////////////////////////////////////////////////////////////////

namespace MTExtDefinitions
{
  public partial class IF_NDTM : OneTapeNDTM
  {
    #region Ctors

    public IF_NDTM(int inputLength)
      : base("NDTM")
    {
      this.inputLength = inputLength;
    }

    #endregion

    #region public members

    public override void Setup()
    {
      Q = new int[]
        {
          qStart,

          (int)SubprogStates.InitReady,
          (int)SubprogStates.GenNumber1Ready,
          (int)SubprogStates.GenNumber2Ready,
          (int)SubprogStates.MultReady,
          (int)SubprogStates.CompareReady,

          (int)InitStates.SetLeftDelim,
          (int)InitStates.InitB,
          (int)InitStates.InitC,
          (int)InitStates.InitD,
          (int)InitStates.StopInit,

          (int)GenNumber1States.GenBit0,
          (int)GenNumber1States.GenBit1,
          (int)GenNumber1States.GenBit,
          (int)GenNumber1States.MoveToDelimiter,
          (int)GenNumber1States.StopGenNumber,

          (int)GenNumber2States.GenBit0,
          (int)GenNumber2States.GenBit1,
          (int)GenNumber2States.GenBit,
          (int)GenNumber2States.MoveToDelimiter,
          (int)GenNumber2States.StopGenNumber,

          (int)MultiplyStates.StartLoopInC,
          (int)MultiplyStates.Process1f_D,
          (int)MultiplyStates.MoveToCRight,
          (int)MultiplyStates.EraseMarkInC,
          (int)MultiplyStates.StartAddC,
          (int)MultiplyStates.AddC0f_D,
          (int)MultiplyStates.AddC0f_sm_D,
          (int)MultiplyStates.MoveToCLeft,
          (int)MultiplyStates.SetMarkInC,
          (int)MultiplyStates.MoveToMarkInB,
          (int)MultiplyStates.MoveToMarkInB_inB,
          (int)MultiplyStates.AddC1f_D,
          (int)MultiplyStates.AddC1f_sm_D,
          (int)MultiplyStates.MoveToMarkInD_L,
          (int)MultiplyStates.MoveToMarkInD_R,
          (int)MultiplyStates.MoveMarkInD,
          (int)MultiplyStates.StopMultiplying,

          (int)AddStates.StartAdding,
          (int)AddStates.AddBitC0,
          (int)AddStates.AddBitC1,

          (int)CompareStates.StartComparing,
          (int)CompareStates.MoveLeftToA,
          (int)CompareStates.MoveToStartA,
          (int)CompareStates.BitLoopStart,
          (int)CompareStates.BitLoopStart_f,
          (int)CompareStates.BitLoopD0,
          (int)CompareStates.BitLoopD1,

          (int)BkwdStates.Bkwd1,

          acceptingState,
          rejectingState
        };

      Gamma = new int[]
      {
        OneTapeTuringMachine.blankSymbol,
        0,
        1,
        delimiter,
        markB0,
        markB1,
        markC0,
        markC1,
        markD0,
        markD1,
        bkwd1,
        bkwd2
      };

      Sigma = new int[]
      {
        OneTapeTuringMachine.blankSymbol,
        0,
        1,
        delimiter
      };

      Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> delta;
      IDebugOptions debugOptions = configuration.Get<IDebugOptions>();

      int frameLength = FrameLength(inputLength);

      if (debugOptions.IntFactTestRules)
      {
        delta = deltaSubprog1Test(frameLength);
      }
      else
      {
        delta = deltaSubprog1Std();
      }

      AppHelper.MergeDictionaryWith(delta, deltaSubprog2(frameLength));
      AppHelper.MergeDictionaryWith(delta, deltaInit(frameLength));
      AppHelper.MergeDictionaryWith(delta, deltaGenNumber1());
      AppHelper.MergeDictionaryWith(delta, deltaGenNumber2());
      AppHelper.MergeDictionaryWith(delta, deltaMultiply1(frameLength));
      AppHelper.MergeDictionaryWith(delta, deltaMultiply2(frameLength));
      AppHelper.MergeDictionaryWith(delta, deltaMultiply3(frameLength));
      AppHelper.MergeDictionaryWith(delta, deltaAdd());
      AppHelper.MergeDictionaryWith(delta, deltaCompare(frameLength));
      AppHelper.MergeDictionaryWith(delta, deltaBkwd(frameLength));

      Delta = delta;

      qStart = qStartState;
      F = new int[1] { acceptingState };

      CheckDeltaRelation();
    }

    public override bool UP => true;
    public override bool FewP => false;
    public override bool LotOfAcceptingPaths => false;
    public override bool AcceptingPathAlwaysExists => true;
    public override bool AllPathsFinite => false;

    public override long GetLTapeBound(long mu, long n) => 0;

    public override long GetRTapeBound(long mu, long n)
    {
      return (5 + (FrameLength((int)n) * 4));
    }

    public override long ExpectedPathLength(long n)
    {
      return (5 * (n * n));
    }

    public override int[] GetOutput(TMInstance tmInstance, long mu, long n)
    {
      return tmInstance.GetTapeSubstr(GetLTapeBound(mu, n), GetRTapeBound(mu, n));
    }

    public static int FactorDelimiter => delimiter;

    #endregion

    #region private members

    private static readonly IKernel configuration = Core.AppContext.Configuration;

    private readonly int inputLength;
    private static int FrameLength(int inputLength) => (inputLength + 2);

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
