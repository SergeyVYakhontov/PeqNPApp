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

    private Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> deltaGenNumber1(int frameLength)
    {
      return new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
        {
          // start generating bit
          {
            new StateSymbolPair()
              {
                State = (int)SubprogStates.GenNumber1Ready,
                Symbol = OneTapeTuringMachine.b
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)GenNumber1States.GenBit0,
                    Symbol = OneTapeTuringMachine.b,
                    Direction = TMDirection.S
                  }
              }
          },

          // generate bit 0 or 1
          {
            new StateSymbolPair()
              {
                State = (int)GenNumber1States.GenBit0,
                Symbol = OneTapeTuringMachine.b
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)GenNumber1States.GenBit1,
                    Symbol = 0,
                    Direction = TMDirection.R
                  },
                new StateSymbolDirectionTriple()
                  {
                    State = (int)GenNumber1States.GenBit1,
                    Symbol = 1,
                    Direction = TMDirection.R
                  }
              }
          },

          // generate bit 0 or 1
          {
            new StateSymbolPair()
              {
                State = (int)GenNumber1States.GenBit1,
                Symbol = OneTapeTuringMachine.b
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)GenNumber1States.GenBit,
                    Symbol = 0,
                    Direction = TMDirection.R
                  },
                new StateSymbolDirectionTriple()
                  {
                    State = (int)GenNumber1States.GenBit,
                    Symbol = 1,
                    Direction = TMDirection.R
                  },
                new StateSymbolDirectionTriple()
                  {
                    State = (int)GenNumber1States.MoveToDelimiter,
                    Symbol = 1,
                    Direction = TMDirection.R
                  }
              }
          },

          // generate bit 0 or 1
          {
            new StateSymbolPair()
              {
                State = (int)GenNumber1States.GenBit,
                Symbol = OneTapeTuringMachine.b
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)GenNumber1States.GenBit,
                    Symbol = 0,
                    Direction = TMDirection.R
                  },
                new StateSymbolDirectionTriple()
                  {
                    State = (int)GenNumber1States.GenBit,
                    Symbol = 1,
                    Direction = TMDirection.R
                  },
                new StateSymbolDirectionTriple()
                  {
                    State = (int)GenNumber1States.MoveToDelimiter,
                    Symbol = 1,
                    Direction = TMDirection.R
                  }
              }
          },

          // move to delimiter
          {
            new StateSymbolPair()
              {
                State = (int)GenNumber1States.MoveToDelimiter,
                Symbol = OneTapeTuringMachine.b
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)GenNumber1States.MoveToDelimiter,
                    Symbol = OneTapeTuringMachine.b,
                    Direction = TMDirection.R
                  }
              }
          },

          // delimiter reached
          {
            new StateSymbolPair()
              {
                State = (int)GenNumber1States.GenBit,
                Symbol = delimiter
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = rejectingState,
                    Symbol = delimiter,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)GenNumber1States.MoveToDelimiter,
                Symbol = delimiter
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)GenNumber1States.StopGenNumber,
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
