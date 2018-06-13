////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;

namespace ExistsAcceptingPath
{
  public abstract class TCPEPSolver : TapeSegContextBase
  {
    #region Ctors

    public TCPEPSolver(MEAPContext meapContext, TapeSegContext tapeSegContext)
      : base(meapContext, tapeSegContext) { }

    #endregion

    #region public methods

    public bool Done { get; protected set; }

    public abstract void Init(LongSegment tapeSeg);
    public abstract void CheckKZetaGraphs();
    public abstract void ReduceCommodities();
    public abstract void RunGaussElimination();
    public abstract void RunLinearProgram();

    #endregion

    #region private members

    protected const String linearProgramStr = "LinearProgramming";
    protected const String linEqSetStr = "LinearSolve";

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
