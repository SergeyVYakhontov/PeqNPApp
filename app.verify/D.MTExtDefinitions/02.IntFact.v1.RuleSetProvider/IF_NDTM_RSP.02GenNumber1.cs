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

    public static Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> Delta1()
    {
      return new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
      {
        // start generating bits
        // generate bit 0 or 1
        [new StateSymbolPair(state: (uint)IF_NDTM.GenNumber1States.GenBitA, symbol: OneTapeTuringMachine.blankSymbol)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)IF_NDTM.GenNumber1States.GenBitA,
              symbol: 0,
              direction: TMDirection.R
            ),
            new StateSymbolDirectionTriple
            (
              state: (uint)IF_NDTM.GenNumber1States.GenBitB,
              symbol: 1,
              direction: TMDirection.R
            )
          },

        [new StateSymbolPair(state: (uint)IF_NDTM.GenNumber1States.GenBitB, symbol: OneTapeTuringMachine.blankSymbol)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)IF_NDTM.GenNumber1States.GenBitB,
              symbol: 0,
              direction: TMDirection.R
            ),
            new StateSymbolDirectionTriple
            (
              state: (uint)IF_NDTM.GenNumber1States.GenBitB,
              symbol: 1,
              direction: TMDirection.R
            ),
            new StateSymbolDirectionTriple
            (
              state: (uint)IF_NDTM.GenNumber1States.MoveRightToDelim2,
              symbol: OneTapeTuringMachine.blankSymbol,
              direction: TMDirection.R
            )
          },

        // delimiter reached
        [new StateSymbolPair(state: (uint)IF_NDTM.GenNumber1States.GenBitA, symbol: IF_NDTM.delimiter2)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: IF_NDTM.rejectingState,
              symbol: IF_NDTM.delimiter2,
              direction: TMDirection.S
            )
          },

        [new StateSymbolPair(state: (uint)IF_NDTM.GenNumber1States.GenBitB, symbol: IF_NDTM.delimiter2)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: IF_NDTM.rejectingState,
              symbol: IF_NDTM.delimiter2,
              direction: TMDirection.S
            )
          },

        [new StateSymbolPair(state: (uint)IF_NDTM.GenNumber1States.MoveRightToDelim2, symbol: OneTapeTuringMachine.blankSymbol)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)IF_NDTM.GenNumber1States.MoveRightToDelim2,
              symbol: OneTapeTuringMachine.blankSymbol,
              direction: TMDirection.R
            )
          },
        [new StateSymbolPair(state: (uint)IF_NDTM.GenNumber1States.MoveRightToDelim2, symbol: IF_NDTM.delimiter2)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (int)IF_NDTM.GenNumber2States.GenBitA,
              symbol: IF_NDTM.delimiter2,
              direction: TMDirection.R
            )
          }
      };
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
