////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using EnsureThat;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public class ReachDefAnalysisContext
  {
    #region Ctors

    public ReachDefAnalysisContext(
      DAG CFG,
      IEnumerable<long> Vars,
      IDictionary<long, ICollection<long>> Assignments,
      IDictionary<long, ICollection<long>> Usages)
    {
      this.CFG = CFG;
      this.Vars = Vars;
      this.Assignments = Assignments;
      this.Usages = Usages;
    }

    #endregion

    #region public members

    public DAG CFG { get; }

    public IEnumerable<long> Vars { get; }
    public IDictionary<long, ICollection<long>> Assignments { get; }
    public IDictionary<long, ICollection<long>> Usages { get; }

    public ulong DefsCount { get; private set; }
    public ulong UsagesCount { get; private set; }

    public SortedDictionary<long, long> DefToVarMap { get; } =
      new SortedDictionary<long, long>();
    public SortedDictionary<long, List<long>> VarToDefsMap { get; } =
      new SortedDictionary<long, List<long>>();
    public SortedDictionary<long, long> NodeToVarMap { get; } =
      new SortedDictionary<long, long>();

    public SortedDictionary<long, List<long>> NodeToDefsSet { get; } =
      new SortedDictionary<long, List<long>>();

    public SortedDictionary<long, List<long>> NodeToUsesSet { get; } =
      new SortedDictionary<long, List<long>>();

    public SortedDictionary<long, long> DefToNodeMap { get; } =
      new SortedDictionary<long, long>();
    public SortedDictionary<long, long> UseToNodeMap { get; } =
      new SortedDictionary<long, long>();

    public void EnumerateDefsAndUsages()
    {
      EnumerateDefs();
      EnumerateUsages();
    }

    #endregion

    #region private members

    private void EnumerateDefs()
    {
      foreach (KeyValuePair<long, ICollection<long>> nodeVariablesPair in Assignments)
      {
        long uNodeId = nodeVariablesPair.Key;

        foreach (long variable in nodeVariablesPair.Value)
        {
          long def = (long)(DefsCount++);
          DefToVarMap[def] = variable;

          if (!CFG.IsSourceNode(uNodeId))
          {
            Ensure.That(NodeToVarMap.ContainsKey(uNodeId)).IsFalse();
            NodeToVarMap[uNodeId] = variable;
          }

          AppHelper.TakeValueByKey(
              VarToDefsMap,
              variable,
              () => new List<long>())
            .Add(def);

          AppHelper.TakeValueByKey(
              NodeToDefsSet,
              uNodeId,
              () => new List<long>())
            .Add(def);

          DefToNodeMap[def] = uNodeId;
        }
      }
    }

    private void EnumerateUsages()
    {
      foreach (KeyValuePair<long, ICollection<long>> nodeVariablesPair in Usages)
      {
        long uNodeId = nodeVariablesPair.Key;

        foreach (long variable in nodeVariablesPair.Value)
        {
          long usage = (long)(UsagesCount++);

          AppHelper.TakeValueByKey(
              NodeToUsesSet,
              uNodeId,
              () => new List<long>())
            .Add(usage);

          if (!CFG.IsSinkNode(uNodeId))
          {
            Ensure.That(NodeToVarMap.ContainsKey(uNodeId)).IsTrue();
          }

          UseToNodeMap[usage] = uNodeId;
        }
      }
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
