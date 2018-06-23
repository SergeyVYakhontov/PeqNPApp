////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public class TapeArray
  {
    #region Ctors

    public TapeArray(OneTapeTuringMachine tm)
    {
      tape = new int[tapeInitSize + 1];
      indexBase = 1;

      AssignBlanks(0, LastCellIndex());
    }

    public TapeArray(TapeArray tapeArray)
    {
      tape = new int[tapeArray.tape.Length];
      indexBase = tapeArray.indexBase;

      tapeArray.tape.CopyTo(tape, 0);
    }

    #endregion

    #region public members

    public void SetValue(long cellIndex, int value)
    {
      IncreaseTapeSize(cellIndex);
      tape[GetCellArrayIndex(cellIndex)] = value;
    }

    public int GetValue(long cellIndex)
    {
      IncreaseTapeSize(cellIndex);
      return tape[GetCellArrayIndex(cellIndex)];
    }

    public int this[long cellIndex]
    {
      get
      {
        return GetValue(cellIndex);
      }
      set
      {
        SetValue(cellIndex, value);
      }
    }

    public int[] GetSubArray(long leftIndex, long rightIndex)
    {
      IncreaseTapeSize(leftIndex);
      IncreaseTapeSize(rightIndex + 1);

      return AppHelper.CreateSubArray(
        tape,
        GetCellArrayIndex(leftIndex),
        GetCellArrayIndex(rightIndex) - GetCellArrayIndex(leftIndex) + 1);
    }

    public string GetStringRepr()
    {
      return AppHelper.ArrayToString(tape);
    }

    #endregion

    #region private members

    private const long tapeInitSize = 32;
    private const long tapeIncreaseSize = 32;

    private int[] tape;
    private long indexBase;

    private long GetCellArrayIndex(long cellIndex)
    {
      return (indexBase + cellIndex - 1);
    }

    private long LastCellIndex()
    {
      return (tape.Length - 1);
    }

    private void AssignBlanks(long fromCellIndex, long toCellIndex)
    {
      for (long i = fromCellIndex; i <= toCellIndex; i++)
      {
        tape[GetCellArrayIndex(i)] = OneTapeTuringMachine.b;
      }
    }

    private bool IncreaseTapeSizeIter(long cellIndex)
    {
      long cellArrayIndex = GetCellArrayIndex(cellIndex);
      if (cellArrayIndex < 0)
      {
        int[] newTape = new int[tape.Length + tapeIncreaseSize];

        AppHelper.FillArray(newTape, OneTapeTuringMachine.b);

        tape.CopyTo(newTape, tapeIncreaseSize);
        tape = newTape;
        indexBase += tapeIncreaseSize;

        return true;
      }

      if (cellArrayIndex >= tape.Length)
      {
        long newTapeSize = Math.Max(cellArrayIndex + 1, tape.Length + tapeIncreaseSize);

        int[] newTape = new int[newTapeSize];
        AppHelper.FillArray(newTape, OneTapeTuringMachine.b);

        tape.CopyTo(newTape, 0);
        tape = newTape;

        return true;
      }

      return false;
    }

    private void IncreaseTapeSize(long cellIndex)
    {
      while (IncreaseTapeSizeIter(cellIndex)) { }
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
