////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using Ninject;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public class NotUsedCommsDetector
  {
    #region Ctors

    public NotUsedCommsDetector(
      MEAPContext meapContext,
      TapeSegContext tapeSegContext,
      SortedDictionary<long, Commodity> commoditiesSubset,
      SortedSet<long> totalExcludedComms,
      NodesCoverageKeeper nodesCoverageKeeper)
    {
      this.meapContext = meapContext;
      this.tapeSegContext = tapeSegContext;
      this.commoditiesSubset = commoditiesSubset;
      this.totalExcludedComms = totalExcludedComms;
      this.nodesCoverageKeeper = nodesCoverageKeeper;
    }

    #endregion

    #region public members

    public bool RemoveOrphanCommodities(
      long zeta,
      SortedSet<long> zetaCommodities,
      TypedDAG<GTCZNodeInfo, StdEdgeInfo> gtcz,
      SortedSet<long> excludedComms)
    {
      List<long> commsToRemove = new List<long>();

      foreach (long commodityId in zetaCommodities)
      {
        Commodity commodity = meapContext.Commodities[commodityId];

        long sNodeId = commodity.Gi.GetSourceNodeId();
        long tNodeId = commodity.Gi.GetSinkNodeId();

        DAGNode gtcz_u = gtcz.NodeEnumeration[sNodeId];
        DAGNode gtcz_v = gtcz.NodeEnumeration[tNodeId];

        if ((!meapContext.TArbSeqCFG.IsSourceNode(sNodeId)) && (!gtcz_u.InEdges.Any()))
        {
          commsToRemove.Add(commodity.Id);
          gtcz.RemoveNodePair(sNodeId, tNodeId);

          continue;
        }

        if ((!meapContext.TArbSeqCFG.IsSinkNode(tNodeId)) && (!gtcz_v.OutEdges.Any()))
        {
          commsToRemove.Add(commodity.Id);
          gtcz.RemoveNodePair(sNodeId, tNodeId);

          continue;
        }
      }

      log.DebugFormat("Removed orphan commodities {0}:", zeta);
      commsToRemove.ForEach(c =>
        {
          nodesCoverageKeeper.RemoveCommodity(c, excludedComms);
          log.DebugFormat("commodity {0}", c);
        });

      return commsToRemove.Any();
    }

    private bool RemoveNotCoveredCommodities(SortedSet<long> excludedComms)
    {
      SortedSet<long> toRemove = new SortedSet<long>();

      foreach (KeyValuePair<long, DAGNode> idNodePair in meapContext.TArbSeqCFG.NodeEnumeration)
      {
        long uNodeId = idNodePair.Key;

        if (tapeSegContext.TArbSeqCFGUnusedNodes.Contains(uNodeId))
        {
          continue;
        }

        SortedSet<long> commoditiesAtNode = AppHelper.TakeValueByKey(
          nodesCoverageKeeper.CommoditiesAtNodes,
          uNodeId,
          () => new SortedSet<long>());

        bool commoditiesNotCovered = false;
        bool varsNotCovered = false;

        if (commoditiesAtNode.Count < tapeSegContext.KSetZetaSubset.Count)
        {
          commoditiesNotCovered = true;
        }
        else
        {
          long varsCount = nodesCoverageKeeper.NodesCoverage[uNodeId].Count(s => s.Value != 0);

          if (varsCount < tapeSegContext.KSetZetaSubset.Count)
          {
            varsNotCovered = true;
          }
        }

        if (commoditiesNotCovered || varsNotCovered)
        {
          foreach (long commodityId in commoditiesAtNode)
          {
            Commodity commodity = meapContext.Commodities[commodityId];

            if (commodity.Gi.IsSourceNode(uNodeId) || commodity.Gi.IsSinkNode(uNodeId))
            {
              toRemove.Add(commodityId);
              log.DebugFormat(
                "Removed not covered commodity {0}",
                commodity.Id);
            }
          }
        }
      }

      toRemove.ForEach(c => nodesCoverageKeeper.RemoveCommodity(c, excludedComms));

      return toRemove.Any();
    }

    private bool RemoveNotConnectedCommodities(SortedSet<long> excludedComms)
    {
      SortedSet<long> toRemove = new SortedSet<long>();

      foreach (KeyValuePair<long, Commodity> idCommList in commoditiesSubset)
      {
        long commodityId = idCommList.Key;
        Commodity commodity = idCommList.Value;

        DAG Gi = commodity.Gi;

        bool isGiConnected = DAG.IsConnected(
          Gi,
          u => !tapeSegContext.TArbSeqCFGUnusedNodes.Contains(u.Id));

        if (!isGiConnected)
        {
          toRemove.Add(commodity.Id);
          log.DebugFormat("Removed not connected commodity {0}", commodity.Id);
        }
      }

      toRemove.ForEach(c => nodesCoverageKeeper.RemoveCommodity(c, excludedComms));

      return toRemove.Any();
    }

    public void MarkUnusedNodes()
    {
      log.DebugFormat("Unused nodes");

      foreach (KeyValuePair<long, DAGNode> idNodePair in meapContext.TArbSeqCFG.NodeEnumeration)
      {
        long uNodeId = idNodePair.Key;

        if (tapeSegContext.TArbSeqCFGUnusedNodes.Contains(uNodeId))
        {
          continue;
        }

        if ((!nodesCoverageKeeper.CommoditiesAt_stNodes.TryGetValue(uNodeId, out long stNodesCount)) ||
            (stNodesCount == 0))
        {
          tapeSegContext.TArbSeqCFGUnusedNodes.Add(uNodeId);
          log.DebugFormat("Unused node (2) {0}", uNodeId);

          continue;
        }

        SortedSet<long> commoditiesAtNode = AppHelper.TakeValueByKey(
          nodesCoverageKeeper.CommoditiesAtNodes,
          uNodeId,
          () => new SortedSet<long>());

        bool commoditiesNotCovered = false;
        bool varsNotCovered = false;

        if (commoditiesAtNode.Count < tapeSegContext.KSetZetaSubset.Count)
        {
          commoditiesNotCovered = true;
        }
        else
        {
          long varsCount = nodesCoverageKeeper.NodesCoverage[uNodeId].Count(s => (s.Value != 0));

          if (varsCount < tapeSegContext.KSetZetaSubset.Count)
          {
            varsNotCovered = true;
          }
        }

        if (commoditiesNotCovered || varsNotCovered)
        {
          tapeSegContext.TArbSeqCFGUnusedNodes.Add(uNodeId);
          log.DebugFormat("Unused node (1) {0}", uNodeId);
        }
      }
    }

    public bool RemoveUnusedCommodities1(SortedSet<long> excludedComms)
    {
      long excludedCommsCount = excludedComms.Count;
      SortedDictionary<long, TypedDAG<GTCZNodeInfo, StdEdgeInfo>> gtczSet = new SortedDictionary<long, TypedDAG<GTCZNodeInfo, StdEdgeInfo>>();

      foreach (KeyValuePair<long, SortedSet<long>> idCommList in tapeSegContext.KSetZetaSubset)
      {
        GraphTConZetaBuilder gtczBuilder = new GraphTConZetaBuilder(meapContext);
        TypedDAG<GTCZNodeInfo, StdEdgeInfo> gtcz = gtczBuilder.Run(idCommList.Value);

        gtczSet[idCommList.Key] = gtcz;
      }

      foreach (KeyValuePair<long, SortedSet<long>> idCommList in tapeSegContext.KSetZetaSubset)
      {
        TypedDAG<GTCZNodeInfo, StdEdgeInfo> gtcz = gtczSet[idCommList.Key];
        while (RemoveOrphanCommodities(idCommList.Key, idCommList.Value, gtcz, excludedComms)) { }
      }

      MarkUnusedNodes();

      while (RemoveNotCoveredCommodities(excludedComms)) { }
      MarkUnusedNodes();

      return (excludedComms.Count > excludedCommsCount);
    }

    public bool RemoveUnusedCommodities2(SortedSet<long> excludedComms)
    {
      long excludedCommsCount = excludedComms.Count;

      RemoveNotConnectedCommodities(excludedComms);
      MarkUnusedNodes();

      return (excludedComms.Count > excludedCommsCount);
    }

    #endregion

    #region private members

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private readonly MEAPContext meapContext;
    private readonly TapeSegContext tapeSegContext;

    private readonly SortedDictionary<long, Commodity> commoditiesSubset;
    private readonly SortedSet<long> totalExcludedComms;

    private readonly NodesCoverageKeeper nodesCoverageKeeper;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
