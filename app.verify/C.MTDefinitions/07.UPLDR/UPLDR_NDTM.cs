////////////////////////////////////////////////////////////////////////////////////////////////////

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

      List<int> gammaList = new List<int> { blankSymbol };

      new IntSegment(variousSymbolsStart, variousSymbolsEnd)
        .ForEach(g => gammaList.Add(g));

      gammaList.Add(acceptingSymbol);
      gammaList.Add(acceptingSymbolReplace);
      Gamma = gammaList.ToArray();

      List<int> sigmaList = new List<int> { blankSymbol };

      new IntSegment(variousSymbolsStart, variousSymbolsEnd)
        .ForEach(g => sigmaList.Add(g));

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
              (
                state: q,
                symbol: s
              );

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
                  new StateSymbolDirectionTriple
                    (
                      state: qNext,
                      symbol: sNext,
                      direction: TMDirection.L
                    ));

              deltaPairValueList.Add(
                  new StateSymbolDirectionTriple
                    (
                      state: qNext,
                      symbol: sNext,
                      direction: TMDirection.R
                    ));
            }
          }

          delta.Add(deltaPairKey, deltaPairValueList);
        }
      }

      foreach (int s in Gamma)
      {
        if ((s == blankSymbol) ||
            (s == acceptingSymbol))
        {
          continue;
        }

        StateSymbolPair preAcceptingDeltaPairKey =
          new StateSymbolPair
            (
              state: qStart,
              symbol: s
            );

        delta[preAcceptingDeltaPairKey].Add(
          new StateSymbolDirectionTriple
            (
              state: preAcceptingState,
              symbol: s,
              direction: TMDirection.R
            ));
      }

      foreach (int s in Gamma)
      {
        if ((s == blankSymbol) ||
            (s == acceptingSymbol))
        {
          continue;
        }

        StateSymbolPair preAcceptingDeltaPairKey =
          new StateSymbolPair
            (
              state: preAcceptingState,
              symbol: s
            );

        List<StateSymbolDirectionTriple> preAcceptingDeltaPairValueList =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
              (
                state: preAcceptingState,
                symbol: s,
                direction: TMDirection.R
              )
          };

        delta.Add(preAcceptingDeltaPairKey, preAcceptingDeltaPairValueList);
      }

      {
        StateSymbolPair acceptingDeltaPairKey =
          new StateSymbolPair
            (
              state: preAcceptingState,
              symbol: acceptingSymbol
            );

        List<StateSymbolDirectionTriple> acceptingDeltaPairValueList =
          new List<StateSymbolDirectionTriple>
          {
            new StateSymbolDirectionTriple
              (
                state: acceptingState,
                symbol: acceptingSymbolReplace,
                direction: TMDirection.S
              )
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
