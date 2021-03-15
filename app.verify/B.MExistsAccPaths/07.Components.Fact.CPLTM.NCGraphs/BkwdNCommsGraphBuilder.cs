////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ninject;
using EnsureThat;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  using NCGraphType = TypedDAG<NestedCommsGraphNodeInfo, StdEdgeInfo>;

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
      SortedDictionary<long, SortedSet<long>> nodeVLevels =
        meapContext.MEAPSharedContext.NodeLevelInfo.NodeVLevels;

      bkwdKStepSequence = CPLTMInfo.BkwdCommsKStepSequence(kStep).ToArray();
      long bkwdKStepLastIndex = bkwdKStepSequence.LastIndex();

      for (long i = (bkwdKStepLastIndex - 1); i >= 0; i--)
      {
        foreach (long uId in nodeVLevels[bkwdKStepSequence[i]])
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

            Connect2CommsByCFGEdge(e.Id, uId, vId);
          }
        }
      }

      foreach (long uId in nodeVLevels[bkwdKStepSequence[bkwdKStepLastIndex]])
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

          ConnectCommAndCFGNodeByCFGEdge(e.Id, uId, vId);
        }
      }
    }

    public long DelimiterNodesCount()
    {
      SortedDictionary<long, SortedSet<long>> nodeVLevels =
        meapContext.MEAPSharedContext.NodeLevelInfo.NodeVLevels;

      return bkwdCFGNodeToNCGNodesMap.Where(t =>
        nodeVLevels[bkwdKStepSequence.Last()].Contains(t.Key))
        .Sum(t => t.Value.Count);
    }

    #endregion

    #region private members

    private readonly MEAPContext meapContext;
    private readonly ICPLTMInfo CPLTMInfo;
    private readonly SortedDictionary<long, LinkedList<long>> nodeToCommoditiesMap;
    private readonly long kStep;
    private long[] bkwdKStepSequence;
    private readonly NCGraphType bkwdNestedCommsGraph;
    private readonly SortedDictionary<long, List<long>> bkwdCFGNodeToNCGNodesMap;
    private readonly SortedDictionary<long, long> bkwdNCGEdgeToCFGEdgeMap;

    private long edgeId;
    private readonly SortedDictionary<long, DAGNode> nodeEnumeration =
      new SortedDictionary<long, DAGNode>();

    private DAGNode GetDAGNode(long uCommId)
    {
      if (!nodeEnumeration.TryGetValue(uCommId, out DAGNode dagNode))
      {
        dagNode = new DAGNode(uCommId);
        bkwdNestedCommsGraph.AddNode(dagNode);

        nodeEnumeration[uCommId] = dagNode;
      }

      return dagNode;
    }

    private void Connect2CommsByCFGEdge(long cfgEdgeId, long uId, long vId)
    {
      if (nodeToCommoditiesMap.TryGetValue(uId, out LinkedList<long> uNodeComms))
      {
        if (nodeToCommoditiesMap.TryGetValue(vId, out LinkedList<long> vNodeComms))
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

    private void ConnectCommAndCFGNodeByCFGEdge(long cfgEdgeId, long uId, long vId)
    {
      if (nodeToCommoditiesMap.TryGetValue(uId, out LinkedList<long> uNodeComms))
      {
        DAGNode vCommNode = GetDAGNode(vId);

        foreach (long uCommId in uNodeComms)
        {
          DAGNode uCommNode = GetDAGNode(uCommId);

          DAGEdge eComm = new DAGEdge(edgeId++, uCommNode, vCommNode);
          bkwdNestedCommsGraph.AddEdge(eComm);

          ICollection<long> uList = AppHelper.TakeValueByKey(
            bkwdCFGNodeToNCGNodesMap, uId, () => new List<long>());
          uList.Add(uCommId);

          ICollection<long> vList = AppHelper.TakeValueByKey(
            bkwdCFGNodeToNCGNodesMap, vId, () => new List<long>());
          vList.Add(vId);

          bkwdNCGEdgeToCFGEdgeMap[eComm.Id] = cfgEdgeId;
        }
      }
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
