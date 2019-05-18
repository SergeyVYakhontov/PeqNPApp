////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace IntegerFactExamplesAppCPLTM
{
  public class DebugOptions : IDebugOptions
  {
    public ulong muStart { get => 1; }
    public bool RunRDA { get => true; }
    public bool ComputeCommoditiesExplicitely { get => true; }

    public bool UsePropSymbols { get => true; }
    public bool UseTapeRestrictions { get => false; }
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
