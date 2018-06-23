////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Ninject;
using Core;
using ExistsAcceptingPath;
using VerifyResults;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace OrdinaryExamplesAppSingleThread
{
  public class TPLOptions : ITPLOptions
  {
    public byte DeterminePathRunnersCount { get => 2; }
    public byte TapeSegRunnersCount { get => 1; }
    public byte SlotsMThRDAProcessCount { get => 1; }

    public byte LinEqSetRunnersCount { get => 1; }
    public byte GaussElimRunnersCount { get => 1; }
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
