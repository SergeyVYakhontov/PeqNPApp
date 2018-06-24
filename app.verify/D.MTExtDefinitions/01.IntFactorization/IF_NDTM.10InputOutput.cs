////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;
using Ninject;
using ExistsAcceptingPath;

namespace MTExtDefinitions
{
  public partial class IF_NDTM : OneTapeNDTM
  {
    #region public members

    public override void PrepareTapeFwd(int[] input, TMInstance instance)
    {
      int frameLength = FrameLength(input.Length);

      long frameStart2 = 1 + frameLength;
      long frameStart3 = 1 + 2 * frameLength;
      long frameStart4 = 1 + 3 * frameLength;
      long frameEnd4 = 1 + 4 * frameLength;

      instance.SetTapeSymbol(frameStart2, delimiter);
      instance.SetTapeSymbol(frameStart3, delimiter);
      instance.SetTapeSymbol(frameStart4, delimiter);
      instance.SetTapeSymbol(frameEnd4, delimiter);

      IDebugOptions debugOptions = configuration.Get<IDebugOptions>();

      if (debugOptions.IntFactTestRules)
      {
        instance.SetTapeSymbol(frameStart2 + 1, 1);
        instance.SetTapeSymbol(frameStart2 + 2, 1);

        instance.SetTapeSymbol(frameStart3 + 1, 1);
        instance.SetTapeSymbol(frameStart3 + 2, 1);
        instance.SetTapeSymbol(frameStart3 + 3, 0);
        instance.SetTapeSymbol(frameStart3 + 4, 1);
        instance.SetTapeSymbol(frameStart3 + 5, 1);
      }
    }

    public override int[] GetAcceptingInstanceOutput(int[] input)
    {
      return acceptingInstance.GetTapeSubstr(
        GetLTapeBound(-1, input.Length),
        GetRTapeBound(-1, input.Length));
    }

    public static void RetrieveFactors(
      int inputLength,
      int[] factorsString,
      out int[] factorX,
      out int[] factorY)
    {
      int frameLength = FrameLength(inputLength);

      long i1 = 1 + frameLength + 1;
      long j1 = Array.IndexOf(factorsString, OneTapeTuringMachine.blankSymbol, (int)i1);

      factorX = AppHelper.CreateSubArray(factorsString, i1, j1 - i1);
      AppHelper.ReplaceInArray(factorX, markB0, 0, (int a, int b) => (a == b));

      AppHelper.ReplaceInArray(factorX, markB1, 1, (int a, int b) => (a == b));

      long i2 = 1 + 2 * frameLength + 1;
      long j2 = Array.IndexOf(factorsString, OneTapeTuringMachine.blankSymbol, (int)i2);

      factorY = AppHelper.CreateSubArray(factorsString, i2, j2 - i2);

      AppHelper.ReplaceInArray(factorX, markC0, 0, (int a, int b) => (a == b));
      AppHelper.ReplaceInArray(factorX, markC1, 1, (int a, int b) => (a == b));
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
