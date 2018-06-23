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
  public class CommodityCheckerMath : CommodityChecker
  {
    #region Ctors

    public CommodityCheckerMath(
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

    /// <summary>
    /// Just to compare linear equation set solvers
    /// </summary>
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

      String linEqSetQuery = linEquationContext.TCPELinProgMatrix.GetLinEqSetQuery();
      tapeSegContext.MathQueryString = linEqSetQuery + "LinearSolve[A,b]";

      MathKernelConnector mathKernelConnector = configuration.Get<MathKernelConnector>();
      String linEqSetOutput = mathKernelConnector.MathKernel.EvaluateToOutputForm(tapeSegContext.MathQueryString, 0);

      return !linEqSetOutput.Contains(linEqSetStr);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
