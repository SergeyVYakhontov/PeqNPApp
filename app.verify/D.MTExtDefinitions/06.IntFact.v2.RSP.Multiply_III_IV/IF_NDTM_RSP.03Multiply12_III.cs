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

    public static IReadOnlyDictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> Delta12 { get; } =
      new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
      {
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.MoveTo01InC_0I,
            symbol: IF_NDTM.delimiter3)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToD01InD_III,
                    symbol: IF_NDTM.delimiter3,
                    direction: TMDirection.R
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.MoveTo01InC_1I,
            symbol: IF_NDTM.delimiter3)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToD01InD_III,
                    symbol: IF_NDTM.delimiter3,
                    direction: TMDirection.R
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.MoveToD01InD_III,
            symbol: IF_NDTM.markD2)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToD01InD_III,
                    symbol: IF_NDTM.markD2,
                    direction: TMDirection.R
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.MoveToD01InD_III,
            symbol: IF_NDTM.markD3)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToD01InD_III,
                    symbol: IF_NDTM.markD3,
                    direction: TMDirection.R
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.MoveToD01InD_III,
            symbol: IF_NDTM.markD0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter4_III,
                    symbol: IF_NDTM.markD2,
                    direction: TMDirection.R
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.MoveToD01InD_III,
            symbol: IF_NDTM.markD1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter4_III,
                    symbol: IF_NDTM.markD3,
                    direction: TMDirection.R
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter4_III,
            symbol: OneTapeTuringMachine.blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter4_III,
                    symbol: OneTapeTuringMachine.blankSymbol,
                    direction: TMDirection.R
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter4_III, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter4_III,
                    symbol: 0,
                    direction: TMDirection.R
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter4_III, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter4_III,
                    symbol: 1,
                    direction: TMDirection.R
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter4_III,
            symbol: IF_NDTM.markD0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter4_III,
                    symbol: 0,
                    direction: TMDirection.R
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter4_III,
            symbol: IF_NDTM.markD1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter4_III,
                    symbol: 1,
                    direction: TMDirection.R
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter4_III,
            symbol: IF_NDTM.delimiter4)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter0_III,
                    symbol: IF_NDTM.delimiter4,
                    direction: TMDirection.L
                  )
              }
      };

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
