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

    public static IReadOnlyDictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> Delta2(int frameLength)
    {
      return new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
      {
        // 0 in C
        // move to D
        [new StateSymbolPair(state: (uint)IF_NDTM.MultiplyStates.StartAddC, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.AddC0f_D,
                    symbol: IF_NDTM.markC0,
                    direction: TMDirection.R,
                    shift: frameLength
                  )
              },

        // move in D
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.AddC0f_D,
            symbol: OneTapeTuringMachine.blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.AddC0f_D,
                    symbol: 0,
                    direction: TMDirection.R
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.MultiplyStates.AddC0f_D, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.AddC0f_D,
                    symbol: 0,
                    direction: TMDirection.R
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.MultiplyStates.AddC0f_D, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.AddC0f_D,
                    symbol: 1,
                    direction: TMDirection.R
                  )
              },

        // markD reached
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.AddC0f_D,
            symbol: IF_NDTM.markD0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.AddC0f_sm_D,
                    symbol: 0,
                    direction: TMDirection.R
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.AddC0f_D,
            symbol: IF_NDTM.markD1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.AddC0f_sm_D,
                    symbol: 1,
                    direction: TMDirection.R
                  )
              },

        // set new mark and move to C
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.AddC0f_sm_D,
            symbol: OneTapeTuringMachine.blankSymbol)] =
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
        [new StateSymbolPair(state: (uint)IF_NDTM.MultiplyStates.AddC0f_sm_D, symbol: 0)] =
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
        [new StateSymbolPair(state: (uint)IF_NDTM.MultiplyStates.AddC0f_sm_D, symbol: 1)] =
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
            state: (uint)IF_NDTM.MultiplyStates.AddC0f_sm_D,
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
            state: (uint)IF_NDTM.MultiplyStates.AddC0f_sm_D,
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
              },

        // move to mark C
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.MoveToCLeft,
            symbol: OneTapeTuringMachine.blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToCLeft,
                    symbol: OneTapeTuringMachine.blankSymbol,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.MultiplyStates.MoveToCLeft, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToCLeft,
                    symbol: 0,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.MultiplyStates.MoveToCLeft, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToCLeft,
                    symbol: 1,
                    direction: TMDirection.L
                  )
              },

        // set new mark
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.MoveToCLeft,
            symbol: IF_NDTM.markC0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.SetMarkInC,
                    symbol: IF_NDTM.markC0,
                    direction: TMDirection.R
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.MoveToCLeft,
            symbol: IF_NDTM.markC1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.SetMarkInC,
                    symbol: IF_NDTM.markC1,
                    direction: TMDirection.R
                  )
              },

        // start new iteration
        [new StateSymbolPair(state: (uint)IF_NDTM.MultiplyStates.SetMarkInC, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.StartAddC,
                    symbol: 0,
                    direction: TMDirection.S
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.MultiplyStates.SetMarkInC, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.StartAddC,
                    symbol: 1,
                    direction: TMDirection.S
                  )
              },

        // move to mark in B
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.SetMarkInC,
            symbol: OneTapeTuringMachine.blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInB,
                    symbol: OneTapeTuringMachine.blankSymbol,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInB, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInB,
                    symbol: 0,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInB, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInB,
                    symbol: 1,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInB,
            symbol: IF_NDTM.markC0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInB,
                    symbol: IF_NDTM.markC0,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInB,
            symbol: IF_NDTM.markC1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInB,
                    symbol: IF_NDTM.markC1,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInB,
            symbol: IF_NDTM.delimiter2)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInB_inB,
                    symbol: IF_NDTM.delimiter2,
                    direction: TMDirection.L
                  )
              },

        //  inB
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInB_inB,
            symbol: OneTapeTuringMachine.blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInB_inB,
                    symbol: OneTapeTuringMachine.blankSymbol,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInB_inB, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInB_inB,
                    symbol: 0,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInB_inB, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInB_inB,
                    symbol: 1,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInB_inB,
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
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInB_inB,
            symbol: IF_NDTM.markB1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MultReady,
                    symbol: IF_NDTM.markB1,
                    direction: TMDirection.R
                  )
              }
      };
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
