﻿////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using Ninject;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public class CommodityCheckerSparseMThreads : CommodityChecker
  {
    #region Ctors

    public CommodityCheckerSparseMThreads(
      MEAPContext meapContext,
      TapeSegContext tapeSegContext,
      Commodity commodity,
      SortedSet<long> tConsistPathComms,
      SortedSet<long> tInconsistPathComms)
      : base(
         meapContext,
         tapeSegContext,
         commodity,
         tConsistPathComms,
         tInconsistPathComms)
    {
      this.configuration = Core.AppContext.GetConfiguration();
    }

    #endregion

    #region private members

    private readonly IReadOnlyKernel configuration;

    protected override bool CheckCommodity()
    {
      linEquationContext.TCPELinProgMatrix = new LinEquationsMatrix();
      ILinEqsAlgorithmProvider linEqsAlgorithmProvider = configuration.Get<ILinEqsAlgorithmProvider>();

      LinEqsSetBuilder tcpeLinProgBuilder =
        linEqsAlgorithmProvider.GetLinEquationsSetBuilder(
          meapContext,
          tapeSegContext,
          linEquationContext);

      tcpeLinProgBuilder.CreateTCPEPLinProgEqsSet();
      AddCommodityCheckEquation();

      SparseMatrix A = linEquationContext.TCPELinProgMatrix.GetLinEqSetSparseMatrix();
      LinEqSetSolverSparseMThreads linEqSetSolver = new(A);

      return linEqSetSolver.IfSolutionExists();
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
