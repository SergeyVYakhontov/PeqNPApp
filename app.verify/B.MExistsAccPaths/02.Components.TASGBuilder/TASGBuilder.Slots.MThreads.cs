﻿////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public class TASGBuilderSlotsMThreads : TASGBuilder
  {
    #region public members

    public override void Init()
    {
      G = new TypedDAG<TASGNodeInfo, StdEdgeInfo>("TASG");
      TapeLBound = 1;
      TapeRBound = 1;

      DAGNode s = new(nodeId++);
      G.AddNode(s);
      G.SetSourceNode(s);

      ComputationStep compStep = new()
      {
        q = MEAPSharedContext.MNP.qStart,
        s = MEAPSharedContext.Input[0],
        qNext = MEAPSharedContext.MNP.qStart,
        sNext = MEAPSharedContext.Input[0],
        m = TMDirection.S,
        Shift = 1,
        kappaTape = 1,
        kappaStep = 0
      };

      nodeEnumeration[s.Id] = s;
      compStepToNode[compStep] = s.Id;

      idToInfoMap[s.Id] = new TASGNodeInfo
      {
        CompStep = compStep
      };

      endNodeIds.Add(0);
    }

    public override void CreateTArbitrarySeqGraph()
    {
      ulong initMu = (processedMu.Any() ? processedMu.Last() : 0);
      log.Info("Building TArbitrarySeqGraph");

      log.Info("Traverse MNP tree");
      TraverseMNPTree();

      endNodeIds.Clear();
      endNodes.ForEach(e => endNodeIds.Add(e.Id));

      log.Info("Remove unused prop syms");

      log.Info("Create sink node");
      CreateSinkNode();
      G.CopyIdToNodeInfoMap(idToInfoMap);

      meapContext.TArbitrarySeqGraph = G;
      ulong newMu = processedMu.Last();

      log.InfoFormat(
        "TArbitrarySeqGraph, mu: {0} {1}",
        initMu,
        newMu);

      Trace_TArbitrarySeqGraph();
    }

    public override void CreateTArbSeqCFG(uint[] states)
    {
      log.Info("Building TArbSeqCFG");

      log.Info("Create TArbSeqGraph copy");
      TypedDAG<TASGNodeInfo, StdEdgeInfo> cfg = new("CFG");
      DAG.CreateCopy(G, cfg);
      cfg.CopyIdToNodeInfoMap(idToInfoMap);

      log.Info("Connect bottomNodes with sink node");
      ConnectBottomNodesWithSinkNode(cfg, states);

      log.Info("Cut chains");
      meapContext.TArbSeqCFG = new TypedDAG<TASGNodeInfo, StdEdgeInfo>("CFG");
      DAG.CutChains_t(cfg, meapContext.TArbSeqCFG);

      log.Info("Create CFG idToInfoMap");
      meapContext.TArbSeqCFG.CopyIdToNodeInfoMap(idToInfoMap);

      log.InfoFormat(
        "idToInfoMap: {0} {1}",
        idToInfoMap.Count,
        meapContext.TArbSeqCFG.IdToNodeInfoMap.Count);

      Trace_TArbSeqCFG();

      endNodes.Clear();
      acceptingNodes.Clear();
    }

    public void Trace_TArbitrarySeqGraph()
    {
      log.InfoFormat("TASGBuilder");
      log.InfoFormat("TArbitrarySeqGraph: {0}", Name);
      log.InfoFormat("TotalNodes = {0}", G.Nodes.Count);
      log.InfoFormat("TotalEdges = {0}", G.Edges.Count);
      log.InfoFormat("TreesCut = {0}", treesCut);
      log.InfoFormat("EndNodes = {0}", endNodes.Count);
      log.InfoFormat("AcceptingNodes = {0}", acceptingNodes.Count);
    }

    public void Trace_TArbSeqCFG()
    {
      log.InfoFormat("TArbSeqCFG: {0}", Name);
      log.InfoFormat("TotalNodes = {0}", meapContext.TArbSeqCFG.Nodes.Count);
      log.InfoFormat("TotalEdges = {0}", meapContext.TArbSeqCFG.Edges.Count);
    }

    public override void Trace()
    {
      Trace_TArbitrarySeqGraph();
      Trace_TArbSeqCFG();
    }

    #endregion

    #region private members

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType);

    private TypedDAG<TASGNodeInfo, StdEdgeInfo> G;
    private long nodeId;
    private long edgeId;
    private readonly SortedDictionary<long, DAGNode> nodeEnumeration = new();
    private readonly SortedDictionary<ComputationStep, long> compStepToNode = new(new CompStepComparer());
    private readonly SortedDictionary<long, TASGNodeInfo> idToInfoMap = new();

    private readonly List<DAGNode> endNodes = new();
    private readonly List<long> endNodeIds = new();
    private readonly List<DAGNode> acceptingNodes = new();
    private long treesCut;
    private readonly SortedSet<ulong> processedMu = new();

    private DAGNode GetDAGNode(ComputationStep compStep)
    {
      if (!compStepToNode.TryGetValue(compStep, out long nodeId))
      {
        return null;
      }

      return nodeEnumeration[nodeId];
    }

    private List<KeyValuePair<StateSymbolPair, List<StateSymbolDirectionTriple>>>
      GetDeltaElements(uint state)
    {
      return meapContext.MEAPSharedContext.MNP.Delta.Where(
        d => (d.Key.State == state)).ToList();
    }

    private void CreateDAGNode(
      Queue<DAGNode> nodeQueue,
      DAGNode fromNode,
      ComputationStep fromCompStep,
      in StateSymbolPair from,
      StateSymbolDirectionTriple p)
    {
      ComputationStep toCompStep = CompStepSequence.GetSequentialCompStep(fromCompStep);

      toCompStep.q = from.State;
      toCompStep.s = from.Symbol;
      toCompStep.qNext = p.State;
      toCompStep.sNext = p.Symbol;
      toCompStep.m = p.Direction;
      toCompStep.Shift = p.Shift;

      TapeLBound = Math.Min(toCompStep.kappaTape - 1, TapeLBound);
      TapeRBound = Math.Max(toCompStep.kappaTape + 1, TapeRBound);

      DAGNode toNode = GetDAGNode(toCompStep);

      if (toNode == null)
      {
        toNode = new DAGNode(nodeId++);
        G.AddNode(toNode);

        nodeEnumeration[toNode.Id] = toNode;
        compStepToNode[toCompStep] = toNode.Id;

        AppHelper.TakeValueByKey(idToInfoMap, toNode.Id, () => new TASGNodeInfo())
          .CompStep = toCompStep;

        nodeQueue.Enqueue(toNode);
      }
      else
      {
        treesCut++;
      }

      DAGEdge e = new(edgeId++, fromNode, toNode);
      G.AddEdge(e);
    }

    private void TraverseMNPTree()
    {
      Queue<DAGNode> nodeQueue = new();

      endNodeIds.ForEach(p =>
          {
            DAGNode u = nodeEnumeration[p];
            nodeQueue.Enqueue(u);
          }
        );

      while (nodeQueue.Any())
      {
        DAGNode fromNode = nodeQueue.Dequeue();
        ComputationStep fromCompStep = idToInfoMap[fromNode.Id].CompStep;

        processedMu.Add(fromCompStep.kappaStep);

        if (fromCompStep.kappaStep == meapContext.mu)
        {
          endNodes.Add(fromNode);

          if (meapContext.MEAPSharedContext.MNP.F.Contains(fromCompStep.qNext))
          {
            acceptingNodes.Add(fromNode);
          }

          continue;
        }

        foreach (KeyValuePair<StateSymbolPair, List<StateSymbolDirectionTriple>> to in
                    GetDeltaElements(fromCompStep.qNext))
        {
          StateSymbolPair from = to.Key;

          foreach (StateSymbolDirectionTriple p in to.Value)
          {
            CreateDAGNode(nodeQueue, fromNode, fromCompStep, from, p);
          }
        }
      }
    }

    private void CreateSinkNode()
    {
      DAGNode t = new(nodeId++);

      G.AddNode(t);
      G.SetSinkNode(t);

      nodeEnumeration[t.Id] = t;

      idToInfoMap[t.Id] = new TASGNodeInfo
      {
        CompStep = new ComputationStep()
      };
    }

    private void ConnectBottomNodesWithSinkNode(
      TypedDAG<TASGNodeInfo, StdEdgeInfo> cfg,
      uint[] states)
    {
      ComputationStep tStep = idToInfoMap[cfg.GetSinkNodeId()].CompStep;

      foreach (long uNodeId in endNodeIds)
      {
        ComputationStep compStep = cfg.IdToNodeInfoMap[uNodeId].CompStep;
        if (states.Contains(compStep.qNext))
        {
          DAGNode v = cfg.NodeEnumeration[uNodeId];
          DAGEdge e = new(edgeId++, v, cfg.t);

          cfg.AddEdge(e);
          tStep.kappaStep = compStep.kappaStep + 1;
        }
      }
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
