﻿////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Core;
using Ninject;
using ExistsAcceptingPath;
using VerifyResults;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace IntegerFactExamplesAppComms
{
  public class CommonOptions : ICommonOptions
  {
    public bool CheckDataStructures => false;
    public ulong RDASlotMaxSize => 256;
    public float LinProgEpsilon => 0.0001F;
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////