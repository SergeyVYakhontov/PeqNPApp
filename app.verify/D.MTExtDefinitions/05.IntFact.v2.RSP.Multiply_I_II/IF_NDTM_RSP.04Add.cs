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
       /*   // start adding
          [new StateSymbolPair(
              state: (uint)IF_NDTM.AddStates.StartAdding,
              symbol: IF_NDTM.markD0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.AddStates.AddBitC0,
                    symbol: IF_NDTM.markD1,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(
              state: (uint)IF_NDTM.AddStates.StartAdding,
              symbol: IF_NDTM.markD1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.AddStates.AddBitC1,
                    symbol: IF_NDTM.markD0,
                    direction: TMDirection.R
                  )
              },

          // add bit with carry = 0
          [new StateSymbolPair(state: (uint)IF_NDTM.AddStates.AddBitC0, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInD_L,
                    symbol: 0,
                    direction: TMDirection.S
                  )
              },
          [new StateSymbolPair(state: (uint)IF_NDTM.AddStates.AddBitC0, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInD_L,
                    symbol: 1,
                    direction: TMDirection.S
                  )
              },
          [new StateSymbolPair(
              state: (uint)IF_NDTM.AddStates.AddBitC0,
              symbol: IF_NDTM.markD0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInD_L,
                    symbol: IF_NDTM.markD0,
                    direction: TMDirection.S
                  )
              },
          [new StateSymbolPair(
              state: (uint)IF_NDTM.AddStates.AddBitC0,
              symbol: IF_NDTM.markD1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInD_L,
                    symbol: IF_NDTM.markD1,
                    direction: TMDirection.S
                  )
              },

          // add bit with carry = 1
          [new StateSymbolPair(state: (uint)IF_NDTM.AddStates.AddBitC1, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInD_L,
                    symbol: 1,
                    direction: TMDirection.S
                  )
              },
          [new StateSymbolPair(state: (uint)IF_NDTM.AddStates.AddBitC1, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.AddStates.AddBitC1,
                    symbol: 0,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(
              state: (uint)IF_NDTM.AddStates.AddBitC1,
              symbol: IF_NDTM.markD0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInD_L,
                    symbol: IF_NDTM.markD1,
                    direction: TMDirection.S
                  )
              },
          [new StateSymbolPair(
              state: (uint)IF_NDTM.AddStates.AddBitC1,
              symbol: IF_NDTM.markD1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.AddStates.AddBitC1,
                    symbol: IF_NDTM.markD0,
                    direction: TMDirection.R
                  )
              },

          // blank reached
          [new StateSymbolPair(
              state: (uint)IF_NDTM.AddStates.AddBitC0,
              symbol: OneTapeTuringMachine.blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInD_L,
                    symbol: 0,
                    direction: TMDirection.S
                  )
              },
          [new StateSymbolPair(
              state: (uint)IF_NDTM.AddStates.AddBitC1,
              symbol: OneTapeTuringMachine.blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInD_L,
                    symbol: 1,
                    direction: TMDirection.S
                  )
              },
          [new StateSymbolPair(
              state: (uint)IF_NDTM.AddStates.AddBitC0,
              symbol: IF_NDTM.delimiter4)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInD_L,
                    symbol: 0,
                    direction: TMDirection.S
                  )
              },
          [new StateSymbolPair(
              state: (uint)IF_NDTM.AddStates.AddBitC1,
              symbol: IF_NDTM.delimiter4)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.MultiplyStates.MoveToMarkInD_L,
                    symbol: 1,
                    direction: TMDirection.S
                  )
              }*/
        };

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
