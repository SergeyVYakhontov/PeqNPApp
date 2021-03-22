////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using Ninject;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public class CommoditiesSelector
  {
    #region Ctors

    public CommoditiesSelector(
      MEAPContext meapContext,
      TapeSegContext tapeSegContext)
    {
      this.meapContext = meapContext;
      this.tapeSegContext = tapeSegContext;
    }

    #endregion

    #region public members

    public Commodity[] Run(long count)
    {
      List<Commodity> commoditiesToCheck = new();

      while (true)
      {
        if (commoditiesToCheck.Count == count)
        {
          return commoditiesToCheck.ToArray();
        }

        Commodity commodity =
          SelectCommodity(tapeSegContext.KSetZetaSubset, commoditiesToCheck) ??
          SelectAnyCommodity(tapeSegContext.KSetZetaSubset);

        if (commodity == null)
        {
          return commoditiesToCheck.ToArray();
        }

        alreadySelected.Add(commodity.Id);
        commoditiesToCheck.Add(commodity);
      }
    }

    #endregion

    #region private members

    private readonly MEAPContext meapContext = default!;
    private readonly TapeSegContext tapeSegContext = default!;

    private readonly SortedSet<long> alreadySelected = new();

    private Commodity SelectCommodity(
      SortedDictionary<long, SortedSet<long>> kSetZetaSubset,
      List<Commodity> selectedComms)
    {
      foreach (KeyValuePair<long, SortedSet<long>> idCommList in kSetZetaSubset)
      {
        GraphTConZetaBuilder gtczBuilder = new(meapContext);
        TypedDAG<GTCZNodeInfo, StdEdgeInfo> gtcz = gtczBuilder.Run(idCommList.Value);

        foreach (long commodityId in idCommList.Value)
        {
          if (alreadySelected.Contains(commodityId))
          {
            continue;
          }

          Commodity commodity = meapContext.Commodities[commodityId];

          long sNodeId = commodity.Gi.GetSourceNodeId();
          long tNodeId = commodity.Gi.GetSinkNodeId();

          DAGNode gtcz_u = gtcz.NodeEnumeration[sNodeId];
          DAGNode gtcz_v = gtcz.NodeEnumeration[tNodeId];

          if (meapContext.TArbSeqCFG.IsSourceNode(sNodeId))
          {
            return commodity;
          }

          if (meapContext.TArbSeqCFG.IsSinkNode(tNodeId))
          {
            return commodity;
          }

          if (gtcz_u.OutEdges.Count == 1)
          {
            return commodity;
          }

          if (gtcz_v.InEdges.Count == 1)
          {
            return commodity;
          }

          IList<Commodity> notNullCommodities = selectedComms.Where(c => c != null).ToList();

          bool sConnected1 = notNullCommodities.Any(c => c.Gi.IsSourceNode(sNodeId));
          bool tConnected1 = notNullCommodities.Any(c => c.Gi.IsSinkNode(tNodeId));
          bool sConnected2 = notNullCommodities.Any(c => c.Gi.IsSourceNode(sNodeId));
          bool tConnected2 = notNullCommodities.Any(c => c.Gi.IsSinkNode(tNodeId));

          if (!sConnected1 && !tConnected1 && !sConnected2 && !tConnected2)
          {
            return commodity;
          }
        }
      }

      return default!;
    }

    private Commodity SelectAnyCommodity(
      SortedDictionary<long, SortedSet<long>> kSetZetaSubset)
    {
      foreach (KeyValuePair<long, SortedSet<long>> idCommList in kSetZetaSubset)
      {
        foreach (long commodityId in idCommList.Value)
        {
          if (alreadySelected.Contains(commodityId))
          {
            continue;
          }

          Commodity commodity = meapContext.Commodities[commodityId];

          return commodity;
        }
      }

      return default!;
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
