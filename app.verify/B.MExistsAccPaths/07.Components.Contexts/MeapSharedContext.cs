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

    public OneTapeTuringMachine MNP { get; set; }
    public int[] Input { get; set; }
    public TMInstance InitInstance { get; set; }

    public ITASGBuilder TASGBuilder { get; set; }

    public ICPLTMInfo CPLTMInfo { get; set; }

    public ulong DeterminePathRunnerDoneMu { get; set; }
    public CancellationTokenSource CancellationTokenSource { get; set; }
    public CancellationToken CancellationToken { get; set; }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
