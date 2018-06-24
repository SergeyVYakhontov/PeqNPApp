using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;
using ExistsAcceptingPath;

namespace MTExtDefinitions
{
  public partial class IF_NDTM
  {
    #region private members

    private Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> deltaMultiply()
    {
      return new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
        {
          {
            new StateSymbolPair
              {
                State = (int)SubprogStates.MultReady,
                Symbol = delimiter
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = acceptingState,
                    Symbol = delimiter,
                    Direction = TMDirection.S
                  }
              }
          }
        };
    }

    #endregion
  }
}
