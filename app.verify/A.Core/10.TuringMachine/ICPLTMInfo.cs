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

    List<long> KTapeLRSubseq();

    int LRSubseqSegLength { get; }

    IEnumerable<long> FwdCommsLevelSequence(long kStep);
    IEnumerable<long> BkwdCommsLevelSequence(long kStep);
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
