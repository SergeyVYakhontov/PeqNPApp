﻿////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public static partial class AppHelper
  {
    #region public members

    public static void SwapElems<T>(ref T e1, ref T e2)
    {
      T t = e1;
      e1 = e2;
      e2 = t;
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
