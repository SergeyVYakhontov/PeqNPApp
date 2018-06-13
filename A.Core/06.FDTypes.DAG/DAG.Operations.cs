////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public partial class DAG
  {
    #region public members

    public static void CreateCopy(DAG graph, DAG copy)
    {
      SortedDictionary<long, DAGNode> graphNodeToSubgraphNode =
        new SortedDictionary<long, DAGNode>();

      CopyNodesAndEdges(
        graph,
        new SortedSet<long>(graph.GetAllNodeIds()),
        copy);

      copy.SetSourceNode(copy.GetNode(graph.GetSourceNodeId()));
      copy.SetSinkNode(copy.GetNode(graph.GetSinkNodeId()));
    }

    public static void MakeSubgraph(
      DAG graph,
      long sNodeId,
      long tNodeId,
      SortedSet<long> nodeIdSubset,
      DAG subgraph)
    {
      CopyNodesAndEdges(
        graph,
        nodeIdSubset,
        subgraph);

      subgraph.SetSourceNode(subgraph.GetNode(sNodeId));
      subgraph.SetSinkNode(subgraph.GetNode(tNodeId));
    }

    public static void MakeCommodity(
      DAG graph,
      long sNodeId,
      long tNodeId,
      DAG commodity)
    {
      MakePathsSubgraph_st(graph, sNodeId, tNodeId, commodity);
    }

    public static void CutChains(DAG graph, DAG newGraph)
    {
      MakePathsSubgraph_st(graph, graph.GetSourceNodeId(), graph.GetSinkNodeId(), newGraph);
    }

    public static void CutChains_t(DAG graph, DAG newGraph)
    {
      MakePathsSubgraph_t(graph, graph.GetSourceNodeId(), graph.GetSinkNodeId(), newGraph);
    }

    public static bool IsConnected(
      DAG graph,
      Predicate<DAGNode> nodeFilter)
    {
      SortedSet<long> labelMap = new SortedSet<long>();

      PropagateProperties(
        graph,
        graph.GetSourceNodeId(),
        GraphDirection.Forward,
        nodeFilter,
        e => true,
        (w) => { labelMap.Add(w.Id); return true; },
        e => true);

      return labelMap.Contains(graph.GetSinkNodeId());
    }

    #endregion

    #region private members

    private static void CopyNodesAndEdges(
      DAG graph,
      SortedSet<long> nodeIdSubset,
      DAG subgraph)
    {
      subgraph.NodeEnumeration = new SortedDictionary<long, DAGNode>();

      foreach (long nodeId in nodeIdSubset)
      {
        DAGNode subgraphNode = new DAGNode(nodeId);
        subgraph.AddNode(subgraphNode);
      }

      List<KeyValuePair<KeyValuePair<long, long>, DAGEdge>> nodePairList1 =
        graph.NodePairEnumeration.Where(
          t => subgraph.NodeEnumeration.ContainsKey(t.Key.Key)).ToList();
      List<KeyValuePair<KeyValuePair<long, long>, DAGEdge>> nodePairList2 =
        nodePairList1.Where(
          t => subgraph.NodeEnumeration.ContainsKey(t.Key.Value)).ToList();

      subgraph.NodePairEnumeration =
        new SortedDictionary<KeyValuePair<long, long>, DAGEdge>(
          new KeyValueComparer());

      foreach (KeyValuePair<KeyValuePair<long, long>, DAGEdge> nodePair in nodePairList2)
      {
        DAGNode uSubgraphNode = subgraph.NodeEnumeration[nodePair.Key.Key];
        DAGNode vSubgraphNode = subgraph.NodeEnumeration[nodePair.Key.Value];

        DAGEdge subgraphEdge = new DAGEdge(nodePair.Value.Id, uSubgraphNode, vSubgraphNode);
        subgraph.AddEdge(subgraphEdge);
      }
    }

    private static void MakePathsSubgraph_st(
      DAG graph,
      long sNodeId,
      long tNodeId,
      DAG subgraph)
    {
      SortedSet<long> labelAMap = new SortedSet<long>();
      SortedSet<long> labelBMap = new SortedSet<long>();

      PropagateProperties(
        graph,
        sNodeId,
        GraphDirection.Forward,
        (v) => true,
        e => true,
        w => { labelAMap.Add(w.Id); return true; },
        e => true);

      PropagateProperties(
        graph,
        tNodeId,
        GraphDirection.Backward,
        (v) => true,
        e => true,
        w => { labelBMap.Add(w.Id); return true; },
        e => true);

      labelAMap.IntersectWith(labelBMap);

      labelAMap.Add(sNodeId);
      labelAMap.Add(tNodeId);

      MakeSubgraph(
        graph,
        sNodeId,
        tNodeId,
        labelAMap,
        subgraph);
    }

    private static void MakePathsSubgraph_t(
      DAG graph,
      long sNodeId,
      long tNodeId,
      DAG subgraph)
    {
      SortedSet<long> labelBMap = new SortedSet<long>();

      PropagateProperties(
        graph,
        tNodeId,
        GraphDirection.Backward,
        (v) => true,
        e => true,
        w => { labelBMap.Add(w.Id); return true; },
        e => true);

      labelBMap.Add(sNodeId);
      labelBMap.Add(tNodeId);

      MakeSubgraph(
        graph,
        sNodeId,
        tNodeId,
        labelBMap,
        subgraph);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////

