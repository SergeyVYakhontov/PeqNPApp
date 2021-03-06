﻿////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Ninject;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public class TConsistPairSetBuilderOrd : ITracable
  {
    #region Ctors

    public TConsistPairSetBuilderOrd(MEAPContext meapContext)
    {
      this.configuration = Core.AppContext.GetConfiguration();

      this.meapContext = meapContext;
    }

    #endregion

    #region public members

    public string Name { get; } = string.Empty;

    public void Run()
    {
      log.Info("Build TConsist pair set");

      CreateVariableSets();
      defUsePairSet = RunReachDefAnalysis();

      meapContext.TConsistPairSet = new SortedSet<CompStepNodePair>(
        new CompStepNodePairComparer());
      SortedDictionary<long, DAGNode> nodeEnumeration = meapContext.TArbSeqCFG.NodeEnumeration;
      SortedDictionary<long, TASGNodeInfo> idToInfoMap = meapContext.TArbSeqCFG.IdToNodeInfoMap;

      foreach (DefUsePair defUsePair in defUsePairSet)
      {
        long defNodeId = defUsePair.DefNode;
        long useNodeId = defUsePair.UseNode;

        ComputationStep defCompStep = idToInfoMap[defNodeId].CompStep;
        ComputationStep useCompStep = idToInfoMap[useNodeId].CompStep;

        bool consistent = false;

        if (meapContext.TArbSeqCFG.IsSinkNode(useNodeId))
        {
          consistent = true;
        }
        else if (meapContext.TArbSeqCFG.IsSourceNode(defNodeId))
        {
          consistent = (useCompStep.s ==
            meapContext.MEAPSharedContext.InitInstance.TapeSymbol(
              (int)useCompStep.kappaTape));
        }
        else
        {
          consistent = (defCompStep.sNext == useCompStep.s);
        }

        if (consistent)
        {
          meapContext.TConsistPairSet.Add(
            new CompStepNodePair
            (
              variable: defUsePair.Variable,
              uNode: defUsePair.DefNode,
              vNode: defUsePair.UseNode
            ));

          meapContext.TConsistPairCount++;
        }

        meapContext.DUPairCount++;
      }

      Trace();
    }

    public void Trace()
    {
      log.Debug("DefUsePairSet:");

      SortedDictionary<long, TASGNodeInfo> idToInfoMap = meapContext.TArbSeqCFG.IdToNodeInfoMap;

      long i = 0;

      defUsePairSet.ForEach(
        p => log.DebugFormat(
          "Edge {0}: {1}, {2}, {3}",
          i++,
          p.ToString(),
          idToInfoMap[p.DefNode].CompStep.ToString(),
          idToInfoMap[p.UseNode].CompStep.ToString()));

      log.Debug("ConsistPairSet:");

      i = 0;

      meapContext.TConsistPairSet.ForEach(
        p => log.DebugFormat(
          "Edge {0}: {1}, {2}, {3}",
          i++,
          p.ToString(),
          idToInfoMap[p.uNode].CompStep.ToString(),
          idToInfoMap[p.vNode].CompStep.ToString()));
    }

    #endregion

    #region private members

    private readonly IReadOnlyKernel configuration;

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType);

    private readonly MEAPContext meapContext = default!;
    private List<DefUsePair> defUsePairSet = new();

    private void CreateVariableSets()
    {
      meapContext.Vars = new SortedSet<long>();
      meapContext.Assignments = new SortedDictionary<long, ICollection<long>>();
      meapContext.Usages = new SortedDictionary<long, ICollection<long>>();

      foreach (KeyValuePair<long, DAGNode> p in meapContext.TArbSeqCFG.NodeEnumeration)
      {
        long nodeId = p.Key;
        DAGNode node = p.Value;

        ComputationStep compStep = meapContext.TArbSeqCFG.IdToNodeInfoMap[nodeId].CompStep;
        long variable = compStep.kappaTape;

        if ((!meapContext.TArbSeqCFG.IsSourceNode(node.Id)) && (!meapContext.TArbSeqCFG.IsSinkNode(node.Id)))
        {
          meapContext.Vars.Add(variable);

          meapContext.Assignments[nodeId] = new SortedSet<long> { variable };
          meapContext.Usages[nodeId] = new SortedSet<long> { variable };
        }
        else
        {
          meapContext.Assignments[nodeId] = new SortedSet<long>();
          meapContext.Usages[nodeId] = new SortedSet<long>();
        }
      }

      ICommonOptions commonOptions = configuration.Get<ICommonOptions>();

      long L = meapContext.TASGBuilder.TapeLBound;
      long R = meapContext.TASGBuilder.TapeRBound;

      for (long variable = L; variable <= R; variable++)
      {
        long sNodeId = meapContext.TArbSeqCFG.GetSourceNodeId();
        long tNodeId = meapContext.TArbSeqCFG.GetSinkNodeId();

        ICollection<long> sNodeAssignments = AppHelper.TakeValueByKey(meapContext.Assignments, sNodeId, () => new SortedSet<long>());
        ICollection<long> sNodeUsages = AppHelper.TakeValueByKey(meapContext.Usages, tNodeId, () => new SortedSet<long>());

        meapContext.Vars.Add(variable);

        meapContext.Assignments[sNodeId].Add(variable);
        meapContext.Usages[tNodeId].Add(variable);
      }
    }

    private List<DefUsePair> RunReachDefAnalysis()
    {
      ReachDefAnalysisContext rdaContext = new
        (
          meapContext.TArbSeqCFG,
          meapContext.Vars,
          meapContext.Assignments,
          meapContext.Usages
        );

      ReachDefAnalysis reachDefAnalysis = new("RDA", rdaContext);

      rdaContext.EnumerateDefsAndUsages();
      reachDefAnalysis.Run();

      return reachDefAnalysis.DefUsePairSet;
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
