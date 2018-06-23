////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public class KeyValueComparer : IComparer<KeyValuePair<long, long>>
  {
    #region public members

    public int Compare(KeyValuePair<long, long> x, KeyValuePair<long, long> y)
    {
      if (x.Key < y.Key)
      {
        return -1;
      }
      else if (x.Key == y.Key)
      {
        if (x.Value < y.Value)
        {
          return -1;
        }
        else if (x.Value == y.Value)
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
