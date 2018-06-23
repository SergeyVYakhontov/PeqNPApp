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
  public class TASGBuilderFactComm : TASGBuilder
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

      propSymbolsKeeper = new PropSymbolsKeeperFactComms(MEAPSharedContext);
      propSymbolsKeeper.Init(s.Id);

      endNodeIds.Add(G.GetSourceNodeId());
    }

    public override void CreateTArbitrarySeqGraph()
    {
      long initMu = (processedMu.Any() ? processedMu.Last() : -1);
      log.Info("Building TArbitrarySeqGraph");

      log.Info("Traverse MNP tree");
      TraverseMNPTree(0);

      newNodeEnumeration.ForEach(t => nodeEnumeration[t.Key] = t.Value);
      newCompStepToNode.ForEach(t => compStepToNode[t.Key] = t.Value);

      newNodeEnumeration.Clear();
      newCompStepToNode.Clear();

      endNodeIds.Clear();
      endNodes.ForEach(e => endNodeIds.Add(e.Id));

      CreateSinkNode();
      G.CopyIdToInfoMap(idToInfoMap);

      meapContext.TArbitrarySeqGraph = G;
      long newMu = processedMu.Last();

      log.InfoFormat(
        "TArbitrarySeqGrap, mu: {0} {1}",
        initMu, newMu);

      log.Info("Remove unused prop syms");
      propSymbolsKeeper.RemoveUnusedSymbols(endNodeIds);

      Trace_TArbitrarySeqGraph();

      meapContext.AcceptingNodes = new SortedSet<long>(acceptingNodes.Select(t => t.Id));

      endNodes.Clear();
      acceptingNodes.Clear();
    }

    public override void CreateTArbSeqCFG(int[] states)
    {
      log.Info("Building TArbSeqCFG");

      log.Info("Create TArbSeqGraph copy");
      TypedDAG<TASGNodeInfo, StdEdgeInfo> cfg = new TypedDAG<TASGNodeInfo, StdEdgeInfo>("CFG");
      DAG.CreateCopy(G, cfg);
      cfg.CopyIdToInfoMap(idToInfoMap);

      log.Info("Create sink node");
      CreateSinkNode();
      log.Info("Connect bottomNodes with sink node");
      ConnectBottomNodesWithSinkNode(cfg, states);

      endNodes.Clear();
      acceptingNodes.Clear();

      log.Info("Cut chains");
      meapContext.TArbSeqCFG = new TypedDAG<TASGNodeInfo, StdEdgeInfo>("CFG");
      DAG.CutChains(cfg, meapContext.TArbSeqCFG);

      log.Info("Create CFG idToInfoMap");
      meapContext.TArbSeqCFG.CopyIdToInfoMap(idToInfoMap);

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

    private TypedDAG<TASGNodeInfo, StdEdgeInfo> G;
    private long nodeId = 0;
    private long edgeId = 0;
    private readonly SortedDictionary<long, DAGNode> nodeEnumeration = new SortedDictionary<long, DAGNode>();
    private readonly SortedDictionary<ComputationStep, long> compStepToNode =
      new SortedDictionary<ComputationStep, long>(new CompStepComparer());
    private SortedDictionary<long, TASGNodeInfo> idToInfoMap = new SortedDictionary<long, TASGNodeInfo>();

    private List<DAGNode> endNodes = new List<DAGNode>();
    private List<long> endNodeIds = new List<long>();
    private List<DAGNode> acceptingNodes = new List<DAGNode>();
    private long treesCut = 0;
    private SortedSet<long> processedMu = new SortedSet<long>();

    private SortedDictionary<long, DAGNode> newNodeEnumeration = new SortedDictionary<long, DAGNode>();
    private SortedDictionary<ComputationStep, long> newCompStepToNode =
      new SortedDictionary<ComputationStep, long>(new CompStepComparer());

    private PropSymbolsKeeperFactComms propSymbolsKeeper;

    private DAGNode GetDAGNode(ComputationStep compStep)
    {
      if (!newCompStepToNode.TryGetValue(compStep, out long nodeId))
      {
        return null;
      }

      return newNodeEnumeration[nodeId];
    }

    private List<KeyValuePair<StateSymbolPair, List<StateSymbolDirectionTriple>>>
      GetDeltaElements(int state)
    {
      return meapContext.MEAPSharedContext.MNP.Delta.Where(
        d => (d.Key.State == state)).ToList();
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

      bool ifThereIsFlowFrom = propSymbolsKeeper.IfThereIsFlowFrom(
        sNodeId,
        fromNode,
        fromCompStep,
        toCompStep);

      if (!ifThereIsFlowFrom)
      {
        return;
      }

      TapeLBound = Math.Min(toCompStep.kappaTape - 1, TapeLBound);
      TapeRBound = Math.Max(toCompStep.kappaTape + 1, TapeRBound);

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

      propSymbolsKeeper.PropagateSymbol(sNodeId, fromNode, toNode, fromCompStep, toCompStep);
    }

    private void TraverseMNPTree(long sNodeId)
    {
      Queue<DAGNode> nodeQueue = new Queue<DAGNode>();

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

        List<KeyValuePair<StateSymbolPair, List<StateSymbolDirectionTriple>>> deltaElements =
          GetDeltaElements(fromCompStep.qNext);

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
      ComputationStep tStep = idToInfoMap[cfg.GetSinkNodeId()].CompStep;

      foreach (long uNodeId in endNodeIds)
      {
        ComputationStep compStep = cfg.IdToInfoMap[uNodeId].CompStep;

        if (states.Contains(compStep.qNext))
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
