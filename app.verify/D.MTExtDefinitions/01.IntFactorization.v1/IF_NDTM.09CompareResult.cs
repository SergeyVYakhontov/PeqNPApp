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
 /* public partial class IF_NDTM
  {
    #region private members

    private Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> deltaCompare(int frameLength)
    {
      return new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
        {
          // start comparing
          [new StateSymbolPair(state: (uint)CompareStates.CompareReady, symbol: blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.StartComparing,
                    symbol: blankSymbol,
                    direction: TMDirection.S
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.StartComparing, symbol: blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveLeftToA,
                    symbol: blankSymbol,
                    direction: TMDirection.L,
                    shift: frameLength
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveLeftToA, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToStartA,
                    symbol: 0,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveLeftToA, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToStartA,
                    symbol: 1,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveLeftToA, symbol: blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveLeftToA,
                    symbol: blankSymbol,
                    direction: TMDirection.L
                  )
              },

          // move to start A
          [new StateSymbolPair(state: (uint)CompareStates.MoveToStartA, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToStartA,
                    symbol: 0,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToStartA, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToStartA,
                    symbol: 1,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToStartA, symbol: blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.BitLoopStart,
                    symbol: blankSymbol,
                    direction: TMDirection.R
                  )
              },

          // shift to D
          [new StateSymbolPair(state: (uint)CompareStates.BitLoopStart, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.BitLoopD0,
                    symbol: 0,
                    direction: TMDirection.R,
                    shift: (frameLength * 3) + 1
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.BitLoopStart, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.BitLoopD1,
                    symbol: 1,
                    direction: TMDirection.R,
                    shift: (frameLength * 3) + 1
                  )
              },

          // compare bit
          [new StateSymbolPair(state: (uint)CompareStates.BitLoopD0, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.BitLoopStart_f,
                    symbol: 0,
                    direction: TMDirection.L,
                    shift: (frameLength * 3) + 1
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.BitLoopD0, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: rejectingState,
                    symbol: 1,
                    direction: TMDirection.S
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.BitLoopD1, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: rejectingState,
                    symbol: 0,
                    direction: TMDirection.S
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.BitLoopD1, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.BitLoopStart_f,
                    symbol: 1,
                    direction: TMDirection.L,
                    shift: (frameLength * 3) + 1
                  )
              },

          // D0, D1
          [new StateSymbolPair(state: (uint)CompareStates.BitLoopD0, symbol: markD0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.BitLoopStart_f,
                    symbol: 0,
                    direction: TMDirection.L,
                    shift: (frameLength * 3) + 1
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.BitLoopD0, symbol: markD1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: rejectingState,
                    symbol: 1,
                    direction: TMDirection.S
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.BitLoopD1, symbol: markD0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: rejectingState,
                    symbol: 0,
                    direction: TMDirection.S
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.BitLoopD1, symbol: markD1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.BitLoopStart_f,
                    symbol: 1,
                    direction: TMDirection.L,
                    shift: (frameLength * 3) + 1
                  )
              },

          // move to next bit
          [new StateSymbolPair(state: (uint)CompareStates.BitLoopStart_f, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.BitLoopStart,
                    symbol: 0,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.BitLoopStart_f, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.BitLoopStart,
                    symbol: 1,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.BitLoopStart_f, symbol: blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: acceptingState,
                    symbol: blankSymbol,
                    direction: TMDirection.R
                  )
              },

          // blank symbol
          [new StateSymbolPair(state: (uint)CompareStates.BitLoopD0, symbol: blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: rejectingState,
                    symbol: blankSymbol,
                    direction: TMDirection.S
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.BitLoopD1, symbol: blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: rejectingState,
                    symbol: blankSymbol,
                    direction: TMDirection.S
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.BitLoopStart, symbol: blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: acceptingState,
                    symbol: blankSymbol,
                    direction: TMDirection.R
                  )
              }
      };
    }

    #endregion
  } */
}

////////////////////////////////////////////////////////////////////////////////////////////////////
