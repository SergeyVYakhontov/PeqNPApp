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

    public static void BFS_VLevels(
      DAG dag,
      GraphDirection dir,
      SortedDictionary<long, SortedSet<long>> nodeVLevels,
      long currentLevel,
      Func<DAGNode, bool> nodeAction,
      Func<long, bool> levelAction)
    {
      SortedDictionary<long, DAGNode> nodeEnumeration = dag.NodeEnumeration;

      foreach (KeyValuePair<long, SortedSet<long>> currentLevelNodes in
        (dir == GraphDirection.Forward ? nodeVLevels : nodeVLevels.Reverse()))
      {
        if (currentLevel > currentLevelNodes.Key)
        {
          continue;
        }

        foreach (long vNodeId in currentLevelNodes.Value)
        {
          if(nodeEnumeration.TryGetValue(vNodeId, out DAGNode vNode))
          {
            nodeAction(vNode);
          }
        }

        levelAction(currentLevelNodes.Key);
      }
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
