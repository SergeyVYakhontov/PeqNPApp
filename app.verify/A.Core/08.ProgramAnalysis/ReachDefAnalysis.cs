﻿////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Ninject;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public class ReachDefAnalysis : ITracable
  {
    #region Ctors

    public ReachDefAnalysis(string name, ReachDefAnalysisContext rdaContext)
    {
      this.configuration = Core.AppContext.GetConfiguration();

      this.Name = name;
      this.rdaContext = rdaContext;
    }

    #endregion

    #region public members

    public string Name { get; } = string.Empty;
    public List<DefUsePair> DefUsePairSet { get; private set; } = new();

    public void Run()
    {
      log.Info("Run RDA analysis");

      CreateVectors();
      InitVectors();

      DAG.BFS(
        rdaContext.CFG,
        new SortedSet<long> { rdaContext.CFG.GetSourceNodeId() },
        _ => true,
        ComputeVectors);

      ComputeDefUsePairSet();
      ClearVectors();

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

      log.DebugFormat("nodeToREACHinVectorMap");
      nodeToREACHinVectorMap.Values.ForEach(r => log.Debug(r.ToString()));

      log.Debug("nodeToREACHoutVectorMap");
      nodeToREACHoutVectorMap.Values.ForEach(r => log.Debug(r.ToString()));
    }

    #endregion

    #region private members

    private readonly IReadOnlyKernel configuration;

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType);

    private readonly ReachDefAnalysisContext rdaContext;

    private readonly SortedDictionary<long, IBitVector> varToDEFSVectorMap = new();

    private readonly SortedDictionary<long, IBitVector> nodeToGENVectorMap = new();
    private readonly SortedDictionary<long, IBitVector> nodeToKILLVectorMap = new();
    private readonly SortedDictionary<long, IBitVector> nodeToREACHinVectorMap = new();
    private readonly SortedDictionary<long, IBitVector> nodeToREACHoutVectorMap = new();

    private IBitVector CreateBitVector(ulong size)
    {
      IBitVectorProvider bitVectorProvider = configuration.Get<IBitVectorProvider>();

      return bitVectorProvider.CreateVector(size);
    }

    private void CreateVectors()
    {
      List<DAGNode> nodes = rdaContext.CFG.Nodes;

      nodes.ForEach(node =>
      { nodeToGENVectorMap[node.Id] = CreateBitVector(rdaContext.DefsCount); });
      nodes.ForEach(node =>
      { nodeToKILLVectorMap[node.Id] = CreateBitVector(rdaContext.DefsCount); });
      nodes.ForEach(node =>
      { nodeToREACHinVectorMap[node.Id] = CreateBitVector(rdaContext.DefsCount); });
      nodes.ForEach(node =>
      { nodeToREACHoutVectorMap[node.Id] = CreateBitVector(rdaContext.DefsCount); });
    }

    private void InitVectors()
    {
      foreach (long variable in rdaContext.Vars)
      {
        IBitVector defsVector = CreateBitVector(rdaContext.DefsCount);
        varToDEFSVectorMap[variable] = defsVector;

        foreach (long def in rdaContext.VarToDefsMap[variable])
        {
          defsVector.SetItem((ulong)def, 1);
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
          genVector.SetItem((ulong)def, 1);

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

      IBitVector vReachInVector = nodeToREACHinVectorMap[vNodeId];
      IBitVector vReachOutVector = nodeToREACHoutVectorMap[vNodeId];
      IBitVector vGenVector = nodeToGENVectorMap[vNodeId];
      IBitVector vKillVector = nodeToKILLVectorMap[vNodeId];

      IBitVectorOperations bitVectorOperations = configuration.Get<IBitVectorOperations>();

      foreach (DAGEdge e in v.InEdges)
      {
        DAGNode u = e.FromNode;
        long uNodeId = u.Id;

        IBitVector uReachOutVector = nodeToREACHoutVectorMap[uNodeId];
        vReachInVector = bitVectorOperations.BitwiseOr(vReachInVector, uReachOutVector);
      }

      nodeToREACHinVectorMap[vNodeId] = vReachInVector;

      IBitVector newVReachOutVector = bitVectorOperations.BitwiseOr(
        vGenVector,
        bitVectorOperations.BitwiseSubtract(vReachInVector, vKillVector));

      if (!newVReachOutVector.Equals(vReachOutVector))
      {
        nodeToREACHoutVectorMap[vNodeId] = newVReachOutVector;

        return true;
      }
      else
      {
        return false;
      }
    }

    private void ComputeDefUsePairSet()
    {
      DefUsePairSet = new List<DefUsePair>();

      foreach (KeyValuePair<long, DAGNode> nodeNumberPair in rdaContext.CFG.NodeEnumeration)
      {
        long useNodeId = nodeNumberPair.Key;
        IBitVector inVector = nodeToREACHinVectorMap[useNodeId];

        foreach (ulong inDef in inVector.GetBit1List())
        {
          long variable = rdaContext.DefToVarMap[(long)inDef];
          long defNodeId = rdaContext.DefToNodeMap[(long)inDef];

          ICollection<long> useList = AppHelper.TakeValueByKey(
            rdaContext.Usages, useNodeId, () => new SortedSet<long>());

          if (useList.Contains(variable))
          {
            DefUsePair defUsePair = new
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
