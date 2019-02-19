////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ninject;
using Core;
using EnsureThat;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public class ComputeKStepDUPairs
  {
    #region Ctors

    public ComputeKStepDUPairs(
      MEAPContext meapContext,
      Tuple<long, long, long> kSteps)
    {
      this.meapContext = meapContext;
      this.kSteps = kSteps;
      this.CPLTMInfo = meapContext.MEAPSharedContext.CPLTMInfo;
      this.nodeVLevels = meapContext.MEAPSharedContext.NodeLevelInfo.NodeVLevels;
    }

    #endregion

    #region public members

    public void Run()
    {
      long kStepA = kSteps.Item2;

      foreach (long nodeId in nodeVLevels[kStepA])
      {
        duPairs.Add(new KeyValuePair<long, long>(nodeId, nodeId));
      }
    }

    #endregion

    #region private members

    private readonly MEAPContext meapContext;
    private readonly Tuple<long, long, long> kSteps;
    private readonly ICPLTMInfo CPLTMInfo;
    private readonly SortedDictionary<long, SortedSet<long>> nodeVLevels;

    private readonly SortedSet<KeyValuePair<long, long>> duPairs =
      new SortedSet<KeyValuePair<long, long>>(new KeyValueComparer());

    #endregion
  }
}
