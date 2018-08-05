﻿////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace IntegerFactExamplesAppComms
{
  public class DebugOptions : IDebugOptions
  {
    public ulong muStart { get => 1; }
    public bool RunRDA { get => true; }
    public bool ComputeCommoditiesExplicitely { get => true; }
    public uint FactTRS_muUpperBound { get => throw new NotSupportedException(); }
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
