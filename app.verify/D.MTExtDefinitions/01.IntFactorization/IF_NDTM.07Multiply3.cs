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

    private Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> deltaMultiply3(int frameLength)
    {
      return new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
        {
          // 1 in C
          // move to D
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.StartAddC,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.AddC1f_D,
                    Symbol = markC1,
                    Direction = TMDirection.R,
                    Shift = frameLength
                  }
              }
          },

          // move in D
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.AddC1f_D,
                Symbol = OneTapeTuringMachine.b
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.AddC1f_D,
                    Symbol = 0,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.AddC1f_D,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.AddC1f_D,
                    Symbol = 0,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.AddC1f_D,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.AddC1f_D,
                    Symbol = 1,
                    Direction = TMDirection.R
                  }
              }
          },

          // markD reached
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.AddC1f_D,
                Symbol = markD0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)AddStates.StartAdding,
                    Symbol = markD0,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.AddC1f_D,
                Symbol = markD1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)AddStates.StartAdding,
                    Symbol = markD1,
                    Direction = TMDirection.S
                  }
              }
          },

          // shift mark
          // move to left delimiter
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.MoveToMarkInD_L,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.MoveToMarkInD_L,
                    Symbol = 0,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.MoveToMarkInD_L,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.MoveToMarkInD_L,
                    Symbol = 1,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.MoveToMarkInD_L,
                Symbol = markD0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.MoveToMarkInD_L,
                    Symbol = markD0,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.MoveToMarkInD_L,
                Symbol = markD1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.MoveToMarkInD_L,
                    Symbol = markD1,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.MoveToMarkInD_L,
                Symbol = delimiter
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.MoveToMarkInD_R,
                    Symbol = delimiter,
                    Direction = TMDirection.R
                  }
              }
          },

          // move to mark right
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.MoveToMarkInD_R,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.MoveToMarkInD_R,
                    Symbol = 0,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.MoveToMarkInD_R,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.MoveToMarkInD_R,
                    Symbol = 1,
                    Direction = TMDirection.R
                  }
              }
          },

          // mark reached
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.MoveToMarkInD_R,
                Symbol = markD0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.MoveMarkInD,
                    Symbol = 0,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.MoveToMarkInD_R,
                Symbol = markD1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)MultiplyStates.MoveMarkInD,
                    Symbol = 1,
                    Direction = TMDirection.R
                  }
              }
          },

          // replace with mark
          {
            new StateSymbolPair()
              {
                State = (int)MultiplyStates.MoveMarkInD,
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
                State = (int)MultiplyStates.MoveMarkInD,
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
                State = (int)MultiplyStates.MoveMarkInD,
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
                State = (int)MultiplyStates.MoveMarkInD,
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
          }
        };
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
