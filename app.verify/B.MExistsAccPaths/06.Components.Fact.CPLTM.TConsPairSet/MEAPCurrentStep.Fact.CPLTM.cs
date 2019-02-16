////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ninject;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public class MEAPCurrentStepFactCPLTM : MEAPCurrentStep
  {
    #region Ctors

    public MEAPCurrentStepFactCPLTM(MEAPContext meapContext)
      : base(meapContext) {}

    #endregion

    #region public members

    public override void Run(uint[] states)
    {
      log.InfoFormat("mu: {0}", meapContext.mu);
      log.DebugFormat("states = {0}", AppHelper.ArrayToString(states));

      CreateTASGBuilder();

      tasgBuilder.CreateTArbitrarySeqGraph();
      IDebugOptions debugOptions = configuration.Get<IDebugOptions>();

      if (!debugOptions.RunRDA)
      {
        return;
      }

      uint maxMu = meapContext.MEAPSharedContext.CPLTMInfo.PathLength;

      if (meapContext.mu < maxMu)
      {
        return;
      }

      tasgBuilder.CreateTArbSeqCFG(states);

      if (meapContext.TArbSeqCFG.IsTrivial())
      {
        return;
      }

      ICommonOptions commonOptions = configuration.Get<ICommonOptions>();

      if (commonOptions.CheckDataStructures)
      {
        CheckDataStructures.CheckTASGHasNoBackAndCrossEdges(meapContext.TArbSeqCFG);
      }

      ComputeDUPairs();

      meapContext.CommoditiesBuilder = new CommoditiesBuilderFactCPLTM(meapContext);
      meapContext.CommoditiesBuilder.EnumeratePairs();
      meapContext.Commodities = meapContext.CommoditiesBuilder.CreateCommodities();
      meapContext.CommoditiesBuilder = null;

      meapContext.UnusedNodes = new SortedSet<long>();

      NestedCommsGraphBuilder nestedCommsGraphBuilder = new NestedCommsGraphBuilder(meapContext);
      nestedCommsGraphBuilder.Setup();
      nestedCommsGraphBuilder.Run();

      NCGJointNodesBuilder ncgJointNodesBuilder = new NCGJointNodesBuilder(meapContext);
      ncgJointNodesBuilder.Setup();
      ncgJointNodesBuilder.Run();

      PathFinderFactCPLTM pathFinder = new PathFinderFactCPLTM(meapContext);
      pathFinder.Run();
    }

    #endregion

    #region private members

    private static readonly IKernel configuration = Core.AppContext.Configuration;
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private TASGBuilderFactCPLTM tasgBuilder;
    private SortedDictionary<long, SortedSet<long>> inVarToVarNodes;

    private void CreateTASGBuilder()
    {
      tasgBuilder = (TASGBuilderFactCPLTM)meapContext.MEAPSharedContext.TASGBuilder;

      meapContext.TASGBuilder = tasgBuilder;
      tasgBuilder.meapContext = meapContext;
    }

    private void ProcessVars_s(long[] varsList)
    {
      long sNodeId = meapContext.TArbSeqCFG.GetSourceNodeId();

      for (long i = 0; i < varsList.Length; i++)
      {
        long var = varsList[i];
        inVarToVarNodes[var] = new SortedSet<long> { sNodeId };
      }
    }

    private void ProcessNode_s()
    {
      ProcessVars_s(meapContext.Vars.ToArray());
    }

    private bool ProcessNode(DAGNode node)
    {
      long nodeId = node.Id;

      if (nodeId == meapContext.TArbSeqCFG.GetSourceNodeId())
      {
        return true;
      }

      if (nodeId == meapContext.TArbSeqCFG.GetSinkNodeId())
      {
        return true;
      }

      long nodeVar = meapContext.NodeToVarMap[nodeId];
      SortedSet<long> varNodes = AppHelper.TakeValueByKey(
        inVarToVarNodes, nodeVar, () => new SortedSet<long>());

      varNodes.Add(nodeId);

      return true;
    }

    private void ComputeDUPairs()
    {
      TConsistPairSetBuilderFactCPLTM tConsistPairSetBuilder = new TConsistPairSetBuilderFactCPLTM(meapContext);
      tConsistPairSetBuilder.Setup();

      inVarToVarNodes = new SortedDictionary<long, SortedSet<long>>();
      ProcessNode_s();

      DAG.BFS_VLevels(
        meapContext.TArbSeqCFG,
        GraphDirection.Forward,
        meapContext.MEAPSharedContext.NodeLevelInfo.NodeVLevels,
        DAG.Level0,
        ProcessNode,
        (_) => true);

      tConsistPairSetBuilder.Run(inVarToVarNodes, DAG.Level0);
      tConsistPairSetBuilder.Trace();

      inVarToVarNodes.Clear();
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
