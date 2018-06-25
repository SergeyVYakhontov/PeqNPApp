////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;
using ExistsAcceptingPath;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace MTExtDefinitions
{
  public partial class IF_NDTM
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
              {
                State = (int)InitStates.MoveToRightDelim,
                Symbol = 0,
                Direction = TMDirection.R
               }
            },
        [new StateSymbolPair(state: qStartState, symbol: 1)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            {
              State = (int)InitStates.MoveToRightDelim,
              Symbol = 1,
              Direction = TMDirection.R
            }
          },
        [new StateSymbolPair(state: (uint)InitStates.MoveToRightDelim, symbol: blankSymbol)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
              {
                State = (int)InitStates.MoveToRightDelim,
                Symbol = blankSymbol,
                Direction = TMDirection.R
              }
            },
        [new StateSymbolPair(state: (uint)InitStates.MoveToRightDelim, symbol: 0)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
              {
                State = (int)InitStates.MoveToRightDelim,
                Symbol = 0,
                Direction = TMDirection.R
              }
            },
        [new StateSymbolPair(state: (uint)InitStates.MoveToRightDelim, symbol: 1)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
              {
                State = (int)InitStates.MoveToRightDelim,
                Symbol = 1,
                Direction = TMDirection.R
              }
            },
        [new StateSymbolPair(state: (uint)InitStates.MoveToRightDelim, symbol: delimiter)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
              {
                State = (int)InitStates.StopInit,
                Symbol = delimiter,
                Direction = TMDirection.R
              }
            }
      };
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
