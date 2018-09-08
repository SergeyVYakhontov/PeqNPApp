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

    public static IReadOnlyDictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> Delta16 { get; } =
      new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
      {
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter0_IV,
            symbol: OneTapeTuringMachine.blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter0_IV,
                    symbol: OneTapeTuringMachine.blankSymbol,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter0_IV, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter0_IV,
                    symbol: 0,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter0_IV, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter0_IV,
                    symbol: 1,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter0_IV,
            symbol: IF_NDTM.delimiter3)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter0_IV,
                    symbol: IF_NDTM.delimiter3,
                    direction: TMDirection.L
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter0_IV,
            symbol: IF_NDTM.delimiter2)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.StartComparing,
                    symbol: IF_NDTM.delimiter2,
                    direction: TMDirection.L
                  )
              }
      };

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
