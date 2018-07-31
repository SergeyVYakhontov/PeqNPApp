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
 /* public partial class IF_NDTM
  {
    #region private members

    private static readonly Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> deltaCompare =
      new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
        {
          // start comparing
          [new StateSymbolPair(state: (uint)CompareStates.StartComparing, symbol: blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToStartA,
                    symbol: blankSymbol,
                    direction: TMDirection.L
                  )
              },

          // move to start A
          [new StateSymbolPair(state: (uint)CompareStates.MoveToStartA, symbol: blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToStartA,
                    symbol: blankSymbol,
                    direction: TMDirection.L
                  )
            },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToStartA, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToStartA,
                    symbol: 0,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToStartA, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToStartA,
                    symbol: 1,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToStartA, symbol: markB0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToStartA,
                    symbol: markB0,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToStartA, symbol: markB1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToStartA,
                    symbol: markB1,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToStartA, symbol: delimiter1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToStartA,
                    symbol: delimiter1,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToStartA, symbol: delimiter0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.BitLoopStart,
                    symbol: delimiter0,
                    direction: TMDirection.R
                  )
              },

          // shift to D, bit 0
          [new StateSymbolPair(state: (uint)CompareStates.BitLoopStart, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter3_bit0,
                    Symbol = markE0,
                    Direction = TMDirection.R
                  }
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToDelimiter3_bit0, symbol: blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter3_bit0,
                    symbol: blankSymbol,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToDelimiter3_bit0, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter3_bit0,
                    symbol: 0,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToDelimiter3_bit0, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter3_bit0,
                    symbol: 1,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToDelimiter3_bit0, symbol: delimiter1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter3_bit0,
                    symbol: delimiter1,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToDelimiter3_bit0, symbol: markB0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter3_bit0,
                    symbol: markB0,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToDelimiter3_bit0, symbol: markB1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter3_bit0,
                    symbol: markB1,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToDelimiter3_bit0, symbol: delimiter2)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter3_bit0,
                    symbol: delimiter2,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToDelimiter3_bit0, symbol: markC0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter3_bit0,
                    symbol: markC0,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToDelimiter3_bit0, symbol: markC1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter3_bit0,
                    symbol: markC1,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToDelimiter3_bit0, symbol: delimiter3)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.SkipF_bit0,
                    symbol: delimiter3,
                    direction: TMDirection.R
                  )
              },

          [new StateSymbolPair(state: (uint)CompareStates.SkipF_bit0, symbol: markF0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.SkipF_bit0,
                    symbol: markF0,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.SkipF_bit0, symbol: markF1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.SkipF_bit0,
                    symbol: markF1,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.SkipF_bit0, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter4,
                    symbol: markF0,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.SkipF_bit0, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: rejectingState,
                    symbol: 1,
                    direction: TMDirection.R
                  )
              },

          // shift to D, bit 1
          [new StateSymbolPair(state: (uint)CompareStates.BitLoopStart, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter3_bit1,
                    symbol: markE1,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToDelimiter3_bit1, symbol: blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter3_bit1,
                    symbol: blankSymbol,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToDelimiter3_bit1, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter3_bit1,
                    symbol: 0,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToDelimiter3_bit1, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter3_bit1,
                    symbol: 1,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToDelimiter3_bit1, symbol: delimiter1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter3_bit1,
                    symbol: delimiter1,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToDelimiter3_bit1, symbol: markB0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter3_bit1,
                    symbol: markB0,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToDelimiter3_bit1, symbol: markB1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter3_bit1,
                    symbol: markB1,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToDelimiter3_bit1, symbol: delimiter2)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter3_bit1,
                    symbol: delimiter2,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToDelimiter3_bit1, symbol: markC0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter3_bit1,
                    symbol: markC0,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToDelimiter3_bit1, symbol: markC1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter3_bit1,
                    symbol: markC1,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToDelimiter3_bit1, symbol: delimiter3)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.SkipF_bit1,
                    symbol: delimiter3,
                    direction: TMDirection.R
                  )
              },

          [new StateSymbolPair(state: (uint)CompareStates.SkipF_bit1, symbol: markF0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.SkipF_bit1,
                    symbol: markF0,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.SkipF_bit1, symbol: markF1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.SkipF_bit1,
                    symbol: markF1,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.SkipF_bit1, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: rejectingState,
                    symbol: 0,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.SkipF_bit1, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter4,
                    symbol: markF1,
                    direction: TMDirection.R
                  )
              },

          // move to delimiter 4
          [new StateSymbolPair(state: (uint)CompareStates.MoveToDelimiter4, symbol: blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter4,
                    symbol: blankSymbol,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToDelimiter4, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter4,
                    symbol: 0,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToDelimiter4, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter4,
                    symbol: 1,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToDelimiter4, symbol: markD0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter4,
                    symbol: markD0,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToDelimiter4, symbol: markD1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter4,
                    symbol: markD1,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToDelimiter4, symbol: delimiter4)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter0,
                    symbol: delimiter4,
                    direction: TMDirection.L
                  )
              },

          // move to delimiter 0
          [new StateSymbolPair(state: (uint)CompareStates.MoveToDelimiter0, symbol: blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter0,
                    symbol: blankSymbol,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToDelimiter0, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter0,
                    symbol: 0,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToDelimiter0, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter0,
                    symbol: 1,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToDelimiter0, symbol: delimiter1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter0,
                    symbol: delimiter1,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToDelimiter0, symbol: delimiter2)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter0,
                    symbol: delimiter2,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToDelimiter0, symbol: delimiter3)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter0,
                    symbol: delimiter3,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToDelimiter0, symbol: markB0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter0,
                    symbol: markB0,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToDelimiter0, symbol: markB1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter0,
                    symbol: markB1,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToDelimiter0, symbol: markC0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter0,
                    symbol: markC0,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToDelimiter0, symbol: markC1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (uint)CompareStates.MoveToDelimiter0,
                    Symbol = markC1,
                    Direction = TMDirection.L
                  }
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToDelimiter0, symbol: markE0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter0,
                    symbol: markE0,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToDelimiter0, symbol: markE1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter0,
                    symbol: markE1,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToDelimiter0, symbol: markF0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter0,
                    symbol: markF0,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToDelimiter0, symbol: markF1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter0,
                    symbol: markF1,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.MoveToDelimiter0, symbol: delimiter0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.SkipE,
                    symbol: delimiter0,
                    direction: TMDirection.R
                  )
              },

          // skip E marks
          [new StateSymbolPair(state: (uint)CompareStates.SkipE, symbol: markE0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.SkipE,
                    symbol: markE0,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.SkipE, symbol: markE1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.SkipE,
                    symbol: markE1,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.SkipE, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter3_bit0,
                    symbol: markE0,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.SkipE, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)CompareStates.MoveToDelimiter3_bit1,
                    symbol: markE0,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)CompareStates.SkipE, symbol: blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: acceptingState,
                    symbol: blankSymbol,
                    direction: TMDirection.R
                  )
              }
      };

    #endregion
  } */
}

////////////////////////////////////////////////////////////////////////////////////////////////////
