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
  public class BkwdNCommsGraphBuilder
  {
    #region Ctors

    public BkwdNCommsGraphBuilder(
      MEAPContext meapContext,
      SortedDictionary<long, LinkedList<long>> nodeToCommoditiesMap,
      long kStep,
      FwdBkwdNCommsGraphPair fwdBkwdNCommsGraphPair)
    {
      this.meapContext = meapContext;
      this.CPLTMInfo = meapContext.MEAPSharedContext.CPLTMInfo;
      this.nodeToCommoditiesMap = nodeToCommoditiesMap;
      this.kStep = kStep;
      this.bkwdNestedCommsGraph = fwdBkwdNCommsGraphPair.BkwdNestedCommsGraph;
      this.bkwdCFGNodeToNCGNodesMap = fwdBkwdNCommsGraphPair.BkwdCFGNodeToNCGNodesMap;
      this.bkwdNCGEdgeToCFGEdgeMap = fwdBkwdNCommsGraphPair.BkwdNCGEdgeToCFGEdgeMap;
    }

    #endregion

    #region public members

    public void Run()
    {
      SortedDictionary<long, SortedSet<long>> nodeVLevels = meapContext.MEAPSharedContext.NodeLevelInfo.NodeVLevels;

      foreach (long i in CPLTMInfo.BkwdCommsKStepSequence(kStep))
      {
        foreach (long uId in nodeVLevels[i])
        {
          if (!meapContext.TArbSeqCFG.HasNode(uId))
          {
            continue;
          }

          DAGNode uNode = meapContext.TArbSeqCFG.GetNode(uId);

          foreach (DAGEdge e in uNode.InEdges)
          {
            DAGNode vNode = e.FromNode;
            long vId = vNode.Id;

            ConnectCommsByCFGEdge(e.Id, uId, vId);
          }
        }
      }
    }

    #endregion

    #region private members

    private readonly MEAPContext meapContext;
    private readonly ICPLTMInfo CPLTMInfo;
    private readonly SortedDictionary<long, LinkedList<long>> nodeToCommoditiesMap;
    private readonly long kStep;
    private readonly TypedDAG<NestedCommsGraphNodeInfo, StdEdgeInfo> bkwdNestedCommsGraph;
    private readonly SortedDictionary<long, List<long>> bkwdCFGNodeToNCGNodesMap;
    private readonly SortedDictionary<long, long> bkwdNCGEdgeToCFGEdgeMap;

    private long nodeId;
    private long edgeId;

    private readonly SortedDictionary<long, DAGNode> nodeEnumeration =
      new SortedDictionary<long, DAGNode>();

    private DAGNode GetDAGNode(long uCommId)
    {
      if (!nodeEnumeration.TryGetValue(uCommId, out DAGNode dagNode))
      {
        dagNode = new DAGNode(nodeId++);
        bkwdNestedCommsGraph.AddNode(dagNode);

        nodeEnumeration[uCommId] = dagNode;
      }

      return dagNode;
    }

    private void ConnectCommsByCFGEdge(long cfgEdgeId, long uId, long vId)
    {
      if (nodeToCommoditiesMap.TryGetValue(uId, out var uNodeComms))
      {
        if (nodeToCommoditiesMap.TryGetValue(vId, out var vNodeComms))
        {
          foreach (long uCommId in uNodeComms)
          {
            DAGNode uCommNode = GetDAGNode(uCommId);

            foreach (long vCommId in vNodeComms)
            {
              DAGNode vCommNode = GetDAGNode(vCommId);

              DAGEdge eComm = new DAGEdge(edgeId++, uCommNode, vCommNode);
              bkwdNestedCommsGraph.AddEdge(eComm);

              ICollection<long> uList = AppHelper.TakeValueByKey(
                bkwdCFGNodeToNCGNodesMap, uId, () => new List<long>());
              uList.Add(uCommId);

              ICollection<long> vList = AppHelper.TakeValueByKey(
                bkwdCFGNodeToNCGNodesMap, vId, () => new List<long>());
              uList.Add(vCommId);

              bkwdNCGEdgeToCFGEdgeMap[eComm.Id] = cfgEdgeId;
            }
          }
        }
      }
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
