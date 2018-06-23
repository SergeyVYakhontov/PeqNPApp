////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ninject;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public readonly struct NestedCommsGraphNodeInfo : IObjectWithId
  {
    #region Ctors

    public NestedCommsGraphNodeInfo(long id)
    {
      this.Id = id;
    }

    #endregion

    #region public members

    public long Id { get; }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////

