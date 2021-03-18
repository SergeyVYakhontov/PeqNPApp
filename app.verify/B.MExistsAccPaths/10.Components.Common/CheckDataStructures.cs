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
  public class CheckDataStructures : ICheckDataStructures
  {
    #region public members

    public void CheckTASGHasNoBackAndCrossEdges(DAG dag)
    {
      log.Info("CheckTASGHasNoBackAndCrossEdges");

      SortedDictionary<long, SortedSet<long>> VLevelSets = new();

      DAG.DFS(
        dag.s,
        GraphDirection.Forward,
        (u, level) =>
        {
          AppHelper.TakeValueByKey(VLevelSets, level,
            () => new SortedSet<long>()).Add(u.Id);
        },
        (_, __) => { }
        );

      (SortedSet<long> backEdges, SortedSet<long> crossEdges) = DAG.ClassifyDAGEdges(dag, VLevelSets);

      Ensure.That(backEdges.Any()).IsFalse();
      Ensure.That(crossEdges.Any()).IsFalse();
    }

    public void CheckCommoditiesHaveNoSingleNodes(MEAPContext meapContext)
    {
      log.Info("CheckCommoditiesHaveNoSingleNodes");

      foreach (Commodity commodity in meapContext.Commodities.Values)
      {
        Ensure.That(IsGraphHasSingleNode(commodity.Gi)).IsFalse();
      }
    }

    public virtual void CheckTASGNodesHaveTheSameSymbol(MEAPContext meapContext) { }
    public virtual void CheckNCGNodesHaveTheSameSymbol(MEAPContext meapContext) { }

    #endregion

    #region private members

    protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType);

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
