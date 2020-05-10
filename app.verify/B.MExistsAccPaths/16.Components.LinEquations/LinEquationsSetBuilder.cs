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
  public class LinEquationsSetBuilder : LinEqsSetBuilder, ITracable
  {
    #region Ctors

    public LinEquationsSetBuilder(
      MEAPContext meapContext,
      TapeSegContext tapeSegContext,
      LinEquationContext linEquationContext)
      : base(meapContext, tapeSegContext, linEquationContext)
    { }

    #endregion

    #region public members

    public String Name { get; }

    public override bool CreateTCPEPLinProgEqsSet()
    {
      tcpeLinProgEqsSet = new DAGLinEquationsSet(linEquationContext.TCPELinProgMatrix, meapContext.TArbSeqCFG);

      CreateTArbSeqCFGEqsSet();
      CreateCommoditiesEquationsSet();

      linEquationContext.KSetZetaLinProgEqsSets = new SortedDictionary<long, DAGLinEquationsSet>();
      foreach (KeyValuePair<long, SortedSet<long>> p in tapeSegContext.KSetZetaSubset)
      {
        linEquationContext.KSetZetaLinProgEqsSets.Add(p.Key, CreateKZetaEquationsSet(p.Value));
      }

      CreateCommIsEqualToCFGEqsSet();

      Trace();
      linEquationContext.TCPELinProgEqsSet = tcpeLinProgEqsSet;

      return true;
    }

    public void Trace()
    {
      log.Debug("TCPELinProg:");

      log.DebugFormat("Vars = {0}", linEquationContext.TCPELinProgMatrix.VarsCount);
      log.DebugFormat("Equations = {0}", linEquationContext.TCPELinProgMatrix.EquationsCount);
    }

    #endregion

    #region private members

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType);

    private readonly SortedSet<long> stCommNodes = new SortedSet<long>();

    private void CreateCommoditiesEquationsSet()
    {
      linEquationContext.KiLinProgEqsSets = new SortedDictionary<long, DAGLinEquationsSet>();

      foreach (KeyValuePair<long, SortedSet<long>> p in tapeSegContext.KSetZetaSubset)
      {
        foreach (long Ki in p.Value)
        {
          Commodity commodity = meapContext.Commodities[Ki];
          DAG Gi = commodity.Gi;

          DAGLinEquationsSet KiEqsSet = DAGLinEquationsSet.CreateEqsSetForDAG(
            linEquationContext.TCPELinProgMatrix,
            Gi,
            tapeSegContext.TArbSeqCFGUnusedNodes);

          linEquationContext.KiLinProgEqsSets.Add(commodity.Id, KiEqsSet);
        }
      }
    }

    private DAGLinEquationsSet CreateKZetaEquationsSet(SortedSet<long> commodities)
    {
      DAGLinEquationsSet KZetaEqsSet = new DAGLinEquationsSet(linEquationContext.TCPELinProgMatrix, meapContext.TArbSeqCFG);

      foreach (KeyValuePair<long, DAGNode> idNodePair in meapContext.TArbSeqCFG.NodeEnumeration)
      {
        long uNodeId = idNodePair.Key;

        if (tapeSegContext.TArbSeqCFGUnusedNodes.Contains(uNodeId))
        {
          continue;
        }

        List<DAGLinEquationsSet> sList = new List<DAGLinEquationsSet>();
        List<DAGLinEquationsSet> tList = new List<DAGLinEquationsSet>();
        List<DAGLinEquationsSet> uList = new List<DAGLinEquationsSet>();

        bool isSNode = meapContext.TArbSeqCFG.IsSourceNode(idNodePair.Value.Id);
        bool isTNode = meapContext.TArbSeqCFG.IsSinkNode(idNodePair.Value.Id);

        foreach (long commodityId in commodities)
        {
          Commodity commodity = meapContext.Commodities[commodityId];
          DAG Gi = commodity.Gi;
          SortedDictionary<long, DAGNode> commNodeEnum = Gi.NodeEnumeration;
          DAGLinEquationsSet commEqsSet = linEquationContext.KiLinProgEqsSets[commodity.Id];

          if (commNodeEnum.ContainsKey(uNodeId) &&
              Gi.IsSourceNode(commNodeEnum[uNodeId].Id))
          {
            sList.Add(commEqsSet);
          }

          if (commNodeEnum.ContainsKey(uNodeId) &&
              Gi.IsSinkNode(commNodeEnum[uNodeId].Id))
          {
            tList.Add(commEqsSet);
          }

          if (commNodeEnum.ContainsKey(uNodeId))
          {
            uList.Add(commEqsSet);
          }
        }

        if (sList.Any() || tList.Any())
        {
          stCommNodes.Add(uNodeId);
          long stNodeSumVar = KZetaEqsSet.AddVarForNode(uNodeId);

          if (sList.Any())
          {
            SortedDictionary<long, RationalNumber> coeffs = new SortedDictionary<long, RationalNumber>();

            foreach (DAGLinEquationsSet eqSet in sList)
            {
              long sNodeVar = eqSet.NodeToVar[uNodeId];
              coeffs[sNodeVar] = RationalNumber.Const_1;
            }

            coeffs[stNodeSumVar] = RationalNumber.Const_Neg1;

            long equation = linEquationContext.TCPELinProgMatrix.AddEquation(coeffs, EquationKind.Equal, RationalNumber.Const_0);
            KZetaEqsSet.AddEquation(equation);
          }

          if (tList.Any())
          {
            SortedDictionary<long, RationalNumber> coeffs = new SortedDictionary<long, RationalNumber>();

            foreach (DAGLinEquationsSet eqSet in tList)
            {
              long tNodeVar = eqSet.NodeToVar[uNodeId];
              coeffs[tNodeVar] = RationalNumber.Const_1;
            }

            coeffs[stNodeSumVar] = RationalNumber.Const_Neg1;

            long equation = linEquationContext.TCPELinProgMatrix.AddEquation(coeffs, EquationKind.Equal, RationalNumber.Const_0);
            KZetaEqsSet.AddEquation(equation);
          }
        }
      }

      return KZetaEqsSet;
    }

    private void CreateCommIsEqualToCFGEqsSet()
    {
      foreach (KeyValuePair<long, DAGNode> idNodePair in meapContext.TArbSeqCFG.NodeEnumeration)
      {
        long uNodeId = idNodePair.Key;

        if (tapeSegContext.TArbSeqCFGUnusedNodes.Contains(uNodeId))
        {
          continue;
        }

        List<DAGEquationsSet> uList = new List<DAGEquationsSet>();
        SortedSet<long> uVars = new SortedSet<long>();

        foreach (KeyValuePair<long, DAGLinEquationsSet> eqsSetPair in linEquationContext.KSetZetaLinProgEqsSets)
        {
          SortedDictionary<long, long> nodeToVar = eqsSetPair.Value.NodeToVar;

          if (nodeToVar.ContainsKey(uNodeId))
          {
            uList.Add(eqsSetPair.Value);
          }
        }

        if (!stCommNodes.Contains(uNodeId))
        {
          long uNodeVar = linEquationContext.TArbSeqCFGLinProgEqsSet.NodeToVar[uNodeId];

          SortedDictionary<long, RationalNumber> coeffs =
            new SortedDictionary<long, RationalNumber>
              {
                [uNodeVar] = RationalNumber.Const_1
              };

          long equation = linEquationContext.TCPELinProgMatrix.AddEquation(coeffs, EquationKind.Equal, RationalNumber.Const_0);
          tcpeLinProgEqsSet.AddEquation(equation);
        }
        else
        {
          foreach (DAGEquationsSet eqsSet in uList)
          {
            SortedDictionary<long, RationalNumber> coeffs = new SortedDictionary<long, RationalNumber>();

            long commVar = eqsSet.NodeToVar[uNodeId];
            coeffs[commVar] = RationalNumber.Const_1;

            long graphVar = linEquationContext.TArbSeqCFGLinProgEqsSet.NodeToVar[uNodeId];
            coeffs[graphVar] = RationalNumber.Const_Neg1;

            long equation = linEquationContext.TCPELinProgMatrix.AddEquation(coeffs, EquationKind.Equal, RationalNumber.Const_0);
            tcpeLinProgEqsSet.AddEquation(equation);
          }
        }
      }
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
