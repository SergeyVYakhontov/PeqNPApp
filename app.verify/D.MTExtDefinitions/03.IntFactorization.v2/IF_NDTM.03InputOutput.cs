////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;
using Ninject;
using ExistsAcceptingPath;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace MTExtDefinitions.v2
{
  public partial class IF_NDTM : OneTapeNDTM
  {
    #region public members

    public override void PrepareTapeFwd(int[] input, TMInstance instance)
    {
      instance.SetTapeSymbol(0, delimiter0);
      instance.SetTapeSymbol(FrameStart1(input.Length), delimiter1);
      instance.SetTapeSymbol(FrameStart2(input.Length), delimiter2);
      instance.SetTapeSymbol(FrameStart3(input.Length), delimiter3);
      instance.SetTapeSymbol(FrameEnd4(input.Length), delimiter4);
    }

    public override int[] GetAcceptingInstanceOutput(int[] input)
    {
      return acceptingInstance.GetTapeSubstr(
        GetLTapeBound(0, (uint)input.Length),
        GetRTapeBound(0, (uint)input.Length));
    }

    public static (int[], int[]) RetrieveFactors(int inputLength, int[] factorsString)
    {
      int frameLength = FrameLength(inputLength);

      long i1 = 1 + frameLength + 1;
      long j1 = Array.IndexOf(factorsString, blankSymbol, (int)i1);

      int[] factorX = AppHelper.CreateSubArray(factorsString, i1, j1 - i1);

      AppHelper.ReplaceInArray(factorX, markB0, 0, (int a, int b) => (a == b));
      AppHelper.ReplaceInArray(factorX, markB1, 1, (int a, int b) => (a == b));

      long i2 = 1 + (2 * frameLength) + 1;
      long j2 = Array.IndexOf(factorsString, blankSymbol, (int)i2);

      int[] factorY = AppHelper.CreateSubArray(factorsString, i2, j2 - i2);

      AppHelper.ReplaceInArray(factorY, markC0, 0, (int a, int b) => (a == b));
      AppHelper.ReplaceInArray(factorY, markC1, 1, (int a, int b) => (a == b));

      return (factorX, factorY);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
