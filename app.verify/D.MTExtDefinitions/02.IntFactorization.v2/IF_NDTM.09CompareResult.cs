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

    private static readonly Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> deltaCompare =
      new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
        {
          // start comparing
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.StartComparing,
                Symbol = blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToStartA,
                    Symbol = blankSymbol,
                    Direction = TMDirection.L
                  }
              }
          },

          // move to start A
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToStartA,
                Symbol = blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToStartA,
                    Symbol = blankSymbol,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToStartA,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToStartA,
                    Symbol = 0,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToStartA,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToStartA,
                    Symbol = 1,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToStartA,
                Symbol = markB0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToStartA,
                    Symbol = markB0,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToStartA,
                Symbol = markB1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToStartA,
                    Symbol = markB1,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToStartA,
                Symbol = delimiter1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToStartA,
                    Symbol = delimiter1,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToStartA,
                Symbol = delimiter0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.BitLoopStart,
                    Symbol = delimiter0,
                    Direction = TMDirection.R
                  }
              }
          },

          // shift to D, bit 0
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.BitLoopStart,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter3_bit0,
                    Symbol = markE0,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToDelimiter3_bit0,
                Symbol = blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter3_bit0,
                    Symbol = blankSymbol,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToDelimiter3_bit0,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter3_bit0,
                    Symbol = 0,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToDelimiter3_bit0,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter3_bit0,
                    Symbol = 1,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToDelimiter3_bit0,
                Symbol = delimiter1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter3_bit0,
                    Symbol = delimiter1,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToDelimiter3_bit0,
                Symbol = markB0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter3_bit0,
                    Symbol = markB0,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToDelimiter3_bit0,
                Symbol = markB1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter3_bit0,
                    Symbol = markB1,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToDelimiter3_bit0,
                Symbol = delimiter2
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter3_bit0,
                    Symbol = delimiter2,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToDelimiter3_bit0,
                Symbol = markC0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter3_bit0,
                    Symbol = markC0,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToDelimiter3_bit0,
                Symbol = markC1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter3_bit0,
                    Symbol = markC1,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToDelimiter3_bit0,
                Symbol = delimiter3
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.SkipF_bit0,
                    Symbol = delimiter3,
                    Direction = TMDirection.R
                  }
              }
          },

          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.SkipF_bit0,
                Symbol = markF0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.SkipF_bit0,
                    Symbol = markF0,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.SkipF_bit0,
                Symbol = markF1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.SkipF_bit0,
                    Symbol = markF1,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.SkipF_bit0,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter4,
                    Symbol = markF0,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.SkipF_bit0,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = rejectingState,
                    Symbol = 1,
                    Direction = TMDirection.R
                  }
              }
          },

          // shift to D, bit 1
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.BitLoopStart,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter3_bit1,
                    Symbol = markE1,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToDelimiter3_bit1,
                Symbol = blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter3_bit1,
                    Symbol = blankSymbol,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToDelimiter3_bit1,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter3_bit1,
                    Symbol = 0,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToDelimiter3_bit1,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter3_bit1,
                    Symbol = 1,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToDelimiter3_bit1,
                Symbol = delimiter1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter3_bit1,
                    Symbol = delimiter1,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToDelimiter3_bit1,
                Symbol = markB0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter3_bit1,
                    Symbol = markB0,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToDelimiter3_bit1,
                Symbol = markB1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter3_bit1,
                    Symbol = markB1,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToDelimiter3_bit1,
                Symbol = delimiter2
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter3_bit1,
                    Symbol = delimiter2,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToDelimiter3_bit1,
                Symbol = markC0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter3_bit1,
                    Symbol = markC0,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToDelimiter3_bit1,
                Symbol = markC1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter3_bit1,
                    Symbol = markC1,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToDelimiter3_bit1,
                Symbol = delimiter3
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.SkipF_bit1,
                    Symbol = delimiter3,
                    Direction = TMDirection.R
                  }
              }
          },

          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.SkipF_bit1,
                Symbol = markF0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.SkipF_bit1,
                    Symbol = markF0,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.SkipF_bit1,
                Symbol = markF1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.SkipF_bit1,
                    Symbol = markF1,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.SkipF_bit1,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = rejectingState,
                    Symbol = 0,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.SkipF_bit1,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter4,
                    Symbol = markF1,
                    Direction = TMDirection.R
                  }
              }
          },

          // move to delimiter 4
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToDelimiter4,
                Symbol = blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter4,
                    Symbol = blankSymbol,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToDelimiter4,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter4,
                    Symbol = 0,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToDelimiter4,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter4,
                    Symbol = 1,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToDelimiter4,
                Symbol = markD0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter4,
                    Symbol = markD0,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToDelimiter4,
                Symbol = markD1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter4,
                    Symbol = markD1,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToDelimiter4,
                Symbol = delimiter4
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter0,
                    Symbol = delimiter4,
                    Direction = TMDirection.L
                  }
              }
          },

          // move to delimiter 0
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToDelimiter0,
                Symbol = blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter0,
                    Symbol = blankSymbol,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToDelimiter0,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter0,
                    Symbol = 0,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToDelimiter0,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter0,
                    Symbol = 1,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToDelimiter0,
                Symbol = delimiter1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter0,
                    Symbol = delimiter1,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToDelimiter0,
                Symbol = delimiter2
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter0,
                    Symbol = delimiter2,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToDelimiter0,
                Symbol = delimiter3
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter0,
                    Symbol = delimiter3,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToDelimiter0,
                Symbol = markB0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter0,
                    Symbol = markB0,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToDelimiter0,
                Symbol = markB1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter0,
                    Symbol = markB1,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToDelimiter0,
                Symbol = markC0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter0,
                    Symbol = markC0,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToDelimiter0,
                Symbol = markC1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter0,
                    Symbol = markC1,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToDelimiter0,
                Symbol = markE0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter0,
                    Symbol = markE0,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToDelimiter0,
                Symbol = markE1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter0,
                    Symbol = markE1,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToDelimiter0,
                Symbol = markF0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter0,
                    Symbol = markF0,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToDelimiter0,
                Symbol = markF1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter0,
                    Symbol = markF1,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.MoveToDelimiter0,
                Symbol = delimiter0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.SkipE,
                    Symbol = delimiter0,
                    Direction = TMDirection.R
                  }
              }
          },

          // skip E marks
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.SkipE,
                Symbol = markE0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.SkipE,
                    Symbol = markE0,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.SkipE,
                Symbol = markE1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.SkipE,
                    Symbol = markE1,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.SkipE,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter3_bit0,
                    Symbol = markE0,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.SkipE,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter3_bit1,
                    Symbol = markE0,
                    Direction = TMDirection.R
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = (uint)CompareStates.SkipE,
                Symbol = blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = acceptingState,
                    Symbol = blankSymbol,
                    Direction = TMDirection.R
                  }
              }
          }
      };

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
