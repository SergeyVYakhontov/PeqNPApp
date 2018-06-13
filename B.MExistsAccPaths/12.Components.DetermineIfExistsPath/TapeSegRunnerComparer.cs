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

    public int Compare(TapeSegRunner r1, TapeSegRunner r2)
    {
      if (r1.TapeSegRunnerState < r2.TapeSegRunnerState)
      {
        return -1;
      }
      else if (r1.TapeSegRunnerState == r2.TapeSegRunnerState)
      {
        if (r1.Id < r2.Id)
        {
          return -1;
        }
        else if (r1.Id == r2.Id)
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
