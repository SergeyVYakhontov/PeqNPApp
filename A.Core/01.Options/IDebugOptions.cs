﻿////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public interface IDebugOptions
  {
    ulong muStart { get; }
    bool RunRDA { get; }
    bool ComputeCommoditiesExplicitely { get; }
    bool IntFactTestRules { get; }
    uint FactTRS_muUpperBound { get; }
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
