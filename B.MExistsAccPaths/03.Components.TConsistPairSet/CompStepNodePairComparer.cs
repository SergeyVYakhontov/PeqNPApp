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

    public int Compare(CompStepNodePair s1, CompStepNodePair s2)
    {
      if (s1.Variable < s2.Variable)
      {
        return -1;
      }
      else if (s1.Variable == s2.Variable)
      {
        if (s1.uNode < s2.uNode)
        {
          return -1;
        }
        else if (s1.uNode == s2.uNode)
        {
          if (s1.vNode < s2.vNode)
          {
            return -1;
          }
          else if (s1.vNode == s2.vNode)
          {
            return 0;
          }
          else
          {
            return 1;
          }
        }
        else
        {
          return 1;
        }
      }
      else
      {
        return 1;
      }

      #endregion
    }
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
