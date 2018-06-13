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
  public class PropSymbolComparer : IComparer<PropSymbol>
  {
    #region public members

    public int Compare(PropSymbol k1, PropSymbol k2)
    {
      if (k1.Variable < k2.Variable)
      {
        return -1;
      }
      else if (k1.Variable == k2.Variable)
      {
        if (k1.Symbol < k2.Symbol)
        {
          return -1;
        }
        else if (k1.Symbol == k2.Symbol)
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

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////

