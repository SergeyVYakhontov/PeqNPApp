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
  public class NestedCommsGraphBuilderFactCPLTM
  {
    #region Ctors

    public NestedCommsGraphBuilderFactCPLTM(MEAPContext meapContext)
    {
      this.CPLTMInfo = meapContext.MEAPSharedContext.CPLTMInfo;
      this.meapContext = meapContext;
    }

    #endregion

    #region public members

    public void Setup()
    {
      meapContext.muToNestedCommsGraphPair = new SortedDictionary<long, FwdBkwdNCommsGraphPair>();
    }

    public void Run()
    {
      log.Info("Creating nested commodities graphs");

      VerifyCommodities();
      FilloutNodeToCommoditiesMap();

      CreateCommsGraphs();
    }

    #endregion

    #region private members

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private readonly ICPLTMInfo CPLTMInfo;
    private readonly MEAPContext meapContext;

    private readonly SortedDictionary<long, LinkedList<long>> sNodeToCommoditiesMap =
      new SortedDictionary<long, LinkedList<long>>();
    private readonly SortedDictionary<long, LinkedList<long>> tNodeToCommoditiesMap =
      new SortedDictionary<long, LinkedList<long>>();

    private void VerifyCommodities()
    {
      SortedDictionary<long, long> nodeToLevel = meapContext.MEAPSharedContext.NodeLevelInfo.NodeToLevel;

      foreach (KeyValuePair<long, Commodity> idCommPair in meapContext.Commodities)
      {
        long commId = idCommPair.Key;
        Commodity commodity = idCommPair.Value;

        long sNodeLevel = nodeToLevel[commodity.sNodeId];
        long tNodeLevel = nodeToLevel[commodity.tNodeId];

        Ensure.That(tNodeLevel - sNodeLevel).IsLte(CPLTMInfo.LRSubseqSegLength * 2);
      }
    }

    private void FilloutNodeToCommoditiesMap()
    {
      foreach(KeyValuePair<long, Commodity> idCommPair in meapContext.Commodities)
      {
        long commId = idCommPair.Key;
        Commodity commodity = idCommPair.Value;

        LinkedList<long> commsAtSNode = AppHelper.TakeValueByKey(
          sNodeToCommoditiesMap,
          commodity.sNodeId,
          () => new LinkedList<long>());

        commsAtSNode.AddLast(commId);

        LinkedList<long> commsAtTNode = AppHelper.TakeValueByKey(
          tNodeToCommoditiesMap,
          commodity.tNodeId,
          () => new LinkedList<long>());

        commsAtTNode.AddLast(commId);
      }
    }

    private void CreateCommsGraphs()
    {
      List<long> kTapeLRSubseq = CPLTMInfo.KTapeLRSubseq();

      foreach (long kStep in kTapeLRSubseq.Take(kTapeLRSubseq.Count - 2))
      {
        log.InfoFormat($"Creating nested commodities graphs at kStep = {kStep}");

        FwdBkwdNCommsGraphPair fwdBkwdNCommsGraphPair = AppHelper.TakeValueByKey(
          meapContext.muToNestedCommsGraphPair,
          kStep,
          () => new FwdBkwdNCommsGraphPair());

        TypedDAG<NestedCommsGraphNodeInfo, StdEdgeInfo> fwdNestedCommsGraph = fwdBkwdNCommsGraphPair.FwdNestedCommsGraph;

        FwdNCommsGraphBuilder fwdNCommsGraphBuilder = new FwdNCommsGraphBuilder(
          meapContext,
          kStep,
          fwdNestedCommsGraph);

        fwdNCommsGraphBuilder.Run();

        log.InfoFormat($"fwdNestedCommsGraph: node count = {fwdNestedCommsGraph.Nodes.Count}");
      }
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
