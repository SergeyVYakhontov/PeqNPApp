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
  public readonly struct NestedCommsGraphNodeInfo :
    IEquatable<NestedCommsGraphNodeInfo>,
    IObjectWithId
  {
    #region Ctors

    public NestedCommsGraphNodeInfo(long id)
    {
      this.Id = id;
    }

    #endregion

    #region public members

    public long Id { get; }

    public override bool Equals(object? obj)
    {
      Ensure.That(obj).IsNotNull();

      NestedCommsGraphNodeInfo other = (NestedCommsGraphNodeInfo)obj!;

      return this == other;
    }

    public override int GetHashCode() => unchecked((int)Id);

    public bool Equals(NestedCommsGraphNodeInfo other) => this == other;

    public static bool operator ==(in NestedCommsGraphNodeInfo left, in NestedCommsGraphNodeInfo right)
    {
      return left.Id == right.Id;
    }

    public static bool operator !=(in NestedCommsGraphNodeInfo left, in NestedCommsGraphNodeInfo right)
    {
      return !(left == right);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////

