////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public class ComputationStep
  {
    #region Ctors

    public ComputationStep() { }

    public ComputationStep(ComputationStep compStep)
    {
      q = compStep.q;
      s = compStep.s;
      qNext = compStep.qNext;
      sNext = compStep.sNext;
      m = compStep.m;
      Shift = compStep.Shift;
      kappaTape = compStep.kappaTape;
      kappaStep = compStep.kappaStep;
    }

    #endregion

    #region public methods

    public int q { get; set; }
    public int s { get; set; }

    public int qNext { get; set; }
    public int sNext { get; set; }

    public TMDirection m { get; set; }
    public long Shift { get; set; }

    public long kappaTape { get; set; }
    public long kappaStep { get; set; }

    public override bool Equals(Object obj)
    {
      ComputationStep compStep = (ComputationStep)obj;

      return
        (q == compStep.q) &&
        (s == compStep.s) &&
        (qNext == compStep.qNext) &&
        (sNext == compStep.sNext) &&
        (m == compStep.m) &&
        (Shift == compStep.Shift) &&
        (kappaTape == compStep.kappaTape) &&
        (kappaStep == compStep.kappaStep);
    }

    public override int GetHashCode()
    {
      return (int)kappaStep;
    }

    public override String ToString()
    {
      return String.Format
        ("(q={0}, s={1}, q'={2}, s'={3}, m={4}, sh={5}, tp={6}, st={7})",
        q, s, qNext, sNext, m, Shift, kappaTape, kappaStep);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
