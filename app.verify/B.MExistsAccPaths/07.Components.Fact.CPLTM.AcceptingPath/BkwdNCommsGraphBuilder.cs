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
      TypedDAG<NestedCommsGraphNodeInfo, StdEdgeInfo> bkwdNestedCommsGraph)
    {
      this.meapContext = meapContext;
      this.nodeToCommoditiesMap = nodeToCommoditiesMap;
      this.kStep = kStep;
      this.bkwdNestedCommsGraph = bkwdNestedCommsGraph;
      this.CPLTMInfo = meapContext.MEAPSharedContext.CPLTMInfo;
    }

    #endregion

    #region public members

    public void Run()
    {
      SortedDictionary<long, SortedSet<long>> nodeVLevels = meapContext.MEAPSharedContext.NodeLevelInfo.NodeVLevels;

      foreach (long i in CPLTMInfo.BkwdCommsLevelSequence(kStep))
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
                  }
                }
              }
            }
          }
        }
      }
    }

    #endregion

    #region private members

    private readonly MEAPContext meapContext;
    private readonly SortedDictionary<long, LinkedList<long>> nodeToCommoditiesMap;
    private readonly long kStep;
    private readonly TypedDAG<NestedCommsGraphNodeInfo, StdEdgeInfo> bkwdNestedCommsGraph;
    private readonly ICPLTMInfo CPLTMInfo;

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

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
