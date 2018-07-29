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
 /* public partial class IF_NDTM
  {
    #region private members

    private Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> deltaInit()
    {
      return new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
      {
        [new StateSymbolPair(state: qStartState, symbol: 0)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)InitStates.MoveRightToDelim1,
              symbol: 0,
              direction: TMDirection.R
            )
          },

        [new StateSymbolPair(state: qStartState, symbol: 1)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)InitStates.MoveRightToDelim1,
              symbol: 1,
              direction: TMDirection.R
            )
          },

        [new StateSymbolPair(state: (uint)InitStates.MoveRightToDelim1, symbol: blankSymbol)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)InitStates.MoveRightToDelim1,
              symbol: blankSymbol,
              direction: TMDirection.R
             )
          },

        [new StateSymbolPair(state: (uint)InitStates.MoveRightToDelim1, symbol: 0)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)InitStates.MoveRightToDelim1,
              symbol: 0,
              direction: TMDirection.R
            )
          },

        [new StateSymbolPair(state: (uint)InitStates.MoveRightToDelim1, symbol: 1)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)InitStates.MoveRightToDelim1,
              symbol: 1,
              direction: TMDirection.R
            )
          },

        [new StateSymbolPair(state: (uint)InitStates.MoveRightToDelim1, symbol: delimiter1)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)GenNumber1States.GenBitA,
              symbol: delimiter1,
              direction: TMDirection.R
             )
          }
      };
    }

    #endregion
  }*/
}

////////////////////////////////////////////////////////////////////////////////////////////////////
