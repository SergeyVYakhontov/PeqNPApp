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
 /* public partial class IF_NDTM
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
    } */

    /* private static readonly Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> deltaSubprog2 =
      new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
      {
        // start comparing
        [new StateSymbolPair(state: (uint)MultiplyStates.StopMultiplying, symbol: blankSymbol)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            {
              State = (int)CompareStates.StartComparing,
              Symbol = blankSymbol,
              Direction = TMDirection.S
            }
          },

        [new StateSymbolPair(state: (uint)MultiplyStates.StopMultiplying, symbol: delimiter3)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            {
              State = (int)rejectingState,
              Symbol = delimiter3,
              Direction = TMDirection.S
            }
          } 
      //};

    #endregion
  } */
}

////////////////////////////////////////////////////////////////////////////////////////////////////
