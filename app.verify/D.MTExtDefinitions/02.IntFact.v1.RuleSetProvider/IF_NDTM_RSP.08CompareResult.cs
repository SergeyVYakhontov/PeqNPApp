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
  public static class IF_NDTM_RSP_CompareResult
  {
    #region public members

    public static Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> Delta(int frameLength)
    {
      return new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
      {
        // start comparing
        [new StateSymbolPair(state: (uint)IF_NDTM.CompareStates.CompareReady, symbol: OneTapeTuringMachine.blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.StartComparing,
                    symbol: OneTapeTuringMachine.blankSymbol,
                    direction: TMDirection.S
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.CompareStates.StartComparing, symbol: OneTapeTuringMachine.blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.MoveLeftToA,
                    symbol: OneTapeTuringMachine.blankSymbol,
                    direction: TMDirection.L,
                    shift: frameLength
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.CompareStates.MoveLeftToA, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.MoveToStartA,
                    symbol: 0,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.CompareStates.MoveLeftToA, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.MoveToStartA,
                    symbol: 1,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.CompareStates.MoveLeftToA, symbol: OneTapeTuringMachine.blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.MoveLeftToA,
                    symbol: OneTapeTuringMachine.blankSymbol,
                    direction: TMDirection.L
                  )
              },

        // move to start A
        [new StateSymbolPair(state: (uint)IF_NDTM.CompareStates.MoveToStartA, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.MoveToStartA,
                    symbol: 0,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.CompareStates.MoveToStartA, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.MoveToStartA,
                    symbol: 1,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.CompareStates.MoveToStartA, symbol: OneTapeTuringMachine.blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.BitLoopStart,
                    symbol: OneTapeTuringMachine.blankSymbol,
                    direction: TMDirection.R
                  )
              },

        // shift to D
        [new StateSymbolPair(state: (uint)IF_NDTM.CompareStates.BitLoopStart, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.BitLoopD0,
                    symbol: 0,
                    direction: TMDirection.R,
                    shift: (frameLength * 3) + 1
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.CompareStates.BitLoopStart, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.BitLoopD1,
                    symbol: 1,
                    direction: TMDirection.R,
                    shift: (frameLength * 3) + 1
                  )
              },

        // compare bit
        [new StateSymbolPair(state: (uint)IF_NDTM.CompareStates.BitLoopD0, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.BitLoopStart_f,
                    symbol: 0,
                    direction: TMDirection.L,
                    shift: (frameLength * 3) + 1
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.CompareStates.BitLoopD0, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: IF_NDTM.rejectingState,
                    symbol: 1,
                    direction: TMDirection.S
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.CompareStates.BitLoopD1, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: IF_NDTM.rejectingState,
                    symbol: 0,
                    direction: TMDirection.S
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.CompareStates.BitLoopD1, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.BitLoopStart_f,
                    symbol: 1,
                    direction: TMDirection.L,
                    shift: (frameLength * 3) + 1
                  )
              },

        // D0, D1
        [new StateSymbolPair(state: (uint)IF_NDTM.CompareStates.BitLoopD0, symbol: IF_NDTM.markD0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.BitLoopStart_f,
                    symbol: 0,
                    direction: TMDirection.L,
                    shift: (frameLength * 3) + 1
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.CompareStates.BitLoopD0, symbol: IF_NDTM.markD1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: IF_NDTM.rejectingState,
                    symbol: 1,
                    direction: TMDirection.S
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.CompareStates.BitLoopD1, symbol: IF_NDTM.markD0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: IF_NDTM.rejectingState,
                    symbol: 0,
                    direction: TMDirection.S
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.CompareStates.BitLoopD1, symbol: IF_NDTM.markD1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.BitLoopStart_f,
                    symbol: 1,
                    direction: TMDirection.L,
                    shift: (frameLength * 3) + 1
                  )
              },

        // move to next bit
        [new StateSymbolPair(state: (uint)IF_NDTM.CompareStates.BitLoopStart_f, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.BitLoopStart,
                    symbol: 0,
                    direction: TMDirection.R
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.CompareStates.BitLoopStart_f, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.BitLoopStart,
                    symbol: 1,
                    direction: TMDirection.R
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.CompareStates.BitLoopStart_f, symbol: OneTapeTuringMachine.blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: IF_NDTM.acceptingState,
                    symbol: OneTapeTuringMachine.blankSymbol,
                    direction: TMDirection.R
                  )
              },

        // blank symbol
        [new StateSymbolPair(state: (uint)IF_NDTM.CompareStates.BitLoopD0, symbol: OneTapeTuringMachine.blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: IF_NDTM.rejectingState,
                    symbol: OneTapeTuringMachine.blankSymbol,
                    direction: TMDirection.S
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.CompareStates.BitLoopD1, symbol: OneTapeTuringMachine.blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: IF_NDTM.rejectingState,
                    symbol: OneTapeTuringMachine.blankSymbol,
                    direction: TMDirection.S
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.CompareStates.BitLoopStart, symbol: OneTapeTuringMachine.blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: IF_NDTM.acceptingState,
                    symbol: OneTapeTuringMachine.blankSymbol,
                    direction: TMDirection.R
                  )
              }
      };
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
