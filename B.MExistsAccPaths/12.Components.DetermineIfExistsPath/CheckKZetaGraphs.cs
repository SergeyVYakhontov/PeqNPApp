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
  public class CheckKZetaGraphs : TapeSegContextBase
  {
    #region Ctors

    public CheckKZetaGraphs(MEAPContext meapContext, TapeSegContext tapeSegContext)
      : base(meapContext, tapeSegContext) {}

    #endregion

    #region public members

    public bool TConsistPathFound { get; set; }
    public bool ThereIsNoTConsistPath { get; set; }
    public bool Finished { get; set; }

    public void Init()
    {
      log.Info("TCPEP optimizer: init");

      log.Info("TCPEP optimizer: KSetZetaSubset");
      commoditiesSubset = new SortedDictionary<long, Commodity>();
      tapeSegContext.KSetZetaSubset.Select(p => p.Value).ToList()
        .ForEach(e => e.ForEach(c => commoditiesSubset[c] =
          meapContext.Commodities[c]));

      totalCommoditiesCount = commoditiesSubset.Count;
    }

    public void Run()
    {
      ComputeUnusedNodes();
      CheckIfPathExists("KZeta");
    }

    #endregion

    #region private members

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private SortedDictionary<long, Commodity> commoditiesSubset;
    private long totalCommoditiesCount;

    private SortedSet<long> FindUsedSubgraph(SortedSet<long> usedNodes)
    {
      SortedSet<long> labelMapA = new SortedSet<long>();
      SortedSet<long> labelMapB = new SortedSet<long>();

      DAG.PropagateProperties(
        meapContext.TArbSeqCFG,
        meapContext.TArbSeqCFG.GetSourceNodeId(),
        GraphDirection.Forward,
        v => usedNodes.Contains(v.Id),
        e => true,
        (w) => { labelMapA.Add(w.Id); return true; },
        e => true);

      DAG.PropagateProperties(
        meapContext.TArbSeqCFG,
        meapContext.TArbSeqCFG.GetSinkNodeId(),
        GraphDirection.Backward,
        v => usedNodes.Contains(v.Id),
        e => true,
        (w) => { labelMapB.Add(w.Id); return true; },
        e => true);

      return new SortedSet<long>(labelMapA.Intersect(labelMapB));
    }

    private void ComputeUnusedNodes()
    {
      SortedSet<long> usedNodes = new SortedSet<long>();

      foreach (KeyValuePair<long, Commodity> idCommList in commoditiesSubset)
      {
        long commodityId = idCommList.Key;
        Commodity commodity = idCommList.Value;

        usedNodes.Add(commodity.sNodeId);
        usedNodes.Add(commodity.tNodeId);
      }

      usedNodes = FindUsedSubgraph(usedNodes);
      tapeSegContext.TArbSeqCFGUnusedNodes.UnionWith(
        new SortedSet<long>(meapContext.TArbSeqCFG.GetAllNodeIds()).Except(usedNodes));

      if (!usedNodes.Any())
      {
        ThereIsNoTConsistPath = true;
      }

      log.InfoFormat(
        "ComputeUnusedNodes: {0}, {1}",
        meapContext.TArbSeqCFG.Nodes.Count,
        usedNodes.Count);
    }

    private bool IsKZetaGraphComplete()
    {
      return (tapeSegContext.KSetZetaSubset.Count(v => v.Value.Any()) ==
        tapeSegContext.KSetZetaSubset.Count);
    }

    private bool IsTArbSeqCFGConnected()
    {
      bool isTArbSeqCFGConnected = DAG.IsConnected(
        meapContext.TArbSeqCFG,
        u => !tapeSegContext.TArbSeqCFGUnusedNodes.Contains(u.Id));

      return isTArbSeqCFGConnected;
    }

    private void CheckIfPathExists(string stepName)
    {
      if (!IsKZetaGraphComplete())
      {
        log.Info("KZeta graph is incomplete");

        ThereIsNoTConsistPath = true;

        return;
      }

      if (!IsTArbSeqCFGConnected())
      {
        log.Info("TArbSeqCFG not connected");

        ThereIsNoTConsistPath = true;

        return;
      }
    }

    #endregion
  }
}