////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public class LinEqsAlgorithmProvider : ILinEqsAlgorithmProvider
  {
    #region public members

    public CommodityChecker GetTCPEPCommChecker(
      MEAPContext meapContext,
      TapeSegContext tapeSegContext,
      Commodity commodity,
      SortedSet<long> tConsistPathComms,
      SortedSet<long> tInconsistPathComms)
    {
      return new CommodityCheckerSparseMThreads(
        meapContext,
        tapeSegContext,
        commodity,
        tConsistPathComms,
        tInconsistPathComms);
    }

    public LinEqsSetBuilder GetLinEquationsSetBuilder(
      MEAPContext meapContext,
      TapeSegContext tapeSegContext,
      LinEquationContext linEquationContext)
    {
      return new LinEquationsSetBuilder(
        meapContext,
        tapeSegContext,
        linEquationContext);
    }

    public TCPEPSolver GetTCPEPSolver(
      MEAPContext meapContext,
      TapeSegContext tapeSegContext)
    {
      return new TCPEPLPSolver(meapContext, tapeSegContext);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
