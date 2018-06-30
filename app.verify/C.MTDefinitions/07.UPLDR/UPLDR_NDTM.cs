﻿////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;
using ExistsAcceptingPath;

////////////////////////////////////////////////////////////////////////////////////////////////////
// UPLDR_NDTM: it has large transition relation
////////////////////////////////////////////////////////////////////////////////////////////////////

namespace MTDefinitions
{
  public class UPLDR_NDTM : OneTapeNDTM
  {
    #region public members

    public UPLDR_NDTM()
      : base("NDTM") { }

    #endregion

    #region public members

    public override void Setup()
    {
      const uint acceptingState = 1;
      const uint rejectingState = 2;

      const int preAcceptingState = 3;
      const int variousStatesStart = 4;

      const int variousStatesEnd = 5;
      const int variousSymbolsStart = 0;
      const int variousSymbolsEnd = 3;

      const int acceptingSymbol = variousSymbolsEnd + 1;
      const int acceptingSymbolReplace = acceptingSymbol + 1;

      List<uint> qList = new List<uint> { qStart, acceptingState, rejectingState };

      new IntSegment(variousStatesStart, variousStatesEnd)
        .ForEach(q => qList.Add((uint)q));

      qList.Add(preAcceptingState);
      Q = qList.ToArray();

      List<int> gammaList = new List<int>() { OneTapeTuringMachine.blankSymbol };

      new IntSegment(variousSymbolsStart, variousSymbolsEnd)
        .ForEach(g => gammaList.Add((int)g));

      gammaList.Add(acceptingSymbol);
      gammaList.Add(acceptingSymbolReplace);
      Gamma = gammaList.ToArray();

      List<int> sigmaList = new List<int>() { OneTapeTuringMachine.blankSymbol, };

      new IntSegment(variousSymbolsStart, variousSymbolsEnd)
        .ForEach(g => sigmaList.Add((int)g));

      sigmaList.Add(acceptingSymbol);
      Sigma = sigmaList.ToArray();

      Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> delta =
        new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>();

      foreach (uint q in Q)
      {
        foreach (int s in Gamma)
        {
          if ((q == preAcceptingState) ||
              (s == acceptingSymbol))
          {
            continue;
          }

          StateSymbolPair deltaPairKey =
            new StateSymbolPair
              {
                State = q,
                Symbol = s
              };

          List<StateSymbolDirectionTriple> deltaPairValueList = new List<StateSymbolDirectionTriple>();

          foreach (uint qNext in Q)
          {
            foreach (int sNext in Gamma)
            {
              if ((qNext == qStart) ||
                  (qNext == preAcceptingState) ||
                  (qNext == acceptingState) ||
                  (sNext == blankSymbol) ||
                  (sNext == acceptingSymbol))
              {
                continue;
              }

              deltaPairValueList.Add(
                  new StateSymbolDirectionTriple()
                    {
                      State = qNext,
                      Symbol = sNext,
                      Direction = TMDirection.L
                    });

              deltaPairValueList.Add(
                  new StateSymbolDirectionTriple()
                    {
                      State = qNext,
                      Symbol = sNext,
                      Direction = TMDirection.R
                    });
            }
          }

          delta.Add(deltaPairKey, deltaPairValueList);
        }
      }

      foreach (int s in Gamma)
      {
        if ((s == OneTapeTuringMachine.blankSymbol) ||
            (s == acceptingSymbol))
        {
          continue;
        }

        StateSymbolPair preAcceptingDeltaPairKey =
          new StateSymbolPair()
          {
            State = qStart,
            Symbol = s
          };

        delta[preAcceptingDeltaPairKey].Add(
          new StateSymbolDirectionTriple()
            {
              State = preAcceptingState,
              Symbol = s,
              Direction = TMDirection.R
            });
      }

      foreach (int s in Gamma)
      {
        if ((s == OneTapeTuringMachine.blankSymbol) ||
            (s == acceptingSymbol))
        {
          continue;
        }

        StateSymbolPair preAcceptingDeltaPairKey =
          new StateSymbolPair()
          {
            State = preAcceptingState,
            Symbol = s
          };

        List<StateSymbolDirectionTriple> preAcceptingDeltaPairValueList =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple()
            {
              State = preAcceptingState,
              Symbol = s,
              Direction = TMDirection.R
            }
          };

        delta.Add(preAcceptingDeltaPairKey, preAcceptingDeltaPairValueList);
      }

      {
        StateSymbolPair acceptingDeltaPairKey =
          new StateSymbolPair()
          {
            State = preAcceptingState,
            Symbol = acceptingSymbol
          };

        List<StateSymbolDirectionTriple> acceptingDeltaPairValueList =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
            {
              State = (int)acceptingState,
              Symbol = acceptingSymbolReplace,
              Direction = TMDirection.S
            }
          };

        delta.Add(acceptingDeltaPairKey, acceptingDeltaPairValueList);
      }

      Delta = delta;

      qStart = 0;
      F = new uint[] { acceptingState };

      CheckDeltaRelation();
    }

    public override bool UP => true;
    public override bool FewP => false;
    public override bool LotOfAcceptingPaths => false;
    public override bool AcceptingPathAlwaysExists => true;
    public override bool AllPathsFinite => false;

    public override long GetLTapeBound(ulong mu, ulong n) => 0;
    public override long GetRTapeBound(ulong mu, ulong n) => (long)(n * n);

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
