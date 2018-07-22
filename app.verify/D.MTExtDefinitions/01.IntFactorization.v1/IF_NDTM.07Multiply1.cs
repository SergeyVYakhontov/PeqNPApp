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

    private Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> deltaMultiply1(int frameLength)
    {
      return new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
        {
          // set mark B*
          [new StateSymbolPair(state: (uint)MultiplyStates.MultReady, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.StartLoopInC,
                    symbol: markB0,
                    direction: TMDirection.S
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.MultReady, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.StartLoopInC,
                    symbol: markB1,
                    direction: TMDirection.S
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.MultReady, symbol: blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.StartLoopInC,
                    symbol: blankSymbol,
                    direction: TMDirection.S
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.MultReady, symbol: delimiter3)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.StartLoopInC,
                    symbol: delimiter3,
                    direction: TMDirection.S
                  )
              },

          // start compare
          [new StateSymbolPair(state: (uint)MultiplyStates.StartLoopInC, symbol: blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.CompareReady,
                    symbol: blankSymbol,
                    direction: TMDirection.S
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.StartLoopInC, symbol: delimiter3)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: rejectingState,
                    symbol: delimiter3,
                    direction: TMDirection.S
                  )
              },

          // process bit 0
          [new StateSymbolPair(state: (uint)MultiplyStates.StartLoopInC, symbol: markB0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MultReady,
                    symbol: markB0,
                    direction: TMDirection.R
                  )
              },

          // process bit 1
          [new StateSymbolPair(state: (uint)MultiplyStates.StartLoopInC, symbol: markB1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.Process1f_D,
                    symbol: markB1,
                    direction: TMDirection.R,
                    shift: frameLength * 2
                  )
              },

          // set mark
          [new StateSymbolPair(state: (uint)MultiplyStates.Process1f_D, symbol: blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToCRight,
                    symbol: markD0,
                    direction: TMDirection.L,
                    shift: frameLength
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.Process1f_D, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToCRight,
                    symbol: markD0,
                    direction: TMDirection.L,
                    shift: frameLength
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.Process1f_D, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToCRight,
                    symbol: markD1,
                    direction: TMDirection.L,
                    shift: frameLength
                  )
              },

          // restore C
          // move to right delimiter
          [new StateSymbolPair(state: (uint)MultiplyStates.MoveToCRight, symbol: blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToCRight,
                    symbol: blankSymbol,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.MoveToCRight, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToCRight,
                    symbol: 0,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.MoveToCRight, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToCRight,
                    symbol: 1,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.MoveToCRight, symbol: markC0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToCRight,
                    symbol: markC0,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.MoveToCRight, symbol: markC1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToCRight,
                    symbol: markC1,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.MoveToCRight, symbol: delimiter3)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.EraseMarkInC,
                    symbol: delimiter3,
                    direction: TMDirection.L
                  )
              },

          // erase marks in C
          [new StateSymbolPair(state: (uint)MultiplyStates.EraseMarkInC, symbol: blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.EraseMarkInC,
                    symbol: blankSymbol,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.EraseMarkInC, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.EraseMarkInC,
                    symbol: 0,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.EraseMarkInC, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.EraseMarkInC,
                    symbol: 1,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.EraseMarkInC, symbol: markC0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.EraseMarkInC,
                    symbol: 0,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.EraseMarkInC, symbol: markC1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (int)MultiplyStates.EraseMarkInC,
                    symbol: 1,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.EraseMarkInC, symbol: delimiter2)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.StartAddC,
                    symbol: delimiter2,
                    direction: TMDirection.R
                  )
              }
        };
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
