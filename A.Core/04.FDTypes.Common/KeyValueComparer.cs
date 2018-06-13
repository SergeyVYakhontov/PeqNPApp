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

    public int Compare(KeyValuePair<long, long> k1, KeyValuePair<long, long> k2)
    {
      if (k1.Key < k2.Key)
      {
        return -1;
      }
      else if (k1.Key == k2.Key)
      {
        if (k1.Value < k2.Value)
        {
          return -1;
        }
        else if (k1.Value == k2.Value)
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
