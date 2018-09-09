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
  public static partial class IF_NDTM_RSP_Multiply
  {
    #region public members

    public static IReadOnlyDictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> Delta13 { get; } =
      new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
      {
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter0_III,
            symbol: OneTapeTuringMachine.blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter0_III,
                    symbol: OneTapeTuringMachine.blankSymbol,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter0_III, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter0_III,
                    symbol: 0,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter0_III, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter0_III,
                    symbol: 1,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter0_III,
            symbol: IF_NDTM.delimiter1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter0_III,
                    symbol: IF_NDTM.delimiter1,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter0_III,
            symbol: IF_NDTM.delimiter2)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter0_III,
                    symbol: IF_NDTM.delimiter2,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter0_III,
            symbol: IF_NDTM.delimiter3)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter0_III,
                    symbol: IF_NDTM.delimiter3,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter0_III,
            symbol: IF_NDTM.markB0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter0_III,
                    symbol: IF_NDTM.markB0,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter0_III,
            symbol: IF_NDTM.markB1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter0_III,
                    symbol: IF_NDTM.markB1,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter0_III,
            symbol: IF_NDTM.markC0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter0_III,
                    symbol: 0,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter0_III,
            symbol: IF_NDTM.markC1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter0_III,
                    symbol: 1,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter0_III,
            symbol: IF_NDTM.markD2)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter0_III,
                    symbol: IF_NDTM.markD2,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter0_III,
            symbol: IF_NDTM.markD3)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter0_III,
                    symbol: IF_NDTM.markD3,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter0_III,
            symbol: IF_NDTM.delimiter0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter1_III,
                    symbol: IF_NDTM.delimiter0,
                    direction: TMDirection.R
                  )
              }
      };

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
