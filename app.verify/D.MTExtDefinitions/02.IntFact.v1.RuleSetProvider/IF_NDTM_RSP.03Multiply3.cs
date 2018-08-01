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
  public static partial class IF_NDTM_RSP_Multiply
  {
    #region public members

    public static IReadOnlyDictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> Delta3(int frameLength)
    {
      return new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
      {
        // 1 in C
        // move to D
        [new StateSymbolPair(state: (uint)IF_NDTM.MultiplyStates.StartAddC, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.AddC1f_D,
                    symbol: IF_NDTM.markC1,
                    direction: TMDirection.R,
                    shift: frameLength
                  )
              },

        // move in D
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.AddC1f_D,
            symbol: OneTapeTuringMachine.blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.AddC1f_D,
                    symbol: 0,
                    direction: TMDirection.R
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.MultiplyStates.AddC1f_D, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.AddC1f_D,
                    symbol: 0,
                    direction: TMDirection.R
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.MultiplyStates.AddC1f_D, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.AddC1f_D,
                    symbol: 1,
                    direction: TMDirection.R
                  )
              },

        // markD reached
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.AddC1f_D,
            symbol: IF_NDTM.markD0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.AddStates.StartAdding,
                    symbol: IF_NDTM.markD0,
                    direction: TMDirection.S
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.AddC1f_D,
            symbol: IF_NDTM.markD1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.AddStates.StartAdding,
                    symbol: IF_NDTM.markD1,
                    direction: TMDirection.S
                  )
              },

        // shift mark
        // move to left delimiter
        [new StateSymbolPair(state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInD_L, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInD_L,
                    symbol: 0,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInD_L, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInD_L,
                    symbol: 1,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInD_L,
            symbol: IF_NDTM.markD0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInD_L,
                    symbol: IF_NDTM.markD0,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInD_L,
            symbol: IF_NDTM.markD1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInD_L,
                    symbol: IF_NDTM.markD1,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInD_L,
            symbol: IF_NDTM.delimiter3)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInD_R,
                    symbol: IF_NDTM.delimiter3,
                    direction: TMDirection.R
                  )
              },

        // move to mark right
        [new StateSymbolPair(state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInD_R, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInD_R,
                    symbol: 0,
                    direction: TMDirection.R
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInD_R, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInD_R,
                    symbol: 1,
                    direction: TMDirection.R
                  )
              },

        // mark reached
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInD_R,
            symbol: IF_NDTM.markD0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveMarkInD,
                    symbol: 0,
                    direction: TMDirection.R
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInD_R,
            symbol: IF_NDTM.markD1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveMarkInD,
                    symbol: 1,
                    direction: TMDirection.R
                  )
              },

        // replace with mark
        [new StateSymbolPair(state: (uint)IF_NDTM.MultiplyStates.MoveMarkInD, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToCLeft,
                    symbol: IF_NDTM.markD0,
                    direction: TMDirection.L,
                    shift: frameLength
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.MultiplyStates.MoveMarkInD, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToCLeft,
                    symbol: IF_NDTM.markD1,
                    direction: TMDirection.L,
                    shift: frameLength
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.MoveMarkInD,
            symbol: IF_NDTM.markD0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToCLeft,
                    symbol: IF_NDTM.markD0,
                    direction: TMDirection.L,
                    shift: frameLength
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.MoveMarkInD,
            symbol: IF_NDTM.markD1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToCLeft,
                    symbol: IF_NDTM.markD1,
                    direction: TMDirection.L,
                    shift: frameLength
                  )
              }
      };
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
