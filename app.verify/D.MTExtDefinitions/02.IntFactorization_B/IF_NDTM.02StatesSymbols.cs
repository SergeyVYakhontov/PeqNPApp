////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;
using ExistsAcceptingPath;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace MTExtDefinitions
{
  public partial class IF_NDTM_B
  {
    #region private members

    private const uint qStartState = 0;
    private const uint acceptingState = 127;
    private const uint rejectingState = 128;

    private const int delimiter0 = 2;
    private const int delimiter1 = 3;
    private const int delimiter2 = 4;
    private const int delimiter3 = 5;
    private const int delimiter4 = 6;

    private const int markB0 = 7;
    private const int markB1 = 8;
    private const int markC0 = 9;
    private const int markC1 = 10;
    private const int markD0 = 11;
    private const int markD1 = 12;
    private const int markE0 = 13;
    private const int markE1 = 14;
    private const int markF0 = 15;
    private const int markF1 = 16;

    private enum SubprogStates : uint
    {
      SubprogStatesBase = qStartState + 1, // 1
      MultReady = SubprogStatesBase + 1, // 4
    }

    private enum InitStates : uint
    {
      InitBase = SubprogStates.MultReady + 1, // 6
      MoveToRightDelim = InitBase, // 6
    }

    private enum GenNumber1States : uint
    {
      GenNumber1Base = InitStates.MoveToRightDelim + 1, // 11
      GenBitA = GenNumber1Base + 1, // 13
      GenBitB = GenBitA + 1, // 13
      MoveToDelimiter2 = GenBitB + 1,
      StopGenNumber = MoveToDelimiter2 + 1 // 15
    }

    private enum GenNumber2States : uint
    {
      GenNumber2Base = GenNumber1States.StopGenNumber + 1, // 16
      GenBitA = GenNumber2Base + 1, // 13
      GenBitB = GenBitA + 1, // 13
      MoveToDelimiter3 = GenBitB + 1,
      MoveToDelimiter4 = MoveToDelimiter3 + 1,
      MoveToDelimiter0 = MoveToDelimiter4 + 1,
      MoveToDelimiter1 = MoveToDelimiter0 + 1,
      StopGenNumber = MoveToDelimiter1 + 1 // 15
    }

    private enum MultiplyStates : uint
    {
      MultiplyBase = GenNumber2States.StopGenNumber + 1, // 21
      StartLoopInC = MultiplyBase, // 21
      Process1f_D = StartLoopInC + 1, // 22
      MoveToCRight = Process1f_D + 1, // 23
      EraseMarkInC = MoveToCRight + 1, // 24
      StartAddC = EraseMarkInC + 1, // 25
      AddC0f_D = StartAddC + 1, // 26
      AddC0f_sm_D = AddC0f_D + 1, // 27
      MoveToCLeft = AddC0f_sm_D + 1, // 28
      SetMarkInC = MoveToCLeft + 1, // 29
      MoveToMarkInB = SetMarkInC + 1, // 30
      MoveToMarkInB_inB = MoveToMarkInB + 1, // 31
      AddC1f_D = MoveToMarkInB_inB + 1, // 32
      AddC1f_sm_D = AddC1f_D + 1, // 33
      MoveToMarkInD_L = AddC1f_sm_D + 1, // 34
      MoveToMarkInD_R = MoveToMarkInD_L + 1, // 35
      MoveMarkInD = MoveToMarkInD_R + 1, // 36
      StopMultiplying = MoveMarkInD + 1 // 37
    }

    private enum AddStates : uint
    {
      AddBase = MultiplyStates.StopMultiplying + 1, // 38
      StartAdding = AddBase, // 38
      AddBitC0 = StartAdding + 1, // 39
      AddBitC1 = AddBitC0 + 1 // 40
    }

    private enum CompareStates : uint
    {
      CompareBase = AddStates.AddBitC1 + 1, // 41
      StartComparing = CompareBase, // 41
      MoveToStartA = StartComparing + 1, // 43
      BitLoopStart = MoveToStartA + 1, // 44
      BitLoopStart_f = BitLoopStart + 1, // 45
      BitLoopD0 = BitLoopStart_f + 1, // 46
      BitLoopD1 = BitLoopD0 + 1, // 47
      MoveToDelimiter3_bit0 = BitLoopD1 + 1,
      MoveToDelimiter3_bit1 = MoveToDelimiter3_bit0 + 1,
      SkipF_bit0 = MoveToDelimiter3_bit1 + 1,
      SkipF_bit1 = SkipF_bit0 + 1,
      MoveToF_bit0 = SkipF_bit1 + 1, // 46
      MoveToF_bit1 = MoveToF_bit0 + 1, // 46
      MoveToDelimiter4 = MoveToF_bit1 + 1,
      MoveToDelimiter0 = MoveToDelimiter4 + 1,
      SkipE = MoveToDelimiter0 + 1,
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
