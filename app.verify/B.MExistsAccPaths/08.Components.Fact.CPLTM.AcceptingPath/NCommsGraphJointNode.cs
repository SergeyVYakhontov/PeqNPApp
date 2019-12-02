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

    public List<long> InCommodityNodes { get; } = new List<long>();
    public List<long> OutCommodityNodes { get; } = new List<long>();

    public void AddInCommodityNode(long nodeId)
    {
      InCommodityNodes.Add(nodeId);
    }

    public void AddOutCommodityNode(long nodeId)
    {
      OutCommodityNodes.Add(nodeId);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
