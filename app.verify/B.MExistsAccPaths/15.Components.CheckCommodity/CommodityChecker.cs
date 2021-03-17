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
  public abstract class CommodityChecker : TapeSegContextBase, ITPLCollectionItem
  {
    #region Ctors

    protected CommodityChecker(
      MEAPContext meapContext,
      TapeSegContext tapeSegContext,
      Commodity commodity,
      SortedSet<long> tConsistPathComms,
      SortedSet<long> tInconsistPathComms)
      : base(meapContext, tapeSegContext)
    {
      this.commodity = commodity;
      this.tConsistPathComms = tConsistPathComms;
      this.tInconsistPathComms = tInconsistPathComms;
    }

    #endregion

    #region public members

    public bool Done { get; protected set; }

    public void Run()
    {
      if (CheckCommodity())
      {
        lock (tConsistPathComms)
        {
          tConsistPathComms.Add(commodity.Id);
        }
      }
      else
      {
        lock (tInconsistPathComms)
        {
          tInconsistPathComms.Add(commodity.Id);
        }
      }
    }

    #endregion

    #region private members

    protected readonly LinEquationContext linEquationContext = new();
    private readonly Commodity commodity;
    private readonly SortedSet<long> tConsistPathComms;
    private readonly SortedSet<long> tInconsistPathComms;

    protected const string linEqSetStr = "LinearSolve";

    protected void AddCommodityCheckEquation()
    {
      SortedDictionary<long, RationalNumber> coeffs = new();
      long sNodeId = commodity.Gi.GetSourceNodeId();

      long sNodeVar = linEquationContext.KiLinProgEqsSets[commodity.Id].NodeToVar[sNodeId];
      coeffs[sNodeVar] = RationalNumber.Const_1;

      long equation = linEquationContext.TCPELinProgMatrix.AddEquation(coeffs, EquationKind.Equal, RationalNumber.Const_1);
      linEquationContext.TArbSeqCFGLinProgEqsSet.AddEquation(equation);
    }

    protected abstract bool CheckCommodity();

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
