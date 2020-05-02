﻿////////////////////////////////////////////////////////////////////////////////////////////////////

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
  public class NCGCommonPathGraphEdgeInfo :
    IEquatable<NCGCommonPathGraphEdgeInfo>,
    IObjectWithId
  {
    #region Ctors

    public NCGCommonPathGraphEdgeInfo(long id)
    {
      this.Id = id;
    }

    #endregion

    #region public members

    public long Id { get; }

    public override bool Equals(object obj)
    {
      Ensure.That(obj).IsNotNull();

      NCGCommonPathGraphEdgeInfo other = (NCGCommonPathGraphEdgeInfo)obj;

      return this == other;
    }

    public override int GetHashCode() => unchecked((int)Id);

    public bool Equals(NCGCommonPathGraphEdgeInfo other) => this == other;

    public static bool operator ==(NCGCommonPathGraphEdgeInfo left, NCGCommonPathGraphEdgeInfo right)
    {
      return left.Id == right.Id;
    }

    public static bool operator !=(NCGCommonPathGraphEdgeInfo left, NCGCommonPathGraphEdgeInfo right)
    {
      return !(left == right);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
