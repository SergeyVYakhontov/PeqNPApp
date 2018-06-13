﻿////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public class StateSymbolDirectionTriple
  {
    #region public members

    public int State { get; set; }
    public int Symbol { get; set; }
    public TMDirection Direction { get; set; }
    public long Shift { get; set; } = 1;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
