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
  public partial class IF_NDTM_A
  {
    #region private members

    private Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> deltaBkwd(int frameLength)
    {
      return new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
        {
          {
            new StateSymbolPair()
              {
                State = (int)BkwdStates.Bkwd1,
                Symbol = bkwd1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.MoveToCLeft,
                    Symbol = markD0,
                    Direction = TMDirection.L,
                    Shift = frameLength
                  }
              }
          },
      };
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
