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
/*  public partial class IF_NDTM
  {
    #region private members

    private static readonly Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> deltaGenNumber2 =
      new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
      {
        // start generating bits
        // generate bit 0 or 1
        [new StateSymbolPair(state: (uint)GenNumber2States.GenBitA, symbol: blankSymbol)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)GenNumber2States.GenBitA,
              symbol: 0,
              direction: TMDirection.R
            ),
            new StateSymbolDirectionTriple
            (
              state: (uint)GenNumber2States.GenBitB,
              symbol: 1,
              direction: TMDirection.R
            )
          },

        [new StateSymbolPair(state: (uint)GenNumber2States.GenBitB, symbol: blankSymbol)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)GenNumber2States.GenBitB,
              symbol: 0,
              direction: TMDirection.R
            ),
            new StateSymbolDirectionTriple
            (
              state: (uint)GenNumber2States.GenBitB,
              symbol: 1,
              direction: TMDirection.R
            ),
            new StateSymbolDirectionTriple
            (
              state: (uint)GenNumber2States.MoveToDelimiter3,
              symbol: blankSymbol,
              direction: TMDirection.R
            )
          },

        // delimiter reached
        [new StateSymbolPair(state: (uint)GenNumber2States.GenBitA, symbol: delimiter3)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: rejectingState,
              symbol: delimiter3,
              direction: TMDirection.S
            )
          },

        [new StateSymbolPair(state: (uint)GenNumber2States.GenBitB, symbol: delimiter3)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: rejectingState,
              symbol: delimiter3,
              direction: TMDirection.S
            )
          },

        [new StateSymbolPair(state: (uint)GenNumber2States.MoveToDelimiter3, symbol: blankSymbol)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)GenNumber2States.MoveToDelimiter3,
              symbol: blankSymbol,
              direction: TMDirection.R
            )
          },

        [new StateSymbolPair(state: (uint)GenNumber2States.MoveToDelimiter3, symbol: delimiter3)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)GenNumber2States.MoveToDelimiter4,
              symbol: delimiter3,
              direction: TMDirection.R
            )
          },

        [new StateSymbolPair(state: (uint)GenNumber2States.MoveToDelimiter4, symbol: blankSymbol)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)GenNumber2States.MoveToDelimiter4,
              symbol: blankSymbol,
              direction: TMDirection.R
            )
          },

        [new StateSymbolPair(state: (uint)GenNumber2States.MoveToDelimiter4, symbol: delimiter4)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)GenNumber2States.MoveToDelimiter0,
              symbol: delimiter4,
              direction: TMDirection.L
            )
          },

        [new StateSymbolPair(state: (uint)GenNumber2States.MoveToDelimiter0, symbol: blankSymbol)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)GenNumber2States.MoveToDelimiter0,
              symbol: blankSymbol,
              direction: TMDirection.L
            )
          },

        [new StateSymbolPair(state: (uint)GenNumber2States.MoveToDelimiter0, symbol: 0)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)GenNumber2States.MoveToDelimiter0,
              symbol: 0,
              direction: TMDirection.L
            )
          },

        [new StateSymbolPair(state: (uint)GenNumber2States.MoveToDelimiter0, symbol: 1)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)GenNumber2States.MoveToDelimiter0,
              symbol: 1,
              direction: TMDirection.L
            )
          },


        [new StateSymbolPair(state: (uint)GenNumber2States.MoveToDelimiter0, symbol: delimiter3)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)GenNumber2States.MoveToDelimiter0,
              symbol: delimiter3,
              direction: TMDirection.L
            )
          },

        [new StateSymbolPair(state: (uint)GenNumber2States.MoveToDelimiter0, symbol: delimiter2)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)GenNumber2States.MoveToDelimiter0,
              symbol: delimiter2,
              direction: TMDirection.L
            )
          },

        [new StateSymbolPair(state: (uint)GenNumber2States.MoveToDelimiter0, symbol: delimiter1)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)GenNumber2States.MoveToDelimiter0,
              symbol: delimiter1,
              direction: TMDirection.L
            )
          },

        [new StateSymbolPair(state: (uint)GenNumber2States.MoveToDelimiter0, symbol: delimiter0)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)GenNumber2States.MoveToDelimiter1,
              symbol: delimiter0,
              direction: TMDirection.R
            )
          },

        [new StateSymbolPair(state: (uint)GenNumber2States.MoveToDelimiter1, symbol: blankSymbol)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)GenNumber2States.MoveToDelimiter1,
              symbol: blankSymbol,
              direction: TMDirection.R
            )
          },

        [new StateSymbolPair(state: (uint)GenNumber2States.MoveToDelimiter1, symbol: 0)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)GenNumber2States.MoveToDelimiter1,
              symbol: 0,
              direction: TMDirection.R
            )
          },

        [new StateSymbolPair(state: (uint)GenNumber2States.MoveToDelimiter1, symbol: 1)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)GenNumber2States.MoveToDelimiter1,
              symbol: 1,
              direction: TMDirection.R
            )
          },

        [new StateSymbolPair(state: (uint)GenNumber2States.MoveToDelimiter1, symbol: delimiter1)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)MultiplyStates.MultReady,
              symbol: delimiter1,
              direction: TMDirection.R
            )
          }
      };

    #endregion
  } */
}

////////////////////////////////////////////////////////////////////////////////////////////////////
