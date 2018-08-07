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
  public abstract class OneTapeTuringMachine : ITracable
  {
    #region Ctors

    protected OneTapeTuringMachine(string name)
    {
      this.Name = name;
    }

    #endregion

    #region public memers

    public string Name { get; }

    public uint[] Q { get; protected set; }
    public int[] Gamma { get; protected set; }
    public const int blankSymbol = -1;
    public int[] Sigma { get; protected set; }
    public Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> Delta
      { get; protected set; }
    public uint qStart { get; protected set; }
    public uint[] F { get; protected set; }

    public bool Accepted { get; private set; }

    public uint[] QAny => Q.Except(new uint[] { qStart }).ToArray();

    public abstract void Setup();

    public abstract bool UP { get; }
    public abstract bool FewP { get; }
    public abstract bool LotOfAcceptingPaths { get; }
    public abstract bool AcceptingPathAlwaysExists { get; }
    public abstract bool AllPathsFinite { get; }

    public virtual long GetLTapeBound(ulong mu, ulong n) => 1 - (long)mu;
    public virtual long GetRTapeBound(ulong mu, ulong n) => 1 + (long)mu;
    public virtual ulong ExpectedPathLength(ulong n) => n;

    public const long InstancesCountLimit = 1024 * 1024;

    public bool ComputationFinished { get; private set; }

    public virtual void PrepareTapeFwd(int[] input, TMInstance instance) { }

    public void Run(int[] input)
    {
      TMInstance instance = new TMInstance(this, input);
      PrepareTapeFwd(input, instance);

      instances.Enqueue(instance);
      instance.Trace();

      while (instances.Any())
      {
        DoStep();

        if ((instancesMaxCount > InstancesCountLimit) ||
            (instancesTotalCount > InstancesCountLimit))
        {
          ComputationFinished = false;

          log.Info("Instances count limit exceeded");
          Trace();

          return;
        }

        if (Accepted && !AllPathsFinite)
        {
          ComputationFinished = true;

          break;
        }
      }

      ComputationFinished = true;

      if (UP)
      {
        Ensure.That(instancesAcceptingCount <= 1, "Unique path TM").IsTrue();
      }

      Trace();
    }

    public virtual int[] GetAcceptingInstanceOutput(int[] input)
    {
      return acceptingInstance.GetOutput();
    }

    public virtual int[] GetOutput(TMInstance tmInstance, ulong mu, ulong n)
    {
      return tmInstance.GetOutput();
    }

    public void Trace()
    {
      long deltaCount = Delta.Sum(r => r.Value.Count);

      log.InfoFormat("deltaCount = {0}", deltaCount);
      log.InfoFormat("instancesMaxCount = {0}", instancesMaxCount);
      log.InfoFormat("instancesTotalCount = {0}", instancesTotalCount);
      log.InfoFormat("instancesAcceptingCount = {0}", instancesAcceptingCount);
      log.InfoFormat("instancesLeafCount = {0}", instancesLeafCount);
      log.InfoFormat("instancesMaxLevel = {0}", instancesMaxLevel);
    }

    #endregion

    #region private members

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private readonly Queue<TMInstance> instances = new Queue<TMInstance>();

    private BigInteger instancesMaxCount;
    private BigInteger instancesTotalCount;
    private BigInteger instancesLeafCount;
    private BigInteger instancesAcceptingCount;
    private BigInteger instancesMaxLevel;

    private void DoStep()
    {
      long instancesCount = instances.Count;
      instancesMaxCount = BigInteger.Max(instancesMaxCount, new BigInteger(instancesCount));
      instancesTotalCount++;

      TMInstance currentInstance = instances.Dequeue();
      instancesMaxLevel = BigInteger.Max(instancesMaxLevel,
        new BigInteger(currentInstance.Level));

      if (currentInstance.IsInFinalState())
      {
        instancesLeafCount++;
        instancesAcceptingCount++;

        Accepted = true;

        if (acceptingInstance == null)
        {
          acceptingInstance = currentInstance;
        }

        return;
      }

      List<TMInstance> newInstances = currentInstance.DoStep();

      if (!newInstances.Any())
      {
        instancesLeafCount++;

        return;
      }

      newInstances.ForEach(i => instances.Enqueue(i));
    }

    #endregion

    #region private members

    protected TMInstance acceptingInstance;

    protected virtual void CheckDeltaRelation()
    {
      Delta.ForEach(p =>
      {
        Ensure.That(Gamma.Contains(p.Key.Symbol)).IsTrue();
        Ensure.That(Q.Contains(p.Key.State)).IsTrue();

        p.Value.ForEach(v =>
        {
          Ensure.That(Gamma.Contains(v.Symbol)).IsTrue();
          Ensure.That(Q.Contains((uint)v.State)).IsTrue();
        }
        );
      });

      SortedSet<StateSymbolDirectionTriple> triples = new SortedSet<StateSymbolDirectionTriple>(
        new StateSymbolDirectionTripleComparer());

      Delta.ForEach(p =>
      {
        Ensure.That(Gamma.Contains(p.Key.Symbol)).IsTrue();
        Ensure.That(Q.Contains(p.Key.State)).IsTrue();

        p.Value.ForEach(e => triples.Add(e));

        Ensure.That(triples.Count == p.Value.Count).IsTrue().WithException(
          (_) => new InvalidOperationException("TM Delta duplicate elements"));

        triples.Clear();
      });
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
