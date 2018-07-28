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

namespace IntegerFactExamplesAppCPLTM
{
  public class TPLOptions : ITPLOptions
  {
    public byte DeterminePathRunnersCount { get => 1; }
    public byte TapeSegRunnersCount { get => 1; }
    public byte SlotsMThRDAProcessCount { get => 1; }

    public byte LinEqSetRunnersCount { get =>
      throw new InvalidOperationException("The option cannot be used in this context"); }

    public byte GaussElimRunnersCount { get =>
      throw new InvalidOperationException("The option cannot be used in this context"); }
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
