////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public class TapeSegRunnerComparer : IComparer<TapeSegRunner>
  {
    #region public members

    public int Compare(TapeSegRunner? x, TapeSegRunner? y)
    {
      if (x!.TapeSegRunnerState < y!.TapeSegRunnerState)
      {
        return -1;
      }
      else if (x.TapeSegRunnerState == y.TapeSegRunnerState)
      {
        if (x.Id < y.Id)
        {
          return -1;
        }
        else if (x.Id == y.Id)
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
