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
    public byte DeterminePathRunnersCount => 2;
    public byte TapeSegRunnersCount => 1;
    public byte SlotsMThRDAProcessCount => 1;

    public byte LinEqSetRunnersCount => 1;
    public byte GaussElimRunnersCount => 1;
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
