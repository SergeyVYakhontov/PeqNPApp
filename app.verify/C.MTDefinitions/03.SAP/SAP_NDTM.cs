////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;
using ExistsAcceptingPath;

////////////////////////////////////////////////////////////////////////////////////////////////////
// SAP_NDTM: it has several accepting paths
////////////////////////////////////////////////////////////////////////////////////////////////////

namespace MTDefinitions
{
  public class SAP_NDTM : OneTapeNDTM
  {
    #region public members

    public SAP_NDTM()
      : base("NDTM") { }

    #endregion

    #region public members

    public override void Setup()
    {
      Q = new uint[] { qStart, 1, 2, acceptingState, rejectingState };
      Gamma = new int[] { blankSymbol, 0, 1 };
      Sigma = new int[] { blankSymbol, 0, 1 };

      Delta = new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
        {
          [new StateSymbolPair(state: qStart, symbol: 0)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: 1,
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
                  ),
                new StateSymbolDirectionTriple
                  (
                    state: 2,
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
                  ),
                new StateSymbolDirectionTriple
                  (
                    state: 2,
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
                    state: 1,
                    symbol: 0,
                    direction: TMDirection.R
                  ),
                new StateSymbolDirectionTriple
                  (
                    state: 2,
                    symbol: 0,
                    direction: TMDirection.R
                  )
              },

          [new StateSymbolPair(state: 2, symbol: 1)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: 1,
                    symbol: 1,
                    direction: TMDirection.R
                  ),
                new StateSymbolDirectionTriple
                  (
                    state: 2,
                    symbol: 1,
                    direction: TMDirection.R
                  )
              },

          // accepts
          [new StateSymbolPair(state: 1, symbol: blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: rejectingState,
                    symbol: 1,
                    direction: TMDirection.S
                  )
              },

          [new StateSymbolPair(state: 2, symbol: blankSymbol)] =
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  (
                    state: acceptingState,
                    symbol: 1,
                    direction: TMDirection.S
                  )
              }
        };

      qStart = 0;
      F = new uint[] { acceptingState };

      CheckDeltaRelation();
    }

    public override bool UP => false;
    public override bool FewP => false;
    public override bool LotOfAcceptingPaths => false;
    public override bool AcceptingPathAlwaysExists => false;
    public override bool AllPathsFinite => true;

    public override long GetLTapeBound(ulong mu, ulong n) => 0;
    public override long GetRTapeBound(ulong mu, ulong n) => (long)n + 1;

    #endregion

    #region private members

    private const uint acceptingState = 3;
    private const uint rejectingState = 4;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
