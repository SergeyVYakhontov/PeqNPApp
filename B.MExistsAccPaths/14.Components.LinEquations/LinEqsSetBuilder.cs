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
  public abstract class LinEqsSetBuilder : LinEquationsContextBase
  {
    #region Ctors

    public LinEqsSetBuilder(
      MEAPContext meapContext,
      TapeSegContext tapeSegContext,
      LinEquationContext linEquationContext)
      : base(meapContext, tapeSegContext, linEquationContext)
    { }

    #endregion

    #region public members

    public abstract bool CreateTCPEPLinProgEqsSet();

    #endregion

    #region private members

    protected DAGLinEquationsSet tcpeLinProgEqsSet;

    protected void CreateTArbSeqCFGEqsSet()
    {
      linEquationContext.TArbSeqCFGLinProgEqsSet = DAGLinEquationsSet.CreateEqsSetForDAG(
        linEquationContext.TCPELinProgMatrix,
        meapContext.TArbSeqCFG,
        tapeSegContext.TArbSeqCFGUnusedNodes);

      {
        SortedDictionary<long, RationalNumber> coeffs = new SortedDictionary<long, RationalNumber>();
        long sNodeId = meapContext.TArbSeqCFG.GetSourceNodeId();

        long sNodeVar = linEquationContext.TArbSeqCFGLinProgEqsSet.NodeToVar[sNodeId];
        coeffs[sNodeVar] = RationalNumber.Const_1;

        long equation = linEquationContext.TCPELinProgMatrix.AddEquation(coeffs, EquationKind.Equal, RationalNumber.Const_1);
        linEquationContext.TArbSeqCFGLinProgEqsSet.AddEquation(equation);
      }

      {
        SortedDictionary<long, RationalNumber> coeffs = new SortedDictionary<long, RationalNumber>();
        long tNodeId = meapContext.TArbSeqCFG.GetSinkNodeId();

        long tNodeVar = linEquationContext.TArbSeqCFGLinProgEqsSet.NodeToVar[tNodeId];
        coeffs[tNodeVar] = RationalNumber.Const_1;

        long equation = linEquationContext.TCPELinProgMatrix.AddEquation(coeffs, EquationKind.Equal, RationalNumber.Const_1);
        linEquationContext.TArbSeqCFGLinProgEqsSet.AddEquation(equation);
      }
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
