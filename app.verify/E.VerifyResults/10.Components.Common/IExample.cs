﻿////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;
using ExistsAcceptingPath;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace VerifyResults
{
  public interface IExample
  {
    String Name { get; }
    int[] Input { get; set; }

    IList<IRunnable> GetRunnables();
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
