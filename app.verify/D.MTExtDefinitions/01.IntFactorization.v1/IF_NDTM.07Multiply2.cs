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

    private Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> deltaMultiply2(int frameLength)
    {
      return new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
        {
          // 0 in C
          // move to D
          [new StateSymbolPair(state: (uint)MultiplyStates.StartAddC, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.AddC0f_D,
                    symbol: markC0,
                    direction: TMDirection.R,
                    shift: frameLength
                  )
              },

          // move in D
          [new StateSymbolPair(state: (uint)MultiplyStates.AddC0f_D, symbol: blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.AddC0f_D,
                    symbol: 0,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.AddC0f_D, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.AddC0f_D,
                    symbol: 0,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.AddC0f_D, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.AddC0f_D,
                    symbol: 1,
                    direction: TMDirection.R
                  )
              },

          // markD reached
          [new StateSymbolPair(state: (uint)MultiplyStates.AddC0f_D, symbol: markD0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.AddC0f_sm_D,
                    symbol: 0,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.AddC0f_D, symbol: markD1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.AddC0f_sm_D,
                    symbol: 1,
                    direction: TMDirection.R
                  )
              },

          // set new mark and move to C
          [new StateSymbolPair(state: (uint)MultiplyStates.AddC0f_sm_D, symbol: blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToCLeft,
                    symbol: markD0,
                    direction: TMDirection.L,
                    shift: frameLength
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.AddC0f_sm_D, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToCLeft,
                    symbol: markD0,
                    direction: TMDirection.L,
                    shift: frameLength
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.AddC0f_sm_D, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToCLeft,
                    symbol: markD1,
                    direction: TMDirection.L,
                    shift: frameLength
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.AddC0f_sm_D, symbol: markD0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToCLeft,
                    symbol: markD0,
                    direction: TMDirection.L,
                    shift: frameLength
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.AddC0f_sm_D, symbol: markD1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToCLeft,
                    symbol: markD1,
                    direction: TMDirection.L,
                    shift: frameLength
                  )
              },

          // move to mark C
          [new StateSymbolPair(state: (uint)MultiplyStates.MoveToCLeft, symbol: blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToCLeft,
                    symbol: blankSymbol,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.MoveToCLeft, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToCLeft,
                    symbol: 0,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.MoveToCLeft, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToCLeft,
                    symbol: 1,
                    direction: TMDirection.L
                  )
              },

          // set new mark
          [new StateSymbolPair(state: (uint)MultiplyStates.MoveToCLeft, symbol: markC0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.SetMarkInC,
                    symbol: markC0,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.MoveToCLeft, symbol: markC1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.SetMarkInC,
                    symbol: markC1,
                    direction: TMDirection.R
                  )
              },

          // start new iteration
          [new StateSymbolPair(state: (uint)MultiplyStates.SetMarkInC, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.StartAddC,
                    symbol: 0,
                    direction: TMDirection.S
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.SetMarkInC, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.StartAddC,
                    symbol: 1,
                    direction: TMDirection.S
                  )
              },

          // move to mark in B
          [new StateSymbolPair(state: (uint)MultiplyStates.SetMarkInC, symbol: blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToMarkInB,
                    symbol: blankSymbol,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.MoveToMarkInB, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToMarkInB,
                    symbol: 0,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.MoveToMarkInB, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToMarkInB,
                    symbol: 1,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.MoveToMarkInB, symbol: markC0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToMarkInB,
                    symbol: markC0,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.MoveToMarkInB, symbol: markC1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToMarkInB,
                    symbol: markC1,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.MoveToMarkInB, symbol: delimiter2)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToMarkInB_inB,
                    symbol: delimiter2,
                    direction: TMDirection.L
                  )
              },

          //  inB
          [new StateSymbolPair(state: (uint)MultiplyStates.MoveToMarkInB_inB, symbol: blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToMarkInB_inB,
                    symbol: blankSymbol,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.MoveToMarkInB_inB, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToMarkInB_inB,
                    symbol: 0,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.MoveToMarkInB_inB, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToMarkInB_inB,
                    symbol: 1,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.MoveToMarkInB_inB, symbol: markB0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MultReady,
                    symbol: markB0,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.MoveToMarkInB_inB, symbol: markB1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MultReady,
                    symbol: markB1,
                    direction: TMDirection.R
                  )
              }
        };
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
