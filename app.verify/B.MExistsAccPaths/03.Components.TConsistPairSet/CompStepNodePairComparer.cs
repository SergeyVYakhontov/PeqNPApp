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
  public class CompStepNodePairComparer : IComparer<CompStepNodePair>
  {
    #region public members

    public int Compare(CompStepNodePair x, CompStepNodePair y)
    {
      if (x.Variable < y.Variable)
      {
        return -1;
      }

      if (x.Variable == y.Variable)
      {
        if (x.uNode < y.uNode)
        {
          return -1;
        }

        if (x.uNode == y.uNode)
        {
          if (x.vNode < y.vNode)
          {
            return -1;
          }

          if (x.vNode == y.vNode)
          {
            return 0;
          }

          return 1;
        }

        return 1;
      }

      return 1;

      #endregion
    }
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
