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

    private enum InitStates : uint
    {
      MoveRightToDelim1 = qStartState + 1,
    }

    private enum GenNumber1States : uint
    {
      GenBitA = InitStates.MoveRightToDelim1 + 1,
      GenBitB,
      MoveRightToDelim2,
      StopGenNumber
    }

    private enum GenNumber2States : uint
    {
      GenBitA = GenNumber1States.StopGenNumber + 1,
      GenBitB,
      MoveRightToDelim3,
      StopGenNumber
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
      StopMultiplying
    }

    private enum AddStates : uint
    {
      StartAdding = MultiplyStates.StopMultiplying + 1,
      AddBitC0,
      AddBitC1
    }

    private enum CompareStates : uint
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
