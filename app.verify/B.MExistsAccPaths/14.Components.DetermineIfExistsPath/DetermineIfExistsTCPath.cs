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
  public class DetermineIfExistsTCPath
  {
    #region Ctors

    public DetermineIfExistsTCPath(MEAPContext meapContext)
    {
      this.configuration = Core.AppContext.GetConfiguration();

      this.meapContext = meapContext;
    }

    #endregion

    #region public members

    public void RunForMultipleTapeSegs()
    {
      log.Info("Run DetermineIfExistsTCSeq");

      meapContext.PathExists = false;
      meapContext.PathFound = false;
      meapContext.TConsistPath = new List<long>();
      meapContext.Output = Array.Empty<int>();

      if (!meapContext.Commodities.Any())
      {
        log.Info("DetermineIfExistsTCSeq: false");

        return;
      }

      CreateKZetaSets();
      CreateTapeSegContextsList();

      List<TapeSegRunnerState> tapeSegRunnerAllowedStates = new()
        {
          TapeSegRunnerState.CheckKZetaGraphs,
          TapeSegRunnerState.ReduceCommodities,
          TapeSegRunnerState.RunGaussElimination,
          TapeSegRunnerState.RunLinearProgram,
          TapeSegRunnerState.Done
        };

      TapeSegLoop(tapeSegRunnerAllowedStates);

      log.Info("DetermineIfExistsTCSeq");
      log.InfoFormat(
        "PathExists = {0}",
        meapContext.TapeSegContext.TapeSegPathExists);
    }

    public void RunToRetrievePath()
    {
      meapContext.PathExists = false;
      meapContext.PathFound = false;
      meapContext.TConsistPath = new List<long>();
      meapContext.Output = Array.Empty<int>();

      if (!meapContext.Commodities.Any())
      {
        log.Info("DetermineIfExistsTCSeq: false");

        return;
      }

      tapeSegContexts = new List<TapeSegContext>
        {
          meapContext.TapeSegContext
        };

      List<TapeSegRunnerState> tapeSegRunnerAllowedStates = new()
        {
          TapeSegRunnerState.CheckKZetaGraphs,
          TapeSegRunnerState.ReduceCommodities,
          TapeSegRunnerState.RunLinearProgram,
          TapeSegRunnerState.Done
        };

      TapeSegLoop(tapeSegRunnerAllowedStates);

      log.Info("DetermineIfExistsTCSeq");
      log.InfoFormat(
        "PathExists = {0}",
        meapContext.TapeSegContext.TapeSegPathExists);
    }

    #endregion

    #region private members

    private readonly IReadOnlyKernel configuration;

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType);

    private readonly MEAPContext meapContext = default!;
    private List<TapeSegContext> tapeSegContexts = new();

    private void CreateKZetaSets()
    {
      meapContext.KSetZetaSets = new SortedDictionary<long, List<Commodity>>();

      foreach (KeyValuePair<long, Commodity> p in meapContext.Commodities)
      {
        long variable = p.Value.Variable;

        AppHelper.TakeValueByKey(meapContext.KSetZetaSets, variable,
          () => new List<Commodity>()).Add(p.Value);
      }
    }

    private void CreateTapeSegContextsList()
    {
      ICommonOptions commonOptions = configuration.Get<ICommonOptions>();

      long from = meapContext.TASGBuilder.TapeLBound;
      long to = meapContext.TASGBuilder.TapeRBound;

      log.InfoFormat("L R {0} {1}", from, to);

      tapeSegContexts = new List<TapeSegContext>();

      for (long L = from; L <= to; L++)
      {
        for (long R = L; R <= to; R++)
        {
          LongSegment tapeSeg = new(L, R);

          if (!tapeSeg.Contains(1))
          {
            continue;
          }

          TapeSegContext tapeSegContext = new();

          tapeSegContext.TapeSeg = tapeSeg;
          tapeSegContext.PartialTConsistPath = new List<long> { meapContext.TArbSeqCFG.GetSourceNodeId() };
          tapeSegContext.TConsistPathNodes = new List<long> { meapContext.TArbSeqCFG.GetSourceNodeId() };

          tapeSegContexts.Add(tapeSegContext);
        }
      }
    }

    private TapeSegRunner ProcessTapeSegRunners(TapeSegRunner[] tapeSegRunners)
    {
      for (long i = 0; i < tapeSegRunners.Length; i++)
      {
        TapeSegContext tapeSegContext = tapeSegRunners[i].TapeSegContext;

        if (tapeSegContext.TapeSegPathExists)
        {
          meapContext.TapeSegContext = tapeSegContext;

          return tapeSegRunners[i];
        }
      }

      return default!;
    }

    private void TapeSegLoop(List<TapeSegRunnerState> tapeSegRunnerAllowedStates)
    {
      List<TapeSegRunner> tapeSegRunnersList = new();
      long id = 0;

      foreach (TapeSegContext tapeSegContext in tapeSegContexts)
      {
        TapeSegRunner tapeSegRunner = new
          (
            id++,
            meapContext,
            tapeSegContext,
            tapeSegRunnerAllowedStates
          );

        tapeSegRunner.Init();
        tapeSegRunnersList.Add(tapeSegRunner);
      }

      ITPLOptions tplOptions = configuration.Get<ITPLOptions>();
      uint tapeSegRunnersCount = tplOptions.TapeSegRunnersCount;

      TPLCollectionRunnerWithQueue<TapeSegRunner> tapeSegCollectionRunner = new
        (
          tapeSegRunnersList,
          tapeSegRunnersCount,
          WaitMethod.WaitAll,
          ProcessTapeSegRunners,
          new TapeSegRunnerComparer()
        );

      tapeSegCollectionRunner.Run();
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
