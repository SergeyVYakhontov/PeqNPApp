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
    #region public members

    public const uint qStartState = 0;
    public const uint acceptingState = 127;
    public const uint rejectingState = 128;

    public const int delimiter0 = 2;
    public const int delimiter1 = 4;
    public const int delimiter2 = 5;
    public const int delimiter3 = 6;
    public const int delimiter4 = 7;

    public const int markB0 = 8;
    public const int markB1 = 9;
    public const int markC0 = 10;
    public const int markC1 = 11;
    public const int markD0 = 12;
    public const int markD1 = 13;

    public enum InitStates : uint
    {
      MoveRightToDelim1 = qStartState + 1,
    }

    public enum GenNumber1States : uint
    {
      GenBitA = InitStates.MoveRightToDelim1 + 1,
      GenBitB,
      MoveRightToDelim2,
      StopGenNumber
    }

    public enum GenNumber2States : uint
    {
      GenBitA = GenNumber1States.StopGenNumber + 1,
      GenBitB,
      MoveRightToDelim3,
      StopGenNumber
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
      StopMultiplying
    }

    public enum AddStates : uint
    {
      StartAdding = MultiplyStates.StopMultiplying + 1,
      AddBitC0,
      AddBitC1
    }

    public enum CompareStates : uint
    {
      CompareReady = AddStates.AddBitC1 + 1,
      StartComparing,
      MoveLeftToA,
      MoveToStartA,
      BitLoopStart,
      BitLoopStart_f,
      BitLoopD0,
      BitLoopD1
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
