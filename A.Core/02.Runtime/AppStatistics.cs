////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Ninject;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public sealed class AppStatistics
  {
    #region public members

    public ulong ReduceCommodities { get; set; }
    public ulong RunGaussElimination { get; set; }
    public ulong RunLinearProgram { get; set; }

    public bool ThereWereErrors { get; set; }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
