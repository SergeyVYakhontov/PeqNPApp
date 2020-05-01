////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Ninject;
using EnsureThat;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public partial class DAG : ITracable
  {
    #region Ctors

    public DAG(string name)
    {
      this.Name = name;

      this.Nodes = new List<DAGNode>();
      this.Edges = new List<DAGEdge>();

      this.NodeEnumeration = new SortedDictionary<long, DAGNode>();
      this.EdgeEnumeration = new SortedDictionary<long, DAGEdge>();
      this.NodePairEnumeration =
        new SortedDictionary<KeyValuePair<long, long>, DAGEdge>(
          new KeyValueComparer());
    }

    #endregion

    #region public members

    public string Name { get; }

    public List<DAGNode> Nodes { get; }
    public List<DAGEdge> Edges { get; }

    public DAGNode s { get; private set; }
    public DAGNode t { get; private set; }

    public SortedDictionary<long, DAGNode> NodeEnumeration { get; private set; }
    public SortedDictionary<long, DAGEdge> EdgeEnumeration { get; private set; }
    public SortedDictionary<KeyValuePair<long, long>, DAGEdge> NodePairEnumeration { get; private set; }

    public void AddNode(DAGNode node)
    {
      Nodes.Add(node);
      NodeEnumeration[node.Id] = node;
    }

    public void AddEdge(DAGEdge edge)
    {
      Edges.Add(edge);

      edge.FromNode.OutEdges.Add(edge);
      edge.ToNode.InEdges.Add(edge);

      EdgeEnumeration[edge.Id] = edge;
      KeyValuePair<long, long> nodePair = new KeyValuePair<long, long>(
        edge.FromNode.Id, edge.ToNode.Id);

      Ensure.That(NodePairEnumeration.ContainsKey(nodePair)).IsFalse();
      NodePairEnumeration[nodePair] = edge;
    }

    public void RemoveNode(DAGNode node)
    {
      Nodes.Remove(node);

      foreach (DAGEdge e in node.InEdges)
      {
        e.FromNode.OutEdges.Remove(e);
        Edges.Remove(e);

        EdgeEnumeration.Remove(e.Id);
        KeyValuePair<long, long> nodePair = new KeyValuePair<long, long>(
          e.FromNode.Id, e.ToNode.Id);
        NodePairEnumeration.Remove(nodePair);
      }

      foreach (DAGEdge e in node.OutEdges)
      {
        e.ToNode.InEdges.Remove(e);
        Edges.Remove(e);

        EdgeEnumeration.Remove(e.Id);
        KeyValuePair<long, long> nodePair = new KeyValuePair<long, long>(
          e.FromNode.Id, e.ToNode.Id);
        NodePairEnumeration.Remove(nodePair);
      }

      NodeEnumeration.Remove(node.Id);
    }

    public void RemoveEdge(DAGEdge edge)
    {
      Edges.Remove(edge);
      edge.FromNode.OutEdges.Remove(edge);
      edge.ToNode.InEdges.Remove(edge);

      EdgeEnumeration.Remove(edge.Id);
      KeyValuePair<long, long> nodePair = new KeyValuePair<long, long>(
        edge.FromNode.Id, edge.ToNode.Id);
      NodePairEnumeration.Remove(nodePair);
    }

    public void AddNodePair(long edgeId, long fromNodeId, long toNodeId)
    {
      DAGNode fromNode = NodeEnumeration[fromNodeId];
      DAGNode toNode = NodeEnumeration[toNodeId];

      DAGEdge edge = new DAGEdge(edgeId, fromNode, toNode);
      Edges.Add(edge);

      EdgeEnumeration[edgeId] = edge;
      KeyValuePair<long, long> nodePair = new KeyValuePair<long, long>(fromNodeId, toNodeId);
      NodePairEnumeration[nodePair] = edge;
    }

    public void RemoveNodePair(long fromNodeId, long toNodeId)
    {
      KeyValuePair<long, long> nodePair = new KeyValuePair<long, long>(fromNodeId, toNodeId);
      DAGEdge e = NodePairEnumeration[nodePair];
      RemoveEdge(e);
    }

    public bool HasNode(long nodeId)
    {
      return NodeEnumeration.ContainsKey(nodeId);
    }

    public bool AreConnected(long uId, long vId)
    {
      KeyValuePair<long, long> nodePair = new KeyValuePair<long, long>(uId, vId);

      return NodePairEnumeration.ContainsKey(nodePair);
    }

    public void SetSourceNode(DAGNode node)
    {
      s = node;
    }

    public void SetSinkNode(DAGNode node)
    {
      t = node;
    }

    public long GetSourceNodeId()
    {
      return s.Id;
    }

    public long GetSinkNodeId()
    {
      return t.Id;
    }

    public bool IsSinkNode(DAGNode node)
    {
      return (node.Id == t.Id);
    }

    public bool IsSourceNode(DAGNode node)
    {
      return (node.Id == s.Id);
    }

    public bool IsSinkNode(long nodeId)
    {
      return (nodeId == t.Id);
    }

    public bool IsSourceNode(long nodeId)
    {
      return (nodeId == s.Id);
    }

    public bool IsInnerNode(long nodeId)
    {
      return (!IsSourceNode(nodeId) && !IsSinkNode(nodeId));
    }

    public DAGNode GetNode(long nodeId)
    {
      return NodeEnumeration[nodeId];
    }

    public IReadOnlyCollection<long> GetInnerNodeIds()
    {
      return NodeEnumeration.Keys.Except(
        new List<long> {s.Id, t.Id }).ToList().AsReadOnly();
    }

    public IReadOnlyCollection<long> GetAllNodeIds()
    {
      return NodeEnumeration.Keys.ToList().AsReadOnly();
    }

    public bool IsTrivial()
    {
      return (Nodes.Count <= 2);
    }

    public void Trace()
    {
      ICommonOptions commonOptions = configuration.Get<ICommonOptions>();

      log.DebugFormat("DAG: {0}", Name);

      log.DebugFormat("NodesCount = {0}", Nodes.Count);
      log.DebugFormat("EdgesCount = {0}", Edges.Count);

      Edges.ForEach(e =>
        log.DebugFormat(
          "Edge Id={0}: ({1},{2})",
          e.Id,
          e.FromNode.Id,
          e.ToNode.Id));
    }

    #endregion

    #region private members

    private static readonly IKernel configuration = Core.AppContext.Configuration;
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
