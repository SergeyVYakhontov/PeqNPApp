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

namespace OrdinaryExamplesAppSlotsMThreads
{
  public class CommonOptions : ICommonOptions
  {
    public bool CheckDataStructures { get => true; }
    public ulong RDASlotMaxSize { get => 512; }
    public float LinProgEpsilon { get => 0.0001F; }
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
