////////////////////////////////////////////////////////////////////////////////////////////////////

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
  public class TConsistPairSetBuilderFactComm : ITracable
  {
    #region Ctors

    public TConsistPairSetBuilderFactComm(MEAPContext meapContext)
    {
      this.meapContext = meapContext;
    }

    #endregion

    #region public members

    public string Name { get; }

    public void Init()
    {
      log.Info("Build TConsist pair set");

      CreateVariableSets();

      rdaContext = new ReachDefAnalysisContext
        (
          meapContext.TArbSeqCFG,
          meapContext.Vars,
          meapContext.Assignments,
          meapContext.Usages
        );

      rdaContext.EnumerateDefsAndUsages();
      meapContext.NodeToVarMap = new SortedDictionary<long, long>(rdaContext.NodeToVarMap);
    }

    public void Run(
      SortedDictionary<long, SortedSet<long>> varToVarNodes,
      long currentLevel)
    {
      defUsePairSet = RunReachDefAnalysis(varToVarNodes, currentLevel);

      SortedDictionary<long, TASGNodeInfo> idToInfoMap = meapContext.TArbSeqCFG.IdToNodeInfoMap;
      meapContext.TConsistPairSet = new SortedSet<CompStepNodePair>(
        new CompStepNodePairComparer());

      foreach (KeyValuePair<long, SortedSet<DefUsePair>> varToDefUsePair in defUsePairSet)
      {
        long variable = varToDefUsePair.Key;

        foreach (DefUsePair defUsePair in varToDefUsePair.Value)
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
              new CompStepNodePair(
                variable: defUsePair.Variable,
                uNode: defUsePair.DefNode,
                vNode: defUsePair.UseNode
              ));

            meapContext.TConsistPairCount++;
          }

          meapContext.DUPairCount++;
        }
      }

      log.InfoFormat(
        "TConsistPairSet: {0}",
        meapContext.TConsistPairSet.Count);

      Trace();
    }

    public void Trace()
    {
      log.Info("DefUsePairSet:");
      SortedDictionary<long, TASGNodeInfo> idToInfoMap = meapContext.TArbSeqCFG.IdToNodeInfoMap;

      long i = 0;
      defUsePairSet.ForEach(v => v.Value.ForEach(
        p => log.DebugFormat(
          "Edge {0}: {1}, {2}, {3}",
          i++,
          p.ToString(),
          idToInfoMap[p.DefNode].CompStep.ToString(),
          idToInfoMap[p.UseNode].CompStep.ToString())));

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

    private static readonly IKernel configuration = Core.AppContext.Configuration;
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private readonly MEAPContext meapContext;
    private ReachDefAnalysisContext rdaContext;
    private SortedDictionary<long, SortedSet<DefUsePair>> defUsePairSet;

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

        if ((!meapContext.TArbSeqCFG.IsSourceNode(node.Id)) &&
            (!meapContext.TArbSeqCFG.IsSinkNode(node.Id)))
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
      long L, R;

      L = meapContext.TASGBuilder.TapeLBound;
      R = meapContext.TASGBuilder.TapeRBound;

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

    private SortedDictionary<long, SortedSet<DefUsePair>> RunReachDefAnalysis(
      SortedDictionary<long, SortedSet<long>> varToVarNodes,
      long currentLevel)
    {
      ReachDefAnalysisSlotsMThreads reachDefAnalysis = new ReachDefAnalysisSlotsMThreads("RDA", rdaContext);
      reachDefAnalysis.Run(varToVarNodes, meapContext.NodeVLevels, currentLevel);

      return reachDefAnalysis.DefUsePairSet;
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
