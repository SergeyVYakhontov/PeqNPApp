////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;
using ExistsAcceptingPath;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace MTExtDefinitions.v1
{
/*  public partial class IF_NDTM
  {
    #region private members

    private Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> deltaAdd()
    {
      return new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
        {
          // start adding
          [new StateSymbolPair(state: (uint)AddStates.StartAdding, symbol: markD0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)AddStates.AddBitC0,
                    symbol: markD1,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)AddStates.StartAdding, symbol: markD1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)AddStates.AddBitC1,
                    symbol: markD0,
                    direction: TMDirection.R
                  )
              },

          // add bit with carry = 0
          [new StateSymbolPair(state: (uint)AddStates.AddBitC0, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToMarkInD_L,
                    symbol: 0,
                    direction: TMDirection.S
                  )
              },
          [new StateSymbolPair(state: (uint)AddStates.AddBitC0, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToMarkInD_L,
                    symbol: 1,
                    direction: TMDirection.S
                  )
              },
          [new StateSymbolPair(state: (uint)AddStates.AddBitC0, symbol: markD0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToMarkInD_L,
                    symbol: markD0,
                    direction: TMDirection.S
                  )
              },
          [new StateSymbolPair(state: (uint)AddStates.AddBitC0, symbol: markD1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToMarkInD_L,
                    symbol: markD1,
                    direction: TMDirection.S
                  )
              },

          // add bit with carry = 1
          [new StateSymbolPair(state: (uint)AddStates.AddBitC1, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToMarkInD_L,
                    symbol: 1,
                    direction: TMDirection.S
                  )
              },
          [new StateSymbolPair(state: (uint)AddStates.AddBitC1, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)AddStates.AddBitC1,
                    symbol: 0,
                    direction: TMDirection.R
                  )
              },
          [new StateSymbolPair(state: (uint)AddStates.AddBitC1, symbol: markD0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToMarkInD_L,
                    symbol: markD1,
                    direction: TMDirection.S
                  )
              },
          [new StateSymbolPair(state: (uint)AddStates.AddBitC1, symbol: markD1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)AddStates.AddBitC1,
                    symbol: markD0,
                    direction: TMDirection.R
                  )
              },

          // blank reached
          [new StateSymbolPair(state: (uint)AddStates.AddBitC0, symbol: blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToMarkInD_L,
                    symbol: 0,
                    direction: TMDirection.S
                  )
              },
          [new StateSymbolPair(state: (uint)AddStates.AddBitC1, symbol: blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (int)MultiplyStates.MoveToMarkInD_L,
                    symbol: 1,
                    direction: TMDirection.S
                  )
              },
          [new StateSymbolPair(state: (uint)AddStates.AddBitC0, symbol: delimiter4)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToMarkInD_L,
                    symbol: 0,
                    direction: TMDirection.S
                  )
              },
          [new StateSymbolPair(state: (uint)AddStates.AddBitC1, symbol: delimiter4)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: (uint)MultiplyStates.MoveToMarkInD_L,
                    symbol: 1,
                    direction: TMDirection.S
                  )
              }
        };
    }

    #endregion
  } */
}

////////////////////////////////////////////////////////////////////////////////////////////////////
