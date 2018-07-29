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
  public partial class IF_NDTM
  {
    #region private members

    private Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> deltaMultiply1(int frameLength)
    {
      return new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
        {
          // set mark
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.MultReady,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.StartLoopInC,
                    Symbol = markB0,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.MultReady,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.StartLoopInC,
                    Symbol = markB1,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.MultReady,
                Symbol = OneTapeTuringMachine.blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.StartLoopInC,
                    Symbol = OneTapeTuringMachine.blankSymbol,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.MultReady,
                Symbol = delimiter3
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.StartLoopInC,
                    Symbol = delimiter3,
                    Direction = TMDirection.S
                  }
              }
          },

          // start compare
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.StartLoopInC,
                Symbol = blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)CompareStates.StartComparing,
                    Symbol = blankSymbol,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.StartLoopInC,
                Symbol = delimiter3
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = rejectingState,
                    Symbol = delimiter3,
                    Direction = TMDirection.S
                  }
              }
          },

          // process 0
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.StartLoopInC,
                Symbol = markB0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.MultReady,
                    Symbol = markB0,
                    Direction = TMDirection.R
                  }
              }
          },

          // process 1
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.StartLoopInC,
                Symbol = markB1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.Process1f_D,
                    Symbol = markB1,
                    Direction = TMDirection.R,
                    Shift = frameLength * 2
                  }
              }
          },

          // set mark
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.Process1f_D,
                Symbol = OneTapeTuringMachine.blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.MoveToCRight,
                    Symbol = markD0,
                    Direction = TMDirection.L,
                    Shift = frameLength
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.Process1f_D,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.MoveToCRight,
                    Symbol = markD0,
                    Direction = TMDirection.L,
                    Shift = frameLength
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.Process1f_D,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.MoveToCRight,
                    Symbol = markD1,
                    Direction = TMDirection.L,
                    Shift = frameLength
                  }
              }
          },

          // restore C
          // move to right delimiter
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.MoveToCRight,
                Symbol = OneTapeTuringMachine.blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.MoveToCRight,
                    Symbol = OneTapeTuringMachine.blankSymbol,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.MoveToCRight,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.MoveToCRight,
                    Symbol = 0,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.MoveToCRight,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.MoveToCRight,
                    Symbol = 1,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.MoveToCRight,
                Symbol = markC0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.MoveToCRight,
                    Symbol = markC0,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.MoveToCRight,
                Symbol = markC1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.MoveToCRight,
                    Symbol = markC1,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.MoveToCRight,
                Symbol = delimiter3
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.EraseMarkInC,
                    Symbol = delimiter3,
                    Direction = TMDirection.L
                  }
              }
          },

          // erase marks in C
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.EraseMarkInC,
                Symbol = OneTapeTuringMachine.blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.EraseMarkInC,
                    Symbol = OneTapeTuringMachine.blankSymbol,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.EraseMarkInC,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.EraseMarkInC,
                    Symbol = 0,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.EraseMarkInC,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.EraseMarkInC,
                    Symbol = 1,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.EraseMarkInC,
                Symbol = markC0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.EraseMarkInC,
                    Symbol = 0,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.EraseMarkInC,
                Symbol = markC1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.EraseMarkInC,
                    Symbol = 1,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.EraseMarkInC,
                Symbol = delimiter2
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.StartAddC,
                    Symbol = delimiter2,
                    Direction = TMDirection.R
                  }
              }
          }
        };
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
