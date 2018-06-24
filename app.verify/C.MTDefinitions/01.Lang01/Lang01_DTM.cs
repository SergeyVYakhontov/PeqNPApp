////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;
using ExistsAcceptingPath;

////////////////////////////////////////////////////////////////////////////////////////////////////
// Lang01_DTM decides language L1: see reference in the paper
////////////////////////////////////////////////////////////////////////////////////////////////////

namespace MTDefinitions
{
  public class Lang01_DTM : OneTapeDTM
  {
    #region public members

    public Lang01_DTM()
      : base("DTM") { }

    #endregion

    #region public members

    public override void Setup()
    {
      Q = new int[] { 0, 1, 2, acceptingState, rejectingState };
      Gamma = new int[] { OneTapeTuringMachine.blankSymbol, 0, 1 };
      Sigma = new int[] { OneTapeTuringMachine.blankSymbol, 0, 1 };

      Delta = new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>()
        {
          // start reading symbols
          [new StateSymbolPair(state: 0, symbol: 0)] =
          new List<StateSymbolDirectionTriple>
            {
              new StateSymbolDirectionTriple()
                {
                  State = 1,
                  Symbol = 0,
                  Direction = TMDirection.R
                }
            },
          [new StateSymbolPair(state: 0, symbol: 1)] =
          new List<StateSymbolDirectionTriple>
            {
              new StateSymbolDirectionTriple()
                {
                  State = 1,
                  Symbol = 1,
                  Direction = TMDirection.R
                }
            },

          // move right for symbol 0
          [new StateSymbolPair(state: 1, symbol: 0)] =
          new List<StateSymbolDirectionTriple>
            {
              new StateSymbolDirectionTriple()
                {
                  State = 1,
                  Symbol = 0,
                  Direction = TMDirection.R
                }
            },

          // move right for symbol 1
          [new StateSymbolPair(state: 1, symbol: 1)] =
          new List<StateSymbolDirectionTriple>
            {
              new StateSymbolDirectionTriple()
                {
                  State = 1,
                  Symbol = 1,
                  Direction = TMDirection.R
                }
            },

          // right blank reached => read last symbol
          [new StateSymbolPair(state: 1, symbol: OneTapeTuringMachine.blankSymbol)]=
          new List<StateSymbolDirectionTriple>
            {
              new StateSymbolDirectionTriple()
                {
                  State = 2,
                  Symbol = OneTapeTuringMachine.blankSymbol,
                  Direction = TMDirection.S
                }
            },
          [new StateSymbolPair(state: 2, symbol: OneTapeTuringMachine.blankSymbol)] =
          new List<StateSymbolDirectionTriple>
            {
              new StateSymbolDirectionTriple()
                {
                  State = 2,
                  Symbol = OneTapeTuringMachine.blankSymbol,
                  Direction = TMDirection.L
                }
            },

          // (if last symbol == 1) => accept
          [new StateSymbolPair(state: 2, symbol: 1)] =
          new List<StateSymbolDirectionTriple>
            {
              new StateSymbolDirectionTriple()
                {
                  State = acceptingState,
                  Symbol = 1,
                  Direction = TMDirection.S
                }
            },

          // (if last symbol == 0) => reject
          [new StateSymbolPair(state: 2, symbol: 0)] =
          new List<StateSymbolDirectionTriple>
            {
              new StateSymbolDirectionTriple()
                {
                  State = rejectingState,
                  Symbol = 0,
                  Direction = TMDirection.S
                }
            }
        };

      qStart = 0;
      F = new int[] { acceptingState };

      CheckDeltaRelation();
    }

    public override bool UP => true;
    public override bool FewP => false;
    public override bool LotOfAcceptingPaths => false;
    public override bool AcceptingPathAlwaysExists => false;
    public override bool AllPathsFinite => true;

    public override long GetLTapeBound(long mu, long n) => 0;
    public override long GetRTapeBound(long mu, long n) => (n + 1);
    #endregion

    #region private members

    private const int acceptingState = 3;
    private const int rejectingState = 4;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
