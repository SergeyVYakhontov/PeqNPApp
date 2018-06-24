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

    public int Compare(StateSymbolDirectionTriple x, StateSymbolDirectionTriple y)
    {
      if (x.State < y.State)
      {
        return -1;
      }
      else if (x.State == y.State)
      {
        if (x.Symbol < y.Symbol)
        {
          return -1;
        }
        else if (x.Symbol == y.Symbol)
        {
          if (x.Direction < y.Direction)
          {
            return -1;
          }
          else if (x.Direction == y.Direction)
          {
            if (x.Shift < y.Shift)
            {
              return -1;
            }
            else if (x.Shift == y.Shift)
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
