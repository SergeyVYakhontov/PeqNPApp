﻿////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Ninject;
using EnsureThat;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public class TASGBuilderFactCPLTMTheSameFrom : TASGBuilder
  {
    #region ctors

    public TASGBuilderFactCPLTMTheSameFrom()
    {
      this.configuration = Core.AppContext.GetConfiguration();
    }

    #endregion

    #region public members

    public override void Init()
    {
      G = new TypedDAG<TASGNodeInfo, StdEdgeInfo>("TASG");
      TapeLBound = 0;
      TapeRBound = 0;

      DAGNode s = new(nodeId++);
      G.AddNode(s);
      G.SetSourceNode(s);

      MEAPSharedContext.NodeLevelInfo.AddNodeAtLevel(s.Id, 0);
      processedMu = 0;

      sCompStep = new ComputationStep
      {
        q = MEAPSharedContext.MNP.qStart,
        s = MEAPSharedContext.Input[0],
        qNext = MEAPSharedContext.MNP.qStart,
        sNext = MEAPSharedContext.Input[0],
        m = TMDirection.R,
        Shift = 1,
        kappaTape = 0,
        kappaStep = 0,
        sTheSame = OneTapeTuringMachine.blankSymbol
      };

      nodeEnumeration[s.Id] = s;

      idToInfoMap[s.Id] = new TASGNodeInfo
      {
        CompStep = sCompStep
      };

      propSymbolsKeeper = new PropSymbolsKeeperFactCPLTM(MEAPSharedContext);
      propSymbolsKeeper.Init(s.Id);

      CPLTMInfo = MEAPSharedContext.CPLTMInfo;

      endNodeIds.Add(G.GetSourceNodeId());
    }

    public override void CreateTArbitrarySeqGraph()
    {
      long initMu = processedMu;
      log.Info("Building TArbitrarySeqGraph");

      log.Info("Traverse MNP tree");
      TraverseMNPTree();

      Ensure.That(newEdgesCount).IsNot(0);

      newNodeEnumeration.ForEach(t => nodeEnumeration[t.Key] = t.Value);
      log.InfoFormat(
        "newNodeEnumeration, nodeEnumeration: {0} {1}",
          newNodeEnumeration.Count,
          nodeEnumeration.Count);

      newNodeEnumeration.Clear();
      newCompStepToNode.Clear();
      newEdgesCount = 0;

      endNodeIds.Clear();
      endNodes.ForEach(e => endNodeIds.Add(e.Id));

      log.Info("Remove unused prop syms");
      propSymbolsKeeper.RemoveUnusedSymbols(endNodeIds);

      meapContext.TArbitrarySeqGraph = G;
      long newMu = processedMu;

      log.InfoFormat(
       "TArbitrarySeqGrap, mu: {0} {1}",
          initMu, newMu);

      log.Info("Remove unused prop syms");

      Trace_TArbitrarySeqGraph();

      meapContext.AcceptingNodes = new SortedSet<long>(acceptingNodes.Select(t => t.Id));

      endNodes.Clear();
      acceptingNodes.Clear();
    }

    public override void CreateTArbSeqCFG(uint[] states)
    {
      log.Info("Building TArbSeqCFG");

      Ensure.That(meapContext.AcceptingNodes).HasItems();

      log.Info("Create sink node");
      CreateSinkNode();

      log.Info("Connect bottomNodes with sink node");
      ConnectBottomNodesWithSinkNode(G, states);

      log.Info("Create TArbSeqGraph copy");
      TypedDAG<TASGNodeInfo, StdEdgeInfo> cfg = new("CFG");
      DAG.CreateCopy(G, cfg);
      cfg.CopyIdToNodeInfoMap(idToInfoMap);

      log.Info("Cut chains");
      meapContext.TArbSeqCFG = new TypedDAG<TASGNodeInfo, StdEdgeInfo>("CFG");
      DAG.CutChains(cfg, meapContext.TArbSeqCFG);

      log.Info("Create CFG idToInfoMap");
      meapContext.TArbSeqCFG.CopyIdToNodeInfoMap(idToInfoMap);

      RemoveUnusedNodeVLevels();

      log.InfoFormat(
        "idToInfoMap: {0} {1}",
          idToInfoMap.Count,
          meapContext.TArbSeqCFG.IdToNodeInfoMap.Count);

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

    private readonly IReadOnlyKernel configuration;

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType);

    private TypedDAG<TASGNodeInfo, StdEdgeInfo> G = default!;
    private long nodeId;
    private long edgeId;
    private readonly SortedDictionary<long, DAGNode> nodeEnumeration = new();
    private readonly SortedDictionary<long, TASGNodeInfo> idToInfoMap = new();

    private readonly List<DAGNode> endNodes = new();
    private readonly List<long> endNodeIds = new();
    private readonly List<DAGNode> acceptingNodes = new();
    private long treesCut;
    private long processedMu;

    private ComputationStep sCompStep = default!;
    private readonly SortedDictionary<long, DAGNode> newNodeEnumeration = new();
    private readonly SortedDictionary<ComputationStep, long> newCompStepToNode = new(new CompStepComparer());
    private long newEdgesCount;

    private PropSymbolsKeeperFactCPLTM propSymbolsKeeper = default!;
    private ICPLTMInfo CPLTMInfo = default!;

    private void GetDAGNode(ComputationStep compStep, out DAGNode toNode)
    {
      toNode = default!;

      foreach (KeyValuePair<ComputationStep, long> itemPair in newCompStepToNode)
      {
        if (itemPair.Key == compStep)
        {
          toNode = newNodeEnumeration[itemPair.Value];
          return;
        }
      }
    }

    private List<KeyValuePair<StateSymbolPair, List<StateSymbolDirectionTriple>>>
      GetDeltaElements(uint state)
    {
      return meapContext.MEAPSharedContext.MNP.Delta.Where(
        d => (d.Key.State == state)).ToList();
    }

    private void CreateDAGNode(
      DAGNode fromNode,
      ComputationStep fromCompStep,
      in StateSymbolPair from,
      StateSymbolDirectionTriple p,
      int sTo)
    {
      ComputationStep toCompStep = CompStepSequence.GetSequentialCompStep(fromCompStep);

      toCompStep.q = from.State;
      toCompStep.s = from.Symbol;
      toCompStep.qNext = p.State;
      toCompStep.sNext = p.Symbol;
      toCompStep.m = p.Direction;
      toCompStep.Shift = p.Shift;

      long nextKTapeMu = CPLTMInfo.CellIndexes()[(int)processedMu + 1];

      if (toCompStep.kappaTape != nextKTapeMu)
      {
        return;
      }

      IDebugOptions debugOptions = configuration.Get<IDebugOptions>();

      if (debugOptions.UsePropSymbols)
      {
        bool ifThereIsFlowFrom = propSymbolsKeeper.IfThereIsFlowFrom(
          fromNode,
          fromCompStep,
          toCompStep);

        if (!ifThereIsFlowFrom)
        {
          return;
        }
      }

      if (debugOptions.UseTapeRestrictions)
      {
        SortedSet<int> tapeSymbols = MEAPSharedContext.MNP.UsedTapeSymbols[toCompStep.kappaTape];

        if (!tapeSymbols.Contains(toCompStep.s))
        {
          return;
        }

        if (!tapeSymbols.Contains(toCompStep.sNext))
        {
          return;
        }
      }

      TapeLBound = Math.Min(toCompStep.kappaTape, TapeLBound);
      TapeRBound = Math.Max(toCompStep.kappaTape, TapeRBound);

      toCompStep.q = from.State;
      toCompStep.s = from.Symbol;
      toCompStep.qNext = p.State;
      toCompStep.sNext = p.Symbol;
      toCompStep.m = p.Direction;
      toCompStep.Shift = p.Shift;
      toCompStep.sTheSame = sTo;

      GetDAGNode(toCompStep, out DAGNode toNode);

      if (toNode == null)
      {
        toNode = new DAGNode(nodeId++);
        G.AddNode(toNode);
        MEAPSharedContext.NodeLevelInfo.AddNodeAtLevel(toNode.Id, processedMu + 1);

        newNodeEnumeration[toNode.Id] = toNode;
        newCompStepToNode[toCompStep] = toNode.Id;

        AppHelper.TakeValueByKey(idToInfoMap, toNode.Id, () => new TASGNodeInfo())
          .CompStep = toCompStep;

        endNodes.Add(toNode);

        if (meapContext.MEAPSharedContext.MNP.F.Contains(toCompStep.qNext))
        {
          acceptingNodes.Add(toNode);
        }
      }
      else
      {
        treesCut++;
      }

      DAGEdge e = new(edgeId++, fromNode, toNode);
      G.AddEdge(e);

      newEdgesCount++;

      if (debugOptions.UsePropSymbols)
      {
        propSymbolsKeeper.PropagateSymbol(fromNode, toNode, toCompStep);
      }
    }

    private void TraverseMNPTree()
    {
      Queue<DAGNode> nodeQueue = new();

      endNodeIds.ForEach(p =>
      {
        DAGNode u = nodeEnumeration[p];

        if (u.InEdges.Any() || G.IsSourceNode(u))
        {
          nodeQueue.Enqueue(u);
        }
      });

      while (nodeQueue.Any())
      {
        DAGNode fromNode = nodeQueue.Dequeue();
        ComputationStep fromCompStep = idToInfoMap[fromNode.Id].CompStep;

        IList<KeyValuePair<StateSymbolPair, List<StateSymbolDirectionTriple>>> deltaElements = GetDeltaElements(fromCompStep.qNext);

        foreach (KeyValuePair<StateSymbolPair, List<StateSymbolDirectionTriple>> to in deltaElements)
        {
          StateSymbolPair from = to.Key;

          foreach (StateSymbolDirectionTriple p in to.Value)
          {
            CreateDAGNode(fromNode, fromCompStep, from, p, fromCompStep.sNext);
          }
        }
      }

      processedMu++;
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

      MEAPSharedContext.NodeLevelInfo.AddNodeAtLevel(t.Id, processedMu + 1);
    }

    private void ConnectBottomNodesWithSinkNode(
      TypedDAG<TASGNodeInfo, StdEdgeInfo> cfg,
      uint[] states)
    {
      ComputationStep tStep = idToInfoMap[cfg.GetSinkNodeId()].CompStep;

      foreach (long uNodeId in endNodeIds)
      {
        ComputationStep compStep = idToInfoMap[uNodeId].CompStep;

        if (states.Contains(compStep.qNext))
        {
          DAGNode v = cfg.NodeEnumeration[uNodeId];
          DAGEdge e = new(edgeId++, v, cfg.t);

          cfg.AddEdge(e);
          tStep.kappaStep = compStep.kappaStep + 1;
        }
      }
    }

    public void RemoveUnusedNodeVLevels()
    {
      foreach(KeyValuePair<long, SortedSet<long>> itemPair in MEAPSharedContext.NodeLevelInfo.NodeVLevels)
      {
        SortedSet<long> levelNodeIds = itemPair.Value;
        IReadOnlyCollection<long> cfgNodeIds = meapContext.TArbSeqCFG.GetAllNodeIds();

        levelNodeIds.IntersectWith(cfgNodeIds);
      }
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
