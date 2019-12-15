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
  using NCGraphType = TypedDAG<NestedCommsGraphNodeInfo, StdEdgeInfo>;

  public class FwdNCommsGraphBuilder
  {
    #region Ctors

    public FwdNCommsGraphBuilder(
      MEAPContext meapContext,
      SortedDictionary<long, LinkedList<long>> nodeToCommoditiesMap,
      long kStep,
      FwdBkwdNCommsGraphPair fwdBkwdNCommsGraphPair)
    {
      this.meapContext = meapContext;
      this.CPLTMInfo = meapContext.MEAPSharedContext.CPLTMInfo;
      this.nodeToCommoditiesMap = nodeToCommoditiesMap;
      this.kStep = kStep;
      this.fwdNestedCommsGraph = fwdBkwdNCommsGraphPair.FwdNestedCommsGraph;
      this.fwdCFGNodeToNCGNodesMap = fwdBkwdNCommsGraphPair.FwdCFGNodeToNCGNodesMap;
      this.fwdNCGEdgeToCFGEdgeMap = fwdBkwdNCommsGraphPair.FwdNCGEdgeToCFGEdgeMap;
    }

    #endregion

    #region public members

    public void Run()
    {
      SortedDictionary<long, SortedSet<long>> nodeVLevels =
        meapContext.MEAPSharedContext.NodeLevelInfo.NodeVLevels;

      fwdKStepSequence = CPLTMInfo.FwdCommsKStepSequence(kStep).ToArray();
      long fwdKStepLastIndex = fwdKStepSequence.LastIndex();

      for (long i = 0; i <= (fwdKStepLastIndex - 1); i++)
      {
        foreach (long uId in nodeVLevels[fwdKStepSequence[i]])
        {
          if (!meapContext.TArbSeqCFG.HasNode(uId))
          {
            continue;
          }

          DAGNode uNode = meapContext.TArbSeqCFG.GetNode(uId);

          foreach (DAGEdge e in uNode.OutEdges)
          {
            DAGNode vNode = e.ToNode;
            long vId = vNode.Id;

            Connect2CommsByCFGEdge(e.Id, uId, vId);
          }
        }
      }

      foreach (long uId in nodeVLevels[fwdKStepSequence[fwdKStepLastIndex]])
      {
        if (!meapContext.TArbSeqCFG.HasNode(uId))
        {
          continue;
        }

        DAGNode uNode = meapContext.TArbSeqCFG.GetNode(uId);

        foreach (DAGEdge e in uNode.OutEdges)
        {
          DAGNode vNode = e.ToNode;
          long vId = vNode.Id;

          ConnectCommAndCFGNodeByCFGEdge(e.Id, uId, vId);
        }
      }
    }

    public long DelimiterNodesCount()
    {
      SortedDictionary<long, SortedSet<long>> nodeVLevels =
        meapContext.MEAPSharedContext.NodeLevelInfo.NodeVLevels;

      return fwdCFGNodeToNCGNodesMap.Where(t =>
        nodeVLevels[fwdKStepSequence.Last()].Contains(t.Key))
        .Sum(t => t.Value.Count);
    }

    #endregion

    #region private members

    private readonly MEAPContext meapContext;
    private readonly ICPLTMInfo CPLTMInfo;
    private readonly SortedDictionary<long, LinkedList<long>> nodeToCommoditiesMap;
    private readonly long kStep;
    private long[] fwdKStepSequence;
    private readonly NCGraphType fwdNestedCommsGraph;
    private readonly SortedDictionary<long, List<long>> fwdCFGNodeToNCGNodesMap;
    private readonly SortedDictionary<long, long> fwdNCGEdgeToCFGEdgeMap;

    private long edgeId;
    private readonly SortedDictionary<long, DAGNode> nodeEnumeration =
      new SortedDictionary<long, DAGNode>();

    private DAGNode GetDAGNode(long uCommId)
    {
      if (!nodeEnumeration.TryGetValue(uCommId, out DAGNode dagNode))
      {
        dagNode = new DAGNode(uCommId);
        fwdNestedCommsGraph.AddNode(dagNode);

        nodeEnumeration[uCommId] = dagNode;
      }

      return dagNode;
    }

    private void Connect2CommsByCFGEdge(long cfgEdgeId, long uId, long vId)
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
              fwdNestedCommsGraph.AddEdge(eComm);

              ICollection<long> uList = AppHelper.TakeValueByKey(
                fwdCFGNodeToNCGNodesMap, uId, () => new List<long>());
              uList.Add(uCommId);

              ICollection<long> vList = AppHelper.TakeValueByKey(
                fwdCFGNodeToNCGNodesMap, vId, () => new List<long>());
              uList.Add(vCommId);

              fwdNCGEdgeToCFGEdgeMap[eComm.Id] = cfgEdgeId;
            }
          }
        }
      }
    }

    private void ConnectCommAndCFGNodeByCFGEdge(long cfgEdgeId, long uId, long vId)
    {
      if (nodeToCommoditiesMap.TryGetValue(uId, out var uNodeComms))
      {
        DAGNode vCommNode = GetDAGNode(vId);

        foreach (long uCommId in uNodeComms)
        {
          DAGNode uCommNode = GetDAGNode(uCommId);

          DAGEdge eComm = new DAGEdge(edgeId++, uCommNode, vCommNode);
          fwdNestedCommsGraph.AddEdge(eComm);

          ICollection<long> uList = AppHelper.TakeValueByKey(
            fwdCFGNodeToNCGNodesMap, uId, () => new List<long>());
          uList.Add(uCommId);

          ICollection<long> vList = AppHelper.TakeValueByKey(
            fwdCFGNodeToNCGNodesMap, vId, () => new List<long>());
          vList.Add(vId);

          fwdNCGEdgeToCFGEdgeMap[eComm.Id] = cfgEdgeId;
        }
      }
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
