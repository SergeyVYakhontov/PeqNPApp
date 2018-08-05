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
  public class ComputationStep :
    IEquatable<ComputationStep>,
    IComparable<ComputationStep>
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

    public uint q { get; set; }
    public int s { get; set; }

    public uint qNext { get; set; }
    public int sNext { get; set; }

    public TMDirection m { get; set; }
    public long Shift { get; set; }

    public long kappaTape { get; set; }
    public ulong kappaStep { get; set; }

    public override bool Equals(Object obj)
    {
      ComputationStep other = (ComputationStep)obj;

      return this == other;
    }

    public override int GetHashCode() => unchecked((int)kappaStep);

    public override String ToString() =>
      $"(q={q}, s={s}, q'={qNext}, s'={sNext}, m={m}, sh={Shift}, tp={kappaTape}, st={kappaStep})";

    public bool Equals(ComputationStep other)
    {
      return this == other;
    }

    public int CompareTo(ComputationStep other)
    {
      return compStepComparer.Compare(this, other);
    }

    public static bool operator ==(ComputationStep left, ComputationStep right)
    {
      return
        (left.q == right.q) &&
        (left.s == right.s) &&
        (left.qNext == right.qNext) &&
        (left.sNext == right.sNext) &&
        (left.m == right.m) &&
        (left.Shift == right.Shift) &&
        (left.kappaTape == right.kappaTape) &&
        (left.kappaStep == right.kappaStep);
    }

    public static bool operator !=(ComputationStep left, ComputationStep right)
    {
      return !(left == right);
    }

    public static bool operator <(ComputationStep left, ComputationStep right)
    {
      return left.CompareTo(right) < 0;
    }

    public static bool operator <=(ComputationStep left, ComputationStep right)
    {
      return left.CompareTo(right) <= 0;
    }

    public static bool operator >(ComputationStep left, ComputationStep right)
    {
      return left.CompareTo(right) > 0;
    }

    public static bool operator >=(ComputationStep left, ComputationStep right)
    {
      return left.CompareTo(right) >= 0;
    }

    #endregion

    #region private members

    private static readonly CompStepComparer compStepComparer = new CompStepComparer();

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
