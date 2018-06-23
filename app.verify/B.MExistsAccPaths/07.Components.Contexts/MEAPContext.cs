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
  public class MEAPContext
  {
    #region public methods

    // the result
    public bool PathExists { get; set; }
    public bool PathFound { get; set; }
    public List<long> TConsistPath { get; set; }
    public int[] Output { get; set; }

    // TArbSeqCFG construction
    public long mu { get; set; }

    public ITASGBuilder TASGBuilder { get; set; }
    public TypedDAG<TASGNodeInfo, StdEdgeInfo> TArbitrarySeqGraph { get; set; }
    //public long TapeLBound { get; set; }
    //public long TapeRBound { get; set; }
    public TypedDAG<TASGNodeInfo, StdEdgeInfo> TArbSeqCFG { get; set; }
    public SortedDictionary<long, SortedSet<long>> NodeVLevels { get; set; }
    public SortedDictionary<long, long> NodeToLevel { get; set; }
    public SortedDictionary<long, long> EdgeToLevel { get; set; }

    // RDA info
    public SortedSet<long> Vars { get; set; }
    public IDictionary<long, ICollection<long>> Assignments { get; set; }
    public IDictionary<long, ICollection<long>> Usages { get; set; }
    public SortedDictionary<long, long> NodeToVarMap { get; set; }
    public long DUPairCount { get; set; }
    public long TConsistPairCount { get; set; }
    public SortedSet<CompStepNodePair> TConsistPairSet { get; set; }
    public SortedSet<CompStepNodePair> TInconsistPairSet { get; set; }

    // commodities found
    public LongSegment CommSeg { get; set; }
    public CommoditiesBuilder CommoditiesBuilder { get; set; }
    public SortedDictionary<long, Commodity> Commodities { get; set; }
    public SortedDictionary<long, List<Commodity>> KSetZetaSets { get; set; }

    // if path found in this tape seg
    public TapeSegContext TapeSegContext { get; set; }

    // Factorization algorithm
    public SortedSet<long> AcceptingNodes { get; set; }
    public SortedSet<long> UnusedNodes { get; set; }

    public TypedDAG<NestedCommsGraphNodeInfo, StdEdgeInfo> NestedCommsGraph { get; set; }

    public MEAPSharedContext MEAPSharedContext { get; set; }

    public bool InCancelationState()
    {
      return
        ((MEAPSharedContext.DeterminePathRunnerDoneMu < mu) &&
          MEAPSharedContext.CancellationToken.IsCancellationRequested);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////