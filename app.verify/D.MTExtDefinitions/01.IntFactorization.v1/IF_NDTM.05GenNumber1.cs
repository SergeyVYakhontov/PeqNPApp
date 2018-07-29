﻿////////////////////////////////////////////////////////////////////////////////////////////////////

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

    private Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> deltaGenNumber1()
    {
      return new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
      {
        // start generating bits
        // generate bit 0 or 1
        [new StateSymbolPair(state: (uint)GenNumber1States.GenBitA, symbol: blankSymbol)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)GenNumber1States.GenBitA,
              symbol: 0,
              direction: TMDirection.R
            ),
            new StateSymbolDirectionTriple
            (
              state: (uint)GenNumber1States.GenBitB,
              symbol: 1,
              direction: TMDirection.R
            )
          },

        [new StateSymbolPair(state: (uint)GenNumber1States.GenBitB, symbol: blankSymbol)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)GenNumber1States.GenBitB,
              symbol: 0,
              direction: TMDirection.R
            ),
            new StateSymbolDirectionTriple
            (
              state: (uint)GenNumber1States.GenBitB,
              symbol: 1,
              direction: TMDirection.R
            ),
            new StateSymbolDirectionTriple
            (
              state: (uint)GenNumber1States.MoveRightToDelim2,
              symbol: blankSymbol,
              direction: TMDirection.R
            )
          },

        // delimiter reached
        [new StateSymbolPair(state: (uint)GenNumber1States.GenBitA, symbol: delimiter2)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: rejectingState,
              symbol: delimiter2,
              direction: TMDirection.S
            )
          },

        [new StateSymbolPair(state: (uint)GenNumber1States.GenBitB, symbol: delimiter2)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: rejectingState,
              symbol: delimiter2,
              direction: TMDirection.S
            )
          },

        [new StateSymbolPair(state: (uint)GenNumber1States.MoveRightToDelim2, symbol: blankSymbol)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)GenNumber1States.MoveRightToDelim2,
              symbol: blankSymbol,
              direction: TMDirection.R
            )
          },
        [new StateSymbolPair(state: (uint)GenNumber1States.MoveRightToDelim2, symbol: delimiter2)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (int)GenNumber2States.GenBitA,
              symbol: delimiter2,
              direction: TMDirection.R
            )
          }
      };
    }

    #endregion
  }*/
}

////////////////////////////////////////////////////////////////////////////////////////////////////
