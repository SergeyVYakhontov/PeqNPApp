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
    #region public members

    public const uint qStartState = 0;
    public const uint acceptingState = 127;
    public const uint rejectingState = 128;

    public const int delimiter0 = 2;
    public const int delimiter1 = 3;
    public const int delimiter2 = 4;
    public const int delimiter3 = 5;
    public const int delimiter4 = 6;

    // frame B; adding
    public const int markB0 = 7;
    public const int markB1 = 8;
    // frame C; adding
    public const int markC0 = 9;
    public const int markC1 = 10;
    // frame D; multiplying
    public const int markD0 = 11;
    public const int markD1 = 12;
    // frame A; comparing
    public const int markE0 = 13;
    public const int markE1 = 14;
    // frame D; comparing
    public const int markF0 = 15;
    public const int markF1 = 16;

    public enum InitStates : uint
    {
      MoveToRightDelim = qStartState + 1
    }

    public enum GenNumber1States : uint
    {
      GenNumber1Base = InitStates.MoveToRightDelim + 1,
      GenBitA,
      GenBitB,
      MoveToDelimiter2,
      StopGenNumber,
    }

    public enum GenNumber2States : uint
    {
      GenBitA = GenNumber1States.StopGenNumber + 1,
      GenBitB,
      MoveToDelimiter3,
      MoveToDelimiter4,
      MoveToDelimiter0,
      MoveToDelimiter1,
      StopGenNumber,
    }

    public enum MultiplyStates : uint
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

    public enum AddStates : uint
    {
      StartAdding = MultiplyStates.StopMultiplying + 1,
      AddBitC0,
      AddBitC1,
    }

    public enum CompareStates : uint
    {
      StartComparing = AddStates.AddBitC1 + 1,
      MoveToStartA,
      BitLoopStart,
      MoveToDelimiter3_bit0,
      MoveToDelimiter3_bit1,
      SkipF_bit0,
      SkipF_bit1,
      MoveToDelimiter4,
      MoveToDelimiter0,

      BitLoopStart_f,
      BitLoopD0,
      BitLoopD1,
      MoveToF_bit0,
      MoveToF_bit1,
      SkipE
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
