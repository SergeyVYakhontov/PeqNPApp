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

    public static IReadOnlyDictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> Delta6 { get; } =
      new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
      {
        // skip E marks
        [new StateSymbolPair(
            state: (uint)IF_NDTM.CompareStates.SkipE,
            symbol: IF_NDTM.markE0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.SkipE,
                    symbol: IF_NDTM.markE0,
                    direction: TMDirection.R
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.CompareStates.SkipE,
            symbol: IF_NDTM.markE1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.SkipE,
                    symbol: IF_NDTM.markE1,
                    direction: TMDirection.R
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.CompareStates.SkipE, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.MoveToDelimiter3_bit0,
                    symbol: IF_NDTM.markE0,
                    direction: TMDirection.R
                  )
              },
        [new StateSymbolPair(state: (uint)IF_NDTM.CompareStates.SkipE, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)IF_NDTM.CompareStates.MoveToDelimiter3_bit1,
                    symbol: IF_NDTM.markE0,
                    direction: TMDirection.R
                  )
              },
        [new StateSymbolPair(
            state: (uint)IF_NDTM.CompareStates.SkipE,
            symbol: OneTapeTuringMachine.blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: IF_NDTM.acceptingState,
                    symbol: OneTapeTuringMachine.blankSymbol,
                    direction: TMDirection.R
                  )
              }
      };

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
