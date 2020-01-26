﻿////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public interface IMeapCurrentStep
  {
    void Run(uint[] states);
    void RetrievePath();
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////