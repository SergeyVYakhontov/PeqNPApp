////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public class StateSymbolDirectionTripleComparer : IComparer<StateSymbolDirectionTriple>
  {
    #region public members

    public int Compare(StateSymbolDirectionTriple k1, StateSymbolDirectionTriple k2)
    {
      if (k1.State < k2.State)
      {
        return -1;
      }
      else if (k1.State == k2.State)
      {
        if (k1.Symbol < k2.Symbol)
        {
          return -1;
        }
        else if (k1.Symbol == k2.Symbol)
        {
          if (k1.Direction < k2.Direction)
          {
            return -1;
          }
          else if (k1.Direction == k2.Direction)
          {
            if (k1.Shift < k2.Shift)
            {
              return -1;
            }
            else if (k1.Shift == k2.Shift)
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
      else
      {
        return 1;
      }
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
