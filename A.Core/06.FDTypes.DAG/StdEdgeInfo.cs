﻿////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public struct StdEdgeInfo : IObjectWithId
  {
    #region Ctors

    public StdEdgeInfo(long id)
    {
      this.Id = id;
    }

    #endregion

    #region public members

    public long Id { get; }
  }

  #endregion
}

////////////////////////////////////////////////////////////////////////////////////////////////////
