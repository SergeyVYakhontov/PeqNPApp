////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ninject;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public sealed class DeterminePathRunnerCtorArgs
  {
    #region public members

    public MEAPSharedContext MEAPSharedContext { get; set; }

    public OneTapeTuringMachine tMachine { get; set; }
    public int[] input { get; set; }
    public long currentMu { get; set; }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
