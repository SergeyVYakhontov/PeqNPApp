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
  public class ReachDefAnalysisSlotsMThreads
  {
    #region Ctors

    public ReachDefAnalysisSlotsMThreads(string name, ReachDefAnalysisContext rdaContext)
    {
      this.configuration = Core.AppContext.GetConfiguration();

      this.Name = name;
      this.rdaContext = rdaContext;
    }

    #endregion

    #region public members

    public string Name { get; } = string.Empty;
    public SortedDictionary<long, SortedSet<DefUsePair>>
      DefUsePairSet { get; private set; } = new();

    public void Run(
      SortedDictionary<long, SortedSet<long>> varToVarNodes,
      SortedDictionary<long, SortedSet<long>> nodeVLevels,
      long currentLevel)
    {
      log.Info("Run RDA analysis");
      log.InfoFormat("varToVarNodes: {0}", varToVarNodes.Count);

      DefUsePairSet = new SortedDictionary<long, SortedSet<DefUsePair>>();
      List<RDARunnerSlotsMThreads> rdaRunnersList = new();

      if (!varToVarNodes.Any())
      {
        return;
      }

      long runnerId = 0;
      long varToVarNodesProcessed = 0;
      long slotNo = 0;

      List<KeyValuePair<long, long>> varToVarNodesPairsList = new();

      varToVarNodes.ForEach(
        t1 => t1.Value.ForEach(
          t2 => varToVarNodesPairsList.Add(new KeyValuePair<long, long>(t1.Key, t2))));

      KeyValuePair<long, long>[] varToVarNodesPairs = varToVarNodesPairsList.ToArray();

      while (true)
      {
        SortedSet<long> varsSet = new();
        SortedSet<long> varNodesSet = new();

        if ((varToVarNodesPairs.Length - varToVarNodesProcessed) == 0)
        {
          break;
        }

        ICommonOptions commonOptions = configuration.Get<ICommonOptions>();
        long slotMaxSize = (long)commonOptions.RDASlotMaxSize;

        long varsToProcess = Math.Min(
          varToVarNodesPairs.Length - varToVarNodesProcessed,
          slotMaxSize);

        long firstVar = varToVarNodesPairs[varToVarNodesProcessed + varsToProcess - 1].Key;
        long nextVarIndex = 0;

        for (long i = varToVarNodesProcessed + varsToProcess; i < varToVarNodesPairs.Length; i++)
        {
          if (varToVarNodesPairs[i].Key != firstVar)
          {
            break;
          }

          nextVarIndex++;
        }

        varsToProcess += nextVarIndex;

        for (long i = 0; i < varsToProcess; i++)
        {
          long var = varToVarNodesPairs[varToVarNodesProcessed + i].Key;
          long varNode = varToVarNodesPairs[varToVarNodesProcessed + i].Value;

          varsSet.Add(var);
          varNodesSet.Add(varNode);
        }

        varToVarNodesProcessed += varsToProcess;

        log.InfoFormat("Current slot: {0}", slotNo++);
        RDARunnerSlotsMThreads rdaRunner = new(runnerId++);

        varsSet.ForEach(t => DefUsePairSet[t] =
          new SortedSet<DefUsePair>(new DefUsePairComparer()));

        rdaRunner.Init(
          rdaContext,
          varsSet,
          varNodesSet,
          nodeVLevels,
          currentLevel,
          DefUsePairSet);
        rdaRunnersList.Add(rdaRunner);
      }

      ITPLOptions tplOptions = configuration.Get<ITPLOptions>();
      uint slotsMThRDAProcessCount = tplOptions.SlotsMThRDAProcessCount;

      TPLCollectionRunner<RDARunnerSlotsMThreads> rdaSetRunner = new
        (
          rdaRunnersList,
          slotsMThRDAProcessCount,
          WaitMethod.WaitAll,
          _ => default!
        );
      rdaSetRunner.Run();
    }

    #endregion

    #region private members

    private readonly IReadOnlyKernel configuration;

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType);

    private readonly ReachDefAnalysisContext rdaContext;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
