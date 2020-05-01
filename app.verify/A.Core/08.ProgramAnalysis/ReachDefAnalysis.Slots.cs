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
  public class ReachDefAnalysisSlots : ITracable
  {
    #region Ctors

    public ReachDefAnalysisSlots(
      string name,
      ReachDefAnalysisContext rdaContext,
      SortedDictionary<long, SortedSet<long>> nodeVLevels)
    {
      this.Name = name;

      this.rdaContext = rdaContext;
      this.nodeVLevels = nodeVLevels;
    }

    #endregion

    #region public members

    public string Name { get; }
    public List<DefUsePair> DefUsePairSet { get; private set; }

    public void Run()
    {
      log.Info("Run RDA analysis");

      ICommonOptions commonOptions = configuration.Get<ICommonOptions>();
      slotMaxSize = (long)commonOptions.RDASlotMaxSize;

      slot = new LongSegment(
        0,
        Math.Min((long)rdaContext.DefsCount, slotMaxSize) - 1);
      DefUsePairSet = new List<DefUsePair>();

      while (true)
      {
        log.InfoFormat("Slot {0} {1}", slot.Left, slot.Right);

        CreateVectors();
        InitVectors();

        DAG.BFS_VLevels(
          rdaContext.CFG,
          GraphDirection.Forward,
          nodeVLevels,
          DAG.Level0,
          ComputeVectors,
          (_) => true);

        ComputeDefUsePairSet();
        ClearVectors();

        if (slot.Right == (long)(rdaContext.DefsCount - 1))
        {
          break;
        }

        long r = Math.Min((long)rdaContext.DefsCount - 1, slot.Right + slotMaxSize);

        slot = new LongSegment(slot.Right + 1, r);
      }

      nodeVLevels.Clear();
      Trace();
    }

    public void Trace()
    {
      log.DebugFormat("RDA: {0}", Name);

      log.DebugFormat("NodesCount = {0}", rdaContext.CFG.Nodes.Count);
      log.DebugFormat("EdgesCount = {0}", rdaContext.CFG.Edges.Count);

      log.Debug("nodeToGENVectorMap");
      nodeToGENVectorMap.Values.ForEach(r => log.Debug(r.ToString()));

      log.Debug("nodeToKILLVectorMap");
      nodeToKILLVectorMap.Values.ForEach(r => log.Debug(r.ToString()));

      log.Debug("nodeToREACHinVectorMap");
      nodeToREACHinVectorMap.Values.ForEach(r => log.Debug(r.ToString()));

      log.Debug("nodeToREACHoutVectorMap");
      nodeToREACHoutVectorMap.Values.ForEach(r => log.Debug(r.ToString()));
    }

    #endregion

    #region private members

    private static readonly IKernel configuration = Core.AppContext.Configuration;
    private static readonly log4net.ILog log =
      log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private readonly ReachDefAnalysisContext rdaContext;
    private readonly SortedDictionary<long, SortedSet<long>> nodeVLevels;

    private long slotMaxSize;
    private LongSegment slot;

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

    private long RelativeIndex(long def)
    {
      return (def - (slot.Left));
    }

    private long AbsoluteIndex(long def)
    {
      return ((slot.Left) + def);
    }

    private void CreateVectors()
    {
      List<DAGNode> nodes = rdaContext.CFG.Nodes;

      nodes.ForEach(node => nodeToGENVectorMap[node.Id] = CreateBitVector(slot.Count));
      nodes.ForEach(node => nodeToKILLVectorMap[node.Id] = CreateBitVector(slot.Count));
      nodes.ForEach(node => nodeToREACHinVectorMap[node.Id] = CreateBitVector(slot.Count));
      nodes.ForEach(node => nodeToREACHoutVectorMap[node.Id] = CreateBitVector(slot.Count));
    }

    private void InitVectors()
    {
      foreach (long variable in rdaContext.Vars)
      {
        IBitVector defsVector = CreateBitVector((ulong)slot.Count);
        varToDEFSVectorMap[variable] = defsVector;

        foreach (long def in rdaContext.VarToDefsMap[variable])
        {
          if (slot.Contains(def))
          {
            defsVector.SetItem((ulong)RelativeIndex(def), 1);
          }
        }
      }

      IBitVectorOperations bitVectorOperations = configuration.Get<IBitVectorOperations>();

      foreach (KeyValuePair<long, List<long>> nodeDefsPair in rdaContext.NodeToDefsSet)
      {
        long nodeId = nodeDefsPair.Key;
        List<long> defs = nodeDefsPair.Value;

        IBitVector genVector = nodeToGENVectorMap[nodeId];
        IBitVector killVector = nodeToKILLVectorMap[nodeId];

        foreach (long def in defs)
        {
          if (slot.Contains(def))
          {
            genVector.SetItem((ulong)RelativeIndex(def), 1);
          }

          long var = rdaContext.DefToVarMap[def];
          IBitVector defsVector = varToDEFSVectorMap[var];

          killVector = bitVectorOperations.BitwiseOr(killVector, defsVector);
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
      bool flowChanged = false;

      IBitVector vReachInVector = nodeToREACHinVectorMap[vNodeId];
      IBitVector vGenVector = nodeToGENVectorMap[vNodeId];
      IBitVector vKillVector = nodeToKILLVectorMap[vNodeId];

      foreach (DAGEdge e in v.InEdges)
      {
        DAGNode u = e.FromNode;
        long uNodeId = u.Id;

        IBitVector uReachOutVector = nodeToREACHoutVectorMap[uNodeId];
        vReachInVector.BitwiseOr(uReachOutVector);

        flowChanged = true;
      }

      if (!flowChanged && v.InEdges.Any() && vGenVector.IsEmpty())
      {
        return false;
      }

      IBitVector newVReachOutVector = CreateBitVector(vReachInVector);
      newVReachOutVector.BitwiseSubtract(vKillVector);
      newVReachOutVector.BitwiseOr(vGenVector);

      nodeToREACHoutVectorMap[vNodeId] = newVReachOutVector;

      return !newVReachOutVector.IsEmpty();
    }

    private void ComputeDefUsePairSet()
    {
      SortedDictionary<long, DAGNode> nodeEnumeration = rdaContext.CFG.NodeEnumeration;
      foreach (KeyValuePair<long, DAGNode> nodeNumberPair in nodeEnumeration)
      {
        long useNodeId = nodeNumberPair.Key;

        IBitVector inVector = nodeToREACHinVectorMap[useNodeId];
        IEnumerable<ulong> inDefRelList = inVector.GetBit1List();

        foreach (ulong inDefRel in inDefRelList)
        {
          long inDef = AbsoluteIndex((long)inDefRel);

          long variable = rdaContext.DefToVarMap[inDef];
          long defNodeId = rdaContext.DefToNodeMap[inDef];

          ICollection<long> useList = AppHelper.TakeValueByKey(
            rdaContext.Usages, useNodeId, () => new SortedSet<long>());

          if (useList.Contains(variable))
          {
            DefUsePair defUsePair = new DefUsePair
              (
                variable,
                defNodeId,
                useNodeId
              );

            DefUsePairSet.Add(defUsePair);
          }
        }
      }
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
