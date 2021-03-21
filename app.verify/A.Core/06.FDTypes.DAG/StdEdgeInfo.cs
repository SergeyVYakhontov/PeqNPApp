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
  public struct StdEdgeInfo :
    IEquatable<StdEdgeInfo>,
    IObjectWithId
  {
    #region Ctors

    public StdEdgeInfo(long id)
    {
      this.Id = id;
    }

    #endregion

    #region public members

    public long Id { get; }

    public override bool Equals(object? obj)
    {
      Ensure.That(obj).IsNotNull();

      StdEdgeInfo other = (StdEdgeInfo)obj!;

      return this == other;
    }

    public override int GetHashCode() => unchecked((int)Id);

    public bool Equals(StdEdgeInfo other) => this == other;

    public static bool operator ==(StdEdgeInfo left, StdEdgeInfo right)
    {
      return left.Id == right.Id;
    }

    public static bool operator !=(StdEdgeInfo left, StdEdgeInfo right)
    {
      return !(left == right);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
