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
          {
            new StateSymbolPair()
              {
                State = qStartState,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)SubprogStates.MultReady,
                    Symbol = 0,
                    Direction = TMDirection.R,
                    Shift = frameLength + 1
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = qStartState,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)SubprogStates.MultReady,
                    Symbol = 1,
                    Direction = TMDirection.R,
                    Shift = frameLength + 1
                  }
              }
          }
      };
    }

    private Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> deltaSubprog1Std()
    {
      return new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
      {
          // start (normal)
          {
            new StateSymbolPair()
              {
                State = qStartState,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)SubprogStates.InitReady,
                    Symbol = 0,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = qStartState,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)SubprogStates.InitReady,
                    Symbol = 1,
                    Direction = TMDirection.S
                  }
              }
          }
      };
    }

    private Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> deltaSubprog2(int frameLength)
    {
      return new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
      {
          // start generating number1
          {
            new StateSymbolPair()
              {
                State = (int)InitStates.StopInit,
                Symbol = OneTapeTuringMachine.blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)SubprogStates.GenNumber1Ready,
                    Symbol = OneTapeTuringMachine.blankSymbol,
                    Direction = TMDirection.S
                  }
              }
          },

          // start generating number2
          {
            new StateSymbolPair()
              {
                State = (int)GenNumber1States.StopGenNumber,
                Symbol = delimiter
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)SubprogStates.GenNumber2Ready,
                    Symbol = delimiter,
                    Direction = TMDirection.R
                  }
              }
          },

          // start multiplying
          {
            new StateSymbolPair()
              {
                State = (int)GenNumber2States.StopGenNumber,
                Symbol = delimiter
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)SubprogStates.MultReady,
                    Symbol = delimiter,
                    Direction = TMDirection.L,
                    Shift = frameLength * 2 - 1
                  }
              }
          },

          // start comparing
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.StopMultiplying,
                Symbol = OneTapeTuringMachine.blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)SubprogStates.CompareReady,
                    Symbol = OneTapeTuringMachine.blankSymbol,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.StopMultiplying,
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
          }
      };
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
