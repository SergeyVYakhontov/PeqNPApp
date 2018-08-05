////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;
using ExistsAcceptingPath;

////////////////////////////////////////////////////////////////////////////////////////////////////
// UPAPMNE_NDTM: it has unique accepting path that may not exists
////////////////////////////////////////////////////////////////////////////////////////////////////

namespace MTDefinitions
{
  public class UPAPMNE_NDTM : OneTapeNDTM
  {
    #region public members

    public UPAPMNE_NDTM()
      : base("NDTM") { }

    #endregion

    #region public members

    public override void Setup()
    {
      const uint acceptingState = 3;
      const uint rejectingState = 4;

      Q = new uint[] { qStart, 1, 2, acceptingState, rejectingState };
      Gamma = new int[] { blankSymbol, 0, 1, 2 };
      Sigma = new int[] { blankSymbol, 0, 1, 2 };

      Delta = new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
        {
          // start
          [new StateSymbolPair(state: qStart, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: 1,
                    symbol: 0,
                    direction: TMDirection.S
                  ),
                new StateSymbolDirectionTriple
                  (
                    state: 2,
                    symbol: 0,
                    direction: TMDirection.S
                  )
              },

          [new StateSymbolPair(state: qStart, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: 1,
                    symbol: 1,
                    direction: TMDirection.S
                  ),
                new StateSymbolDirectionTriple
                  (
                    state: 2,
                    symbol: 1,
                    direction: TMDirection.S
                  )
              },

          // state 1
          [new StateSymbolPair(state: 1, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: 1,
                    symbol: 0,
                    direction: TMDirection.R
                  )
              },

          [new StateSymbolPair(state: 1, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: 1,
                    symbol: 1,
                    direction: TMDirection.R
                  )
              },

          // state 2
          [new StateSymbolPair(state: 2, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: 2,
                    symbol: 0,
                    direction: TMDirection.R
                  ),
                new StateSymbolDirectionTriple
                  (
                    state: 2,
                    symbol: 0,
                    direction: TMDirection.L
                  )
              },

          [new StateSymbolPair(state: 2, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: 2,
                    symbol: 1,
                    direction: TMDirection.R
                  ),
                new StateSymbolDirectionTriple
                  (
                    state: 2,
                    symbol: 1,
                    direction: TMDirection.L
                  )
              },

          // accepts
          [new StateSymbolPair(state: 1, symbol: 2)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: acceptingState,
                    symbol: blankSymbol,
                    direction: TMDirection.S
                  )
              },

          // rejects
          [new StateSymbolPair(state: 1, symbol: blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: rejectingState,
                    symbol: blankSymbol,
                    direction: TMDirection.S
                  )
              },

          [new StateSymbolPair(state: 2, symbol: blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: rejectingState,
                    symbol: blankSymbol,
                    direction: TMDirection.S
                  )
              }
        };

      qStart = 0;
      F = new uint[] { acceptingState };

      CheckDeltaRelation();
    }

    public override bool UP => true;
    public override bool FewP => false;
    public override bool LotOfAcceptingPaths => false;
    public override bool AcceptingPathAlwaysExists => false;
    public override bool AllPathsFinite => false;

    public override long GetLTapeBound(ulong mu, ulong n) => 1;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
