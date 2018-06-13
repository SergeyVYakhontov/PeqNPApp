////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using EnsureThat;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public abstract class OneTapeNDTM : OneTapeTuringMachine
  {
    #region Ctors

    public OneTapeNDTM(string name)
      : base(name) { }

    #endregion

    #region public members

    public override bool UP => false;
    public override bool FewP => false;
    public override bool LotOfAcceptingPaths => false;
    public override bool AllPathsFinite => false;
    public override bool AcceptingPathAlwaysExists => false;

    #endregion

    #region private members

    protected override void CheckDeltaRelation()
    {
      base.CheckDeltaRelation();

      Ensure.That(Delta.Any(p => (p.Value.Count > 1))).IsTrue();
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
