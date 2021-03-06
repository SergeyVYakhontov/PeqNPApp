﻿////////////////////////////////////////////////////////////////////////////////////////////////////

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
      Q = new uint[] { 0, 1, 2, acceptingState, rejectingState };
      Gamma = new int[] { blankSymbol, 0, 1 };
      Sigma = new int[] { blankSymbol, 0, 1 };

      Delta = new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
        {
          // start reading symbols
          [new StateSymbolPair(state: 0, symbol: 0)] =
          new List<StateSymbolDirectionTriple>
            {
              new StateSymbolDirectionTriple
                (
                  state: 1,
                  symbol: 0,
                  direction: TMDirection.R
                )
            },
          [new StateSymbolPair(state: 0, symbol: 1)] =
          new List<StateSymbolDirectionTriple>
            {
              new StateSymbolDirectionTriple
                (
                  state: 1,
                  symbol: 1,
                  direction: TMDirection.R
                )
            },

          // move right for symbol 0
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

          // move right for symbol 1
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

          // right blank reached => read last symbol
          [new StateSymbolPair(state: 1, symbol: blankSymbol)]=
          new List<StateSymbolDirectionTriple>
            {
              new StateSymbolDirectionTriple
                (
                  state: 2,
                  symbol: blankSymbol,
                  direction: TMDirection.S
                )
            },
          [new StateSymbolPair(state: 2, symbol: blankSymbol)] =
          new List<StateSymbolDirectionTriple>
            {
              new StateSymbolDirectionTriple
                (
                  state: 2,
                  symbol: blankSymbol,
                  direction: TMDirection.L
                )
            },

          // (if last symbol == 1) => accept
          [new StateSymbolPair(state: 2, symbol: 1)] =
          new List<StateSymbolDirectionTriple>
            {
              new StateSymbolDirectionTriple
                (
                  state: acceptingState,
                  symbol: 1,
                  direction: TMDirection.S
                )
            },

          // (if last symbol == 0) => reject
          [new StateSymbolPair(state: 2, symbol: 0)] =
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

      qStart = 0;
      F = new uint[] { acceptingState };

      CheckDeltaRelation();
    }

    public override bool UP => true;
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
