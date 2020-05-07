////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Ninject;
using Core;
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
      this.configuration = Core.AppContext.GetConfiguration();

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

          (uint)GenNumber2States.GenBitA,
          (uint)GenNumber2States.GenBitB,
          (uint)GenNumber2States.MoveToDelimiter3,
          (uint)GenNumber2States.MoveToDelimiter4,
          (uint)GenNumber2States.MoveToDelimiter0,
          (uint)GenNumber2States.MoveToDelimiter1,

          (uint)MultiplyStates.MultReady,
          (uint)MultiplyStates.MoveToDelimeter2_0I,
          (uint)MultiplyStates.MoveTo01InC_0I,
          (uint)MultiplyStates.MoveToDelimeter3_0II,
          (uint)MultiplyStates.MoveTo01InD_0II,
          (uint)MultiplyStates.MoveToDelimeter4_0II,
          (uint)MultiplyStates.MoveToDelimeter0_0II,
          (uint)MultiplyStates.MoveToDelimeter2_0II,
          (uint)MultiplyStates.MoveToDelimeter2_1I,
          (uint)MultiplyStates.MoveTo01InC_1I,
          (uint)MultiplyStates.MoveToDelimeter3_Bit0_1II,
          (uint)MultiplyStates.MoveTo01InD_Bit0_1II,
          (uint)MultiplyStates.MoveToDelimeter4_Bit0_1II,
          (uint)MultiplyStates.MoveToDelimeter0_Bit0_1II,
          (uint)MultiplyStates.MoveToDelimeter2_Bit0_1II,
          (uint)MultiplyStates.MoveToDelimeter3_Bit1_1II,
          (uint)MultiplyStates.MoveTo01InD_Bit1_1II,
          (uint)MultiplyStates.MoveToDelimeter0_Bit1_1II,
          (uint)MultiplyStates.MoveToDelimeter2_Bit1_1II,
          (uint)MultiplyStates.MoveToD01InD_III,
          (uint)MultiplyStates.MoveToDelimeter4_III,
          (uint)MultiplyStates.MoveToDelimeter0_III,
          (uint)MultiplyStates.MoveToDelimeter1_III,
          (uint)MultiplyStates.MoveTo01InB_III,
          (uint)MultiplyStates.MoveToDelimeter4_IV,
          (uint)MultiplyStates.MoveToDelimeter0_IV,
          (uint)MultiplyStates.MoveToDelimeter1_IV,
          (uint)MultiplyStates.MoveToBlankInB_IV,

          (uint)AddStates.AddBit0,
          (uint)AddStates.AddBit1,

          (uint)CompareStates.StartComparing,
          (uint)CompareStates.MoveToStartA,
          (uint)CompareStates.BitLoopStart,
          (uint)CompareStates.MoveToDelimiter3_bit0,
          (uint)CompareStates.MoveToDelimiter3_bit1,
          (uint)CompareStates.SkipF_bit0,
          (uint)CompareStates.SkipF_bit1,
          (uint)CompareStates.MoveToDelimiter4,
          (uint)CompareStates.MoveToDelimiter0,
          (uint)CompareStates.SkipE,
          (uint)CompareStates.MoveToDelimiter4_accept,

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
        markD2,
        markD3,
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

      AppHelper.MergeDictionaryWith(delta, IF_NDTM_RSP_Multiply.Delta01);
      AppHelper.MergeDictionaryWith(delta, IF_NDTM_RSP_Multiply.Delta02);
      AppHelper.MergeDictionaryWith(delta, IF_NDTM_RSP_Multiply.Delta03);
      AppHelper.MergeDictionaryWith(delta, IF_NDTM_RSP_Multiply.Delta04);
      AppHelper.MergeDictionaryWith(delta, IF_NDTM_RSP_Multiply.Delta05);
      AppHelper.MergeDictionaryWith(delta, IF_NDTM_RSP_Multiply.Delta06);
      AppHelper.MergeDictionaryWith(delta, IF_NDTM_RSP_Multiply.Delta07);
      AppHelper.MergeDictionaryWith(delta, IF_NDTM_RSP_Multiply.Delta08);
      AppHelper.MergeDictionaryWith(delta, IF_NDTM_RSP_Multiply.Delta09);
      AppHelper.MergeDictionaryWith(delta, IF_NDTM_RSP_Multiply.Delta10);
      AppHelper.MergeDictionaryWith(delta, IF_NDTM_RSP_Multiply.Delta11);
      AppHelper.MergeDictionaryWith(delta, IF_NDTM_RSP_Multiply.Delta12);
      AppHelper.MergeDictionaryWith(delta, IF_NDTM_RSP_Multiply.Delta13);
      AppHelper.MergeDictionaryWith(delta, IF_NDTM_RSP_Multiply.Delta14);
      AppHelper.MergeDictionaryWith(delta, IF_NDTM_RSP_Multiply.Delta15);
      AppHelper.MergeDictionaryWith(delta, IF_NDTM_RSP_Multiply.Delta16);
      AppHelper.MergeDictionaryWith(delta, IF_NDTM_RSP_Add.Delta);

      AppHelper.MergeDictionaryWith(delta, IF_NDTM_RSP_CompareResult.Delta1);
      AppHelper.MergeDictionaryWith(delta, IF_NDTM_RSP_CompareResult.Delta2);
      AppHelper.MergeDictionaryWith(delta, IF_NDTM_RSP_CompareResult.Delta3);
      AppHelper.MergeDictionaryWith(delta, IF_NDTM_RSP_CompareResult.Delta4);
      AppHelper.MergeDictionaryWith(delta, IF_NDTM_RSP_CompareResult.Delta5);
      AppHelper.MergeDictionaryWith(delta, IF_NDTM_RSP_CompareResult.Delta6);

      Delta = delta;

      qStart = qStartState;
      F = new uint[] { acceptingState };

      CheckDeltaRelation();

      SetupTapeSymbol();
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

    public override SortedDictionary<long, SortedSet<int>> UsedTapeSymbols { get; set;  }

    public override int[] GetOutput(TMInstance tmInstance, ulong mu, ulong n)
    {
      return tmInstance.GetTapeSubstr(GetLTapeBound(mu, n), GetRTapeBound(mu, n));
    }

    public static int FrameLength(int inputLength) => inputLength + 2;

    public static int FrameStart1(int inputLength) => 1 + FrameLength(inputLength);
    public static int FrameStart2(int inputLength) => 2 + (2 * FrameLength(inputLength));
    public static int FrameStart3(int inputLength) => 3 + (3 * FrameLength(inputLength));
    public static int FrameEnd4(int inputLength) => 4 + (4 * FrameLength(inputLength));

    #endregion

    #region private members

    private readonly IReadOnlyKernel configuration;

    private readonly int inputLength;

    private void addSymbol(int tapeIndex, int tapeSymbol)
    {
      AppHelper.TakeValueByKey(UsedTapeSymbols, tapeIndex,
        () => new SortedSet<int>()).Add(tapeSymbol);
    }

    private void SetupTapeSymbol()
    {
      UsedTapeSymbols = new SortedDictionary<long, SortedSet<int>>();

      int frameLength = IF_NDTM.FrameLength(inputLength);
      int frameStart1 = IF_NDTM.FrameStart1(inputLength);
      int frameStart2 = IF_NDTM.FrameStart2(inputLength);
      int frameStart3 = IF_NDTM.FrameStart3(inputLength);
      int frameEnd4 = IF_NDTM.FrameEnd4(inputLength);

      addSymbol(0, delimiter0);
      addSymbol(frameStart1, delimiter1);
      addSymbol(frameStart2, delimiter2);
      addSymbol(frameStart3, delimiter3);
      addSymbol(frameEnd4, delimiter4);

      // frame A
      for (int i = 1; i < frameStart1; i++)
      {
        addSymbol(i, OneTapeTuringMachine.blankSymbol);
        addSymbol(i, 0);
        addSymbol(i, 1);

        addSymbol(i, markE0);
        addSymbol(i, markE1);
      }

      // frame B
      for (int i = (frameStart1 + 1); i < frameStart2; i++)
      {
        addSymbol(i, OneTapeTuringMachine.blankSymbol);
        addSymbol(i, 0);
        addSymbol(i, 1);

        addSymbol(i, markB0);
        addSymbol(i, markB1);
      }

      // frame C
      for (int i = (frameStart2 + 1); i < frameStart3; i++)
      {
        addSymbol(i, OneTapeTuringMachine.blankSymbol);
        addSymbol(i, 0);
        addSymbol(i, 1);

        addSymbol(i, markC0);
        addSymbol(i, markC1);
      }

      // frame D
      for (int i = (frameStart3 + 1); i < frameEnd4; i++)
      {
        addSymbol(i, OneTapeTuringMachine.blankSymbol);
        addSymbol(i, 0);
        addSymbol(i, 1);

        addSymbol(i, markD0);
        addSymbol(i, markD1);
        addSymbol(i, markD2);
        addSymbol(i, markD3);

        addSymbol(i, markF0);
        addSymbol(i, markF1);
      }
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
