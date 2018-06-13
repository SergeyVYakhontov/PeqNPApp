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
  public class TPLOptions : ITPLOptions
  {
    public byte DeterminePathRunnersCount => 1;
    public byte TapeSegRunnersCount => 1;
    public byte SlotsMThRDAProcessCount => 4;

    public byte LinEqSetRunnersCount
    {
      get
      {
        throw new InvalidOperationException("The option cannot be used in this context");
      }
    }

    public byte GaussElimRunnersCount
    {
      get
      {
        throw new InvalidOperationException("The option cannot be used in this context");
      }
    }
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
