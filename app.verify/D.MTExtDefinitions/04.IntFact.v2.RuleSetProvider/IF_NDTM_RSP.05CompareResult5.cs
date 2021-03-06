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

    public static IReadOnlyDictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> Delta5 { get; } =
      new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
      {
        // move to delimiter 0
        [new StateSymbolPair(
            state: (uint)IF_NDTM.CompareStates.MoveToDelimiter0,
            symbol: OneTapeTuringMachine.blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.MoveToDelimiter0,
                    symbol: OneTapeTuringMachine.blankSymbol,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.CompareStates.MoveToDelimiter0, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.MoveToDelimiter0,
                    symbol: 0,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.CompareStates.MoveToDelimiter0, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.MoveToDelimiter0,
                    symbol: 1,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.CompareStates.MoveToDelimiter0,
            symbol: IF_NDTM.delimiter1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.MoveToDelimiter0,
                    symbol: IF_NDTM.delimiter1,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.CompareStates.MoveToDelimiter0,
            symbol: IF_NDTM.delimiter2)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.MoveToDelimiter0,
                    symbol: IF_NDTM.delimiter2,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.CompareStates.MoveToDelimiter0,
            symbol: IF_NDTM.delimiter3)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.MoveToDelimiter0,
                    symbol: IF_NDTM.delimiter3,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.CompareStates.MoveToDelimiter0,
            symbol: IF_NDTM.markE0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.MoveToDelimiter0,
                    symbol: IF_NDTM.markE0,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.CompareStates.MoveToDelimiter0,
            symbol: IF_NDTM.markE1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.MoveToDelimiter0,
                    symbol: IF_NDTM.markE1,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.CompareStates.MoveToDelimiter0,
            symbol: IF_NDTM.markD0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.MoveToDelimiter0,
                    symbol: IF_NDTM.markD0,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.CompareStates.MoveToDelimiter0,
            symbol: IF_NDTM.markD1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.MoveToDelimiter0,
                    symbol: IF_NDTM.markD1,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(
           state: (uint)IF_NDTM.CompareStates.MoveToDelimiter0,
           symbol: IF_NDTM.markF0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.MoveToDelimiter0,
                    symbol: IF_NDTM.markF0,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(
           state: (uint)IF_NDTM.CompareStates.MoveToDelimiter0,
           symbol: IF_NDTM.markF1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.MoveToDelimiter0,
                    symbol: IF_NDTM.markF1,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.CompareStates.MoveToDelimiter0,
            symbol: IF_NDTM.delimiter0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.SkipE,
                    symbol: IF_NDTM.delimiter0,
                    direction: TMDirection.R
                  )
              }
      };

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
