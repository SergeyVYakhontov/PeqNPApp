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
  public partial class IF_NDTM_B
  {
    #region private members

    private Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> deltaMultiply2(int frameLength)
    {
      return new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
        {
          // 0 in C
          // move to D
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.StartAddC,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.AddC0f_D,
                    Symbol = markC0,
                    Direction = TMDirection.R,
                    Shift = frameLength
                  }
              }
          },

          // move in D
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.AddC0f_D,
                Symbol = OneTapeTuringMachine.blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.AddC0f_D,
                    Symbol = 0,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.AddC0f_D,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.AddC0f_D,
                    Symbol = 0,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.AddC0f_D,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.AddC0f_D,
                    Symbol = 1,
                    Direction = TMDirection.R
                  }
              }
          },

          // markD reached
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.AddC0f_D,
                Symbol = markD0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.AddC0f_sm_D,
                    Symbol = 0,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.AddC0f_D,
                Symbol = markD1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.AddC0f_sm_D,
                    Symbol = 1,
                    Direction = TMDirection.R
                  }
              }
          },

          // set new mark and move to C
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.AddC0f_sm_D,
                Symbol = OneTapeTuringMachine.blankSymbol
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
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.AddC0f_sm_D,
                Symbol = 0
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
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.AddC0f_sm_D,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.MoveToCLeft,
                    Symbol = markD1,
                    Direction = TMDirection.L,
                    Shift = frameLength
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.AddC0f_sm_D,
                Symbol = markD0
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
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.AddC0f_sm_D,
                Symbol = markD1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.MoveToCLeft,
                    Symbol = markD1,
                    Direction = TMDirection.L,
                    Shift = frameLength
                  }
              }
          },

          // move to mark C
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.MoveToCLeft,
                Symbol = OneTapeTuringMachine.blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.MoveToCLeft,
                    Symbol = OneTapeTuringMachine.blankSymbol,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.MoveToCLeft,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.MoveToCLeft,
                    Symbol = 0,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.MoveToCLeft,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.MoveToCLeft,
                    Symbol = 1,
                    Direction = TMDirection.L
                  }
              }
          },

          // set new mark
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.MoveToCLeft,
                Symbol = markC0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.SetMarkInC,
                    Symbol = markC0,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.MoveToCLeft,
                Symbol = markC1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.SetMarkInC,
                    Symbol = markC1,
                    Direction = TMDirection.R
                  }
              }
          },

          // start new iteration
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.SetMarkInC,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.StartAddC,
                    Symbol = 0,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.SetMarkInC,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.StartAddC,
                    Symbol = 1,
                    Direction = TMDirection.S
                  }
              }
          },

          // move to mark in B
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.SetMarkInC,
                Symbol = OneTapeTuringMachine.blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.MoveToMarkInB,
                    Symbol = OneTapeTuringMachine.blankSymbol,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.MoveToMarkInB,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.MoveToMarkInB,
                    Symbol = 0,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.MoveToMarkInB,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.MoveToMarkInB,
                    Symbol = 1,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.MoveToMarkInB,
                Symbol = markC0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.MoveToMarkInB,
                    Symbol = markC0,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.MoveToMarkInB,
                Symbol = markC1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.MoveToMarkInB,
                    Symbol = markC1,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.MoveToMarkInB,
                Symbol = delimiter2
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.MoveToMarkInB_inB,
                    Symbol = delimiter2,
                    Direction = TMDirection.L
                  }
              }
          },

          //  inB
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.MoveToMarkInB_inB,
                Symbol = OneTapeTuringMachine.blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.MoveToMarkInB_inB,
                    Symbol = OneTapeTuringMachine.blankSymbol,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.MoveToMarkInB_inB,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.MoveToMarkInB_inB,
                    Symbol = 0,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.MoveToMarkInB_inB,
                Symbol =  1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.MoveToMarkInB_inB,
                    Symbol = 1,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.MoveToMarkInB_inB,
                Symbol = markB0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)SubprogStates.MultReady,
                    Symbol = markB0,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.MoveToMarkInB_inB,
                Symbol = markB1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)SubprogStates.MultReady,
                    Symbol = markB1,
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
