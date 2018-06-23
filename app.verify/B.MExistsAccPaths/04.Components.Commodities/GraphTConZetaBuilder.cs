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
  public class GraphTConZetaBuilder
  {
    #region Ctors

    public GraphTConZetaBuilder(MEAPContext meapContext)
    {
      this.meapContext = meapContext;
    }

    #endregion

    #region public members

    public TypedDAG<GTCZNodeInfo, StdEdgeInfo> Run(SortedSet<long> commodities)
    {
      TypedDAG<GTCZNodeInfo, StdEdgeInfo> gtcz = new TypedDAG<GTCZNodeInfo, StdEdgeInfo>("GTCZ");
      SortedDictionary<long, DAGNode> nodeEnumeration = new SortedDictionary<long, DAGNode>();
      long edgeId = 0;

      foreach (long commodityId in commodities)
      {
        Commodity commodity = meapContext.Commodities[commodityId];
        
        long sNodeId = commodity.sNodeId;
        long tNodeId = commodity.tNodeId;

        if (!nodeEnumeration.ContainsKey(sNodeId))
        {
          DAGNode newNode = new DAGNode(sNodeId);
          gtcz.AddNode(newNode);

          if (meapContext.TArbSeqCFG.IsSourceNode(sNodeId))
          {
            gtcz.SetSourceNode(newNode);
          }

          nodeEnumeration[sNodeId] = newNode;
        }

        if (!nodeEnumeration.ContainsKey(tNodeId))
        {
          DAGNode newNode = new DAGNode(tNodeId);
          gtcz.AddNode(newNode);

          if (meapContext.TArbSeqCFG.IsSinkNode(tNodeId))
          {
            gtcz.SetSinkNode(newNode);
          }

          nodeEnumeration[tNodeId] = newNode;
        }

        DAGNode from = nodeEnumeration[sNodeId];
        DAGNode to = nodeEnumeration[tNodeId];

        if (!gtcz.AreConnected(from.Id, to.Id))
        {
          DAGEdge newEdge = new DAGEdge(edgeId++, from, to);
          gtcz.AddEdge(newEdge);
        }
      }

      return gtcz;
    }

    #endregion

    #region private members

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private readonly MEAPContext meapContext;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
