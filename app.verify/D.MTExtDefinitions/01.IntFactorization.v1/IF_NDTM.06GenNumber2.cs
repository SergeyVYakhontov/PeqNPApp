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
  public partial class IF_NDTM
  {
    #region private members

    private Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> deltaGenNumber2(int frameLength)
    {
      return new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
        {
        // start generating bits
        // generate bit 0 or 1
        [new StateSymbolPair(state: (uint)GenNumber2States.GenBitA, symbol: blankSymbol)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)GenNumber2States.GenBitA,
              symbol: 0,
              direction: TMDirection.R
            ),
            new StateSymbolDirectionTriple
            (
              state: (uint)GenNumber2States.GenBitB,
              symbol: 1,
              direction: TMDirection.R
            )
          },

        [new StateSymbolPair(state: (uint)GenNumber2States.GenBitB, symbol: blankSymbol)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)GenNumber2States.GenBitB,
              symbol: 0,
              direction: TMDirection.R
            ),
            new StateSymbolDirectionTriple
            (
              state: (uint)GenNumber2States.GenBitB,
              symbol: 1,
              direction: TMDirection.R
            ),
            new StateSymbolDirectionTriple
            (
              state: (uint)GenNumber2States.MoveRightToDelim3,
              symbol: blankSymbol,
              direction: TMDirection.R
            )
          },

        // delimiter reached
        [new StateSymbolPair(state: (uint)GenNumber2States.GenBitA, symbol: delimiter3)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: rejectingState,
              symbol: delimiter3,
              direction: TMDirection.S
            )
          },

        [new StateSymbolPair(state: (uint)GenNumber2States.GenBitB, symbol: delimiter3)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: rejectingState,
              symbol: delimiter3,
              direction: TMDirection.S
            )
          },

        [new StateSymbolPair(state: (uint)GenNumber2States.MoveRightToDelim3, symbol: blankSymbol)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)GenNumber2States.MoveRightToDelim3,
              symbol: blankSymbol,
              direction: TMDirection.R
            )
          },
        [new StateSymbolPair(state: (uint)GenNumber2States.MoveRightToDelim3, symbol: delimiter3)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)MultiplyStates.MultReady,
              symbol: delimiter3,
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
