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
  public abstract class CommoditiesBuilder : ITracable
  {
    #region Ctors

    protected CommoditiesBuilder(MEAPContext meapContext)
    {
      this.meapContext = meapContext;

      this.sNodeId = meapContext.TArbSeqCFG.GetSourceNodeId();
      this.tNodeId = meapContext.TArbSeqCFG.GetSinkNodeId();
    }

    #endregion

    #region public members

    public string Name { get; }

    public abstract void EnumeratePairs();
    public abstract SortedDictionary<long, Commodity> CreateCommodities();

    private void ExcludeDefs()
    {
      foreach (KeyValuePair<long, Commodity> p in meapContext.Commodities)
      {
        Commodity commodity = p.Value;
        long commVar = commodity.Variable;

        List<long> toRemove = new List<long>();

        foreach (long uNodeId in commodity.Gi.GetInnerNodeIds())
        {
          ICollection<long> uVars = meapContext.Assignments[uNodeId];

          if (uVars.Contains(commVar))
          {
            toRemove.Add(uNodeId);
          }
        }

        toRemove.ForEach(r => commodity.Gi.RemoveNode(commodity.Gi.NodeEnumeration[r]));

        toRemove.ForEach(r =>
          log.Debug(
            "var " + commVar + " " +
            commodity.Gi.GetSourceNodeId() + ", " +
            commodity.Gi.GetSinkNodeId() +
            " remove: " + r));

        DAG newGi = new DAG("Gi");
        DAG.CutChains(commodity.Gi, newGi);
        commodity.Gi = newGi;

        excludedDefs.AddRange(toRemove);
      }

      CheckDataStructures.CheckCommoditiesHaveNoSingleNodes(meapContext);
    }

    public void CreateCommodityGraphs()
    {
      lock (objectToLock)
      {
        log.DebugFormat(
          "CreateCommodityGraphs: {0}",
          meapContext.Commodities.Count);

        foreach (KeyValuePair<long, Commodity> p in meapContext.Commodities)
        {
          long commodityId = p.Key;
          Commodity commodity = p.Value;

          DAG Gi = new DAG("Gi");
          DAG.MakeCommodity(meapContext.TArbSeqCFG, commodity.sNodeId, commodity.tNodeId, Gi);

          commodity.Gi = Gi;
        }

        ICommonOptions commonOptions = configuration.Get<ICommonOptions>();

        if (commonOptions.CheckDataStructures)
        {
          CheckDataStructures.CheckCommoditiesHaveNoSingleNodes(meapContext);
        }

        ExcludeDefs();

        if (commonOptions.CheckDataStructures)
        {
          CheckDataStructures.CheckCommoditiesHaveNoSingleNodes(meapContext);
        }
      }
    }

    public void Trace()
    {
      log.Debug("Commodities");

      commodities.Values.ForEach(c =>
      {
        log.Debug(
          "Commodity " + c.Id + " " + c.Variable +
          " " + c.sNodeId + " " + c.tNodeId);
      });

      log.InfoFormat(
        "Commodities count: {0}",
        commodities.Count);
    }

    #endregion

    #region private members

    private static readonly IKernel configuration = Core.AppContext.Configuration;
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    private static readonly object objectToLock = new object();

    protected readonly MEAPContext meapContext;

    protected readonly long sNodeId;
    protected readonly long tNodeId;

    protected long pairCount;
    protected readonly SortedDictionary<long, CompStepNodePair> compStepNodePairEnum =
      new SortedDictionary<long, CompStepNodePair>();

    protected readonly SortedDictionary<long, Commodity> commodities =
      new SortedDictionary<long, Commodity>();
    protected readonly List<long> excludedDefs = new List<long>();

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
