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
  public class CommodityCheckerSparse : CommodityChecker
  {
    #region Ctors

    public CommodityCheckerSparse(
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
    { }

    #endregion

    #region private members

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

      SparseMatrix A;
      linEquationContext.TCPELinProgMatrix.GetLinEqSetSparseMatrix(out A);

      LinEqSetSolverSparse linEqSetSolver = new LinEqSetSolverSparse(A);

      return linEqSetSolver.IfSolutionExists();
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////