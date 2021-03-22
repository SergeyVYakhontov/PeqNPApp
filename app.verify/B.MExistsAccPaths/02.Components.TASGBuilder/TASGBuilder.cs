////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Ninject;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public abstract class TASGBuilder : ITASGBuilder, ITracable
  {
    #region public members

    public MEAPContext meapContext { get; set; } = default!;
    public MEAPSharedContext MEAPSharedContext { get; set; } = default!;

    public String Name { get; protected set; } = string.Empty;

    public long TapeLBound { get; protected set; }
    public long TapeRBound { get; protected set; }

    public abstract void Init();
    public abstract void CreateTArbitrarySeqGraph();
    public abstract void CreateTArbSeqCFG(uint[] states);

    public abstract void Trace();

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
