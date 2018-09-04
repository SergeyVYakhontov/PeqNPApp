﻿////////////////////////////////////////////////////////////////////////////////////////////////////

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
    public const int markD2 = 13;
    public const int markD3 = 14;
    // frame A; comparing
    public const int markE0 = 15;
    public const int markE1 = 16;
    // frame D; comparing
    public const int markF0 = 17;
    public const int markF1 = 18;

    public enum InitStates : uint
    {
      MoveToRightDelim = qStartState + 1
    }

    public enum GenNumber1States : uint
    {
      GenBitA = InitStates.MoveToRightDelim + 1,
      GenBitB,
      MoveToDelimiter2,
    }

    public enum GenNumber2States : uint
    {
      GenBitA = GenNumber1States.MoveToDelimiter2 + 1,
      GenBitB,
      MoveToDelimiter3,
      MoveToDelimiter4,
      MoveToDelimiter0,
      MoveToDelimiter1,
    }

    public enum MultiplyStates : uint
    {
      MultReady = GenNumber2States.MoveToDelimiter1 + 1,
      MoveToDelimeter2_0I,
      MoveTo01InC_0I,
      MoveToDelimeter3_0II,
      MoveTo01InD_0II,
      MoveToDelimeter4_0II,
      MoveToDelimeter0_0II,
      MoveToDelimeter2_0II,

      MoveToMarkInD_L
    }

    public enum AddStates : uint
    {
      StartAdding = MultiplyStates.MoveToMarkInD_L + 1,
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
      SkipE
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
