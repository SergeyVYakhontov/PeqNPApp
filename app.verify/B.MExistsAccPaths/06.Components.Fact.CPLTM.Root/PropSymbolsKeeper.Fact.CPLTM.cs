////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public class PropSymbolsKeeperFactCPLTM : ITracable
  {
    #region Ctors

    public PropSymbolsKeeperFactCPLTM(MEAPSharedContext MEAPSharedContext)
    {
      this.MEAPSharedContext = MEAPSharedContext;
    }

    #endregion

    #region public members

    public string Name { get; } = string.Empty;

    public void Init(long sNodeId)
    {
      SortedSet<PropSymbol> nodeProcSym = AppHelper.TakeValueByKey(
        propagatedSymbols, sNodeId,
        () => new SortedSet<PropSymbol>(new PropSymbolComparer()));

      uint inputLength = (uint)MEAPSharedContext.Input.Length;

      long L = MEAPSharedContext.MNP.GetLTapeBound(0, inputLength);
      long R = MEAPSharedContext.MNP.GetRTapeBound(0, inputLength);

      for (long i = L; i <= R; i++)
      {
        int tapeSymbol = MEAPSharedContext.InitInstance.TapeSymbol((int)i);

        nodeProcSym.Add(new PropSymbol(i, tapeSymbol));
      }
    }

    public bool IfThereIsFlowFrom(
      DAGNode fromNode,
      ComputationStep fromCompStep,
      ComputationStep toCompStep)
    {
      if ((fromCompStep.m == TMDirection.S) &&
          (fromCompStep.sNext != toCompStep.s))
      {
        return false;
      }

      SortedSet<PropSymbol> procSymPrev = AppHelper.TakeValueByKey(
        propagatedSymbols, fromNode.Id,
        () => new SortedSet<PropSymbol>(new PropSymbolComparer()));

      IEnumerable<PropSymbol> prevPairs = procSymPrev.Where(
        t => (t.Variable == toCompStep.kappaTape));

      IEnumerable<PropSymbol> prevMatchPairs =
        prevPairs.Where(t => t.Symbol == toCompStep.s);

      return prevMatchPairs.Any();
    }

    public void PropagateSymbol(
      DAGNode fromNode,
      DAGNode toNode,
      ComputationStep toCompStep)
    {
      SortedSet<PropSymbol> procSymPrev = AppHelper.TakeValueByKey(
        propagatedSymbols, fromNode.Id,
        () => new SortedSet<PropSymbol>(new PropSymbolComparer()));

      SortedSet<PropSymbol> nodeProcSym = AppHelper.TakeValueByKey(
        propagatedSymbols, toNode.Id,
        () => new SortedSet<PropSymbol>(new PropSymbolComparer()));

      foreach (PropSymbol p in procSymPrev.Where(
        t => t.Variable != toCompStep.kappaTape))
      {
        nodeProcSym.Add(p);
      }

      nodeProcSym.Add(new PropSymbol(
        toCompStep.kappaTape,
        toCompStep.sNext));
    }

    public void RemoveUnusedSymbols(List<long> endNodeIds)
    {
      SortedSet<long> toRemove = new();

      propagatedSymbols
        .Where(t => !endNodeIds.Contains(t.Key))
          .ToList().ForEach(t => toRemove.Add(t.Key));

      toRemove.ForEach(t => propagatedSymbols.Remove(t));
    }

    public void Trace()
    {
      log.InfoFormat(
        "Propagated symbols map size: {0}",
         propagatedSymbols.Count);
    }

    #endregion

    #region private members

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType);

    private readonly MEAPSharedContext MEAPSharedContext;

    private readonly SortedDictionary<long, SortedSet<PropSymbol>> propagatedSymbols = new();

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
