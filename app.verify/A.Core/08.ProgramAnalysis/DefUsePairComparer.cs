////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Diagnostics.CodeAnalysis;
using Ninject;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public class DefUsePairComparer : IComparer<DefUsePair>
  {
    #region public members

    public int Compare(DefUsePair? x, DefUsePair? y)
    {
      if (x!.Variable < y!.Variable)
      {
        return -1;
      }
      else if (x.Variable == y.Variable)
      {
        if (x.DefNode < y.DefNode)
        {
          return -1;
        }
        else if (x.DefNode == y.DefNode)
        {
          if (x.UseNode < y.UseNode)
          {
            return -1;
          }
          else if (x.UseNode == y.UseNode)
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
