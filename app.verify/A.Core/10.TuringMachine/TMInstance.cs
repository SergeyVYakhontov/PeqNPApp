////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Ninject;
using EnsureThat;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public class TMInstance : ITracable
  {
    #region Ctors

    public TMInstance(OneTapeTuringMachine owner, int[] input)
    {
      this.owner = owner;
      Level = 0;

      tape = new TapeArray();
      cellIndex = 1;
      state = owner.qStart;

      for (long i = 1; i <= input.Length; i++)
      {
        int currentInputSymbol = input[i - 1];

        Ensure.That(owner.Sigma.Contains(currentInputSymbol)).IsTrue();

        tape[i] = currentInputSymbol;
      }
    }

    public TMInstance(TMInstance other)
    {
      owner = other.owner;
      Level = other.Level + 1;

      tape = new TapeArray(other.tape);
      cellIndex = other.cellIndex;
      state = other.state;
    }

    #endregion

    #region public members

    public string Name { get; }
    public long Level { get; private set; }

    public bool IsInFinalState()
    {
      return IsFinalState(state);
    }

    public List<TMInstance> DoStep()
    {
      List<TMInstance> newInstances = new();

      if (IsFinalState(state))
      {
        return newInstances;
      }

      StateSymbolPair from = new(state, CurrentSymbol);

      if (owner.Delta.ContainsKey(from))
      {
        foreach (StateSymbolDirectionTriple to in owner.Delta[from])
        {
          TMInstance newInstance = new(this);

          MoveToNextConfiguration(to, newInstance);
          newInstance.Level = Level + 1;

          newInstances.Add(newInstance);
          newInstance.Trace();
        }
      }

      return newInstances;
    }

    public int[] GetOutput()
    {
      if (CurrentSymbol == OneTapeTuringMachine.blankSymbol)
      {
        return Array.Empty<int>();
      }

      long leftIndex = 1;
      long rightIndex = 1;

      while (tape[leftIndex - 1] != OneTapeTuringMachine.blankSymbol)
      {
        leftIndex--;
      }

      while (tape[rightIndex + 1] != OneTapeTuringMachine.blankSymbol)
      {
        rightIndex++;
      }

      return tape.GetSubArray(leftIndex, rightIndex);
    }

    public void SetTapeSymbol(long tapeIndex, int tapeSymbol)
    {
      tape[tapeIndex] = tapeSymbol;
    }

    public TapeArray GetTape()
    {
      return tape;
    }

    public int[] GetTapeSubstr(long leftIndex, long rightIndex)
    {
      return tape.GetSubArray(leftIndex, rightIndex);
    }

    public long CellIndex() => cellIndex;
    public uint State() => state;
    public int TapeSymbol(int tapeIndex) => tape.GetValue(tapeIndex);

    public StateSymbolPair CurrentStateSymbolPair
    {
      get
      {
        return new StateSymbolPair(state, CurrentSymbol);
      }
    }

    public void SetupConfiguration(long cellIndex, uint state)
    {
      this.cellIndex = cellIndex;
      this.state = state;
    }

    public static void MoveToNextConfiguration(
      StateSymbolDirectionTriple to,
      TMInstance instance)
    {
      instance.state = to.State;

      switch (to.Direction)
      {
        case TMDirection.L:
          {
            instance.CurrentSymbol = to.Symbol;
            instance.cellIndex -= to.Shift;
            break;
          }
        case TMDirection.R:
          {
            instance.CurrentSymbol = to.Symbol;
            instance.cellIndex += to.Shift;
            break;
          }
        case TMDirection.S:
          {
            instance.CurrentSymbol = to.Symbol;
            break;
          }
      }
    }

    public void Trace()
    {
      log.DebugFormat("instance.Name = {0}", Name);
      log.DebugFormat("instance.state = {0}", state);
      log.DebugFormat("instance.symbol = {0}", CurrentSymbol);
      log.DebugFormat("instance.cellIndex = {0}", cellIndex);
      log.DebugFormat("instance.tape = {0}", tape.GetStringRepr());
    }

    #endregion

    #region private members

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType);

    private readonly OneTapeTuringMachine owner;

    private readonly TapeArray tape;
    private long cellIndex;
    private uint state;

    private int CurrentSymbol
    {
      get
      {
        return tape[cellIndex];
      }

      set
      {
        tape[cellIndex] = value;
      }
    }

    private bool IsFinalState(uint s)
    {
      return owner.F.Contains(s);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
