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

    public static IReadOnlyDictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> Delta2(int frameLength)
    {
      return new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
        {
          // 0 in C
          // move to D
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.StartAddC,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.AddC0f_D,
                    Symbol = IF_NDTM.markC0,
                    Direction = TMDirection.R,
                    Shift = frameLength
                  }
              }
          },

          // move in D
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.AddC0f_D,
                Symbol = OneTapeTuringMachine.blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.AddC0f_D,
                    Symbol = 0,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.AddC0f_D,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.AddC0f_D,
                    Symbol = 0,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.AddC0f_D,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.AddC0f_D,
                    Symbol = 1,
                    Direction = TMDirection.R
                  }
              }
          },

          // markD reached
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.AddC0f_D,
                Symbol = IF_NDTM.markD0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.AddC0f_sm_D,
                    Symbol = 0,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.AddC0f_D,
                Symbol = IF_NDTM.markD1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.AddC0f_sm_D,
                    Symbol = 1,
                    Direction = TMDirection.R
                  }
              }
          },

          // set new mark and move to C
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.AddC0f_sm_D,
                Symbol = OneTapeTuringMachine.blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToCLeft,
                    Symbol = IF_NDTM.markD0,
                    Direction = TMDirection.L,
                    Shift = frameLength
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.AddC0f_sm_D,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToCLeft,
                    Symbol = IF_NDTM.markD0,
                    Direction = TMDirection.L,
                    Shift = frameLength
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.AddC0f_sm_D,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToCLeft,
                    Symbol = IF_NDTM.markD1,
                    Direction = TMDirection.L,
                    Shift = frameLength
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.AddC0f_sm_D,
                Symbol = IF_NDTM.markD0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToCLeft,
                    Symbol = IF_NDTM.markD0,
                    Direction = TMDirection.L,
                    Shift = frameLength
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.AddC0f_sm_D,
                Symbol = IF_NDTM.markD1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToCLeft,
                    Symbol = IF_NDTM.markD1,
                    Direction = TMDirection.L,
                    Shift = frameLength
                  }
              }
          },

          // move to mark C
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.MoveToCLeft,
                Symbol = OneTapeTuringMachine.blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToCLeft,
                    Symbol = OneTapeTuringMachine.blankSymbol,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.MoveToCLeft,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToCLeft,
                    Symbol = 0,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.MoveToCLeft,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToCLeft,
                    Symbol = 1,
                    Direction = TMDirection.L
                  }
              }
          },

          // set new mark
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.MoveToCLeft,
                Symbol = IF_NDTM.markC0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.SetMarkInC,
                    Symbol = IF_NDTM.markC0,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.MoveToCLeft,
                Symbol = IF_NDTM.markC1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.SetMarkInC,
                    Symbol = IF_NDTM.markC1,
                    Direction = TMDirection.R
                  }
              }
          },

          // start new iteration
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.SetMarkInC,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.StartAddC,
                    Symbol = 0,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.SetMarkInC,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.StartAddC,
                    Symbol = 1,
                    Direction = TMDirection.S
                  }
              }
          },

          // move to mark in B
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.SetMarkInC,
                Symbol = OneTapeTuringMachine.blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToMarkInB,
                    Symbol = OneTapeTuringMachine.blankSymbol,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.MoveToMarkInB,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToMarkInB,
                    Symbol = 0,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.MoveToMarkInB,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToMarkInB,
                    Symbol = 1,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.MoveToMarkInB,
                Symbol = IF_NDTM.markC0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToMarkInB,
                    Symbol = IF_NDTM.markC0,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.MoveToMarkInB,
                Symbol = IF_NDTM.markC1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToMarkInB,
                    Symbol = IF_NDTM.markC1,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.MoveToMarkInB,
                Symbol = IF_NDTM.delimiter2
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToMarkInB_inB,
                    Symbol = IF_NDTM.delimiter2,
                    Direction = TMDirection.L
                  }
              }
          },

          //  inB
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.MoveToMarkInB_inB,
                Symbol = OneTapeTuringMachine.blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToMarkInB_inB,
                    Symbol = OneTapeTuringMachine.blankSymbol,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.MoveToMarkInB_inB,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToMarkInB_inB,
                    Symbol = 0,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.MoveToMarkInB_inB,
                Symbol =  1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToMarkInB_inB,
                    Symbol = 1,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.MoveToMarkInB_inB,
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
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.MoveToMarkInB_inB,
                Symbol = IF_NDTM.markB1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.MultReady,
                    Symbol = IF_NDTM.markB1,
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
