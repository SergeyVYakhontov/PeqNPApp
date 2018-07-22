////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;
using ExistsAcceptingPath;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace MTExtDefinitions.v1
{
  public partial class IF_NDTM
  {
    #region private members

    private const uint qStartState = 0;
    private const uint acceptingState = 127;
    private const uint rejectingState = 128;

    private const int delimiter0 = 2;
    private const int delimiter1 = 4;
    private const int delimiter2 = 5;
    private const int delimiter3 = 6;
    private const int delimiter4 = 7;

    private const int markB0 = 8;
    private const int markB1 = 9;
    private const int markC0 = 10;
    private const int markC1 = 11;
    private const int markD0 = 12;
    private const int markD1 = 13;
    private const int bkwd1 = 14;
    private const int bkwd2 = 15;

    private enum SubprogStates : uint
    {
      SubprogStatesBase = qStartState + 1, // 1
      MultReady = SubprogStatesBase + 1, // 4
      CompareReady = MultReady + 1 // 5
    }

    private enum InitStates : uint
    {
      InitBase = SubprogStates.CompareReady + 1, // 6
      MoveToRightDelim = InitBase, // 6
    }

    private enum GenNumber1States : uint
    {
      GenNumber1Base = InitStates.MoveToRightDelim + 1, // 11
      GenBitA = GenNumber1Base + 1, // 13
      GenBitB = GenBitA + 1, // 13
      MoveToDelimiter = GenBitB + 1,
      StopGenNumber = MoveToDelimiter + 1 // 15
    }

    private enum GenNumber2States : uint
    {
      GenNumber2Base = GenNumber1States.StopGenNumber + 1, // 16
      GenBitA = GenNumber2Base + 1, // 13
      GenBitB = GenBitA + 1, // 13
      MoveToDelimiter = GenBitB + 1,
      StopGenNumber = MoveToDelimiter + 1 // 15
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
      MoveLeftToA = StartComparing + 1, // 42
      MoveToStartA = MoveLeftToA + 1, // 43
      BitLoopStart = MoveToStartA + 1, // 44
      BitLoopStart_f = BitLoopStart + 1, // 45
      BitLoopD0 = BitLoopStart_f + 1, // 46
      BitLoopD1 = BitLoopD0 + 1 // 47
    }

    private enum BkwdStates : uint
    {
      BkwdStatesBase = CompareStates.BitLoopD1 + 1, // 48
      Bkwd1 = BkwdStatesBase, // 48
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
