////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using EnsureThat;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public interface ICPLTMInfo
  {
    void ComputeSequences();

    uint PathLength { get; }
    SortedDictionary<int, long> CellIndexes();
    List<long> KTapeLRSubseq();
    int LRSubseqSegLength { get; }

    IEnumerable<long> FwdCommsKStepSequence(long startKStep);
    IEnumerable<long> BkwdCommsKStepSequence(long startKStep);
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
