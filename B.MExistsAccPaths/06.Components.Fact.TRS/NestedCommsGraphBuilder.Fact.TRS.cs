﻿////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ninject;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public class NestedCommsGraphBuilderFactTRS
  {
    #region Ctors

    public NestedCommsGraphBuilderFactTRS(MEAPContext meapContext)
    {
      this.meapContext = meapContext;
    }

    #endregion

    #region public members

    public string Name { get; }

    public void Init()
    {
      meapContext.NestedCommsGraph =
        new TypedDAG<NestedCommsGraphNodeInfo, StdEdgeInfo>(
          "CommsGraph");
    }

    public void Run()
    {
      log.Info("Creating commodities graph");

      meapContext.NestedCommsGraph =
        new TypedDAG<NestedCommsGraphNodeInfo, StdEdgeInfo>(
          "CommsGraph");

      DAGNode sNode = new DAGNode(nodeId++);
      DAGNode tNode = new DAGNode(nodeId++);

      meapContext.NestedCommsGraph.AddNode(sNode);
      meapContext.NestedCommsGraph.AddNode(tNode);
      meapContext.NestedCommsGraph.SetSourceNode(sNode);
      meapContext.NestedCommsGraph.SetSinkNode(tNode);

      CreateNodes();
      ConnectSNodeCommodities();

      ConnectOrdinaryCommodities();
    }

    public void Trace()
    {
      log.DebugFormat(
        "CommsGraph total nodes = {0}",
        meapContext.NestedCommsGraph.Nodes.Count);

      log.DebugFormat(
        "CommsGraph total edges = {0}",
        meapContext.NestedCommsGraph.Edges.Count);
    }

    #endregion

    #region private members

    private static readonly IKernel configuration = Core.AppContext.Configuration;
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private readonly MEAPContext meapContext;
    private long nodeId = 0;

    private SortedDictionary<long, DAGNode> commToNodeMap =
      new SortedDictionary<long, DAGNode>();
    private SortedDictionary<long, LinkedList<long>> nodeToCommoditiesMap =
      new SortedDictionary<long, LinkedList<long>>();

    private SortedDictionary<long, LinkedList<long>> varToCommoditiesMap =
      new SortedDictionary<long, LinkedList<long>>();

    private void CreateNodes()
    {
      foreach (KeyValuePair<long, Commodity> idCommPair in meapContext.Commodities)
      {
        long commId = idCommPair.Key;
        Commodity comm = idCommPair.Value;
      }
    }

    private void ConnectSNodeCommodities()
    {
      long sNodeId = meapContext.TArbSeqCFG.GetSourceNodeId();

      foreach (KeyValuePair<long, Commodity> idCommPair in meapContext.Commodities)
      {
        long commId = idCommPair.Key;
        Commodity comm = idCommPair.Value;
        long commVar = comm.Variable;
      }
    }

    private bool ProcessNode(DAGNode node)
    {
      long nodeId = node.Id;
      long sNodeId = meapContext.TArbSeqCFG.GetSourceNodeId();

      return true;
    }

    private void ConnectOrdinaryCommodities()
    {
      long sNodeId = meapContext.TArbSeqCFG.GetSourceNodeId();
      long tNodeId = meapContext.TArbSeqCFG.GetSinkNodeId();

      DAG.BFS_VLevels(
        meapContext.TArbSeqCFG,
        meapContext.TArbSeqCFG.s,
        GraphDirection.Forward,
        meapContext.NodeVLevels,
        DAG.Level0,
        ProcessNode,
        (level) => { return true; });
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////