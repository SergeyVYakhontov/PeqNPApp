////////////////////////////////////////////////////////////////////////////////////////////////////

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
  public class MEAPSharedContext
  {
    #region public memebrs

    public OneTapeTuringMachine MNP { get; set; } = default!;
    public int[] Input { get; set; } = Array.Empty<int>();
    public TMInstance InitInstance { get; set; } = default!;

    public ICPLTMInfo CPLTMInfo { get; set; } = default!;
    public ITASGBuilder TASGBuilder { get; set; } = default!;
    public NodeLevelInfo NodeLevelInfo { get; set; } = default!;

    public ulong DeterminePathRunnerDoneMu { get; set; }
    public CancellationTokenSource CancellationTokenSource { get; set; } = default!;
    public CancellationToken CancellationToken { get; set; }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
