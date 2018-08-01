////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;
using ExistsAcceptingPath;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace MTExtDefinitions.v1
{
  public static class IF_NDTM_RSP_Init
  {
    #region public members

    public static IReadOnlyDictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> Delta()
    {
      return new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
      {
        [new StateSymbolPair(state: IF_NDTM.qStartState, symbol: 0)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)IF_NDTM.InitStates.MoveRightToDelim1,
              symbol: 0,
              direction: TMDirection.R
            )
          },

        [new StateSymbolPair(state: IF_NDTM.qStartState, symbol: 1)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)IF_NDTM.InitStates.MoveRightToDelim1,
              symbol: 1,
              direction: TMDirection.R
            )
          },

        [new StateSymbolPair(
            state: (uint)IF_NDTM.InitStates.MoveRightToDelim1,
            symbol: OneTapeTuringMachine.blankSymbol)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)IF_NDTM.InitStates.MoveRightToDelim1,
              symbol: OneTapeTuringMachine.blankSymbol,
              direction: TMDirection.R
             )
          },

        [new StateSymbolPair(state: (uint)IF_NDTM.InitStates.MoveRightToDelim1, symbol: 0)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)IF_NDTM.InitStates.MoveRightToDelim1,
              symbol: 0,
              direction: TMDirection.R
            )
          },

        [new StateSymbolPair(state: (uint)IF_NDTM.InitStates.MoveRightToDelim1, symbol: 1)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)IF_NDTM.InitStates.MoveRightToDelim1,
              symbol: 1,
              direction: TMDirection.R
            )
          },

        [new StateSymbolPair(
            state: (uint)IF_NDTM.InitStates.MoveRightToDelim1,
            symbol: IF_NDTM.delimiter1)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)IF_NDTM.GenNumber1States.GenBitA,
              symbol: IF_NDTM.delimiter1,
              direction: TMDirection.R
             )
          }
      };
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
