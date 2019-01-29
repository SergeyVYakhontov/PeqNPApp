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
  public class NCommsGraphJointNode
  {
    #region public members

    public List<KeyValuePair<long, long>> InCommodityPairs { get; } = new List<KeyValuePair<long, long>>();
    public List<KeyValuePair<long, long>> OutCommodityPairs { get; } = new List<KeyValuePair<long, long>>();

    public void AddInCommodityPair(long node1, long node2)
    {
      KeyValuePair<long, long> inPair = new KeyValuePair<long, long>(node1, node2);

      InCommodityPairs.Add(inPair);
      commPairToIdMap[inPair] = pairId++;
    }

    public void AddOutCommodityPair(long node1, long node2)
    {
      KeyValuePair<long, long> outPair = new KeyValuePair<long, long>(node1, node2);

      OutCommodityPairs.Add(outPair);
      commPairToIdMap[outPair] = pairId++;
    }

    #endregion

    #region public members

    private long pairId;
    private readonly SortedDictionary<KeyValuePair<long, long>, long> commPairToIdMap =
      new SortedDictionary<KeyValuePair<long, long>, long>();

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
