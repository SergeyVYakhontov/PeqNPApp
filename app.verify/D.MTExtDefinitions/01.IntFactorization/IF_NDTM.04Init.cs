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

    private Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> deltaInit(int frameLength)
    {
      return new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
        {
          {
            new StateSymbolPair()
              {
                State = (int)SubprogStates.InitReady,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)InitStates.StopInit,
                    Symbol = 0,
                    Direction = TMDirection.R,
                    Shift = frameLength + 1
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)SubprogStates.InitReady,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)InitStates.StopInit,
                    Symbol = 1,
                    Direction = TMDirection.R,
                    Shift = frameLength + 1
                  }
              }
          }
        };
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
