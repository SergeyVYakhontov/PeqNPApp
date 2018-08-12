﻿////////////////////////////////////////////////////////////////////////////////////////////////////

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

namespace MTExtDefinitions.v2
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

          (uint)InitStates.MoveToRightDelim,

          (uint)GenNumber1States.GenBitA,
          (uint)GenNumber1States.GenBitB,
          (uint)GenNumber1States.MoveToDelimiter2,
          (uint)GenNumber1States.StopGenNumber,

          (uint)GenNumber2States.GenBitA,
          (uint)GenNumber2States.GenBitB,
          (uint)GenNumber2States.MoveToDelimiter3,
          (uint)GenNumber2States.MoveToDelimiter4,
          (uint)GenNumber2States.MoveToDelimiter0,
          (uint)GenNumber2States.MoveToDelimiter1,
          (uint)GenNumber2States.StopGenNumber,

          (uint)MultiplyStates.MultReady,
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
          (uint)CompareStates.MoveToStartA,
          (uint)CompareStates.BitLoopStart,
          (uint)CompareStates.BitLoopStart_f,
          (uint)CompareStates.BitLoopD0,
          (uint)CompareStates.BitLoopD1,
          (uint)CompareStates.MoveToDelimiter3_bit0,
          (uint)CompareStates.MoveToDelimiter3_bit1,
          (uint)CompareStates.SkipF_bit0,
          (uint)CompareStates.SkipF_bit1,
          (uint)CompareStates.MoveToF_bit0,
          (uint)CompareStates.MoveToF_bit1,
          (uint)CompareStates.MoveToDelimiter4,
          (uint)CompareStates.MoveToDelimiter0,
          (uint)CompareStates.SkipE,

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
        markE0,
        markE1,
        markF0,
        markF1
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

      AppHelper.MergeDictionaryWith(delta, IF_NDTM_RSP_Init.Delta);
      AppHelper.MergeDictionaryWith(delta, IF_NDTM_RSP_GenNumber.Delta1);
      AppHelper.MergeDictionaryWith(delta, IF_NDTM_RSP_GenNumber.Delta2);
      AppHelper.MergeDictionaryWith(delta, IF_NDTM_RSP_Multiply.Delta1(frameLength));
      AppHelper.MergeDictionaryWith(delta, IF_NDTM_RSP_Multiply.Delta2(frameLength));
      AppHelper.MergeDictionaryWith(delta, IF_NDTM_RSP_Multiply.Delta3(frameLength));
      AppHelper.MergeDictionaryWith(delta, IF_NDTM_RSP_Add.Delta);
      AppHelper.MergeDictionaryWith(delta, IF_NDTM_RSP_CompareResult.Delta1);
      AppHelper.MergeDictionaryWith(delta, IF_NDTM_RSP_CompareResult.Delta2);
      AppHelper.MergeDictionaryWith(delta, IF_NDTM_RSP_CompareResult.Delta3);
      AppHelper.MergeDictionaryWith(delta, IF_NDTM_RSP_CompareResult.Delta4);
      AppHelper.MergeDictionaryWith(delta, IF_NDTM_RSP_CompareResult.Delta5);

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