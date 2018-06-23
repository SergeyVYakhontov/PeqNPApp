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

namespace OrdinaryExamplesAppSlotsMThreads
{
  public class TPLOptions : ITPLOptions
  {
    public byte DeterminePathRunnersCount { get => 1; }
    public byte TapeSegRunnersCount { get => 2; }
    public byte SlotsMThRDAProcessCount { get => 32; }

    public byte LinEqSetRunnersCount { get => 4; }
    public byte GaussElimRunnersCount { get => 8; }
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
