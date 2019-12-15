////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public static class AppExtensions
  {
    #region public members

    public static long LastIndex<T>(this T[] data)
    {
      return data.Length - 1;
    }
  }

  #endregion
}
