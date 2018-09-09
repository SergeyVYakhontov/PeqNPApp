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
  public static class IF_NDTM_RSP_Add
  {
    #region public members

    public static IReadOnlyDictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> Delta { get; } =
      new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
      {
        // add bit 0
        [new StateSymbolPair(
            state: (uint)IF_NDTM.AddStates.AddBit0,
            symbol: OneTapeTuringMachine.blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.AddStates.AddBit0,
                    symbol: OneTapeTuringMachine.blankSymbol,
                    direction: TMDirection.R
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.AddStates.AddBit0, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.AddStates.AddBit0,
                    symbol: 0,
                    direction: TMDirection.R
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.AddStates.AddBit0, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.AddStates.AddBit0,
                    symbol: 1,
                    direction: TMDirection.R
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.AddStates.AddBit0,
            symbol: IF_NDTM.delimiter4)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToDelimeter0_Bit1_1II,
                    symbol: IF_NDTM.delimiter4,
                    direction: TMDirection.L
                  )
              },
        // add bit 1
        [new StateSymbolPair(
            state: (uint)IF_NDTM.AddStates.AddBit1,
            symbol: OneTapeTuringMachine.blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.AddStates.AddBit0,
                    symbol: 1,
                    direction: TMDirection.R
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.AddStates.AddBit1, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.AddStates.AddBit0,
                    symbol: 1,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)IF_NDTM.AddStates.AddBit1, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.AddStates.AddBit1,
                    symbol: 0,
                    direction: TMDirection.R
                  )
              }
      };

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
