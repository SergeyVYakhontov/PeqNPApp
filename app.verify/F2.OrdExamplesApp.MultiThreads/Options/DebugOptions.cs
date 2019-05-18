////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace OrdinaryExamplesAppSlotsMThreads
{
  public class DebugOptions : IDebugOptions
  {
    public ulong muStart { get => 1; }
    public bool RunRDA { get => true; }
    public bool ComputeCommoditiesExplicitely { get => true; }

    public bool UsePropSymbols { get =>
        throw new InvalidOperationException("The option cannot be used in this context"); }
    public bool UseTapeRestrictions { get =>
        throw new InvalidOperationException("The option cannot be used in this context"); }
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
