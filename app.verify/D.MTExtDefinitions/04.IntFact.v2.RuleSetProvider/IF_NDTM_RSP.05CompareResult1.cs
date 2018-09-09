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
  public static partial class IF_NDTM_RSP_CompareResult
  {
    #region public members

    public static IReadOnlyDictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> Delta1 { get; } =
      new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
      {
        // start comparing
        [new StateSymbolPair(
            state: (uint)IF_NDTM.CompareStates.StartComparing,
            symbol: OneTapeTuringMachine.blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.MoveToStartA,
                    symbol: OneTapeTuringMachine.blankSymbol,
                    direction: TMDirection.L
                  )
              },

        // move to start A
        [new StateSymbolPair(
            state: (uint)IF_NDTM.CompareStates.MoveToStartA,
            symbol: OneTapeTuringMachine.blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.MoveToStartA,
                    symbol: OneTapeTuringMachine.blankSymbol,
                    direction: TMDirection.L
                  )
            },
        [new StateSymbolPair(state: (uint)IF_NDTM.CompareStates.MoveToStartA, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.MoveToStartA,
                    symbol: 0,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.CompareStates.MoveToStartA, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.MoveToStartA,
                    symbol: 1,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.CompareStates.MoveToStartA,
            symbol: IF_NDTM.markB0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.MoveToStartA,
                    symbol: 0,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.CompareStates.MoveToStartA,
            symbol: IF_NDTM.markB1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.MoveToStartA,
                    symbol: 1,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.CompareStates.MoveToStartA,
            symbol: IF_NDTM.delimiter1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.MoveToStartA,
                    symbol: IF_NDTM.delimiter1,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.CompareStates.MoveToStartA,
            symbol: IF_NDTM.delimiter0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.BitLoopStart,
                    symbol: IF_NDTM.delimiter0,
                    direction: TMDirection.R
                  )
              }
      };

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
