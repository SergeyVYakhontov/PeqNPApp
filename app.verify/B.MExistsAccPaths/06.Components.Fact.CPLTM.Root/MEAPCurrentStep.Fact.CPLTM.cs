﻿////////////////////////////////////////////////////////////////////////////////////////////////////

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
      : base(meapContext)
    {
      this.configuration = Core.AppContext.GetConfiguration();

      this.tasgBuilder = meapContext.MEAPSharedContext.TASGBuilder;
      this.meapContext.TASGBuilder = tasgBuilder;
      this.tasgBuilder.meapContext = meapContext;
      this.CPLTMInfo = meapContext.MEAPSharedContext.CPLTMInfo;
    }

    #endregion

    #region public members

    public override void Run(uint[] states)
    {
      log.InfoFormat("mu: {0}", meapContext.mu);
      log.DebugFormat("states = {0}", AppHelper.ArrayToString(states));

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
      ICheckDataStructures checkDataStructures = configuration.Get<ICheckDataStructures>();

      ComputeDUPairs();

      meapContext.CommoditiesBuilder = new CommoditiesBuilderFactCPLTM(meapContext);
      meapContext.CommoditiesBuilder.EnumeratePairs();
      meapContext.Commodities = meapContext.CommoditiesBuilder.CreateCommodities();
      meapContext.CommoditiesBuilder = default!;

      meapContext.UnusedNodes = new SortedSet<long>();

      NestedCommsGraphBuilder nestedCommsGraphBuilder = new(meapContext);
      nestedCommsGraphBuilder.Setup();
      nestedCommsGraphBuilder.Run();
      nestedCommsGraphBuilder = default!;

      PathFinderFactCPLTM pathFinder = new(meapContext);
      pathFinder.Run();
    }

    #endregion

    #region private members

    private readonly IReadOnlyKernel configuration;

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
    System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType);

    private readonly ITASGBuilder tasgBuilder;
    private readonly ICPLTMInfo CPLTMInfo;

    private void ComputeDUPairs()
    {
      meapContext.TConsistPairCount = 0;
      meapContext.TConsistPairSet = new SortedSet<CompStepNodePair>(
        new CompStepNodePairComparer());

      long[] kTapeLRSubseq = CPLTMInfo.KTapeLRSubseq().ToArray();

      for (int i = 1; i <= (kTapeLRSubseq.Length - 2); i++)
      {
        long kStepA = kTapeLRSubseq[i - 1];
        long kStepB = kTapeLRSubseq[i];
        long kStepC = kTapeLRSubseq[i + 1];

        log.InfoFormat($"Computing DU pairs at kStep = {kStepB}");

        ComputeKStepDUPairs computeKStepDUPairs = new
          (
            meapContext,
            new Tuple<long, long, long>(kStepA, kStepB, kStepC)
          );

        computeKStepDUPairs.Run();
      }
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
