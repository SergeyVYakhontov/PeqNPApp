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
  public class MEAPCurrentStepOrd : MEAPCurrentStep
  {
    #region Ctors

    public MEAPCurrentStepOrd(MEAPContext meapContext)
      : base(meapContext)
    {
      this.configuration = Core.AppContext.GetConfiguration();
    }

    #endregion

    #region public members

    public override void Run(uint[] states)
    {
      log.InfoFormat("mu: {0}", meapContext.mu);

      ITASGBuilder tasgBuilder = configuration.Get<ITASGBuilder>();

      meapContext.TASGBuilder = tasgBuilder;
      tasgBuilder.meapContext = meapContext;

      tasgBuilder.Init();
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

      meapContext.DUPairCount = 0;
      meapContext.TConsistPairCount = 0;

      TConsistPairSetBuilderOrd tConsistPairSetBuilder = new(meapContext);
      tConsistPairSetBuilder.Run();

      log.InfoFormat("defUsePairSet: {0}", meapContext.DUPairCount);
      log.InfoFormat("TConsistPairSet: {0}", meapContext.TConsistPairCount);

      meapContext.CommoditiesBuilder = new CommoditiesBuilderOrd(meapContext);
      meapContext.CommoditiesBuilder.EnumeratePairs();

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

      log.DebugFormat("states = {0}", AppHelper.ArrayToString(states));

      DetermineIfExistsTCPath determineIfExistsTCSeq = new(meapContext);
      determineIfExistsTCSeq.RunForMultipleTapeSegs();

      CopyResultFromTapeSegContext();
    }

    #endregion

    #region private members

    private readonly IReadOnlyKernel configuration;

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType);

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
