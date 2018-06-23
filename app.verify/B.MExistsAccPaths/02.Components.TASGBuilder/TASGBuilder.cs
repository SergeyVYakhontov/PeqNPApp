////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public abstract class TASGBuilder : ITASGBuilder, ITracable
  {
    #region public members

    public String Name { get; protected set; }

    public long TapeLBound { get; protected set; }
    public long TapeRBound { get; protected set; }

    public abstract void CreateTArbitrarySeqGraph();
    public abstract void CreateTArbSeqCFG(int[] states);

    public abstract void Trace();

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
