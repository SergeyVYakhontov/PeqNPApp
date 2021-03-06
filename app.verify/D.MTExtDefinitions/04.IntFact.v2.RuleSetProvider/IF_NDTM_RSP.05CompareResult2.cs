﻿////////////////////////////////////////////////////////////////////////////////////////////////////

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
  public static partial class IF_NDTM_RSP_CompareResult
  {
    #region public members

    public static IReadOnlyDictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> Delta2 { get; } =
      new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
      {
        // shift to D, bit 0
        [new StateSymbolPair(state: (uint)IF_NDTM.CompareStates.BitLoopStart, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.MoveToDelimiter3_bit0,
                    symbol: IF_NDTM.markE0,
                    direction: TMDirection.R
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.CompareStates.MoveToDelimiter3_bit0,
            symbol: OneTapeTuringMachine.blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.MoveToDelimiter3_bit0,
                    symbol: OneTapeTuringMachine.blankSymbol,
                    direction: TMDirection.R
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.CompareStates.MoveToDelimiter3_bit0, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.MoveToDelimiter3_bit0,
                    symbol: 0,
                    direction: TMDirection.R
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.CompareStates.MoveToDelimiter3_bit0, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.MoveToDelimiter3_bit0,
                    symbol: 1,
                    direction: TMDirection.R
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.CompareStates.MoveToDelimiter3_bit0,
            symbol: IF_NDTM.delimiter1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.MoveToDelimiter3_bit0,
                    symbol: IF_NDTM.delimiter1,
                    direction: TMDirection.R
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.CompareStates.MoveToDelimiter3_bit0,
            symbol: IF_NDTM.delimiter2)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.MoveToDelimiter3_bit0,
                    symbol: IF_NDTM.delimiter2,
                    direction: TMDirection.R
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.CompareStates.MoveToDelimiter3_bit0,
            symbol: IF_NDTM.delimiter3)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.SkipF_bit0,
                    symbol: IF_NDTM.delimiter3,
                    direction: TMDirection.R
                  )
              },

        [new StateSymbolPair(
            state: (uint)IF_NDTM.CompareStates.SkipF_bit0,
            symbol: IF_NDTM.markF0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.SkipF_bit0,
                    symbol: IF_NDTM.markF0,
                    direction: TMDirection.R
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.CompareStates.SkipF_bit0,
            symbol: IF_NDTM.markF1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.SkipF_bit0,
                    symbol: IF_NDTM.markF1,
                    direction: TMDirection.R
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.CompareStates.SkipF_bit0, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.MoveToDelimiter4,
                    symbol: IF_NDTM.markF0,
                    direction: TMDirection.R
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.CompareStates.SkipF_bit0, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: IF_NDTM.rejectingState,
                    symbol: 1,
                    direction: TMDirection.R
                  )
              }
      };

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
