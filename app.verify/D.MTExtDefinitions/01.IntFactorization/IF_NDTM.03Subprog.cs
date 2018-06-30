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

    private Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> deltaSubprog1Test(int frameLength)
    {
      return new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
      {
        // start (test)
        [new StateSymbolPair(state: qStartState, symbol: 0)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            {
              State = (int)SubprogStates.MultReady,
              Symbol = 0,
              Direction = TMDirection.R,
              Shift = frameLength + 1
            }
          },
        [new StateSymbolPair(state: qStartState, symbol: 1)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            {
              State = (int)SubprogStates.MultReady,
              Symbol = 1,
              Direction = TMDirection.R,
              Shift = frameLength + 1
            }
          }
      };
    }

    private Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> deltaSubprog2(int frameLength)
    {
      return new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
      {
        // start generating number2
        [new StateSymbolPair(state: (uint)GenNumber1States.StopGenNumber, symbol: delimiter)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            {
              State = (int)SubprogStates.GenNumber2Ready,
              Symbol = delimiter,
              Direction = TMDirection.R
            }
          },

        // start multiplying
        [new StateSymbolPair(state: (uint)GenNumber2States.StopGenNumber, symbol: delimiter)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            {
              State = (int)SubprogStates.MultReady,
              Symbol = delimiter,
              Direction = TMDirection.L,
              Shift = frameLength * 2 - 1
            }
          },

        // start comparing
        [new StateSymbolPair(state: (uint)MultiplyStates.StopMultiplying, symbol: blankSymbol)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            {
              State = (int)SubprogStates.CompareReady,
              Symbol = blankSymbol,
              Direction = TMDirection.S
            }
          },
        [new StateSymbolPair(state: (uint)MultiplyStates.StopMultiplying, symbol: delimiter)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            {
              State = (int)rejectingState,
              Symbol = delimiter,
              Direction = TMDirection.S
            }
          }
      };
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
