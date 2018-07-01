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
  public partial class IF_NDTM_A
  {
    #region private members

    private Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> deltaAdd()
    {
      return new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
        {
          // start adding
          {
            new StateSymbolPair
              {
                State = (int)AddStates.StartAdding,
                Symbol = markD0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (int)AddStates.AddBitC0,
                    Symbol = markD1,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (int)AddStates.StartAdding,
                Symbol = markD1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (int)AddStates.AddBitC1,
                    Symbol = markD0,
                    Direction = TMDirection.R
                  }
              }
          },

          // add bit with carry = 0
          {
            new StateSymbolPair
              {
                State = (int)AddStates.AddBitC0,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (int)MultiplyStates.MoveToMarkInD_L,
                    Symbol = 0,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (int)AddStates.AddBitC0,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (int)MultiplyStates.MoveToMarkInD_L,
                    Symbol = 1,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (int)AddStates.AddBitC0,
                Symbol = markD0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (int)MultiplyStates.MoveToMarkInD_L,
                    Symbol = markD0,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (int)AddStates.AddBitC0,
                Symbol = markD1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (int)MultiplyStates.MoveToMarkInD_L,
                    Symbol = markD1,
                    Direction = TMDirection.S
                  }
              }
          },

          // add bit with carry = 1
          {
            new StateSymbolPair
              {
                State = (int)AddStates.AddBitC1,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (int)MultiplyStates.MoveToMarkInD_L,
                    Symbol = 1,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (int)AddStates.AddBitC1,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (int)AddStates.AddBitC1,
                    Symbol = 0,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (int)AddStates.AddBitC1,
                Symbol = markD0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (int)MultiplyStates.MoveToMarkInD_L,
                    Symbol = markD1,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (int)AddStates.AddBitC1,
                Symbol = markD1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (int)AddStates.AddBitC1,
                    Symbol = markD0,
                    Direction = TMDirection.R
                  }
              }
          },

          // blank reached
          {
            new StateSymbolPair
              {
                State = (int)AddStates.AddBitC0,
                Symbol = blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (int)MultiplyStates.MoveToMarkInD_L,
                    Symbol = 0,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (int)AddStates.AddBitC1,
                Symbol = blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (int)MultiplyStates.MoveToMarkInD_L,
                    Symbol = 1,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (int)AddStates.AddBitC0,
                Symbol = delimiter4
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (int)MultiplyStates.MoveToMarkInD_L,
                    Symbol = 0,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (int)AddStates.AddBitC1,
                Symbol = delimiter4

              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (int)MultiplyStates.MoveToMarkInD_L,
                    Symbol = 1,
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
