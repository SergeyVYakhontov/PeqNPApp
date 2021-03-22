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
  public class MEAPContext
  {
    #region public methods

    // the result
    public bool PathExists { get; set; }
    public bool PathFound { get; set; }
    public List<long> TConsistPath { get; set; } = new();
    public int[] Output { get; set; } = Array.Empty<int>();

    // TArbSeqCFG construction
    public ulong mu { get; set; }

    public ITASGBuilder TASGBuilder { get; set; } = default!;
    public TypedDAG<TASGNodeInfo, StdEdgeInfo> TArbitrarySeqGraph { get; set; } = default!;
    public TypedDAG<TASGNodeInfo, StdEdgeInfo> TArbSeqCFG { get; set; } = default!;
    public SortedDictionary<long, SortedSet<long>> NodeVLevels { get; set; } = new();
    public SortedDictionary<long, long> NodeToLevel { get; set; } = new();
    public SortedDictionary<long, long> EdgeToLevel { get; set; } = new();

    // RDA info
    public SortedSet<long> Vars { get; set; } = new();
    public IDictionary<long, ICollection<long>> Assignments { get; set; } = default!;
    public IDictionary<long, ICollection<long>> Usages { get; set; } = default!;
    public SortedDictionary<long, long> NodeToVarMap { get; set; } = new();
    public long DUPairCount { get; set; }
    public long TConsistPairCount { get; set; }
    public SortedSet<CompStepNodePair> TConsistPairSet { get; set; } = new();
    public SortedSet<CompStepNodePair> TInconsistPairSet { get; set; } = new();

    // commodities found
    public LongSegment CommSeg { get; set; } = default!;
    public CommoditiesBuilder CommoditiesBuilder { get; set; } = default!;
    public SortedDictionary<long, Commodity> Commodities { get; set; } = new();
    public SortedDictionary<long, List<Commodity>> KSetZetaSets { get; set; } = new();

    // if path found in this tape seg
    public TapeSegContext TapeSegContext { get; set; } = default!;

    // Factorization algorithm
    public SortedSet<long> AcceptingNodes { get; set; } = new();
    public SortedSet<long> UnusedNodes { get; set; } = new();

    public SortedDictionary<long, FwdBkwdNCommsGraphPair> muToNestedCommsGraphPair { get; set; } = new();
    public SortedDictionary<long, NCommsGraphJointNode> CfgNodeIdToNCGJointNode { get; set; } = new();
    public MEAPSharedContext MEAPSharedContext { get; set; } = default!;

    public bool InCancelationState()
    {
      return
        (MEAPSharedContext.DeterminePathRunnerDoneMu < mu) &&
        MEAPSharedContext.CancellationToken.IsCancellationRequested;
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
