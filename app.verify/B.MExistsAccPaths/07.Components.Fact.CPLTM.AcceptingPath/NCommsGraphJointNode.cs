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

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
