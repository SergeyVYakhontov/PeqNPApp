////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Ninject;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public class RDARunnerSlotsMThreads : IObjectWithId, ITracable, ITPLCollectionItem
  {
    #region Ctors

    public RDARunnerSlotsMThreads(long id)
    {
      this.Id = id;
    }

    #endregion

    #region public members

    public long Id { get; }
    public string Name { get; }

    public bool Done { get; private set; }

    public void Init(
      ReachDefAnalysisContext rdaContext,
      SortedSet<long> vars,
      SortedSet<long> varNodes,
      SortedDictionary<long, SortedSet<long>> nodeVLevels,
      long currentLevel,
      SortedDictionary<long, SortedSet<DefUsePair>> defUsePairSet)
    {
      this.rdaContext = rdaContext;
      this.vars = vars;
      this.varNodes = varNodes;
      this.nodeVLevels = nodeVLevels;
      this.currentLevel = currentLevel;
      this.defUsePairSet = defUsePairSet;
    }

    public void Run()
    {
      ComputeNodesToProcess();
      ComputeSlot();

      if (!slotBitSet.Any())
      {
        Done = true;

        return;
      }

      log.InfoFormat($"Slot bits {slotBitSet.Count}");

      CreateVectors();
      InitVectors();

      DAG.BFS_VLevels(
        rdaContext.CFG,
        GraphDirection.Forward,
        nodeVLevels,
        currentLevel,
        ComputeVectors,
        _ => true);

      ComputeDefUsePairSet();
      ClearVectors();

      slotBitSet.Clear();
      Done = true;
    }

    public void Trace()
    {
      log.DebugFormat("NodesCount = {0}", rdaContext.CFG.Nodes.Count);
      log.DebugFormat("EdgesCount = {0}", rdaContext.CFG.Edges.Count);

      log.Debug("nodeToGENVectorMap");
      nodeToGENVectorMap.Values.ForEach(r => log.Debug(r.ToString()));

      log.Debug("nodeToKILLVectorMap");
      nodeToKILLVectorMap.Values.ForEach(r => log.Debug(r.ToString()));

      log.Debug("nodeToREACHinVectorMap");
      nodeToREACHinVectorMap.Values.ForEach(r => log.Debug(r.ToString()));

      log.Debug("nodeToREACHoutVectorMap");
      nodeToREACHoutVectorMap.Values.ForEach(r => log.DebugFormat(r.ToString()));
    }

    #endregion

    #region private members

    private static readonly IKernel configuration = Core.AppContext.Configuration;
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private ReachDefAnalysisContext rdaContext;
    private SortedSet<long> vars;
    private SortedSet<long> varNodes;
    private SortedDictionary<long, SortedSet<long>> nodeVLevels;
    private long currentLevel;
    private SortedDictionary<long, SortedSet<DefUsePair>> defUsePairSet;

    private readonly SortedSet<long> nodesToProcess = new SortedSet<long>();
    private readonly SortedSet<long> slotBitSet = new SortedSet<long>();
    private SortedDictionary<long, long> fBits;
    private SortedDictionary<long, long> bBits;

    private readonly SortedDictionary<long, IBitVector> varToDEFSVectorMap =
      new SortedDictionary<long, IBitVector>();

    private readonly SortedDictionary<long, IBitVector> nodeToGENVectorMap =
      new SortedDictionary<long, IBitVector>();
    private readonly SortedDictionary<long, IBitVector> nodeToKILLVectorMap =
      new SortedDictionary<long, IBitVector>();
    private readonly SortedDictionary<long, IBitVector> nodeToREACHinVectorMap =
      new SortedDictionary<long, IBitVector>();
    private readonly SortedDictionary<long, IBitVector> nodeToREACHoutVectorMap =
      new SortedDictionary<long, IBitVector>();

    private static IBitVector CreateBitVector(ulong size)
    {
      IBitVectorProvider bitVectorProvider = configuration.Get<IBitVectorProvider>();

      return bitVectorProvider.CreateVector(size);
    }

    private static IBitVector CreateBitVector(IBitVector v)
    {
      IBitVectorProvider bitVectorProvider = configuration.Get<IBitVectorProvider>();

      return bitVectorProvider.CreateVector(v);
    }

    private void ComputeSlot()
    {
      foreach (long nodeId in varNodes)
      {
        rdaContext.NodeToDefsSet.TryGetValue(nodeId, out List<long> defs);

        foreach (long def in defs)
        {
          long variable = rdaContext.DefToVarMap[def];

          if (!vars.Contains(variable))
          {
            continue;
          }

          slotBitSet.Add(def);
        }
      }

      fBits = new SortedDictionary<long, long>();
      bBits = new SortedDictionary<long, long>();

      long i = 0;

      foreach (long b in slotBitSet)
      {
        fBits[i] = b;
        bBits[b] = i;
        i++;
      }
    }

    private long RelativeIndex(long def)
    {
      return bBits[def];
    }

    private long AbsoluteIndex(long def)
    {
      return fBits[def];
    }

    private void ComputeNodesToProcess()
    {
      foreach (KeyValuePair<long, SortedSet<long>> currentLevelNodes in nodeVLevels)
      {
        if (currentLevelNodes.Key >= currentLevel)
        {
          currentLevelNodes.Value.ForEach(v => nodesToProcess.Add(v));
        }
      }
    }

    private void CreateVectors()
    {
      nodesToProcess.ForEach(node =>
      { nodeToGENVectorMap[node] = CreateBitVector((ulong)slotBitSet.Count); });
      nodesToProcess.ForEach(node =>
      { nodeToKILLVectorMap[node] = CreateBitVector((ulong)slotBitSet.Count); });
      nodesToProcess.ForEach(node =>
      { nodeToREACHinVectorMap[node] = CreateBitVector((ulong)slotBitSet.Count); });
      nodesToProcess.ForEach(node =>
      { nodeToREACHoutVectorMap[node] = CreateBitVector((ulong)slotBitSet.Count); });
    }

    private void InitVectors()
    {
      foreach (long variable in vars)
      {
        IBitVector defsVector = CreateBitVector((ulong)slotBitSet.Count);
        varToDEFSVectorMap[variable] = defsVector;

        foreach (long def in rdaContext.VarToDefsMap[variable])
        {
          if (slotBitSet.Contains(def))
          {
            defsVector.SetItem((ulong)RelativeIndex(def), 1);
          }
        }
      }

      IBitVectorOperations bitVectorOperations = configuration.Get<IBitVectorOperations>();

      foreach (long nodeId in varNodes)
      {
        if (nodeId == rdaContext.CFG.GetSinkNodeId())
        {
          continue;
        }

        List<long> defs = rdaContext.NodeToDefsSet[nodeId];

        IBitVector genVector = nodeToGENVectorMap[nodeId];
        IBitVector killVector = nodeToKILLVectorMap[nodeId];

        foreach (long def in defs)
        {
          if (slotBitSet.Contains(def))
          {
            genVector.SetItem((ulong)RelativeIndex(def), 1);

            long variable = rdaContext.DefToVarMap[def];
            IBitVector defsVector = varToDEFSVectorMap[variable];
            killVector = bitVectorOperations.BitwiseOr(killVector, defsVector);
          }
        }

        nodeToKILLVectorMap[nodeId] = killVector;
      }
    }

    private void ClearVectors()
    {
      varToDEFSVectorMap.Clear();

      nodeToGENVectorMap.Clear();
      nodeToKILLVectorMap.Clear();
      nodeToREACHinVectorMap.Clear();
      nodeToREACHoutVectorMap.Clear();
    }

    private bool ComputeVectors(DAGNode v)
    {
      long vNodeId = v.Id;

      if (!nodesToProcess.Contains(vNodeId))
      {
        return false;
      }

      IBitVector vReachInVector = nodeToREACHinVectorMap[vNodeId];
      IBitVector vGenVector = nodeToGENVectorMap[vNodeId];
      IBitVector vKillVector = nodeToKILLVectorMap[vNodeId];

      foreach (DAGEdge e in v.InEdges)
      {
        DAGNode u = e.FromNode;
        long uNodeId = u.Id;

        if (!nodesToProcess.Contains(uNodeId))
        {
          continue;
        }

        IBitVector uReachOutVector = nodeToREACHoutVectorMap[uNodeId];
        vReachInVector.BitwiseOr(uReachOutVector);
      }

      IBitVector newVReachOutVector = CreateBitVector(vReachInVector);
      newVReachOutVector.BitwiseSubtract(vKillVector);
      newVReachOutVector.BitwiseOr(vGenVector);

      nodeToREACHoutVectorMap[vNodeId] = newVReachOutVector;

      return !newVReachOutVector.IsEmpty();
    }

    private void ComputeDefUsePairSet()
    {
      foreach (long useNodeId in nodesToProcess)
      {
        IBitVector inVector = nodeToREACHinVectorMap[useNodeId];
        IEnumerable<ulong> inDefRelList = inVector.GetBit1List();

        ICollection<long> useList = AppHelper.TakeValueByKey(
          rdaContext.Usages, useNodeId, () => new SortedSet<long>());

        foreach (long inDefRel in inDefRelList)
        {
          long inDef = AbsoluteIndex(inDefRel);
          if (!slotBitSet.Contains(inDef))
          {
            continue;
          }

          long variable = rdaContext.DefToVarMap[inDef];

          if (useList.Contains(variable))
          {
            long defNodeId = rdaContext.DefToNodeMap[inDef];

            DefUsePair defUsePair = new DefUsePair
              (
                variable,
                defNodeId,
                useNodeId
              );

            lock (defUsePairSet)
            {
              defUsePairSet[variable].Add(defUsePair);
            }
          }
        }
      }
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
