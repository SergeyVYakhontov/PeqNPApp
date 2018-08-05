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
  public static partial class IF_NDTM_RSP_Multiply
  {
    #region public members

    public static IReadOnlyDictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> Delta1(int frameLength)
    {
      return new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
        {
          // set mark
          [new StateSymbolPair(state: (uint)IF_NDTM.MultiplyStates.MultReady, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.StartLoopInC,
                    symbol: IF_NDTM.markB0,
                    direction: TMDirection.S
                  )
              },
          [new StateSymbolPair(state: (uint)IF_NDTM.MultiplyStates.MultReady, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.StartLoopInC,
                    symbol: IF_NDTM.markB1,
                    direction: TMDirection.S
                  )
              },
          [new StateSymbolPair(
              state: (uint)IF_NDTM.MultiplyStates.MultReady,
              symbol: OneTapeTuringMachine.blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.StartLoopInC,
                    symbol: OneTapeTuringMachine.blankSymbol,
                    direction: TMDirection.S
                  )
              },
          [new StateSymbolPair(
              state: (uint)IF_NDTM.MultiplyStates.MultReady,
              symbol: IF_NDTM.delimiter3)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.StartLoopInC,
                    symbol: IF_NDTM.delimiter3,
                    direction: TMDirection.S
                  )
              },

          // start compare
          [new StateSymbolPair(
              state: (uint)IF_NDTM.MultiplyStates.StartLoopInC,
              symbol: OneTapeTuringMachine.blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.StartComparing,
                    symbol: OneTapeTuringMachine.blankSymbol,
                    direction: TMDirection.S
                  )
              },
          [new StateSymbolPair(
              state: (uint)IF_NDTM.MultiplyStates.StartLoopInC,
              symbol: IF_NDTM.delimiter3)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: IF_NDTM.rejectingState,
                    symbol: IF_NDTM.delimiter3,
                    direction: TMDirection.S
                  )
              },

          // process 0
          [new StateSymbolPair(
              state: (uint)IF_NDTM.MultiplyStates.StartLoopInC,
              symbol: IF_NDTM.markB0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MultReady,
                    symbol: IF_NDTM.markB0,
                    direction: TMDirection.R
                  )
              },

          // process bit 1
          [new StateSymbolPair(
              state: (uint)IF_NDTM.MultiplyStates.StartLoopInC,
              symbol: IF_NDTM.markB1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.Process1f_D,
                    symbol: IF_NDTM.markB1,
                    direction: TMDirection.R,
                    shift: frameLength * 2
                  )
              },

          // set mark
          [new StateSymbolPair(
              state: (uint)IF_NDTM.MultiplyStates.Process1f_D,
              symbol: OneTapeTuringMachine.blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToCRight,
                    symbol: IF_NDTM.markD0,
                    direction: TMDirection.L,
                    shift: frameLength
                  )
              },
          [new StateSymbolPair(state: (uint)IF_NDTM.MultiplyStates.Process1f_D, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)IF_NDTM.MultiplyStates.MoveToCRight,
                    Symbol = IF_NDTM.markD0,
                    Direction = TMDirection.L,
                    Shift = frameLength
                  }
              },
          [new StateSymbolPair(state: (uint)IF_NDTM.MultiplyStates.Process1f_D, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)IF_NDTM.MultiplyStates.MoveToCRight,
                    Symbol = IF_NDTM.markD1,
                    Direction = TMDirection.L,
                    Shift = frameLength
                  }
              },

          // restore C
          // move to right delimiter
          [new StateSymbolPair(
              state: (uint)IF_NDTM.MultiplyStates.MoveToCRight,
              symbol: OneTapeTuringMachine.blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToCRight,
                    symbol: OneTapeTuringMachine.blankSymbol,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)IF_NDTM.MultiplyStates.MoveToCRight, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToCRight,
                    symbol: 0,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)IF_NDTM.MultiplyStates.MoveToCRight, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToCRight,
                    symbol: 1,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(
              state: (uint)IF_NDTM.MultiplyStates.MoveToCRight,
              symbol: IF_NDTM.markC0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToCRight,
                    symbol: IF_NDTM.markC0,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(
              state: (uint)IF_NDTM.MultiplyStates.MoveToCRight,
              symbol: IF_NDTM.markC1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToCRight,
                    symbol: IF_NDTM.markC1,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(
              state: (uint)IF_NDTM.MultiplyStates.MoveToCRight,
              symbol: IF_NDTM.delimiter3)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.EraseMarkInC,
                    symbol: IF_NDTM.delimiter3,
                    direction: TMDirection.L
                  )
              },

          // erase marks in C
          [new StateSymbolPair(
              state: (uint)IF_NDTM.MultiplyStates.EraseMarkInC,
              symbol: OneTapeTuringMachine.blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.EraseMarkInC,
                    symbol: OneTapeTuringMachine.blankSymbol,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)IF_NDTM.MultiplyStates.EraseMarkInC, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.EraseMarkInC,
                    symbol: 0,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)IF_NDTM.MultiplyStates.EraseMarkInC, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.EraseMarkInC,
                    symbol: 1,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(
              state: (uint)IF_NDTM.MultiplyStates.EraseMarkInC,
              symbol: IF_NDTM.markC0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.EraseMarkInC,
                    symbol: 0,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(
              state: (uint)IF_NDTM.MultiplyStates.EraseMarkInC,
              symbol: IF_NDTM.markC1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.EraseMarkInC,
                    symbol: 1,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(
              state: (uint)IF_NDTM.MultiplyStates.EraseMarkInC,
              symbol: IF_NDTM.delimiter2)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.StartAddC,
                    symbol: IF_NDTM.delimiter2,
                    direction: TMDirection.R
                  )
              }
        };
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
