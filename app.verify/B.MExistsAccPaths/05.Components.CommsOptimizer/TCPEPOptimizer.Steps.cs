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
  public class TCPEPOptimizer : TapeSegContextBase
  {
    #region Ctors

    public TCPEPOptimizer(MEAPContext meapContext, TapeSegContext tapeSegContext)
      : base(meapContext, tapeSegContext)
    {
      this.configuration = Core.AppContext.GetConfiguration();
    }

    #endregion

    #region public members

    public bool TConsistPathFound { get; set; }
    public bool ThereIsNoTConsistPath { get; set; }
    public bool Finished { get; set; }

    public void Init()
    {
      log.Info("TCPEP optimizer: init");

      log.Info("TCPEP optimizer: KSetZetaSubset");
      commoditiesSubset = new SortedDictionary<long, Commodity>();
      tapeSegContext.KSetZetaSubset.Select(p => p.Value).ToList()
        .ForEach(e => e.ForEach(c => commoditiesSubset[c] =
          meapContext.Commodities[c]));

      totalCommoditiesCount = commoditiesSubset.Count;

      log.Info("TCPEP optimizer: nodes coverage");

      nodesCoverageKeeper = new NodesCoverageKeeper(
        meapContext,
        tapeSegContext,
        commoditiesSubset,
        totalExcludedComms);

      notUsedCommsDetector = new NotUsedCommsDetector(
        meapContext,
        tapeSegContext,
        commoditiesSubset,
        totalExcludedComms,
        nodesCoverageKeeper);

      commoditiesSelector = new CommoditiesSelector(meapContext, tapeSegContext);

      nodesCoverageKeeper.ComputeCoverage();
    }

    public void Step1()
    {
      log.Info("TCPEP optimizer: step1");

      ReduceCommoditiesSet("Step1");
    }

    public void Step2()
    {
      log.Info("TCPEP optimizer: step2");

      ITPLOptions tplOptions = configuration.Get<ITPLOptions>();
      uint linEqSetRunnersCount = tplOptions.LinEqSetRunnersCount;

      Commodity[] commoditiesToCheck = commoditiesSelector.Run(linEqSetRunnersCount);

      if (commoditiesToCheck.Length == 0)
      {
        Finished = true;

        return;
      }

      SortedSet<long> tConsistPathComms = new SortedSet<long>();
      SortedSet<long> tInconsistPathComms = new SortedSet<long>();
      List<CommodityChecker> commCheckers = new List<CommodityChecker>();

      ILinEqsAlgorithmProvider linEqsAlgorithmProvider = configuration.Get<ILinEqsAlgorithmProvider>();

      for (long i = 0; i < commoditiesToCheck.Length; i++)
      {
        Commodity commodity = commoditiesToCheck[i];

        commCheckers.Add(
          linEqsAlgorithmProvider.GetTCPEPCommChecker(
            meapContext,
            tapeSegContext,
            commodity,
            tConsistPathComms,
            tInconsistPathComms));
      }

      TPLCollectionRunner<CommodityChecker> commodityCheckerRunner =
        new TPLCollectionRunner<CommodityChecker>(
          commCheckers,
          linEqSetRunnersCount,
          WaitMethod.WaitAll,
          _ => null);

      commodityCheckerRunner.Run();

      log.DebugFormat(
        "Gauss elimination process, excluded: {0}",
        tInconsistPathComms.Count);

      SortedSet<long> excludedComms = new SortedSet<long>();
      tInconsistPathComms.ForEach(
        c =>
        {
          nodesCoverageKeeper.RemoveCommodity(c, excludedComms);
          log.DebugFormat("Removed tinconsist commodity {0}", c);
        });

      ReduceCommoditiesSet("Step2");
    }

    #endregion

    #region private members

    private readonly IReadOnlyKernel configuration;

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private SortedDictionary<long, Commodity> commoditiesSubset;
    private readonly SortedSet<long> totalExcludedComms = new SortedSet<long>();
    private long totalCommoditiesCount;

    private NodesCoverageKeeper nodesCoverageKeeper;
    private NotUsedCommsDetector notUsedCommsDetector;
    private CommoditiesSelector commoditiesSelector;

    private bool IsKZetaGraphComplete()
    {
      return (tapeSegContext.KSetZetaSubset.Count(v => v.Value.Any()) ==
        tapeSegContext.KSetZetaSubset.Count);
    }

    private bool IsTArbSeqCFGConnected()
    {
      bool isTArbSeqCFGConnected = DAG.IsConnected(
        meapContext.TArbSeqCFG, u => !tapeSegContext.TArbSeqCFGUnusedNodes.Contains(u.Id));

      return isTArbSeqCFGConnected;
    }

    private bool TryToFindPath(String stepName)
    {
      CommTConsistPathFinder commTConsistPathFinder = new CommTConsistPathFinder(meapContext, tapeSegContext, null);
      commTConsistPathFinder.FindTConsistPath();

      if (tapeSegContext.TapeSegPathFound)
      {
        log.DebugFormat("CommTConsistPathFinder, {0}: path found", stepName);
        commTConsistPathFinder.ExtractTConsistSeq();
        TConsistPathFound = true;

        return true;
      }

      return false;
    }

    private void CheckIfPathExists(string stepName)
    {
      if (!IsKZetaGraphComplete())
      {
        log.Info("KZeta graph is incomplete");
        ThereIsNoTConsistPath = true;
        return;
      }

      if (!IsTArbSeqCFGConnected())
      {
        log.Info("TArbSeqCFG not connected");
        ThereIsNoTConsistPath = true;
        return;
      }

      if (TryToFindPath(stepName))
      {
        log.Info("step1: path found");
      }
    }

    private void ReduceCommoditiesSet(String stepName)
    {
      SortedSet<long> excludedComms = new SortedSet<long>();
      while (notUsedCommsDetector.RemoveUnusedCommodities1(excludedComms)) { }

      long totalExcludedCommsCount = totalExcludedComms.Count;

      if (totalExcludedCommsCount > 0)
      {
        log.InfoFormat("excludedComms.count = {0} / {1}", totalExcludedCommsCount, totalCommoditiesCount);
      }

      CheckIfPathExists(stepName);

      if (ThereIsNoTConsistPath || TConsistPathFound)
      {
        return;
      }

      while (notUsedCommsDetector.RemoveUnusedCommodities2(excludedComms) ||
        notUsedCommsDetector.RemoveUnusedCommodities1(excludedComms)) { }

      totalExcludedCommsCount = totalExcludedComms.Count;

      if (totalExcludedCommsCount > 0)
      {
        log.InfoFormat("excludedComms.count = {0} / {1}", totalExcludedCommsCount, totalCommoditiesCount);
      }

      long usedCommsCount = tapeSegContext.KSetZetaSubset.Sum(p => p.Value.Count);

      if (usedCommsCount > 0)
      {
        log.InfoFormat("used commodities = {0}", usedCommsCount);
      }

      CheckIfPathExists(stepName);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
