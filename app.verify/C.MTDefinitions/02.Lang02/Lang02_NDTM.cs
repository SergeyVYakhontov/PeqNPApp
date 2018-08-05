////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;
using ExistsAcceptingPath;

////////////////////////////////////////////////////////////////////////////////////////////////////
// Lang02_NDTM decides language L2: see reference in the paper
////////////////////////////////////////////////////////////////////////////////////////////////////

namespace MTDefinitions
{
  public class Lang02_NDTM : OneTapeNDTM
  {
    #region Ctors

    public Lang02_NDTM() : base("NDTM") { }

    #endregion

    #region public members

    public override void Setup()
    {
      Q = new uint[] {
        qStart, 1, 2, 3, 4, 5, 6, 7, 8, 9,
        acceptingState, rejectingState };
      Gamma = new int[] { blankSymbol, 0, 1, 2 };
      Sigma = new int[] { blankSymbol, 0, 1 };

      qStart = 0;
      F = new uint[] { acceptingState };

      Delta = new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
        {
          [new StateSymbolPair(state: qStart, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: 1,
                    symbol: 0,
                    direction: TMDirection.L
                  )
              },

          [new StateSymbolPair(state: qStart, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: 1,
                    symbol: 1,
                    direction: TMDirection.L
                  )
              },

          [new StateSymbolPair(state: 1, symbol: blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: 2,
                    symbol: blankSymbol,
                    direction: TMDirection.L
                  )
              },

          [new StateSymbolPair(state: 2, symbol: blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: 3,
                    symbol: blankSymbol,
                    direction: TMDirection.R
                  )
              },

          [new StateSymbolPair(state: 3, symbol: blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: 4,
                    symbol: blankSymbol,
                    direction: TMDirection.R
                  )
              },

          // move right or start erasing
          [new StateSymbolPair(state: 4, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: 4,
                    symbol: 0,
                    direction: TMDirection.R
                  ),
                new StateSymbolDirectionTriple
                  (
                    state: 5,
                    symbol: 0,
                    direction: TMDirection.S
                  )
              },

          // move right or start erasing
          [new StateSymbolPair(state: 4, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: 4,
                    symbol: 1,
                    direction: TMDirection.R
                  ),
                new StateSymbolDirectionTriple
                  (
                    state: 5,
                    symbol: 1,
                    direction: TMDirection.S
                  )
              },

          // stop if right delimiter reached
          [new StateSymbolPair(state: 4, symbol: blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: rejectingState,
                    symbol: blankSymbol,
                    direction: TMDirection.S
                  )
              },

          // erasing
          [new StateSymbolPair(state: 5, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: 6,
                    symbol: 2,
                    direction: TMDirection.S
                  )
              },
          [new StateSymbolPair(state: 5, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: 7,
                    symbol: 2,
                    direction: TMDirection.S
                  )
              },

          // move to left until 0 or 1 reached
          [new StateSymbolPair(state: 6, symbol: 2)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: 8,
                    symbol: 2,
                    direction: TMDirection.L
                  )
              },
          [new StateSymbolPair(state: 7, symbol: 2)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: 9,
                    symbol: 2,
                    direction: TMDirection.L
                  )
              },

          // left 0 or 1 reached
          [new StateSymbolPair(state: 8, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: acceptingState,
                    symbol: 0,
                    direction: TMDirection.S
                  )
              },
          [new StateSymbolPair(state: 8, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: rejectingState,
                    symbol: 1,
                    direction: TMDirection.S
                  )
              },
          [new StateSymbolPair(state: 9, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: acceptingState,
                    symbol: 2,
                    direction: TMDirection.S
                  )
              },
          [new StateSymbolPair(state: 9, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: rejectingState,
                    symbol: 0,
                    direction: TMDirection.S
                  )
              }
        };

      CheckDeltaRelation();
    }

    public override bool UP => false;
    public override bool FewP => false;
    public override bool LotOfAcceptingPaths => false;
    public override bool AcceptingPathAlwaysExists => false;
    public override bool AllPathsFinite => true;

    public override long GetLTapeBound(ulong mu, ulong n) => -2;
    public override long GetRTapeBound(ulong mu, ulong n) => (long)n + 1;

    #endregion

    #region private members

    private const uint acceptingState = 10;
    private const uint rejectingState = 11;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
