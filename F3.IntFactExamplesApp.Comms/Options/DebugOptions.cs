////////////////////////////////////////////////////////////////////////////////////////////////////

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
    public ulong muStart => 1;
    public bool RunRDA => true;
    public bool ComputeCommoditiesExplicitely => true;
    public bool IntFactTestRules => false;
    public uint FactTRS_muUpperBound => throw new NotImplementedException();
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
