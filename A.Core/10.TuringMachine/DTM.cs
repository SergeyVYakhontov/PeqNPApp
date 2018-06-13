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
  public abstract class OneTapeDTM : OneTapeTuringMachine
  {
    #region Ctors

    public OneTapeDTM(String name)
      : base(name) { }

    #endregion

    #region public members

    public override bool UP => true;
    public override bool FewP => false;
    public override bool LotOfAcceptingPaths => false;
    public override bool AllPathsFinite => false;
    public override bool AcceptingPathAlwaysExists => false;

    #endregion

    #region private members

    protected override void CheckDeltaRelation()
    {
      base.CheckDeltaRelation();

      Ensure.That(Delta.All(p => (p.Value.Count <= 1))).IsTrue();
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
