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
    #region public members

    public Lang02_NDTM()
      : base("NDTM") { }

    #endregion

    #region public members

    public override void Setup()
    {
      Q = new int[] { qStart, 1, 2, 3, 4, 5, 6, 7, acceptingState, rejectingState };
      Gamma = new int[] { OneTapeTuringMachine.b, 0, 1, 2 };
      Sigma = new int[] { OneTapeTuringMachine.b, 0, 1 };

      Delta = new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>()
        {
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
                    Direction = TMDirection.L,
                    Shift = 2
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
                    Direction = TMDirection.L,
                    Shift = 2
                  }
              }
          },

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
                    State = 2,
                    Symbol = OneTapeTuringMachine.b,
                    Direction = TMDirection.R,
                    Shift = 2
                  }
              }
          },

          // move right or start erasing
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
                    State = 3,
                    Symbol = 0,
                    Direction = TMDirection.S
                  }
              }
          },

          // move right or start erasing
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
                    State = 3,
                    Symbol = 1,
                    Direction = TMDirection.S
                  }
              }
          },

          // stop if right delimiter reached
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
          },

          // erasing
          {
            new StateSymbolPair()
              {
                State = 3,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = 4,
                    Symbol = 2,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = 3,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = 5,
                    Symbol = 2,
                    Direction = TMDirection.S
                  }
              }
          },

          // move to left until 0 or 1 reached
          {
            new StateSymbolPair()
              {
                State = 4,
                Symbol = 2
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = 6,
                    Symbol = 2,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = 5,
                Symbol = 2
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = 7,
                    Symbol = 2,
                    Direction = TMDirection.L
                  }
              }
          },

          // left 0 or 1 reached
          {
            new StateSymbolPair()
              {
                State = 6,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = acceptingState,
                    Symbol = 0,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = 6,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = rejectingState,
                    Symbol = 1,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair()
              {
                State = 7,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = acceptingState,
                    Symbol = 2,
                    Direction = TMDirection.S
                  },
              }
          },
          {
            new StateSymbolPair()
              {
                State = 7,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple()
                  {
                    State = rejectingState,
                    Symbol = 0,
                    Direction = TMDirection.S
                  }
              }
          }
        };

      qStart = 0;
      F = new int[1] { acceptingState };

      CheckDeltaRelation();
    }

    public override bool UP => false;
    public override bool FewP => false;
    public override bool LotOfAcceptingPaths => false;
    public override bool AcceptingPathAlwaysExists => false;
    public override bool AllPathsFinite => true;

    public override long GetLTapeBound(long mu, long n) => 0;
    public override long GetRTapeBound(long mu, long n) => (n + 1);

    #endregion

    #region private members

    private const int acceptingState = 8;
    private const int rejectingState = 9;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
