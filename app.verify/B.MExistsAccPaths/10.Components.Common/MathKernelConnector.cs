﻿////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Wolfram.NETLink;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public class MathKernelConnector
  {
    #region Ctors

    public MathKernelConnector()
    {
      MathKernel = default!;
    }

    #endregion

    #region public members

    public IKernelLink MathKernel { get; private set; }

    public void LoadMathKernel()
    {
      MathKernel = MathLinkFactory.CreateKernelLink();
      MathKernel.WaitAndDiscardAnswer();
    }

    public void UnloadMathKernel()
    {
      MathKernel.Close();
      MathKernel = default!;
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
