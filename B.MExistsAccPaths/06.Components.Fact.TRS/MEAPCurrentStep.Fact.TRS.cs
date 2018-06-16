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
  public class MEAPCurrentStepFactTRS : MEAPCurrentStep
  {
    #region Ctors

    public MEAPCurrentStepFactTRS(MEAPContext meapContext)
      : base(meapContext) {}

    #endregion

    #region public members

    public override void Run(int[] states)
    {
      log.InfoFormat("mu: {0}", meapContext.mu);

      CreateTASGBuilder();

      tasgBuilder.CreateTArbitrarySeqGraph();
      tasgBuilder.CreateTArbSeqCFG(states);

      CopyResultFromTapeSegContext();

      if (meapContext.TArbSeqCFG.IsTrivial())
      {
        return;
      }

      ComputeNodeVLevels(meapContext.TArbSeqCFG);
      ICommonOptions commonOptions = configuration.Get<ICommonOptions>();

      if (commonOptions.CheckDataStructures)
      {
        CheckDataStructures.CheckTASGHasNoBackAndCrossEdges(
          meapContext, meapContext.TArbSeqCFG);
      }

      log.DebugFormat("states = {0}", AppHelper.ArrayToString(states));

      IDebugOptions debugOptions = configuration.Get<IDebugOptions>();

      if (!debugOptions.RunRDA)
      {
        return;
      }

      ComputeDUPairs();

      meapContext.CommoditiesBuilder = new CommoditiesBuilderFactTRS(meapContext);
      meapContext.CommoditiesBuilder.EnumeratePairs();
      meapContext.Commodities = meapContext.CommoditiesBuilder.CreateCommodities();
      meapContext.CommoditiesBuilder = null;

      EliminateOverlappingCommsFactTRS eliminateOverlapping = new EliminateOverlappingCommsFactTRS(meapContext);
      eliminateOverlapping.Run();
      eliminateOverlapping = null;

      meapContext.UnusedNodes = new SortedSet<long>();

      NestedCommsGraphBuilderFactTRS nestedCommsGraphBuilder = new NestedCommsGraphBuilderFactTRS(meapContext);
      nestedCommsGraphBuilder.Setup();
      nestedCommsGraphBuilder.Run();
      nestedCommsGraphBuilder.Trace();
      nestedCommsGraphBuilder = null;

      PathFinderFactTRS pathFinder = new PathFinderFactTRS(meapContext);
      pathFinder.Run();
      pathFinder = null;
    }

    #endregion

    #region private members

    private static readonly IKernel configuration = Core.AppContext.Configuration;
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private TASGBuilderFactTRS tasgBuilder;
    private SortedDictionary<long, SortedSet<long>> inVarToVarNodes;

    private void CreateTASGBuilder()
    {
      tasgBuilder = (TASGBuilderFactTRS)meapContext.MEAPSharedContext.TASGBuilder;

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
      TConsistPairSetBuilderFactTRS tConsistPairSetBuilder = new TConsistPairSetBuilderFactTRS(meapContext);
      tConsistPairSetBuilder.Setup();

      inVarToVarNodes = new SortedDictionary<long, SortedSet<long>>();
      ProcessNode_s();

      DAG.BFS_VLevels(
        meapContext.TArbSeqCFG,
        meapContext.TArbSeqCFG.s,
        GraphDirection.Forward,
        meapContext.NodeVLevels,
        DAG.Level0,
        ProcessNode,
        (level) => { return true; });

      tConsistPairSetBuilder.Run(inVarToVarNodes, DAG.Level0);
      tConsistPairSetBuilder.Trace();

      inVarToVarNodes.Clear();
      tConsistPairSetBuilder = null;
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
