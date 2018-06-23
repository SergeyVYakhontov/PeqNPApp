////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using EnsureThat;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public static class CheckDataStructures
  {
    #region public members

    public static void CheckTASGHasNoBackAndCrossEdges(MEAPContext meapContext, DAG dag)
    {
      log.Info("CheckTASGHasNoBackAndCrossEdges");
      SortedDictionary<long, SortedSet<long>> VLevelSets =
        new SortedDictionary<long,SortedSet<long>>();

      DAG.DFS(
        dag,
        dag.s,
        GraphDirection.Forward,
        (u, level) =>
        {
          AppHelper.TakeValueByKey(VLevelSets, level,
            () => new SortedSet<long>()).Add(u.Id);
        },
        (e, level) => { }
        );

      DAG.ClassifyDAGEdges(
        dag,
        VLevelSets,
        out SortedSet<long> backEdges,
        out SortedSet<long> crossEdges);

      Ensure.That(backEdges.Any()).IsFalse();
      Ensure.That(crossEdges.Any()).IsFalse();
    }

    public static void CheckCommoditiesHaveNoSingleNodes(
      MEAPContext meapContext)
    {
      foreach(Commodity commodity in meapContext.Commodities.Values)
      {
        Ensure.That(IsGraphHasSingleNode(commodity.Gi)).IsFalse();
      }
    }

    #endregion

    #region private members

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private static bool IsGraphHasSingleNode(DAG dag)
    {
      return dag.GetInnerNodeIds().Any(uNodeId =>
      {
        DAGNode uNode = dag.NodeEnumeration[uNodeId];

        return ((!uNode.InEdges.Any()) || (!uNode.OutEdges.Any()));
      });
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
