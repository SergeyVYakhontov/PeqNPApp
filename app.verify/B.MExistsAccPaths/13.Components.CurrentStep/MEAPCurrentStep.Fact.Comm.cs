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
  public class MEAPCurrentStepFactComm : MEAPCurrentStep
  {
    #region Ctors

    public MEAPCurrentStepFactComm(MEAPContext meapContext)
      : base(meapContext)
    {
      this.configuration = Core.AppContext.GetConfiguration();
    }

    #endregion

    #region public members

    public override void Run(uint[] states)
    {
      log.InfoFormat("mu: {0}", meapContext.mu);

      CreateTASGBuilder();

      tasgBuilder.CreateTArbitrarySeqGraph();
      tasgBuilder.CreateTArbSeqCFG(states);
      meapContext.TArbSeqCFG.Trace();

      if (meapContext.TArbSeqCFG.IsTrivial())
      {
        return;
      }

      ComputeNodeVLevels(meapContext.TArbSeqCFG);

      ICommonOptions commonOptions = configuration.Get<ICommonOptions>();
      ICheckDataStructures checkDataStructures = configuration.Get<ICheckDataStructures>();

      if (commonOptions.CheckDataStructures)
      {
        checkDataStructures.CheckTASGHasNoBackAndCrossEdges(meapContext.TArbSeqCFG);
      }

      log.InfoFormat("states = {0}", AppHelper.ArrayToString(states));

      IDebugOptions debugOptions = configuration.Get<IDebugOptions>();

      if (!debugOptions.RunRDA)
      {
        return;
      }

      ComputeCommodities();

      DetermineIfExistsTCPath determineIfExistsTCSeq = new(meapContext);
      determineIfExistsTCSeq.RunForMultipleTapeSegs();

      CopyResultFromTapeSegContext();
    }

    #endregion

    #region private members

    private readonly IReadOnlyKernel configuration;

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType);

    private ITASGBuilder tasgBuilder = default!;
    private TConsistPairSetBuilderFactComm tConsistPairSetBuilder = default!;
    private SortedDictionary<long, SortedSet<long>> inVarToVarNodes = new();

    private void CreateTASGBuilder()
    {
      tasgBuilder = meapContext.MEAPSharedContext.TASGBuilder;

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

    private void ComputeCommodities()
    {
      meapContext.DUPairCount = 0;
      meapContext.TConsistPairCount = 0;

      tConsistPairSetBuilder = new TConsistPairSetBuilderFactComm(meapContext);
      tConsistPairSetBuilder.Init();

      meapContext.CommoditiesBuilder = new CommoditiesBuilderFactComm(meapContext);
      inVarToVarNodes = new SortedDictionary<long, SortedSet<long>>();

      ProcessNode_s();

      DAG.BFS_VLevels(
        meapContext.TArbSeqCFG,
        GraphDirection.Forward,
        meapContext.NodeVLevels,
        DAG.Level0,
        ProcessNode,
        (_) => true);

      tConsistPairSetBuilder.Run(inVarToVarNodes, DAG.Level0);
      meapContext.CommoditiesBuilder.EnumeratePairs();
      inVarToVarNodes.Clear();

      log.InfoFormat("defUsePairSet: {0}", meapContext.DUPairCount);
      log.InfoFormat("TConsistPairSet: {0}", meapContext.TConsistPairCount);

      IDebugOptions debugOptions = configuration.Get<IDebugOptions>();

      if (debugOptions.ComputeCommoditiesExplicitely)
      {
        meapContext.Commodities = meapContext.CommoditiesBuilder.CreateCommodities();
        meapContext.CommoditiesBuilder.CreateCommodityGraphs();
      }
      else
      {
        meapContext.Commodities = new SortedDictionary<long, Commodity>();
      }
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
