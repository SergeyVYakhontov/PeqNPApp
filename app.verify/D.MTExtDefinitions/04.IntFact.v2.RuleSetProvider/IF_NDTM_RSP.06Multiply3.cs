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

    public static Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> Delta3(int frameLength)
    {
      return new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
        {
          // 1 in C
          // move to D
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.StartAddC,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.AddC1f_D,
                    Symbol = IF_NDTM.markC1,
                    Direction = TMDirection.R,
                    Shift = frameLength
                  }
              }
          },

          // move in D
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.AddC1f_D,
                Symbol = OneTapeTuringMachine.blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.AddC1f_D,
                    Symbol = 0,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.AddC1f_D,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.AddC1f_D,
                    Symbol = 0,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.AddC1f_D,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.AddC1f_D,
                    Symbol = 1,
                    Direction = TMDirection.R
                  }
              }
          },

          // markD reached
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.AddC1f_D,
                Symbol = IF_NDTM.markD0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.AddStates.StartAdding,
                    Symbol = IF_NDTM.markD0,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.AddC1f_D,
                Symbol = IF_NDTM.markD1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.AddStates.StartAdding,
                    Symbol = IF_NDTM.markD1,
                    Direction = TMDirection.S
                  }
              }
          },

          // shift mark
          // move to left delimiter
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.MoveToMarkInD_L,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToMarkInD_L,
                    Symbol = 0,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.MoveToMarkInD_L,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToMarkInD_L,
                    Symbol = 1,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.MoveToMarkInD_L,
                Symbol = IF_NDTM.markD0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToMarkInD_L,
                    Symbol = IF_NDTM.markD0,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.MoveToMarkInD_L,
                Symbol = IF_NDTM.markD1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToMarkInD_L,
                    Symbol = IF_NDTM.markD1,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.MoveToMarkInD_L,
                Symbol = IF_NDTM.delimiter3
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToMarkInD_R,
                    Symbol = IF_NDTM.delimiter3,
                    Direction = TMDirection.R
                  }
              }
          },

          // move to mark right
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.MoveToMarkInD_R,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToMarkInD_R,
                    Symbol = 0,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.MoveToMarkInD_R,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToMarkInD_R,
                    Symbol = 1,
                    Direction = TMDirection.R
                  }
              }
          },

          // mark reached
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.MoveToMarkInD_R,
                Symbol = IF_NDTM.markD0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveMarkInD,
                    Symbol = 0,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.MoveToMarkInD_R,
                Symbol = IF_NDTM.markD1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveMarkInD,
                    Symbol = 1,
                    Direction = TMDirection.R
                  }
              }
          },

          // replace with mark
          {
            new StateSymbolPair()
              {
                State = (int)IF_NDTM.MultiplyStates.MoveMarkInD,
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
                State = (int)IF_NDTM.MultiplyStates.MoveMarkInD,
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
                State = (int)IF_NDTM.MultiplyStates.MoveMarkInD,
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
                State = (int)IF_NDTM.MultiplyStates.MoveMarkInD,
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
          }
        };
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
