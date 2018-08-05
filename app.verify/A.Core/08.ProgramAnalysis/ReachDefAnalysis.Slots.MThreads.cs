////////////////////////////////////////////////////////////////////////////////////////////////////

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
      this.Name = name;
      this.rdaContext = rdaContext;
    }

    #endregion

    #region public members

    public string Name { get; }
    public SortedDictionary<long, SortedSet<DefUsePair>> DefUsePairSet { get; private set; }

    public void Run(
      SortedDictionary<long, SortedSet<long>> varToVarNodes,
      SortedDictionary<long, SortedSet<long>> nodeVLevels,
      long currentLevel)
    {
      log.Info("Run RDA analysis");
      log.InfoFormat("varToVarNodes: {0}", varToVarNodes.Count);

      DefUsePairSet = new SortedDictionary<long, SortedSet<DefUsePair>>();
      List<RDARunnerSlotsMThreads> rdaRunnersList = new List<RDARunnerSlotsMThreads>();

      if (!varToVarNodes.Any())
      {
        return;
      }

      long runnerId = 0;
      long varToVarNodesProcessed = 0;
      long slotNo = 0;

      List<KeyValuePair<long, long>> varToVarNodesPairsList =
        new List<KeyValuePair<long, long>>();

      varToVarNodes.ForEach(
        t1 => t1.Value.ForEach(
          t2 => varToVarNodesPairsList.Add(new KeyValuePair<long, long>(t1.Key, t2))));

      KeyValuePair<long, long>[] varToVarNodesPairs = varToVarNodesPairsList.ToArray();

      while (true)
      {
        SortedSet<long> varsSet = new SortedSet<long>();
        SortedSet<long> varNodesSet = new SortedSet<long>();

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
        RDARunnerSlotsMThreads rdaRunner = new RDARunnerSlotsMThreads(runnerId++);

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

      TPLCollectionRunner<RDARunnerSlotsMThreads> rdaSetRunner =
        new TPLCollectionRunner<RDARunnerSlotsMThreads>(
          rdaRunnersList,
          slotsMThRDAProcessCount,
          WaitMethod.WaitAll,
          _ => null);
      rdaSetRunner.Run();
    }

    #endregion

    #region private members

    private static readonly IKernel configuration = Core.AppContext.Configuration;
    private static readonly log4net.ILog log =
      log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private readonly ReachDefAnalysisContext rdaContext;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
