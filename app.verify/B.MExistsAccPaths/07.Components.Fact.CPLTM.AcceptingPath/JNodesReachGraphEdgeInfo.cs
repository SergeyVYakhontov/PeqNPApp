////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ninject;
using EnsureThat;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public class JNodesReachGraphEdgeInfo :
    IEquatable<JNodesReachGraphEdgeInfo>,
    IObjectWithId
  {
    #region Ctors

    public JNodesReachGraphEdgeInfo(long id)
    {
      this.Id = id;
    }

    #endregion

    #region public members

    public long Id { get; }

    public override bool Equals(object obj)
    {
      Ensure.That(obj).IsNotNull();

      JNodesReachGraphEdgeInfo other = (JNodesReachGraphEdgeInfo)obj;

      return this == other;
    }

    public override int GetHashCode() => unchecked((int)Id);

    public bool Equals(JNodesReachGraphEdgeInfo other) => this == other;

    public static bool operator ==(in JNodesReachGraphEdgeInfo left, in JNodesReachGraphEdgeInfo right)
    {
      return left.Id == right.Id;
    }

    public static bool operator !=(in JNodesReachGraphEdgeInfo left, in JNodesReachGraphEdgeInfo right)
    {
      return !(left == right);
    }

    #endregion

    #region public members

    public SortedSet<long> InnerJointNodes { get; } = new SortedSet<long>();

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
