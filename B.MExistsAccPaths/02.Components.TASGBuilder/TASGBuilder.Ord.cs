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
  public class TASGBuilderOrd : TASGBuilder
  {
    #region Ctors

    public TASGBuilderOrd(MEAPContext meapContext)
    {
      this.meapContext = meapContext;
    }

    #endregion

    #region public members

    public override void CreateTArbitrarySeqGraph()
    {
      log.Info("Building TArbitrarySeqGraph");

      Init();
      TraverseMNPTree(0);
      CreateSinkNode();

      G.CopyIdToInfoMap(idToInfoMap);
      meapContext.TArbitrarySeqGraph = G;

      Trace_TArbitrarySeqGraph();
    }

    public override void CreateTArbSeqCFG(int[] states)
    {
      log.Info("Building TArbSeqCFG");

      log.Info("Create TArbSeqGraph copy");
      TypedDAG<TASGNodeInfo, StdEdgeInfo> cfg = new TypedDAG<TASGNodeInfo, StdEdgeInfo>("CFG");
      DAG.CreateCopy(meapContext.TArbitrarySeqGraph, cfg);
      cfg.CopyIdToInfoMap(idToInfoMap);

      endNodes.ForEach(e => endNodeIds.Add(e.Id));
      ConnectBottomNodesWithSinkNode(cfg, states);
      endNodeIds.Clear();
      acceptingNodes.Clear();

      log.Info("Cut chains");
      meapContext.TArbSeqCFG = new TypedDAG<TASGNodeInfo, StdEdgeInfo>("CFG");
      DAG.CutChains_t(cfg, meapContext.TArbSeqCFG);

      meapContext.TArbSeqCFG.CopyIdToInfoMap(idToInfoMap);
      meapContext.TArbitrarySeqGraph = null;

      log.InfoFormat(
        "idToInfoMap: {0} {1}",
        idToInfoMap.Count,
        meapContext.TArbSeqCFG.IdToInfoMap.Count);

      Trace_TArbSeqCFG();
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
      System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private readonly MEAPContext meapContext;

    private TypedDAG<TASGNodeInfo, StdEdgeInfo> G;
    private long nodeId = 0;
    private long edgeId = 0;
    private SortedDictionary<long, DAGNode> nodeEnumeration = new SortedDictionary<long, DAGNode>();
    private SortedDictionary<ComputationStep, long> compStepToNode =
      new SortedDictionary<ComputationStep, long>(new CompStepComparer());
    private SortedDictionary<long, TASGNodeInfo> idToInfoMap = new SortedDictionary<long, TASGNodeInfo>();

    private List<DAGNode> endNodes = new List<DAGNode>();
    private List<long> endNodeIds = new List<long>();
    private List<DAGNode> acceptingNodes = new List<DAGNode>();
    private long treesCut = 0;

    private DAGNode GetDAGNode(ComputationStep compStep)
    {
      long nodeId;

      if (!compStepToNode.TryGetValue(compStep, out nodeId))
      {
        return null;
      }

      return nodeEnumeration[nodeId];
    }

    private List<KeyValuePair<StateSymbolPair, List<StateSymbolDirectionTriple>>>
      GetDeltaElements(int state)
    {
      return meapContext.MEAPSharedContext.MNP.Delta.Where(
        d => (d.Key.State == state)).ToList();
    }

    private void Init()
    {
      G = new TypedDAG<TASGNodeInfo, StdEdgeInfo>("TASG");

      TapeLBound = 1;
      TapeRBound = 1;

      DAGNode s = new DAGNode(nodeId++);
      G.AddNode(s);
      G.SetSourceNode(s);

      ComputationStep compStep = new ComputationStep()
      {
        q = meapContext.MEAPSharedContext.MNP.qStart,
        s = meapContext.MEAPSharedContext.Input[0],
        qNext = meapContext.MEAPSharedContext.MNP.qStart,
        sNext = meapContext.MEAPSharedContext.Input[0],
        m = TMDirection.S,
        kappaTape = 1,
        kappaStep = 0
      };

      nodeEnumeration[s.Id] = s;
      compStepToNode[compStep] = s.Id;
      idToInfoMap[s.Id] = new TASGNodeInfo()
      {
        CompStep = compStep
      };
    }

    private void CreateDAGNode(
      Queue<DAGNode> nodeQueue,
      long sNodeId,
      DAGNode fromNode,
      ComputationStep fromCompStep,
      StateSymbolPair from,
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

      DAGEdge e = new DAGEdge(edgeId++, fromNode, toNode);
      G.AddEdge(e);
    }

    private void TraverseMNPTree(long sNodeId)
    {
      Queue<DAGNode> nodeQueue = new Queue<DAGNode>();
      nodeQueue.Enqueue(G.s);

      while (nodeQueue.Any())
      {
        DAGNode fromNode = nodeQueue.Dequeue();
        ComputationStep fromCompStep = idToInfoMap[fromNode.Id].CompStep;
        List<KeyValuePair<StateSymbolPair, List<StateSymbolDirectionTriple>>> deltaElements =
          GetDeltaElements(fromCompStep.qNext);

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
          deltaElements)
        {
          StateSymbolPair from = to.Key;

          foreach (StateSymbolDirectionTriple p in to.Value)
          {
            CreateDAGNode(
              nodeQueue,
              sNodeId,
              fromNode,
              fromCompStep,
              from,
              p);
          }
        }
      }
    }

    private void CreateSinkNode()
    {
      DAGNode t = new DAGNode(nodeId++);

      G.AddNode(t);
      G.SetSinkNode(t);

      nodeEnumeration[t.Id] = t;
      idToInfoMap[t.Id] = new TASGNodeInfo()
      {
        CompStep = new ComputationStep()
      };
    }

    private void ConnectBottomNodesWithSinkNode(
      TypedDAG<TASGNodeInfo, StdEdgeInfo> cfg,
      int[] states)
    {
      foreach (long uNodeId in endNodeIds)
      {
        ComputationStep compStep = cfg.IdToInfoMap[uNodeId].CompStep;

        if (states.Contains(compStep.qNext))
        {
          DAGNode v = cfg.NodeEnumeration[uNodeId];

          DAGEdge e = new DAGEdge(edgeId++, v, cfg.t);
          cfg.AddEdge(e);
        }
      }
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////