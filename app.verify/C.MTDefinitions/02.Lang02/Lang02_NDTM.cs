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
      Q = new uint[] { qStart, 1, 2, 3, 4, 5, 6, 7, 8, 9, acceptingState, rejectingState };
      Gamma = new int[] { blankSymbol, 0, 1, 2 };
      Sigma = new int[] { blankSymbol, 0, 1 };

      qStart = 0;
      F = new uint[] { acceptingState };

      Delta = new Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>>
        {
          {
            new StateSymbolPair
              {
                State = qStart,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = 1,
                    Symbol = 0,
                    Direction = TMDirection.L
                  }
              }
          },

          {
            new StateSymbolPair
              {
                State = qStart,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = 1,
                    Symbol = 1,
                    Direction = TMDirection.L
                  }
              }
          },

          {
            new StateSymbolPair
              {
                State = 1,
                Symbol = blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = 2,
                    Symbol = blankSymbol,
                    Direction = TMDirection.L
                  }
              }
          },

          {
            new StateSymbolPair
              {
                State = 2,
                Symbol = blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = 3,
                    Symbol = blankSymbol,
                    Direction = TMDirection.R
                  }
              }
          },

          {
            new StateSymbolPair
              {
                State = 3,
                Symbol = blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = 4,
                    Symbol = blankSymbol,
                    Direction = TMDirection.R
                  }
              }
          },

          // move right or start erasing
          {
            new StateSymbolPair
              {
                State = 4,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = 4,
                    Symbol = 0,
                    Direction = TMDirection.R
                  },
                new StateSymbolDirectionTriple
                  {
                    State = 5,
                    Symbol = 0,
                    Direction = TMDirection.S
                  }
              }
          },

          // move right or start erasing
          {
            new StateSymbolPair
              {
                State = 4,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = 4,
                    Symbol = 1,
                    Direction = TMDirection.R
                  },
                new StateSymbolDirectionTriple
                  {
                    State = 5,
                    Symbol = 1,
                    Direction = TMDirection.S
                  }
              }
          },

          // stop if right delimiter reached
          {
            new StateSymbolPair
              {
                State = 4,
                Symbol = blankSymbol
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (int)rejectingState,
                    Symbol = blankSymbol,
                    Direction = TMDirection.S
                  }
              }
          },

          // erasing
          {
            new StateSymbolPair
              {
                State = 5,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = 6,
                    Symbol = 2,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = 5,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = 7,
                    Symbol = 2,
                    Direction = TMDirection.S
                  }
              }
          },

          // move to left until 0 or 1 reached
          {
            new StateSymbolPair
              {
                State = 6,
                Symbol = 2
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = 8,
                    Symbol = 2,
                    Direction = TMDirection.L
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = 7,
                Symbol = 2
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = 9,
                    Symbol = 2,
                    Direction = TMDirection.L
                  }
              }
          },

          // left 0 or 1 reached
          {
            new StateSymbolPair
              {
                State = 8,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (int)acceptingState,
                    Symbol = 0,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = 8,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (int)rejectingState,
                    Symbol = 1,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = 9,
                Symbol = 1
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (int)acceptingState,
                    Symbol = 2,
                    Direction = TMDirection.S
                  }
              }
          },
          {
            new StateSymbolPair
              {
                State = 9,
                Symbol = 0
              },
            new List<StateSymbolDirectionTriple>
              {
                new StateSymbolDirectionTriple
                  {
                    State = (int)rejectingState,
                    Symbol = 0,
                    Direction = TMDirection.S
                  }
              }
          }
        };

      CheckDeltaRelation();
    }

    public override bool UP => false;
    public override bool FewP => false;
    public override bool LotOfAcceptingPaths => false;
    public override bool AcceptingPathAlwaysExists => false;
    public override bool AllPathsFinite => true;

    public override long GetLTapeBound(long mu, long n) => -2;
    public override long GetRTapeBound(long mu, long n) => n + 1;

    #endregion

    #region private members

    private const uint acceptingState = 10;
    private const uint rejectingState = 11;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
