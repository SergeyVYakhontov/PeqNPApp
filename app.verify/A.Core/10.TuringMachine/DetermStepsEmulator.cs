////////////////////////////////////////////////////////////////////////////////////////////////////

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
      IntSegment segment1n = new IntSegment(1, (int)n);
      segment1n.ForEach(DoStep1);
    }

    public void DoStepN(uint n, IReadOnlyDictionary<int, byte> indexMap)
    {
      for (int i = 1; i <= n; i++)
      {
        if (indexMap.ContainsKey(i - 1))
        {
          DoStep1(indexMap[i - 1]);
        }
        else
        {
          DoStep1(0);
        }
      }
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
