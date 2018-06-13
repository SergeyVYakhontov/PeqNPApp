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

    private Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> deltaCompare(int frameLength)
    {
      return new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
        {
          // start comparing
          {
            new StateSymbolPair()
              {
                State = (int)SubprogStates.CompareReady,
                Symbol = OneTapeTuringMachine.b
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)CompareStates.StartComparing,
                    Symbol = OneTapeTuringMachine.b,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)CompareStates.StartComparing,
                Symbol = OneTapeTuringMachine.b
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)CompareStates.MoveLeftToA,
                    Symbol = OneTapeTuringMachine.b,
                    Direction = TMDirection.L,
                    Shift = frameLength
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)CompareStates.MoveLeftToA,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)CompareStates.MoveToStartA,
                    Symbol = 0,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)CompareStates.MoveLeftToA,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)CompareStates.MoveToStartA,
                    Symbol = 1,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)CompareStates.MoveLeftToA,
                Symbol = OneTapeTuringMachine.b
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)CompareStates.MoveLeftToA,
                    Symbol = OneTapeTuringMachine.b,
                    Direction = TMDirection.L
                  }
              }
          },

          // move to start A
          {
            new StateSymbolPair()
              {
                State = (int)CompareStates.MoveToStartA,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)CompareStates.MoveToStartA,
                    Symbol = 0,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)CompareStates.MoveToStartA,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)CompareStates.MoveToStartA,
                    Symbol = 1,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)CompareStates.MoveToStartA,
                Symbol = OneTapeTuringMachine.b
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)CompareStates.BitLoopStart,
                    Symbol = OneTapeTuringMachine.b,
                    Direction = TMDirection.R
                  }
              }
          },

          // shift to D
          {
            new StateSymbolPair()
              {
                State = (int)CompareStates.BitLoopStart,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)CompareStates.BitLoopD0,
                    Symbol = 0,
                    Direction = TMDirection.R,
                    Shift = frameLength * 3 + 1
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)CompareStates.BitLoopStart,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)CompareStates.BitLoopD1,
                    Symbol = 1,
                    Direction = TMDirection.R,
                    Shift = frameLength * 3 + 1
                  }
              }
          },

          // compare bit
          {
            new StateSymbolPair()
              {
                State = (int)CompareStates.BitLoopD0,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)CompareStates.BitLoopStart_f,
                    Symbol = 0,
                    Direction = TMDirection.L,
                    Shift = frameLength * 3 + 1
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)CompareStates.BitLoopD0,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = rejectingState,
                    Symbol = 1,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)CompareStates.BitLoopD1,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = rejectingState,
                    Symbol = 0,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)CompareStates.BitLoopD1,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)CompareStates.BitLoopStart_f,
                    Symbol = 1,
                    Direction = TMDirection.L,
                    Shift = frameLength * 3 + 1
                  }
              }
          },

          // D0, D1
          {
            new StateSymbolPair()
              {
                State = (int)CompareStates.BitLoopD0,
                Symbol = markD0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)CompareStates.BitLoopStart_f,
                    Symbol = 0,
                    Direction = TMDirection.L,
                    Shift = frameLength * 3 + 1
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)CompareStates.BitLoopD0,
                Symbol = markD1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = rejectingState,
                    Symbol = 1,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)CompareStates.BitLoopD1,
                Symbol = markD0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = rejectingState,
                    Symbol = 0,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)CompareStates.BitLoopD1,
                Symbol = markD1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)CompareStates.BitLoopStart_f,
                    Symbol = 1,
                    Direction = TMDirection.L,
                    Shift = frameLength * 3 + 1
                  }
              }
          },

          // move to next bit
          {
            new StateSymbolPair()
              {
                State = (int)CompareStates.BitLoopStart_f,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)CompareStates.BitLoopStart,
                    Symbol = 0,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)CompareStates.BitLoopStart_f,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = (int)CompareStates.BitLoopStart,
                    Symbol = 1,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)CompareStates.BitLoopStart_f,
                Symbol = OneTapeTuringMachine.b
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = acceptingState,
                    Symbol = OneTapeTuringMachine.b,
                    Direction = TMDirection.R
                  }
              }
          },

          // blank symbol
          {
            new StateSymbolPair()
              {
                State = (int)CompareStates.BitLoopD0,
                Symbol = OneTapeTuringMachine.b
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = rejectingState,
                    Symbol = OneTapeTuringMachine.b,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = (int)CompareStates.BitLoopD1,
                Symbol = OneTapeTuringMachine.b
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = rejectingState,
                    Symbol = OneTapeTuringMachine.b,
                    Direction = TMDirection.S
                  }
              }
          },
            {
            new StateSymbolPair()
              {
                State = (int)CompareStates.BitLoopStart,
                Symbol = OneTapeTuringMachine.b
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = acceptingState,
                     Symbol = OneTapeTuringMachine.b,
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
