﻿////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;
using Ninject;
using ExistsAcceptingPath;
using MTExtDefinitions;

////////////////////////////////////////////////////////////////////////////////////////////////////
// bit numbers in examples are written from left to right
////////////////////////////////////////////////////////////////////////////////////////////////////

namespace VerifyResults
{
  public abstract class IF_ExampleSets : ExampleSet
  {
    #region public members

    public override string Name { get; set; }

    public override ICheckAlgorithm GetCheckAlgorithm()
    {
      return new IF_CheckAlgorithm();
    }

    #endregion

    #region private members

    protected static readonly IKernel configuration = Core.AppContext.Configuration;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////