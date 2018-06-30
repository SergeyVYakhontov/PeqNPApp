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

    private Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> deltaGenNumber1()
    {
      return new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
      {
        // start generating bit
        // generate bit 0 or 1
        [new StateSymbolPair(state: (uint)GenNumber1States.GenBit0, symbol: blankSymbol)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
             state: (uint)GenNumber1States.GenBit1,
             symbol: 0,
             direction: TMDirection.R
            ),
            new StateSymbolDirectionTriple
            (
              state: (uint)GenNumber1States.GenBit1,
              symbol: 1,
              direction: TMDirection.R
            )
          },

        // generate bit 0 or 1
        [new StateSymbolPair(state: (uint)GenNumber1States.GenBit1, symbol: blankSymbol)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)GenNumber1States.GenBit,
              symbol: 0,
              direction: TMDirection.R
            ),
            new StateSymbolDirectionTriple
            (
              state: (uint)GenNumber1States.GenBit,
              symbol: 1,
              direction: TMDirection.R
            ),
            new StateSymbolDirectionTriple
            (
              state: (uint)GenNumber1States.MoveToDelimiter,
              symbol: 1,
              direction: TMDirection.R
            )
          },

        // generate bit 0 or 1
        [new StateSymbolPair(state: (uint)GenNumber1States.GenBit, symbol: blankSymbol)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)GenNumber1States.GenBit,
              symbol: 0,
              direction: TMDirection.R
            ),
            new StateSymbolDirectionTriple
            (
              state: (uint)GenNumber1States.GenBit,
              symbol: 1,
              direction: TMDirection.R
            ),
            new StateSymbolDirectionTriple
            (
              state: (int)GenNumber1States.MoveToDelimiter,
              symbol: 1,
              direction: TMDirection.R
            )
          },

        // move to delimiter
        [new StateSymbolPair(state: (uint)GenNumber1States.MoveToDelimiter, symbol: blankSymbol)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (uint)GenNumber1States.MoveToDelimiter,
              symbol: blankSymbol,
              direction: TMDirection.R
            )
          },

        // delimiter reached
        [new StateSymbolPair(state: (uint)GenNumber1States.GenBit, symbol: delimiter)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: rejectingState,
              symbol: delimiter,
              direction: TMDirection.S
            )
          },

        [new StateSymbolPair(state: (uint)GenNumber1States.MoveToDelimiter, symbol: delimiter)] =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            (
              state: (int)GenNumber1States.StopGenNumber,
              symbol: delimiter,
              direction: TMDirection.S
            )
          }
        };
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
