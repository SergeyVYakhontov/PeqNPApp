////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using EnsureThat;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public readonly struct CompStepNodePair :
    IEquatable<CompStepNodePair>,
    IComparable<CompStepNodePair>
  {
    #region Ctors

    public CompStepNodePair(long variable, long uNode, long vNode)
    {
      this.Variable = variable;
      this.uNode = uNode;
      this.vNode = vNode;
    }

    #endregion

    #region public members

    public long Variable { get; }

    public long uNode { get; }
    public long vNode { get; }

    public override bool Equals(object obj)
    {
      Ensure.That(obj).IsNotNull();

      CompStepNodePair other = (CompStepNodePair)obj;

      return this == other;
    }

    public override int GetHashCode() => unchecked((int)Variable);

    public override string ToString() => $"({Variable}, {uNode}, {vNode})";

    public bool Equals(CompStepNodePair other) => this == other;

    public int CompareTo(CompStepNodePair other)
    {
      return compStepNodePairComparer.Compare(this, other);
    }

    public static bool operator ==(CompStepNodePair left, CompStepNodePair right)
    {
      return
        (left.Variable == right.Variable) &&
        (left.uNode == right.uNode) &&
        (left.vNode == right.vNode);
    }

    public static bool operator !=(CompStepNodePair left, CompStepNodePair right)
    {
      return !(left == right);
    }

    public static bool operator <(CompStepNodePair left, CompStepNodePair right)
    {
      return left.CompareTo(right) < 0;
    }

    public static bool operator <=(CompStepNodePair left, CompStepNodePair right)
    {
      return left.CompareTo(right) <= 0;
    }

    public static bool operator >(CompStepNodePair left, CompStepNodePair right)
    {
      return left.CompareTo(right) > 0;
    }

    public static bool operator >=(CompStepNodePair left, CompStepNodePair right)
    {
      return left.CompareTo(right) >= 0;
    }

    #endregion

    #region private members

    private static readonly CompStepNodePairComparer compStepNodePairComparer =
      new CompStepNodePairComparer();

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
