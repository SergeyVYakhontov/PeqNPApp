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
  public partial class IF_NDTM
  {
    #region private members

    private const int qStartState = 0;
    private const int acceptingState = 127;
    private const int rejectingState = 128;

    private const int delimiter = 2;
    private const int markB0 = 3;
    private const int markB1 = 4;
    private const int markC0 = 5;
    private const int markC1 = 6;
    private const int markD0 = 7;
    private const int markD1 = 8;
    private const int bkwd1 = 9;
    private const int bkwd2 = 10;

    private enum SubprogStates : byte
    {
      SubprogStatesBase = qStartState + 1, // 1
      InitReady = SubprogStatesBase, // 1
      GenNumber1Ready = InitReady + 1, // 2
      GenNumber2Ready = GenNumber1Ready + 1, // 3
      MultReady = GenNumber2Ready + 1, // 4
      CompareReady = MultReady + 1 // 5
    }

    private enum InitStates : byte
    {
      InitBase = SubprogStates.CompareReady + 1, // 6
      SetLeftDelim = InitBase, // 6
      InitB = SetLeftDelim + 1, // 7
      InitC = InitB + 1, // 8
      InitD = InitC + 1, // 9
      StopInit = InitD + 1 // 10
    }

    private enum GenNumber1States : byte
    {
      GenNumber1Base = InitStates.StopInit + 1, // 11
      GenBit0 = GenNumber1Base, // 11
      GenBit1 = GenBit0 + 1, // 12
      GenBit = GenBit1 + 1, // 13
      MoveToDelimiter = GenBit + 1, // 14
      StopGenNumber = MoveToDelimiter + 1 // 15
    }

    private enum GenNumber2States : byte
    {
      GenNumber2Base = GenNumber1States.StopGenNumber + 1, // 16
      GenBit0 = GenNumber2Base, // 16
      GenBit1 = GenBit0 + 1, // 17
      GenBit = GenBit1 + 1, // 18
      MoveToDelimiter = GenBit + 1, // 19
      StopGenNumber = MoveToDelimiter + 1 // 20
    }

    private enum MultiplyStates : byte
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

    private enum AddStates : byte
    {
      AddBase = MultiplyStates.StopMultiplying + 1, // 38
      StartAdding = AddBase, // 38
      AddBitC0 = StartAdding + 1, // 39
      AddBitC1 = AddBitC0 + 1 // 40
    }

    private enum CompareStates : byte
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

    private enum BkwdStates : byte
    {
      BkwdStatesBase = CompareStates.BitLoopD1 + 1, // 48
      Bkwd1 = BkwdStatesBase, // 48
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
