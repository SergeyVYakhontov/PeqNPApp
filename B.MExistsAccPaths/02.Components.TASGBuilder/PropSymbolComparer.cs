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

    public int Compare(PropSymbol x, PropSymbol y)
    {
      if (x.Variable < y.Variable)
      {
        return -1;
      }

      if (x.Variable == y.Variable)
      {
        if (x.Symbol < y.Symbol)
        {
          return -1;
        }

        if (x.Symbol == y.Symbol)
        {
          return 0;
        }

        return 1;
      }

      return 1;
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////

