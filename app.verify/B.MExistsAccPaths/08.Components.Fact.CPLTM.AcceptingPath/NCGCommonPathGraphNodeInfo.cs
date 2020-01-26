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
  public readonly struct NCGCommonPathGraphNodeInfo :
    IEquatable<NCGCommonPathGraphNodeInfo>,
    IObjectWithId
  {
    #region Ctors

    public NCGCommonPathGraphNodeInfo(long id)
    {
      this.Id = id;
    }

    #endregion

    #region public members

    public long Id { get; }

    public override bool Equals(object obj)
    {
      Ensure.That(obj).IsNotNull();

      NCGCommonPathGraphNodeInfo other = (NCGCommonPathGraphNodeInfo)obj;

      return this == other;
    }

    public override int GetHashCode() => unchecked((int)Id);

    public bool Equals(NCGCommonPathGraphNodeInfo other) => this == other;

    public static bool operator ==(in NCGCommonPathGraphNodeInfo left, in NCGCommonPathGraphNodeInfo right)
    {
      return left.Id == right.Id;
    }

    public static bool operator !=(in NCGCommonPathGraphNodeInfo left, in NCGCommonPathGraphNodeInfo right)
    {
      return !(left == right);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////

