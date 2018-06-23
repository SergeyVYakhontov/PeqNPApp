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
  public class CompStepNodePair : IComparable<CompStepNodePair>
  {
    #region Ctors

    public int CompareTo(CompStepNodePair s)
    {
      return compStepNodePairComparer.Compare(this, s);
    }

    #endregion

    #region public members

    public long Variable { get; set; }

    public long uNode { get; set; }
    public long vNode { get; set; }

    public override string ToString()
    {
      return $"({Variable}, {uNode}, {vNode})";
    }

    #endregion

    #region private members

    private static readonly CompStepNodePairComparer compStepNodePairComparer =
      new CompStepNodePairComparer();

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
