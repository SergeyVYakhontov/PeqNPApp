////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ninject;
using EnsureThat;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public class NodeLevelInfo
  {
    #region public members

    public SortedDictionary<long, SortedSet<long>> NodeVLevels { get; } = new SortedDictionary<long, SortedSet<long>>();
    public SortedDictionary<long, long> NodeToLevel { get; } = new SortedDictionary<long, long>();
    public SortedDictionary<long, long> EdgeToLevel { get; } = new SortedDictionary<long, long>();

    public void AddNodeAtLevel(long nodeId, long level)
    {
      AppHelper.TakeValueByKey(
        NodeVLevels,
        level,
        () => new SortedSet<long>()).Add(nodeId);

      NodeToLevel[nodeId] = level;
    }

    public void AddEdgeAtLevel(long edgeId, long level)
    {
      EdgeToLevel[edgeId] = level;
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
