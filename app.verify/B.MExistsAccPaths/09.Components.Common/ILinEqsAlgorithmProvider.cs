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
  public interface ILinEqsAlgorithmProvider
  {
    CommodityChecker GetTCPEPCommChecker(
      MEAPContext meapContext,
      TapeSegContext tapeSegContext,
      Commodity commodity,
      SortedSet<long> tConsistPathComms,
      SortedSet<long> tInconsistPathComms);

    LinEqsSetBuilder GetLinEquationsSetBuilder(
      MEAPContext meapContext,
      TapeSegContext tapeSegContext,
      LinEquationContext linEquationContext);

    TCPEPSolver GetTCPEPSolver(
      MEAPContext meapContext,
      TapeSegContext tapeSegContext);
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
