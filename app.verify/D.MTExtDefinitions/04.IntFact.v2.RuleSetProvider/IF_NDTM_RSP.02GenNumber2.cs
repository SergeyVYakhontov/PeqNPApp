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
  public static partial class IF_NDTM_RSP_GenNumber
  {
    #region public members

    public static IReadOnlyDictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> Delta2 { get; } =
      new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
      {
        // start generating bits
        // generate bit 0 or 1
        [new StateSymbolPair(
          state: (uint)IF_NDTM.GenNumber2States.GenBitA,
          symbol: OneTapeTuringMachine.blankSymbol)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)IF_NDTM.GenNumber2States.GenBitA,
              symbol: 0,
              direction: TMDirection.R
            ),
            new StateSymbolDirectionTriple
            (
              state: (uint)IF_NDTM.GenNumber2States.GenBitB,
              symbol: 1,
              direction: TMDirection.R
            )
          },

        [new StateSymbolPair(
            state: (uint)IF_NDTM.GenNumber2States.GenBitB,
            symbol: OneTapeTuringMachine.blankSymbol)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)IF_NDTM.GenNumber2States.GenBitB,
              symbol: 0,
              direction: TMDirection.R
            ),
            new StateSymbolDirectionTriple
            (
              state: (uint)IF_NDTM.GenNumber2States.GenBitB,
              symbol: 1,
              direction: TMDirection.R
            ),
            new StateSymbolDirectionTriple
            (
              state: (uint)IF_NDTM.GenNumber2States.MoveToDelimiter3,
              symbol: OneTapeTuringMachine.blankSymbol,
              direction: TMDirection.R
            )
          },

        // delimiter reached
        [new StateSymbolPair(
            state: (uint)IF_NDTM.GenNumber2States.GenBitA,
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

        [new StateSymbolPair(
            state: (uint)IF_NDTM.GenNumber2States.GenBitB,
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

        [new StateSymbolPair(
            state: (uint)IF_NDTM.GenNumber2States.MoveToDelimiter3,
            symbol: OneTapeTuringMachine.blankSymbol)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)IF_NDTM.GenNumber2States.MoveToDelimiter3,
              symbol: OneTapeTuringMachine.blankSymbol,
              direction: TMDirection.R
            )
          },

        [new StateSymbolPair(
            state: (uint)IF_NDTM.GenNumber2States.MoveToDelimiter3,
            symbol: IF_NDTM.delimiter3)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)IF_NDTM.GenNumber2States.MoveToDelimiter4,
              symbol: IF_NDTM.delimiter3,
              direction: TMDirection.R
            )
          },

        [new StateSymbolPair(
            state: (uint)IF_NDTM.GenNumber2States.MoveToDelimiter4,
            symbol: OneTapeTuringMachine.blankSymbol)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)IF_NDTM.GenNumber2States.MoveToDelimiter4,
              symbol: OneTapeTuringMachine.blankSymbol,
              direction: TMDirection.R
            )
          },

        [new StateSymbolPair(
            state: (uint)IF_NDTM.GenNumber2States.MoveToDelimiter4,
            symbol: IF_NDTM.delimiter4)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)IF_NDTM.GenNumber2States.MoveToDelimiter0,
              symbol: IF_NDTM.delimiter4,
              direction: TMDirection.L
            )
          },

        [new StateSymbolPair(
            state: (uint)IF_NDTM.GenNumber2States.MoveToDelimiter0,
            symbol: OneTapeTuringMachine.blankSymbol)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)IF_NDTM.GenNumber2States.MoveToDelimiter0,
              symbol: OneTapeTuringMachine.blankSymbol,
              direction: TMDirection.L
            )
          },

        [new StateSymbolPair(state: (uint)IF_NDTM.GenNumber2States.MoveToDelimiter0, symbol: 0)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)IF_NDTM.GenNumber2States.MoveToDelimiter0,
              symbol: 0,
              direction: TMDirection.L
            )
          },

        [new StateSymbolPair(state: (uint)IF_NDTM.GenNumber2States.MoveToDelimiter0, symbol: 1)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)IF_NDTM.GenNumber2States.MoveToDelimiter0,
              symbol: 1,
              direction: TMDirection.L
            )
          },

        [new StateSymbolPair(
            state: (uint)IF_NDTM.GenNumber2States.MoveToDelimiter0,
            symbol: IF_NDTM.delimiter3)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)IF_NDTM.GenNumber2States.MoveToDelimiter0,
              symbol: IF_NDTM.delimiter3,
              direction: TMDirection.L
            )
          },

        [new StateSymbolPair(
            state: (uint)IF_NDTM.GenNumber2States.MoveToDelimiter0,
            symbol: IF_NDTM.delimiter2)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)IF_NDTM.GenNumber2States.MoveToDelimiter0,
              symbol: IF_NDTM.delimiter2,
              direction: TMDirection.L
            )
          },

        [new StateSymbolPair(
            state: (uint)IF_NDTM.GenNumber2States.MoveToDelimiter0,
            symbol: IF_NDTM.delimiter1)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)IF_NDTM.GenNumber2States.MoveToDelimiter0,
              symbol: IF_NDTM.delimiter1,
              direction: TMDirection.L
            )
          },

        [new StateSymbolPair(
            state: (uint)IF_NDTM.GenNumber2States.MoveToDelimiter0,
            symbol: IF_NDTM.delimiter0)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)IF_NDTM.GenNumber2States.MoveToDelimiter1,
              symbol: IF_NDTM.delimiter0,
              direction: TMDirection.R
            )
          },

        [new StateSymbolPair(
            state: (uint)IF_NDTM.GenNumber2States.MoveToDelimiter1,
            symbol: OneTapeTuringMachine.blankSymbol)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)IF_NDTM.GenNumber2States.MoveToDelimiter1,
              symbol: OneTapeTuringMachine.blankSymbol,
              direction: TMDirection.R
            )
          },

        [new StateSymbolPair(state: (uint)IF_NDTM.GenNumber2States.MoveToDelimiter1, symbol: 0)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)IF_NDTM.GenNumber2States.MoveToDelimiter1,
              symbol: 0,
              direction: TMDirection.R
            )
          },

        [new StateSymbolPair(state: (uint)IF_NDTM.GenNumber2States.MoveToDelimiter1, symbol: 1)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)IF_NDTM.GenNumber2States.MoveToDelimiter1,
              symbol: 1,
              direction: TMDirection.R
            )
          },

        [new StateSymbolPair(
            state: (uint)IF_NDTM.GenNumber2States.MoveToDelimiter1,
            symbol: IF_NDTM.delimiter1)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)IF_NDTM.MultiplyStates.MultReady,
              symbol: IF_NDTM.delimiter1,
              direction: TMDirection.R
            )
          }
      };

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
