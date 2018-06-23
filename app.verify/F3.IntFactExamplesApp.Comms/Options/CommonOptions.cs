////////////////////////////////////////////////////////////////////////////////////////////////////

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
    public bool CheckDataStructures { get => false; }
    public ulong RDASlotMaxSize { get => 256; }
    public float LinProgEpsilon { get => 0.0001F; }
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
