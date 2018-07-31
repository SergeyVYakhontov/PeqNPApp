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
  public static partial class IF_NDTM_RSP_Multiply
  {
    #region public members

    public static Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> Delta1(int frameLength)
    {
      return new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
        {
          // set mark
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.MultReady,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.StartLoopInC,
                    Symbol = IF_NDTM.markB0,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.MultReady,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.StartLoopInC,
                    Symbol = IF_NDTM.markB1,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.MultReady,
                Symbol = OneTapeTuringMachine.blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.StartLoopInC,
                    Symbol = OneTapeTuringMachine.blankSymbol,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.MultReady,
                Symbol = IF_NDTM.delimiter3
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.StartLoopInC,
                    Symbol = IF_NDTM.delimiter3,
                    Direction = TMDirection.S
                  }
              }
          },

          // start compare
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.StartLoopInC,
                Symbol = OneTapeTuringMachine.blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.CompareStates.StartComparing,
                    Symbol = OneTapeTuringMachine.blankSymbol,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.StartLoopInC,
                Symbol = IF_NDTM.delimiter3
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = IF_NDTM.rejectingState,
                    Symbol = IF_NDTM.delimiter3,
                    Direction = TMDirection.S
                  }
              }
          },

          // process 0
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.StartLoopInC,
                Symbol = IF_NDTM.markB0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.MultReady,
                    Symbol = IF_NDTM.markB0,
                    Direction = TMDirection.R
                  }
              }
          },

          // process 1
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.StartLoopInC,
                Symbol = IF_NDTM.markB1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.Process1f_D,
                    Symbol = IF_NDTM.markB1,
                    Direction = TMDirection.R,
                    Shift = frameLength * 2
                  }
              }
          },

          // set mark
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.Process1f_D,
                Symbol = OneTapeTuringMachine.blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToCRight,
                    Symbol = IF_NDTM.markD0,
                    Direction = TMDirection.L,
                    Shift = frameLength
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.Process1f_D,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToCRight,
                    Symbol = IF_NDTM.markD0,
                    Direction = TMDirection.L,
                    Shift = frameLength
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.Process1f_D,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToCRight,
                    Symbol = IF_NDTM.markD1,
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
                State = (int)IF_NDTM.MultiplyStates.MoveToCRight,
                Symbol = OneTapeTuringMachine.blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToCRight,
                    Symbol = OneTapeTuringMachine.blankSymbol,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.MoveToCRight,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToCRight,
                    Symbol = 0,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.MoveToCRight,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToCRight,
                    Symbol = 1,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.MoveToCRight,
                Symbol = IF_NDTM.markC0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToCRight,
                    Symbol = IF_NDTM.markC0,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.MoveToCRight,
                Symbol = IF_NDTM.markC1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToCRight,
                    Symbol = IF_NDTM.markC1,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.MoveToCRight,
                Symbol = IF_NDTM.delimiter3
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.EraseMarkInC,
                    Symbol = IF_NDTM.delimiter3,
                    Direction = TMDirection.L
                  }
              }
          },

          // erase marks in C
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.EraseMarkInC,
                Symbol = OneTapeTuringMachine.blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.EraseMarkInC,
                    Symbol = OneTapeTuringMachine.blankSymbol,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.EraseMarkInC,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.EraseMarkInC,
                    Symbol = 0,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.EraseMarkInC,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.EraseMarkInC,
                    Symbol = 1,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.EraseMarkInC,
                Symbol = IF_NDTM.markC0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.EraseMarkInC,
                    Symbol = 0,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.EraseMarkInC,
                Symbol = IF_NDTM.markC1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.EraseMarkInC,
                    Symbol = 1,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.EraseMarkInC,
                Symbol = IF_NDTM.delimiter2
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.StartAddC,
                    Symbol = IF_NDTM.delimiter2,
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
