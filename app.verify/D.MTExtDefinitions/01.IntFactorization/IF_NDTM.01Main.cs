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

    public IF_NDTM(int inputLength) : base("NDTM")
    {
      this.inputLength = inputLength;
    }

    #endregion

    #region public members

    public override void Setup()
    {
      Q = new uint[]
        {
          qStart,

          (uint)SubprogStates.GenNumber2Ready,
          (uint)SubprogStates.MultReady,
          (uint)SubprogStates.CompareReady,

          (uint)InitStates.MoveToRightDelim,

          (uint)GenNumber1States.GenBitA,
          (uint)GenNumber1States.GenBitB,
          (uint)GenNumber1States.MoveToDelimiter,
          (uint)GenNumber1States.StopGenNumber,

          (uint)GenNumber2States.GenBitA,
          (uint)GenNumber2States.GenBitB,
          (uint)GenNumber2States.MoveToDelimiter,
          (uint)GenNumber2States.StopGenNumber,

          (uint)MultiplyStates.StartLoopInC,
          (uint)MultiplyStates.Process1f_D,
          (uint)MultiplyStates.MoveToCRight,
          (uint)MultiplyStates.EraseMarkInC,
          (uint)MultiplyStates.StartAddC,
          (uint)MultiplyStates.AddC0f_D,
          (uint)MultiplyStates.AddC0f_sm_D,
          (uint)MultiplyStates.MoveToCLeft,
          (uint)MultiplyStates.SetMarkInC,
          (uint)MultiplyStates.MoveToMarkInB,
          (uint)MultiplyStates.MoveToMarkInB_inB,
          (uint)MultiplyStates.AddC1f_D,
          (uint)MultiplyStates.AddC1f_sm_D,
          (uint)MultiplyStates.MoveToMarkInD_L,
          (uint)MultiplyStates.MoveToMarkInD_R,
          (uint)MultiplyStates.MoveMarkInD,
          (uint)MultiplyStates.StopMultiplying,

          (uint)AddStates.StartAdding,
          (uint)AddStates.AddBitC0,
          (uint)AddStates.AddBitC1,

          (uint)CompareStates.StartComparing,
          (uint)CompareStates.MoveLeftToA,
          (uint)CompareStates.MoveToStartA,
          (uint)CompareStates.BitLoopStart,
          (uint)CompareStates.BitLoopStart_f,
          (uint)CompareStates.BitLoopD0,
          (uint)CompareStates.BitLoopD1,

          (uint)BkwdStates.Bkwd1,

          acceptingState,
          rejectingState
        };

      Gamma = new int[]
      {
        blankSymbol,
        0,
        1,
        delimiter0,
        delimiter1,
        delimiter2,
        delimiter3,
        delimiter4,
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
        blankSymbol,
        0,
        1,
        delimiter0,
        delimiter1,
        delimiter2,
        delimiter3,
        delimiter4
      };

      Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> delta;
      IDebugOptions debugOptions = configuration.Get<IDebugOptions>();

      int frameLength = FrameLength(inputLength);
      delta = new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>();

      if (debugOptions.IntFactTestRules)
      {
        AppHelper.MergeDictionaryWith(delta, deltaSubprog1Test(frameLength));
      }

      AppHelper.MergeDictionaryWith(delta, deltaSubprog2(frameLength));
      AppHelper.MergeDictionaryWith(delta, deltaInit());
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
      F = new uint[] { acceptingState };

      CheckDeltaRelation();
    }

    public override bool UP => true;
    public override bool FewP => false;
    public override bool LotOfAcceptingPaths => false;
    public override bool AcceptingPathAlwaysExists => true;
    public override bool AllPathsFinite => false;

    public override long GetLTapeBound(ulong mu, ulong n) => 0;

    public override long GetRTapeBound(ulong mu, ulong n)
    {
      return 5 + (FrameLength((int)n) * 4);
    }

    public override ulong ExpectedPathLength(ulong n)
    {
      return 5 * (n * n);
    }

    public override int[] GetOutput(TMInstance tmInstance, ulong mu, ulong n)
    {
      return tmInstance.GetTapeSubstr(GetLTapeBound(mu, n), GetRTapeBound(mu, n));
    }

    #endregion

    #region private members

    private static readonly IKernel configuration = Core.AppContext.Configuration;

    private readonly int inputLength;
    private static int FrameLength(int inputLength) => inputLength + 2;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
