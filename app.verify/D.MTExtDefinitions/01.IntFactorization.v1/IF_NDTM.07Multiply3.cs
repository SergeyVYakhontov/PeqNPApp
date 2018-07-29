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

    private Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> deltaMultiply3(int frameLength)
    {
      return new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
        {
          // 1 in C
          // move to D
          [new StateSymbolPair(state: (uint)MultiplyStates.StartAddC, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.AddC1f_D,
                    symbol: markC1,
                    direction: TMDirection.R,
                    shift: frameLength
                  )
              },

          // move in D
          [new StateSymbolPair(state: (uint)MultiplyStates.AddC1f_D, symbol: blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.AddC1f_D,
                    symbol: 0,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.AddC1f_D, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.AddC1f_D,
                    symbol: 0,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.AddC1f_D, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.AddC1f_D,
                    symbol: 1,
                    direction: TMDirection.R
                  )
              },

          // markD reached
          [new StateSymbolPair(state: (uint)MultiplyStates.AddC1f_D, symbol: markD0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)AddStates.StartAdding,
                    symbol: markD0,
                    direction: TMDirection.S
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.AddC1f_D, symbol: markD1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)AddStates.StartAdding,
                    symbol: markD1,
                    direction: TMDirection.S
                  )
              },

          // shift mark
          // move to left delimiter
          [new StateSymbolPair(state: (uint)MultiplyStates.MoveToMarkInD_L, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToMarkInD_L,
                    symbol: 0,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.MoveToMarkInD_L, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToMarkInD_L,
                    symbol: 1,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.MoveToMarkInD_L, symbol: markD0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToMarkInD_L,
                    symbol: markD0,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.MoveToMarkInD_L, symbol: markD1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToMarkInD_L,
                    symbol: markD1,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.MoveToMarkInD_L, symbol: delimiter3)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToMarkInD_R,
                    symbol: delimiter3,
                    direction: TMDirection.R
                  )
              },

          // move to mark right
          [new StateSymbolPair(state: (uint)MultiplyStates.MoveToMarkInD_R, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToMarkInD_R,
                    symbol: 0,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.MoveToMarkInD_R, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToMarkInD_R,
                    symbol: 1,
                    direction: TMDirection.R
                  )
              },

          // mark reached
          [new StateSymbolPair(state: (uint)MultiplyStates.MoveToMarkInD_R, symbol: markD0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveMarkInD,
                    symbol: 0,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)MultiplyStates.MoveToMarkInD_R, symbol: markD1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveMarkInD,
                    symbol: 1,
                    direction: TMDirection.R
                  )
              },

          // replace with mark
          [new StateSymbolPair(state: (uint)MultiplyStates.MoveMarkInD, symbol: 0)] =
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
          [new StateSymbolPair(state: (uint)MultiplyStates.MoveMarkInD, symbol: 1)] =
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
          [new StateSymbolPair(state: (uint)MultiplyStates.MoveMarkInD, symbol: markD0)] =
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
          [new StateSymbolPair(state: (uint)MultiplyStates.MoveMarkInD, symbol: markD1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToCLeft,
                    symbol: markD1,
                    direction: TMDirection.L,
                    shift: frameLength
                  )
              }
        };
    }

    #endregion
  } */
}

////////////////////////////////////////////////////////////////////////////////////////////////////
