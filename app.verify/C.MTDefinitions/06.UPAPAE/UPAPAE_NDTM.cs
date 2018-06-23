////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;
using ExistsAcceptingPath;

////////////////////////////////////////////////////////////////////////////////////////////////////
// UPAPAE_NDTM: it has unique accepting path that always exists;
// the machine has 2^n total amount of computation paths on inputs with length n
////////////////////////////////////////////////////////////////////////////////////////////////////

namespace MTDefinitions
{
  public class UPAPAE_NDTM : OneTapeNDTM
  {
    #region public members

    public UPAPAE_NDTM()
      : base("NDTM") { }

    #endregion

    #region public members

    public override void Setup()
    {
      Q = new int[] { qStart, 1, 2, acceptingState, rejectingState };
      Gamma = new int[] { OneTapeTuringMachine.b, 0, 1 };
      Sigma = new int[] { OneTapeTuringMachine.b, 0, 1 };

      Delta = new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>()
        {
          // start
          {
            new StateSymbolPair()
              {
                State = qStart,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = 1,
                    Symbol = 0,
                    Direction = TMDirection.S
                  },
                new StateSymbolDirectionTriple()
                  {
                    State = 2,
                    Symbol = 0,
                    Direction = TMDirection.S
                  }
              }
          },

          {
            new StateSymbolPair()
              {
                State = qStart,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = 1,
                    Symbol = 1,
                    Direction = TMDirection.S
                  },
                new StateSymbolDirectionTriple()
                  {
                    State = 2,
                    Symbol = 1,
                    Direction = TMDirection.S
                  }
              }
          },

          // state 1
          {
            new StateSymbolPair()
              {
                State = 1,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = 1,
                    Symbol = 0,
                    Direction = TMDirection.R
                  }
              }
          },

          {
            new StateSymbolPair()
              {
                State = 1,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = 1,
                    Symbol = 1,
                    Direction = TMDirection.R
                  }
              }
          },

          // state 2
          {
            new StateSymbolPair()
              {
                State = 2,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = 2,
                    Symbol = 0,
                    Direction = TMDirection.R
                  },
                new StateSymbolDirectionTriple()
                  {
                    State = 2,
                    Symbol = 0,
                    Direction = TMDirection.L
                  }
              }
          },

          {
            new StateSymbolPair()
              {
                State = 2,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = 2,
                    Symbol = 1,
                    Direction = TMDirection.R
                  },
                new StateSymbolDirectionTriple()
                  {
                    State = 2,
                    Symbol = 1,
                    Direction = TMDirection.L
                  }
              }
          },

          // accepts
          {
            new StateSymbolPair()
              {
                State = 1,
                Symbol = OneTapeTuringMachine.b
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = acceptingState,
                    Symbol = OneTapeTuringMachine.b,
                    Direction = TMDirection.S
                  }
              }
          },

          // rejects
          {
            new StateSymbolPair()
              {
                State = 2,
                Symbol = OneTapeTuringMachine.b
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = rejectingState,
                    Symbol = OneTapeTuringMachine.b,
                    Direction = TMDirection.S
                  }
              }
          }
        };

      qStart = 0;
      F = new int[1] { acceptingState };

      CheckDeltaRelation();
    }

    public override bool UP => true;
    public override bool FewP => false;
    public override bool LotOfAcceptingPaths => false;
    public override bool AcceptingPathAlwaysExists => true;
    public override bool AllPathsFinite => false;

    public override long GetLTapeBound(long mu, long n) => 1;

    #endregion

    #region private members

    private const int acceptingState = 3;
    private const int rejectingState = 4;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
