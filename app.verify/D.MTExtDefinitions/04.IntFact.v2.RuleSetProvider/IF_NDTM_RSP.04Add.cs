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
  public static class IF_NDTM_RSP_Add
  {
    #region public members

    public static IReadOnlyDictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> Delta { get; } =
      new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
      {
          // start adding
          {
            new StateSymbolPair
              {
                State = (int)IF_NDTM.AddStates.StartAdding,
                Symbol = IF_NDTM.markD0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (int)IF_NDTM.AddStates.AddBitC0,
                    Symbol = IF_NDTM.markD1,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (int)IF_NDTM.AddStates.StartAdding,
                Symbol = IF_NDTM.markD1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (int)IF_NDTM.AddStates.AddBitC1,
                    Symbol = IF_NDTM.markD0,
                    Direction = TMDirection.R
                  }
              }
          },

          // add bit with carry = 0
          {
            new StateSymbolPair
              {
                State = (int)IF_NDTM.AddStates.AddBitC0,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToMarkInD_L,
                    Symbol = 0,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (int)IF_NDTM.AddStates.AddBitC0,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToMarkInD_L,
                    Symbol = 1,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (int)IF_NDTM.AddStates.AddBitC0,
                Symbol = IF_NDTM.markD0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToMarkInD_L,
                    Symbol = IF_NDTM.markD0,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (int)IF_NDTM.AddStates.AddBitC0,
                Symbol = IF_NDTM.markD1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToMarkInD_L,
                    Symbol = IF_NDTM.markD1,
                    Direction = TMDirection.S
                  }
              }
          },

          // add bit with carry = 1
          {
            new StateSymbolPair
              {
                State = (int)IF_NDTM.AddStates.AddBitC1,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToMarkInD_L,
                    Symbol = 1,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (int)IF_NDTM.AddStates.AddBitC1,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (int)IF_NDTM.AddStates.AddBitC1,
                    Symbol = 0,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (int)IF_NDTM.AddStates.AddBitC1,
                Symbol = IF_NDTM.markD0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToMarkInD_L,
                    Symbol = IF_NDTM.markD1,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (int)IF_NDTM.AddStates.AddBitC1,
                Symbol = IF_NDTM.markD1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (int)IF_NDTM.AddStates.AddBitC1,
                    Symbol = IF_NDTM.markD0,
                    Direction = TMDirection.R
                  }
              }
          },

          // blank reached
          {
            new StateSymbolPair
              {
                State = (int)IF_NDTM.AddStates.AddBitC0,
                Symbol = OneTapeTuringMachine.blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToMarkInD_L,
                    Symbol = 0,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (int)IF_NDTM.AddStates.AddBitC1,
                Symbol = OneTapeTuringMachine.blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToMarkInD_L,
                    Symbol = 1,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (int)IF_NDTM.AddStates.AddBitC0,
                Symbol = IF_NDTM.delimiter4
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToMarkInD_L,
                    Symbol = 0,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (int)IF_NDTM.AddStates.AddBitC1,
                Symbol = IF_NDTM.delimiter4

              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (int)IF_NDTM.MultiplyStates.MoveToMarkInD_L,
                    Symbol = 1,
                    Direction = TMDirection.S
                  }
              }
          }
        };

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
