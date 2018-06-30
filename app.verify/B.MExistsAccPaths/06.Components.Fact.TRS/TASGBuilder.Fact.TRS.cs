////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public class TASGBuilderFactTRS : TASGBuilder
  {
    #region public members

    public MEAPContext meapContext { get; set; }

    public void Init(MEAPSharedContext MEAPSharedContext)
    {
      G = new TypedDAG<TASGNodeInfo, StdEdgeInfo>("TASG");
      TapeLBound = 1;
      TapeRBound = 1;

      DAGNode s = new DAGNode(nodeId++);
      G.AddNode(s);
      G.SetSourceNode(s);

      ComputationStep compStep = new ComputationStep()
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
      idToInfoMap[s.Id] = new TASGNodeInfo()
      {
        CompStep = compStep
      };

      propSymbolsKeeper = new PropSymbolsKeeperFactTRS(MEAPSharedContext);
      propSymbolsKeeper.Init(s.Id);

      endNodeIds.Add(G.GetSourceNodeId());
    }

    public override void CreateTArbitrarySeqGraph()
    {
      ulong initMu = (processedMu.Any() ? processedMu.Last() : 0);
      log.Info("Building TArbitrarySeqGraph");

      log.Info("Traverse MNP tree");
      TraverseMNPTree();

      newNodeEnumeration.ForEach(t => nodeEnumeration[t.Key] = t.Value);
      log.InfoFormat(
        "newNodeEnumeration, nodeEnumeration: {0} {1}",
          newNodeEnumeration.Count,
          nodeEnumeration.Count);
      newNodeEnumeration.Clear();

      newCompStepToNode.ForEach(t => compStepToNode[t.Key] = t.Value);
      log.InfoFormat(
        "newCompStepToNode, compStepToNode: {0} {1}",
          newCompStepToNode.Count,
          compStepToNode.Count);
      newCompStepToNode.Clear();

      endNodeIds.Clear();
      endNodes.ForEach(e => endNodeIds.Add(e.Id));

      log.Info("Remove unused prop syms");
      propSymbolsKeeper.RemoveUnusedSymbols(endNodeIds);

      CreateSinkNode();
      G.CopyIdToNodeInfoMap(idToInfoMap);

      meapContext.TArbitrarySeqGraph = G;
      ulong newMu = processedMu.Last();

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

      log.Info("Create TArbSeqGraph copy");
      TypedDAG<TASGNodeInfo, StdEdgeInfo> cfg = new TypedDAG<TASGNodeInfo, StdEdgeInfo>("CFG");
      DAG.CreateCopy(G, cfg);
      cfg.CopyIdToNodeInfoMap(idToInfoMap);

      log.Info("Create sink node");
      CreateSinkNode();
      log.Info("Connect bottomNodes with sink node");
      ConnectBottomNodesWithSinkNode(cfg, states);

      log.Info("Cut chains");
      meapContext.TArbSeqCFG = new TypedDAG<TASGNodeInfo, StdEdgeInfo>("CFG");
      DAG.CutChains(cfg, meapContext.TArbSeqCFG);

      log.Info("Create CFG idToInfoMap");
      meapContext.TArbSeqCFG.CopyIdToNodeInfoMap(idToInfoMap);

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

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private TypedDAG<TASGNodeInfo, StdEdgeInfo> G;
    private long nodeId;
    private long edgeId;
    private readonly SortedDictionary<long, DAGNode> nodeEnumeration =
      new SortedDictionary<long, DAGNode>();
    private readonly SortedDictionary<ComputationStep, long> compStepToNode =
      new SortedDictionary<ComputationStep, long>(new CompStepComparer());
    private readonly SortedDictionary<long, TASGNodeInfo> idToInfoMap =
      new SortedDictionary<long, TASGNodeInfo>();

    private readonly List<DAGNode> endNodes = new List<DAGNode>();
    private readonly List<long> endNodeIds = new List<long>();
    private readonly List<DAGNode> acceptingNodes = new List<DAGNode>();
    private long treesCut;
    private readonly SortedSet<ulong> processedMu = new SortedSet<ulong>();

    private readonly SortedDictionary<long, DAGNode> newNodeEnumeration =
      new SortedDictionary<long, DAGNode>();
    private readonly SortedDictionary<ComputationStep, long> newCompStepToNode =
      new SortedDictionary<ComputationStep, long>(new CompStepComparer());

    private PropSymbolsKeeperFactTRS propSymbolsKeeper;

    private DAGNode GetDAGNode(ComputationStep compStep)
    {
      if (!newCompStepToNode.TryGetValue(compStep, out long nodeIdToTake))
      {
        return null;
      }

      return newNodeEnumeration[nodeIdToTake];
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

      bool ifThereIsFlowFrom = propSymbolsKeeper.IfThereIsFlowFrom(
        fromNode,
        fromCompStep,
        toCompStep);

      if (!ifThereIsFlowFrom)
      {
        return;
      }

      TapeLBound = Math.Min(toCompStep.kappaTape - 1, TapeLBound);
      TapeRBound = Math.Max(toCompStep.kappaTape + 1, TapeRBound);

      toCompStep.q = from.State;
      toCompStep.s = from.Symbol;
      toCompStep.qNext = p.State;
      toCompStep.sNext = p.Symbol;
      toCompStep.m = p.Direction;
      toCompStep.Shift = p.Shift;

      DAGNode toNode = GetDAGNode(toCompStep);

      if (toNode == null)
      {
        toNode = new DAGNode(nodeId++);
        G.AddNode(toNode);

        newNodeEnumeration[toNode.Id] = toNode;
        newCompStepToNode[toCompStep] = toNode.Id;

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

      propSymbolsKeeper.PropagateSymbol(fromNode, toNode, toCompStep);
    }

    private void TraverseMNPTree()
    {
      Queue<DAGNode> nodeQueue = new Queue<DAGNode>();

      endNodeIds.ForEach(p =>
      {
        DAGNode u = nodeEnumeration[p];
        nodeQueue.Enqueue(u);
      });

      while (nodeQueue.Any())
      {
        DAGNode fromNode = nodeQueue.Dequeue();
        ComputationStep fromCompStep = idToInfoMap[fromNode.Id].CompStep;

        processedMu.Add(fromCompStep.kappaStep);

        if (fromCompStep.kappaStep == meapContext.mu)
        {
          endNodes.Add(fromNode);

          if (meapContext.MEAPSharedContext.MNP.F.Contains((uint)fromCompStep.qNext))
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
            CreateDAGNode(
              nodeQueue,
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
      uint[] states)
    {
      ComputationStep tStep = idToInfoMap[cfg.GetSinkNodeId()].CompStep;

      foreach (long uNodeId in endNodeIds)
      {
        ComputationStep compStep = cfg.IdToNodeInfoMap[uNodeId].CompStep;

        if (states.Contains((uint)compStep.qNext))
        {
          DAGNode v = cfg.NodeEnumeration[uNodeId];
          DAGEdge e = new DAGEdge(edgeId++, v, cfg.t);

          cfg.AddEdge(e);
          tStep.kappaStep = compStep.kappaStep + 1;
        }
      }
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
