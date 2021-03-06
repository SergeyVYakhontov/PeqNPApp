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

    public void DoStepsN(uint n)
    {
      IntSegment segment1n = new(1, (int)n);
      segment1n.ForEach(DoStep1);
    }

    public void DoStepsN(uint n, IReadOnlyDictionary<int, byte> indexMap)
    {
      for (int i = 1; i <= n; i++)
      {
        if (indexMap.ContainsKey(i))
        {
          DoStep1(indexMap[i]);
        }
        else
        {
          DoStep1(0);
        }
      }
    }

    public void DoStepsWhile(
      IReadOnlyDictionary<int, byte> indexMap,
      Func<bool> condition,
      Action<int> action)
    {
      for (int stepNumber = 1; condition(); stepNumber++)
      {
        action(stepNumber);

        if (indexMap.ContainsKey(stepNumber))
        {
          DoStep1(indexMap[stepNumber]);
        }
        else
        {
          DoStep1(0);
        }
      }
    }

    #endregion

    #region private members

    private readonly Dictionary<StateSymbolPair, List<StateSymbolDirectionTriple>> delta;
    private readonly TMInstance tmInstance;

    private void DoStep1()
    {
      StateSymbolPair from = tmInstance.CurrentStateSymbolPair;
      IList<StateSymbolDirectionTriple> to = delta[from];

      Ensure.That(to).SizeIs(1);

      TMInstance.MoveToNextConfiguration(to[0], tmInstance);
    }

    private void DoStep1(byte itemIndex)
    {
      StateSymbolPair from = tmInstance.CurrentStateSymbolPair;
      IList<StateSymbolDirectionTriple> to = delta[from];

      TMInstance.MoveToNextConfiguration(to[itemIndex], tmInstance);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
