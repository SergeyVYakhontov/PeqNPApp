////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ninject;
using Core;
using EnsureThat;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public class NCGJointNodesBuilder
  {
    #region Ctors

    public NCGJointNodesBuilder(MEAPContext meapContext)
    {
      this.CPLTMInfo = meapContext.MEAPSharedContext.CPLTMInfo;
      this.meapContext = meapContext;
    }

    #endregion

    #region public members

    public void Setup()
    {
      meapContext.CfgNodeIdToNCGJointNode = new SortedDictionary<long, NCommsGraphJointNode>();
    }

    public void Run()
    {
      log.Info("Creating NCG joint nodes");

      long[] kTapeLRSubseq = CPLTMInfo.KTapeLRSubseq().ToArray();

      for (int i = 0; i < (kTapeLRSubseq.Length - 2); i++)
      {
        long kStep = kTapeLRSubseq[i];
        long kStepNext = kTapeLRSubseq[i+1];

        FwdBkwdNCommsGraphPair fwdBkwdNCommsGraphPairL = AppHelper.TakeValueByKey(
          meapContext.muToNestedCommsGraphPair,
          kStep,
          () => new FwdBkwdNCommsGraphPair());

        FwdBkwdNCommsGraphPair fwdBkwdNCommsGraphPairR = AppHelper.TakeValueByKey(
          meapContext.muToNestedCommsGraphPair,
          kStepNext,
          () => new FwdBkwdNCommsGraphPair());

        SortedDictionary<long, SortedSet<long>> nodeVLevels =
          meapContext.MEAPSharedContext.NodeLevelInfo.NodeVLevels;

        long[] bkwdKStepSequence = CPLTMInfo.BkwdCommsKStepSequence(kStep).ToArray();

        SortedDictionary<long, List<long>> bkwdCFGNodeToNCGNodesMap =
          fwdBkwdNCommsGraphPairL.BkwdCFGNodeToNCGNodesMap;
        SortedDictionary<long, List<long>> fwdCFGNodeToNCGNodesMap =
          fwdBkwdNCommsGraphPairR.FwdCFGNodeToNCGNodesMap;

        log.InfoFormat($"building ncg nodes for kStep = {kStep}");

        foreach (long level in bkwdKStepSequence)
        {
          SortedSet<long> cfgNodesAtLevel = nodeVLevels[level];

          foreach(long cfgNodeId in cfgNodesAtLevel)
          {
            NCommsGraphJointNode ncgJointNode = AppHelper.TakeValueByKey(
              meapContext.CfgNodeIdToNCGJointNode,
              cfgNodeId,
              () => new NCommsGraphJointNode());

            if (bkwdCFGNodeToNCGNodesMap.TryGetValue(cfgNodeId, out List<long> bkwdCfgNodes))
            {
              if (fwdCFGNodeToNCGNodesMap.TryGetValue(cfgNodeId, out List<long> fwdCfgNodes))
              {
                ProcessBkwdFwdNodesLists(
                  fwdBkwdNCommsGraphPairL,
                  fwdBkwdNCommsGraphPairR,
                  bkwdCfgNodes,
                  fwdCfgNodes,
                  ncgJointNode);
              }
            }
          }
        }
      }

      log.InfoFormat($"ncg jointNodes count: {meapContext.CfgNodeIdToNCGJointNode.Count}");
      log.InfoFormat($"total InCommodityNodes count: {meapContext.CfgNodeIdToNCGJointNode.Sum(t => t.Value.InCommodityNodes.Count)}");
      log.InfoFormat($"total OutCommodityNodes count: {meapContext.CfgNodeIdToNCGJointNode.Sum(t => t.Value.OutCommodityNodes.Count)}");
    }

    #endregion

    #region private members

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private readonly ICPLTMInfo CPLTMInfo;
    private readonly MEAPContext meapContext;

    private static void ProcessBkwdFwdNodesLists(
      FwdBkwdNCommsGraphPair fwdBkwdNCommsGraphPairL,
      FwdBkwdNCommsGraphPair fwdBkwdNCommsGraphPairR,
      List<long> bkwdCfgNodes,
      List<long> fwdCfgNodes,
      NCommsGraphJointNode ncgJointNode)
    {
      TypedDAG<NestedCommsGraphNodeInfo, StdEdgeInfo> bkwdNestedCommsGraph =
        fwdBkwdNCommsGraphPairL.BkwdNestedCommsGraph;
      TypedDAG<NestedCommsGraphNodeInfo, StdEdgeInfo> fwdNestedCommsGraph =
        fwdBkwdNCommsGraphPairR.FwdNestedCommsGraph;

      SortedDictionary<long, long> bkwdNCGEdgeToCFGEdgeMap =
        fwdBkwdNCommsGraphPairL.BkwdNCGEdgeToCFGEdgeMap;
      SortedDictionary<long, long> fwdNCGEdgeToCFGEdgeMap =
        fwdBkwdNCommsGraphPairR.FwdNCGEdgeToCFGEdgeMap;

      foreach (long bkwdNodeId in bkwdCfgNodes)
      {
        DAGNode bkwdNode = bkwdNestedCommsGraph.GetNode(bkwdNodeId);

        foreach (long fwdNodeId in fwdCfgNodes)
        {
          DAGNode fwdNode = fwdNestedCommsGraph.GetNode(fwdNodeId);

          ncgJointNode.AddInCommodityNode(bkwdNodeId);
          ncgJointNode.AddOutCommodityNode(fwdNodeId);
        }
      }
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
