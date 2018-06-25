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

    private Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> deltaGenNumber2()
    {
      return new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
        {
          // start generating bit
          {
            new StateSymbolPair()
              {
                State = (int)SubprogStates.GenNumber2Ready,
                Symbol = OneTapeTuringMachine.blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)GenNumber2States.GenBit0,
                    Symbol = OneTapeTuringMachine.blankSymbol,
                    Direction = TMDirection.S
                  }
              }
          },

          // generate bit 0 or 1
          {
            new StateSymbolPair()
              {
                State = (int)GenNumber2States.GenBit0,
                Symbol = OneTapeTuringMachine.blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)GenNumber2States.GenBit1,
                    Symbol = 0,
                    Direction = TMDirection.R
                  },
                new StateSymbolDirectionTriple()
                  {
                    State = (int)GenNumber2States.GenBit1,
                    Symbol = 1,
                    Direction = TMDirection.R
                  }
              }
          },

          // generate bit 0 or 1
          {
            new StateSymbolPair()
              {
                State = (int)GenNumber2States.GenBit1,
                Symbol = OneTapeTuringMachine.blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)GenNumber2States.GenBit,
                    Symbol = 0,
                    Direction = TMDirection.R
                  },
                new StateSymbolDirectionTriple()
                  {
                    State = (int)GenNumber2States.GenBit,
                    Symbol = 1,
                    Direction = TMDirection.R
                  },
                new StateSymbolDirectionTriple()
                  {
                    State = (int)GenNumber2States.MoveToDelimiter,
                    Symbol = 1,
                    Direction = TMDirection.R
                  }
              }
          },

          // generate bit 0 or 1
          {
            new StateSymbolPair()
              {
                State = (int)GenNumber2States.GenBit,
                Symbol = OneTapeTuringMachine.blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)GenNumber2States.GenBit,
                    Symbol = 0,
                    Direction = TMDirection.R
                  },
                new StateSymbolDirectionTriple()
                  {
                    State = (int)GenNumber2States.GenBit,
                    Symbol = 1,
                    Direction = TMDirection.R
                  },
                new StateSymbolDirectionTriple()
                  {
                    State = (int)GenNumber2States.MoveToDelimiter,
                    Symbol = 1,
                    Direction = TMDirection.R
                  }
              }
          },

          // move to delimiter
          {
            new StateSymbolPair()
              {
                State = (int)GenNumber2States.MoveToDelimiter,
                Symbol = OneTapeTuringMachine.blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)GenNumber2States.MoveToDelimiter,
                    Symbol = OneTapeTuringMachine.blankSymbol,
                    Direction = TMDirection.R
                  }
              }
          },

          // delimiter reached
          {
            new StateSymbolPair()
              {
                State = (int)GenNumber2States.GenBit,
                Symbol = delimiter
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)rejectingState,
                    Symbol = delimiter,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)GenNumber2States.MoveToDelimiter,
                Symbol = delimiter
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)GenNumber2States.StopGenNumber,
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

////////////////////////////////////////////////////////////////////////////////////////////////////
