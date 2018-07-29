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
  public static partial class IF_NDTM_RSP_GenNumber
  {
    #region public members

    public static Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> Delta2(int frameLength)
    {
      return new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
      {
        // start generating bits
        // generate bit 0 or 1
        [new StateSymbolPair(state: (uint)IF_NDTM.GenNumber2States.GenBitA, symbol: OneTapeTuringMachine.blankSymbol)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)IF_NDTM.GenNumber2States.GenBitA,
              symbol: 0,
              direction: TMDirection.R
            ),
            new StateSymbolDirectionTriple
            (
              state: (uint)IF_NDTM.GenNumber2States.GenBitB,
              symbol: 1,
              direction: TMDirection.R
            )
          },

        [new StateSymbolPair(state: (uint)IF_NDTM.GenNumber2States.GenBitB, symbol: OneTapeTuringMachine.blankSymbol)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)IF_NDTM.GenNumber2States.GenBitB,
              symbol: 0,
              direction: TMDirection.R
            ),
            new StateSymbolDirectionTriple
            (
              state: (uint)IF_NDTM.GenNumber2States.GenBitB,
              symbol: 1,
              direction: TMDirection.R
            ),
            new StateSymbolDirectionTriple
            (
              state: (uint)IF_NDTM.GenNumber2States.MoveRightToDelim3,
              symbol: OneTapeTuringMachine.blankSymbol,
              direction: TMDirection.R
            )
          },

        // delimiter reached
        [new StateSymbolPair(state: (uint)IF_NDTM.GenNumber2States.GenBitA, symbol: IF_NDTM.delimiter3)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: IF_NDTM.rejectingState,
              symbol: IF_NDTM.delimiter3,
              direction: TMDirection.S
            )
          },

        [new StateSymbolPair(state: (uint)IF_NDTM.GenNumber2States.GenBitB, symbol: IF_NDTM.delimiter3)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: IF_NDTM.rejectingState,
              symbol: IF_NDTM.delimiter3,
              direction: TMDirection.S
            )
          },

        [new StateSymbolPair(state: (uint)IF_NDTM.GenNumber2States.MoveRightToDelim3, symbol: OneTapeTuringMachine.blankSymbol)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)IF_NDTM.GenNumber2States.MoveRightToDelim3,
              symbol: OneTapeTuringMachine.blankSymbol,
              direction: TMDirection.R
            )
          },
        [new StateSymbolPair(state: (uint)IF_NDTM.GenNumber2States.MoveRightToDelim3, symbol: IF_NDTM.delimiter3)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)IF_NDTM.MultiplyStates.MultReady,
              symbol: IF_NDTM.delimiter3,
              direction: TMDirection.L,
              shift: (frameLength * 2) - 1
            )
          }
      };
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
