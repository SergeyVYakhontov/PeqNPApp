////////////////////////////////////////////////////////////////////////////////////////////////////

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
    public ulong muStart => 1;
    public bool RunRDA => false;
    public bool ComputeCommoditiesExplicitely => true;
    public bool IntFactTestRules => false;
    public uint FactTRS_muUpperBound => 100;
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
