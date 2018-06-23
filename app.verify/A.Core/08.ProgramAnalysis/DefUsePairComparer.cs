////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Ninject;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public class DefUsePairComparer : IComparer<DefUsePair>
  {
    #region public members

    public int Compare(DefUsePair k1, DefUsePair k2)
    {
      if (k1.Variable < k2.Variable)
      {
        return -1;
      }
      else if (k1.Variable == k2.Variable)
      {
        if (k1.DefNode < k2.DefNode)
        {
          return -1;
        }
        else if (k1.DefNode == k2.DefNode)
        {
          if (k1.UseNode < k2.UseNode)
          {
            return -1;
          }
          else if (k1.UseNode == k2.UseNode)
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
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
