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
  public class FwdNCommsGraphBuilder
  {
    #region Ctors

    public FwdNCommsGraphBuilder(
      MEAPContext meapContext,
      long kStep,
      TypedDAG<NestedCommsGraphNodeInfo, StdEdgeInfo> fwdNestedCommsGraph)
    {
      this.meapContext = meapContext;
      this.kStep = kStep;
      this.fwdNestedCommsGraph = fwdNestedCommsGraph;
      this.CPLTMInfo = meapContext.MEAPSharedContext.CPLTMInfo;
    }

    #endregion

    #region public members

    public void Run()
    {
      SortedDictionary<long, SortedSet<long>> nodeVLevels = meapContext.MEAPSharedContext.NodeLevelInfo.NodeVLevels;

      foreach (long i in CPLTMInfo.FwdCommsLevelSequence(kStep))
      {
        foreach (long uId in nodeVLevels[i])
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
    private readonly long kStep;
    private readonly TypedDAG<NestedCommsGraphNodeInfo, StdEdgeInfo> fwdNestedCommsGraph;
    private readonly ICPLTMInfo CPLTMInfo;

    private readonly SortedDictionary<long, LinkedList<long>> nodeToCommoditiesMap =
      new SortedDictionary<long, LinkedList<long>>();

    private long nodeId;
    private long edgeId;

    private readonly SortedDictionary<long, DAGNode> sNodeEnumeration =
      new SortedDictionary<long, DAGNode>();

    private DAGNode GetDAGNode(long uCommId)
    {
      if (!sNodeEnumeration.TryGetValue(uCommId, out DAGNode dagNode))
      {
        dagNode = new DAGNode(nodeId++);
        fwdNestedCommsGraph.AddNode(dagNode);

        sNodeEnumeration[uCommId] = dagNode;
      }

      return dagNode;
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
