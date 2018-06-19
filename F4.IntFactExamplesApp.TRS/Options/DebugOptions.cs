﻿////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace IntegerFactExamplesAppTRS
{
  public class DebugOptions : IDebugOptions
  {
    public ulong muStart { get => 1; }
    public bool RunRDA { get => false; }
    public bool ComputeCommoditiesExplicitely { get => true; }
    public bool IntFactTestRules { get => false; }
    public uint FactTRS_muUpperBound { get => 150; }
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
