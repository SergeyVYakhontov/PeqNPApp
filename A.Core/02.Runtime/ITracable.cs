﻿////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public interface ITracable
  {
    string Name { get; }
    void Trace();
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
