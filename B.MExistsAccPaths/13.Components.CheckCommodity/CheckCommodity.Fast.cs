////////////////////////////////////////////////////////////////////////////////////////////////////

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
  public class CommodityCheckerFast : CommodityChecker
  {
    #region Ctors

    public CommodityCheckerFast(
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

      linEquationContext.TCPELinProgMatrix.GetLinEqSetMatrixFast(out long[][] A_p, out long[][] A_q);

      LinEqSetSolverPtrs linEqSetSolver = new LinEqSetSolverPtrs(A_p, A_q);

      return linEqSetSolver.IfSolutionExists();
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
