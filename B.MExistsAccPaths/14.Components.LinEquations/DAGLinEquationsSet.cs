////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using EnsureThat;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public class DAGLinEquationsSet : DAGEquationsSet
  {
    #region Ctors

    public DAGLinEquationsSet(LinEquationsMatrix linProgMatrix, DAG graph)
      : base(graph)
    {
      this.LinProgMatrix = linProgMatrix;
    }

    #endregion

    #region public members

    public LinEquationsMatrix LinProgMatrix { get; private set; }

    public override long AddVar()
    {
      long var = LinProgMatrix.AddVariable();
      FVars.Add(var);

      return var;
    }

    public override long AddVarForNode(long nodeId)
    {
      Ensure.That(NodeToVar.ContainsKey(nodeId)).IsFalse();
      
      long var = LinProgMatrix.AddVariable();
      FVars.Add(var);
      NodeToVar[nodeId] = var;

      return var;
    }

    public override long AddVarForEdge(long edgeId)
    {
      Ensure.That(EdgeToVar.ContainsKey(edgeId)).IsFalse();

      long var = LinProgMatrix.AddVariable();

      HVars.Add(var);
      EdgeToVar[edgeId] = var;

      return var;
    }

    public static DAGLinEquationsSet CreateEqsSetForDAG(
      LinEquationsMatrix linProgMatrix,
      DAG graph,
      SortedSet<long> unusedNodes)
    {
      DAGLinEquationsSet eqsSet = new DAGLinEquationsSet(linProgMatrix, graph);
      
      SortedDictionary<long, long> nodeToVar = eqsSet.NodeToVar;
      SortedDictionary<long, long> edgeToVar = eqsSet.EdgeToVar;

      long sNodeId = graph.GetSourceNodeId();
      eqsSet.sVar = eqsSet.AddVarForNode(sNodeId);

      long tNodeId = graph.GetSinkNodeId();
      eqsSet.tVar = eqsSet.AddVarForNode(tNodeId);

      foreach (long uNodeId in graph.GetInnerNodeIds())
      {
        if (unusedNodes.Contains(uNodeId))
        {
          continue;
        }

        eqsSet.AddVarForNode(uNodeId);
      }

      foreach (DAGEdge e in graph.Edges)
      {
        long edgeId = e.Id;
        eqsSet.AddVarForEdge(edgeId);
      }

      foreach (DAGNode u in graph.Nodes)
      {
        long uNodeId = u.Id;

        if (unusedNodes.Contains(uNodeId))
        {
          continue;
        }

        long uNodeVar = nodeToVar[uNodeId];
        
        SortedDictionary<long, RationalNumber> coeffsIn = new SortedDictionary<long, RationalNumber>();
        coeffsIn[uNodeVar] = RationalNumber.Const_1;

        if (u.InEdges.Any())
        {
          foreach (DAGEdge e in u.InEdges)
          {
            long fromNodeId = e.FromNode.Id;
            long toNodeId = e.ToNode.Id;

            if (unusedNodes.Contains(fromNodeId))
            {
              continue;
            }

            if (unusedNodes.Contains(toNodeId))
            {
              continue;
            }

            long eEdgeVar = edgeToVar[e.Id];
            coeffsIn[eEdgeVar] = RationalNumber.Const_Neg1;
          }

          long equationIn = eqsSet.LinProgMatrix.AddEquation(coeffsIn, EquationKind.Equal, RationalNumber.Const_0);
          eqsSet.AddEquation(equationIn);
        }

        SortedDictionary<long, RationalNumber> coeffsOut = new SortedDictionary<long, RationalNumber>();
        coeffsOut[uNodeVar] = RationalNumber.Const_1;

        if (u.OutEdges.Any())
        {
          foreach (DAGEdge e in u.OutEdges)
          {
            long fromNodeId = e.FromNode.Id;
            long toNodeId = e.ToNode.Id;

            if (unusedNodes.Contains(fromNodeId))
            {
              continue;
            }

            if (unusedNodes.Contains(toNodeId))
            {
              continue;
            }

            long eVar = edgeToVar[e.Id];
            coeffsOut[eVar] = RationalNumber.Const_Neg1;
          }

          long equationOut = eqsSet.LinProgMatrix.AddEquation(coeffsOut, EquationKind.Equal, RationalNumber.Const_0);
          eqsSet.AddEquation(equationOut);
        }
      }

      return eqsSet;
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
