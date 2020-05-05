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
  public interface ITASGBuilder
  {
    MEAPContext meapContext { get; set; }
    MEAPSharedContext MEAPSharedContext { get; set; }

    long TapeLBound { get; }
    long TapeRBound { get; }

    void Init();
    void CreateTArbitrarySeqGraph();
    void CreateTArbSeqCFG(uint[] states);
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
