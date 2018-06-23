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
  public class CommoditiesBuilderOrd : CommoditiesBuilder
  {
    #region Ctors

    public CommoditiesBuilderOrd(MEAPContext meapContext)
      :base(meapContext) {}

    #endregion

    #region public members

    public override void EnumeratePairs()
    {
      foreach (CompStepNodePair compStepNodePair in meapContext.TConsistPairSet)
      {
        compStepNodePairEnum[pairCount] = compStepNodePair;
        pairCount++;
      }
    }

    public override SortedDictionary<long, Commodity> CreateCommodities()
    {
      log.DebugFormat(
        "Build commodities: {0}",
        meapContext.TConsistPairSet.Count);

      long i = 0;
      IReadOnlyDictionary<long, DAGNode> nodeEnumeration = meapContext.TArbSeqCFG.NodeEnumeration;

      foreach (KeyValuePair<long, CompStepNodePair> compStepNodePairPair in compStepNodePairEnum)
      {
        CompStepNodePair compStepNodePair = compStepNodePairPair.Value;

        DAGNode sNode = nodeEnumeration[compStepNodePair.uNode];
        DAGNode tNode = nodeEnumeration[compStepNodePair.vNode];

        log.DebugFormat("Build commodites: {0}", i);

        Commodity newCommodity = new Commodity("Ki", i, compStepNodePair.Variable, null);

        newCommodity.sNodeId = sNode.Id;
        newCommodity.tNodeId = tNode.Id;

        commodities[i] = newCommodity;
        i++;
      }

      meapContext.CommSeg = new LongSegment(1, commodities.Count);

      Trace();

      return commodities;
    }

    #endregion

    #region private members

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
