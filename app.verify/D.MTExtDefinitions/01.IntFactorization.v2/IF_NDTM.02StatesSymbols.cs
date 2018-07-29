////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;
using ExistsAcceptingPath;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace MTExtDefinitions.v2
{
  public partial class IF_NDTM
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

    private enum InitStates : uint
    {
      MoveToRightDelim = qStartState + 1
    }

    private enum GenNumber1States : uint
    {
      GenNumber1Base = InitStates.MoveToRightDelim + 1,
      GenBitA,
      GenBitB,
      MoveToDelimiter2,
      StopGenNumber,
    }

    private enum GenNumber2States : uint
    {
      GenBitA = GenNumber1States.StopGenNumber + 1,
      GenBitB,
      MoveToDelimiter3,
      MoveToDelimiter4,
      MoveToDelimiter0,
      MoveToDelimiter1,
      StopGenNumber,
    }

    private enum MultiplyStates : uint
    {
      MultReady = GenNumber2States.StopGenNumber + 1,
      StartLoopInC,
      Process1f_D,
      MoveToCRight,
      EraseMarkInC,
      StartAddC,
      AddC0f_D,
      AddC0f_sm_D,
      MoveToCLeft,
      SetMarkInC,
      MoveToMarkInB,
      MoveToMarkInB_inB,
      AddC1f_D,
      AddC1f_sm_D,
      MoveToMarkInD_L,
      MoveToMarkInD_R,
      MoveMarkInD,
      StopMultiplying,
    }

    private enum AddStates : uint
    {
      StartAdding = MultiplyStates.StopMultiplying + 1,
      AddBitC0,
      AddBitC1,
    }

    private enum CompareStates : uint
    {
      StartComparing = AddStates.AddBitC1 + 1,
      MoveToStartA,
      BitLoopStart,
      BitLoopStart_f,
      BitLoopD0,
      BitLoopD1,
      MoveToDelimiter3_bit0,
      MoveToDelimiter3_bit1,
      SkipF_bit0,
      SkipF_bit1,
      MoveToF_bit0,
      MoveToF_bit1,
      MoveToDelimiter4,
      MoveToDelimiter0,
      SkipE
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
