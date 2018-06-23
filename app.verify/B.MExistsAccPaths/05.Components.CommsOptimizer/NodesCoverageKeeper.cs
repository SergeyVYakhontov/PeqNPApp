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
  public class NodesCoverageKeeper
  {
    #region Ctors

    public NodesCoverageKeeper(
      MEAPContext meapContext,
      TapeSegContext tapeSegContext,
      SortedDictionary<long, Commodity> commoditiesSubset,
      SortedSet<long> totalExcludedComms)
    {
      this.meapContext = meapContext;
      this.tapeSegContext = tapeSegContext;
      this.commoditiesSubset = commoditiesSubset;
      this.totalExcludedComms = totalExcludedComms;
    }

    #endregion

    #region public members

    public SortedDictionary<long, SortedSet<long>> CommoditiesAtNodes { get; private set; }
    public SortedDictionary<long, SortedDictionary<long, long>> NodesCoverage { get; private set; }
    public SortedDictionary<long, long> CommoditiesAt_stNodes { get; private set; }

    public void ComputeCoverage()
    {
      CommoditiesAtNodes = new SortedDictionary<long, SortedSet<long>>();
      NodesCoverage = new SortedDictionary<long, SortedDictionary<long, long>>();
      CommoditiesAt_stNodes = new SortedDictionary<long, long>();

      foreach (KeyValuePair<long, Commodity> idCommList in commoditiesSubset)
      {
        long commodityId = idCommList.Key;
        Commodity commodity = idCommList.Value;

        DAG Gi = commodity.Gi;
        SortedDictionary<long, DAGNode> commNodeEnum = Gi.NodeEnumeration;

        foreach (KeyValuePair<long, DAGNode> uNodePair in commNodeEnum)
        {
          long uNodeId = uNodePair.Key;

          if (tapeSegContext.TArbSeqCFGUnusedNodes.Contains(uNodeId))
          {
            continue;
          }

          SortedSet<long> commoditiesAtNode = AppHelper.TakeValueByKey(CommoditiesAtNodes, uNodeId, () => new SortedSet<long>());
          SortedDictionary<long, long> nodeCoverages = AppHelper.TakeValueByKey(NodesCoverage, uNodeId, () => new SortedDictionary<long, long>());

          commoditiesAtNode.Add(commodity.Id);
          AppHelper.AddPairToSortedDictionary(nodeCoverages, commodity.Variable, () => 0);
          nodeCoverages[commodity.Variable]++;

          if (Gi.IsSourceNode(uNodeId) || Gi.IsSinkNode(uNodeId))
          {
            AppHelper.AddPairToSortedDictionary(CommoditiesAt_stNodes, uNodeId, () => 0);
            CommoditiesAt_stNodes[uNodeId]++;
          }
        }
      }
    }

    public void RemoveCommodity(long commodityId, SortedSet<long> excludedComms)
    {
      foreach (KeyValuePair<long, SortedSet<long>> idCommList in tapeSegContext.KSetZetaSubset)
      {
        if (idCommList.Value.Remove(commodityId))
        {
          Commodity commodity = meapContext.Commodities[commodityId];
          DAG Gi = commodity.Gi;
          SortedDictionary<long, DAGNode> commNodeEnum = Gi.NodeEnumeration;

          foreach (KeyValuePair<long, DAGNode> uNodePair in commNodeEnum)
          {
            long uNodeId = uNodePair.Key;

            if (tapeSegContext.TArbSeqCFGUnusedNodes.Contains(uNodeId))
            {
              continue;
            }

            CommoditiesAtNodes[uNodeId].Remove(commodityId);
            NodesCoverage[uNodeId][commodity.Variable]--;

            if (Gi.IsSourceNode(uNodeId) || Gi.IsSinkNode(uNodeId))
            {
              CommoditiesAt_stNodes[uNodeId]--;
            }
          }

          break;
        }
      }

      tapeSegContext.KSetCommodities.Remove(commodityId);
      commoditiesSubset.Remove(commodityId);

      excludedComms.Add(commodityId);
      totalExcludedComms.Add(commodityId);
    }

    #endregion

    #region private members

    private static readonly IKernel configuration = Core.AppContext.Configuration;
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private readonly MEAPContext meapContext;
    private readonly TapeSegContext tapeSegContext;

    private readonly SortedDictionary<long, Commodity> commoditiesSubset;
    private readonly SortedSet<long> totalExcludedComms;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
