////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public partial class DAG
  {
    #region public members

    public static long Level0 => 0;

    public static bool BFS(
      DAG dag,
      SortedSet<long> initNodes,
      Predicate<DAGNode> filter,
      Predicate<DAGNode> action)
    {
      SortedSet<long> processedNodes = new SortedSet<long>();
      QueueWithIdSet<DAGNode> nodeQueue = new QueueWithIdSet<DAGNode>();
      bool status = false;

      initNodes.ForEach(u =>
      {
        DAGNode uNode = dag.NodeEnumeration[u];

        if (!nodeQueue.Contains(uNode))
        {
          nodeQueue.Enqueue(uNode);
        }
        processedNodes.Add(u);
      });

      Predicate<DAGNode> inFlowComputed =
        w =>
          (!w.InEdges.Any() ||
           initNodes.Contains(w.Id)) ||
           w.InEdges.All(e => (!filter(e.FromNode) || processedNodes.Contains(e.FromNode.Id)));

      while (nodeQueue.Any())
      {
        DAGNode v = nodeQueue.Dequeue();

        if (!filter(v))
        {
          continue;
        }

        if (inFlowComputed(v))
        {
          processedNodes.Add(v.Id);

          bool actionStatus = action(v);
          status = status || actionStatus;

          foreach (DAGEdge e in v.OutEdges)
          {
            if (!nodeQueue.Contains(e.ToNode))
            {
              nodeQueue.Enqueue(e.ToNode);
            }
          }
        }
      }

      return status;
    }

    public static void DFS(
      DAGNode s,
      GraphDirection direction,
      Action<DAGNode, long> nodeAction,
      Action<DAGEdge, long> edgeAction)
    {
      SortedSet<long> processedNodes = new SortedSet<long>();
      DFS(s, processedNodes, direction, 0, nodeAction, edgeAction);
    }

    public static bool PropagateProperties(
      DAG dag,
      long fromNodeId,
      GraphDirection direction,
      Predicate<DAGNode> nodeFilter,
      Predicate<DAGEdge> edgeFilter,
      Predicate<DAGNode> nodeAction,
      Predicate<DAGEdge> edgeAction)
    {
      QueueWithIdSet<DAGNode> nodeQueue = new QueueWithIdSet<DAGNode>();
      bool status = false;
      SortedSet<long> processedNodes = new SortedSet<long>();
      DAGNode s = dag.NodeEnumeration[fromNodeId];

      if (nodeFilter(s))
      {
        nodeQueue.Enqueue(s);
      }

      while (nodeQueue.Any())
      {
        DAGNode u = nodeQueue.Dequeue();
        if (processedNodes.Contains(u.Id))
        {
          continue;
        }
        processedNodes.Add(u.Id);

        bool nodeActionStatus = nodeAction(u);
        status = status || nodeActionStatus;

        foreach (DAGEdge e in (direction == GraphDirection.Forward ?
          u.OutEdges : u.InEdges))
        {
          DAGNode v = (direction == GraphDirection.Forward ? e.ToNode : e.FromNode);

          if(!edgeFilter(e))
          {
            continue;
          }

          bool edgeActionStatus = edgeAction(e);
          status = status || edgeActionStatus;

          if (nodeFilter(v) && (!nodeQueue.Contains(v)))
          {
            nodeQueue.Enqueue((direction == GraphDirection.Forward ? e.ToNode : e.FromNode));
          }
        }
      }

      return status;
    }

    public static void FindPath_Greedy(
      DAGNode s,
      DAGNode t,
      GraphDirection direction,
      Predicate<DAGNode> nodeFilter,
      Predicate<DAGEdge> edgeFilter,
      Action<DAGNode> nodeAction,
      Action<DAGEdge> edgeAction,
      out List<long> path,
      out bool pathFound)
    {
      path = new List<long>();
      pathFound = false;

      SortedSet<long> processedNodes = new SortedSet<long>();
      DAGNode currentNode = (direction == GraphDirection.Forward ? s : t);

      if (!nodeFilter(currentNode))
      {
        pathFound = false;

        return;
      }

      while (true)
      {
        processedNodes.Add(currentNode.Id);
        bool nextNodeFound = false;

        path.Add(currentNode.Id);
        nodeAction(currentNode);

        if (currentNode == (direction == GraphDirection.Forward ? t : s))
        {
          pathFound = true;

          return;
        }

        List<DAGEdge> nextEdges = (direction == GraphDirection.Forward ?
          currentNode.OutEdges : currentNode.InEdges);

        foreach (DAGEdge e in nextEdges)
        {
          DAGNode v = (direction == GraphDirection.Forward ? e.ToNode : e.FromNode);
          if (edgeFilter(e) && nodeFilter(v))
          {
            currentNode = v;
            edgeAction(e);

            nextNodeFound = true;

            break;
          }
        }

        if (!nextNodeFound)
        {
          return;
        }
      }
    }

    public static void FindPath_DFS(
      DAG dag,
      DAGNode s,
      GraphDirection direction,
      Predicate<DAGNode> nodeFilter,
      Predicate<DAGEdge> edgeFilter,
      Action<DAGNode, long> nodeAction,
      Action<DAGEdge, long> edgeAction,
      List<long> path,
      ref bool pathFound)
    {
      SortedSet<long> processedNodes = new SortedSet<long>();

      FindPath_DFS(
        s,
        dag.t,
        processedNodes,
        direction,
        0,
        nodeFilter,
        edgeFilter,
        nodeAction,
        edgeAction,
        path,
        ref pathFound);
    }

    public static void ClassifyDAGEdges(
      DAG graph,
      SortedDictionary<long, SortedSet<long>> VLevelSets,
      out SortedSet<long> backEdges,
      out SortedSet<long> crossEdges)
    {
      backEdges = new SortedSet<long>();
      crossEdges = new SortedSet<long>();

      foreach (DAGEdge e in graph.Edges)
      {
        long uNodeId = e.FromNode.Id;
        long vNodeId = e.ToNode.Id;

        foreach (KeyValuePair<long, SortedSet<long>> VLevelSetsPair1 in VLevelSets)
        {
          long levelNum1 = VLevelSetsPair1.Key;
          SortedSet<long> levelSet1 = VLevelSetsPair1.Value;

          if (levelSet1.Contains(uNodeId))
          {
            foreach (KeyValuePair<long, SortedSet<long>> VLevelSetsPair2 in VLevelSets)
            {
              long levelNum2 = VLevelSetsPair2.Key;
              SortedSet<long> levelSet2 = VLevelSetsPair2.Value;

              if (levelSet2.Contains(vNodeId))
              {
                if (levelNum2 < levelNum1)
                {
                  backEdges.Add(e.Id);
                }

                if (levelNum1 == levelNum2)
                {
                  crossEdges.Add(e.Id);
                }
              }
            }
          }
        }
      }
    }

    #endregion

    #region private members

    private static void DFS(
      DAGNode u,
      SortedSet<long> processedNodes,
      GraphDirection direction,
      long level,
      Action<DAGNode, long> nodeAction,
      Action<DAGEdge, long> edgeAction)
    {
      if (processedNodes.Contains(u.Id))
      {
        return;
      }

      nodeAction(u, level);
      processedNodes.Add(u.Id);

      foreach (DAGEdge e in (direction == GraphDirection.Forward ? u.OutEdges : u.InEdges))
      {
        DFS((direction == GraphDirection.Forward ? e.ToNode : e.FromNode),
          processedNodes, direction, level + 1, nodeAction, edgeAction);
        edgeAction(e, level);
      }
    }

    private static void FindPath_DFS(
      DAGNode u,
      DAGNode t,
      SortedSet<long> processedNodes,
      GraphDirection direction,
      long level,
      Predicate<DAGNode> nodeFilter,
      Predicate<DAGEdge> edgeFilter,
      Action<DAGNode, long> nodeAction,
      Action<DAGEdge, long> edgeAction,
      List<long> path,
      ref bool pathFound)
    {
      if (!nodeFilter(u))
      {
        return;
      }

      if (processedNodes.Contains(u.Id))
      {
        return;
      }

      nodeAction(u, level);
      path.Add(u.Id);

      processedNodes.Add(u.Id);

      if (u.Id == t.Id)
      {
        pathFound = true;
        return;
      }

      foreach (DAGEdge e in (direction == GraphDirection.Forward ? u.OutEdges : u.InEdges))
      {
        if (!edgeFilter(e))
        {
          continue;
        }

        FindPath_DFS(
          (direction == GraphDirection.Forward ? e.ToNode : e.FromNode),
          t,
          processedNodes,
          direction,
          level + 1,
          nodeFilter,
          edgeFilter,
          nodeAction,
          edgeAction,
          path,
          ref pathFound);

        edgeAction(e, level);

        if (pathFound)
        {
          return;
        }
      }

      path.RemoveAt(path.Count - 1);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
