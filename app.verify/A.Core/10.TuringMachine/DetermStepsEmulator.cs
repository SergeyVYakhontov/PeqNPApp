﻿////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Ninject;
using EnsureThat;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public class DetermStepsEmulator
  {
    #region Ctors

    public DetermStepsEmulator(
      Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> delta,
      TMInstance tmInstance)
    {
      this.delta = delta;
      this.tmInstance = tmInstance;
    }

    public void SetupConfiguration(long cellIndex, uint state)
    {
      tmInstance.SetupConfiguration(cellIndex, state);
    }

    #endregion

    #region public members

    public void DoStepN(uint n)
    {
      (new IntSegment(1, (int)n)).ForEach(DoStep1);
    }

    #endregion

      #region private members

    private static readonly IKernel configuration = Core.AppContext.Configuration;

    private readonly Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> delta;
    private readonly TMInstance tmInstance;

    private void DoStep1()
    {
      StateSymbolPair from = tmInstance.CurrentStateSymbolPair;
      IList<StateSymbolDirectionTriple> to = delta[from];

      Ensure.That(to).SizeIs(1);

      TMInstance.MoveToNextConfiguration(to[0], tmInstance);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
