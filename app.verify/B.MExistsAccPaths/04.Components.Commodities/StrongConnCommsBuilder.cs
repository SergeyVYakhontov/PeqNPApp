////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////
// just an optimization for LP: a tconsistent path contains connected commodities only
////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public class StrongConnCommsBuilder : TapeSegContextBase
  {
    #region Ctors

    public StrongConnCommsBuilder(MEAPContext meapContext, TapeSegContext tapeSegContext)
      :base(meapContext, tapeSegContext) {}

    #endregion

    #region public members


    public void Run()
    {
      log.Debug("Strong connected commodities:");

      tapeSegContext.KiToZetaToKjIntSet = new SortedDictionary<long, SortedDictionary<long, SortedSet<long>>>();
      tapeSegContext.KjToZetaToKiIntSet = new SortedDictionary<long, SortedDictionary<long, SortedSet<long>>>();
      tapeSegContext.StrongConnCommodityPairs = new SortedSet<KeyValuePair<long, long>>(new KeyValueComparer());

      foreach (KeyValuePair<long, SortedSet<long>> zetaCommoditiesSetPair_i in tapeSegContext.KSetZetaSubset)
      {
        long zeta_i = zetaCommoditiesSetPair_i.Key;
        SortedSet<long> zetaCommoditiesSet_i = zetaCommoditiesSetPair_i.Value;

        foreach (long commodityId_i in zetaCommoditiesSet_i)
        {
          foreach (KeyValuePair<long, SortedSet<long>> zetaCommoditiesSetPair_j in tapeSegContext.KSetZetaSubset)
          {
            long zeta_j = zetaCommoditiesSetPair_j.Key;
            SortedSet<long> zetaCommoditiesSet_j = zetaCommoditiesSetPair_j.Value;

            if (zeta_j == zeta_i)
            {
              continue;
            }

            foreach (long commodityId_j in zetaCommoditiesSet_j)
            {
              if (commodityId_j == commodityId_i)
              {
                continue;
              }

              if (IsStrongConnectedPair(
                meapContext.Commodities[commodityId_i],
                meapContext.Commodities[commodityId_j]))
              {
                {
                  SortedDictionary<long, SortedSet<long>> commConnIndexesSet =
                    AppHelper.TakeValueByKey(
                      tapeSegContext.KiToZetaToKjIntSet,
                      commodityId_i,
                      () => new SortedDictionary<long, SortedSet<long>>());

                  SortedSet<long> zetaConnIndexesSet =
                    AppHelper.TakeValueByKey(
                      commConnIndexesSet,
                      zeta_j,
                      () => new SortedSet<long>());

                  zetaConnIndexesSet.Add(commodityId_j);
                }

                {
                  SortedDictionary<long, SortedSet<long>> KjToKiIntSet =
                    AppHelper.TakeValueByKey(
                      tapeSegContext.KjToZetaToKiIntSet,
                      commodityId_j,
                      () => new SortedDictionary<long, SortedSet<long>>());

                  AppHelper.TakeValueByKey(
                    KjToKiIntSet,
                    zeta_i,
                    () => new SortedSet<long>())
                  .Add(commodityId_i);
                }

                tapeSegContext.StrongConnCommodityPairs.Add(
                  new KeyValuePair<long, long>(
                    commodityId_i,
                    commodityId_j));

                log.DebugFormat(
                  "{0} {1} {2}",
                  commodityId_i,
                  zeta_j,
                  commodityId_j);
              }
            }
          }
        }
      }
    }

    #endregion

    #region private members

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private static bool IsStrongConnectedPair(
      Commodity commodity_i,
      Commodity commodity_j)
    {
      DAG commodity_graph_i = commodity_i.Gi;
      DAG commodity_graph_j = commodity_j.Gi;

      long s_nodeId_i = commodity_graph_i.GetSourceNodeId();
      long t_nodeId_i = commodity_graph_i.GetSinkNodeId();
      long s_nodeId_j = commodity_graph_j.GetSourceNodeId();
      long t_nodeId_j = commodity_graph_j.GetSinkNodeId();

      return (commodity_graph_i.HasNode(s_nodeId_j) &&
              !commodity_graph_i.IsSinkNode(s_nodeId_j) &&
              (commodity_graph_i.HasNode(t_nodeId_j) ||
               commodity_graph_j.HasNode(t_nodeId_i)));
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
