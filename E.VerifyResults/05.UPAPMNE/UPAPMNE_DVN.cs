////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;
using ExistsAcceptingPath;
using MTDefinitions;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace VerifyResults
{
  public class UPAPMNE_DVN : IAlgorithm
  {
    #region public members

    public bool Decide(int[] input)
    {
      return (input.Last() == 2);
    }

    public int[] Compute(int[] input)
    {
      return new int[] {};
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
