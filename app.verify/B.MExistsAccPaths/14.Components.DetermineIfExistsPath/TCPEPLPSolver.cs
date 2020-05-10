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
  public class TCPEPLPSolver : TCPEPSolver
  {
    #region Ctors

    public TCPEPLPSolver(MEAPContext meapContext, TapeSegContext tapeSegContext)
      : base(meapContext, tapeSegContext)
    {
      this.configuration = Core.AppContext.GetConfiguration();
    }

    #endregion

    #region public methods

    public override void Init(LongSegment tapeSeg)
    {
      tapeSegContext.KSetCommodities = new SortedDictionary<long, Commodity>();
      tapeSegContext.KSetZetaSubset = new SortedDictionary<long, SortedSet<long>>();

      tapeSeg.ForEach(e => tapeSegContext.KSetZetaSubset[(long)e] = new SortedSet<long>());

      meapContext.KSetZetaSets.Where(s => tapeSeg.Contains(s.Key)).ToList()
        .ForEach(
          p =>
          {
            List<Commodity> commodities = p.Value;
            commodities.ForEach(w => tapeSegContext.KSetCommodities[w.Id] = w);

            tapeSegContext.KSetZetaSubset[p.Key].UnionWith(
              p.Value.Select(v => v.Id));
          });

      tapeSegContext.TArbSeqCFGUnusedNodes = new SortedSet<long>();
    }

    public override void CheckKZetaGraphs()
    {
      if (!tapeSegContext.KSetZetaSubset.Any())
      {
        Done = true;

        return;
      }

      checkKZetaGraphs = new CheckKZetaGraphs(meapContext, tapeSegContext);
      checkKZetaGraphs.Init();

      checkKZetaGraphs.Run();

      if (checkKZetaGraphs.ThereIsNoTConsistPath)
      {
        Done = true;

        return;
      }

      if (checkKZetaGraphs.TConsistPathFound)
      {
        Done = true;
      }
    }

    public override void ReduceCommodities()
    {
      if (!tapeSegContext.KSetZetaSubset.Any())
      {
        Done = true;

        return;
      }

      tcpepOptimizer = new TCPEPOptimizer(meapContext, tapeSegContext);
      tcpepOptimizer.Init();

      tcpepOptimizer.Step1();

      if (tcpepOptimizer.ThereIsNoTConsistPath)
      {
        Done = true;

        return;
      }

      if (tcpepOptimizer.TConsistPathFound)
      {
        Done = true;
      }
    }

    public override void RunGaussElimination()
    {
      if (meapContext.MEAPSharedContext.MNP.LotOfAcceptingPaths)
      {
        return;
      }

      if (meapContext.InCancelationState())
      {
        log.Debug("Cancelation requested");

        Done = true;

        return;
      }

      while (!tcpepOptimizer.Finished)
      {
        tcpepOptimizer.Step2();

        if (tcpepOptimizer.ThereIsNoTConsistPath)
        {
          Done = true;

          return;
        }

        if (tcpepOptimizer.TConsistPathFound)
        {
          Done = true;

          return;
        }

        if (meapContext.InCancelationState())
        {
          log.Debug("Cancelation requested");

          Done = true;

          return;
        }
      }
    }

    public override void RunLinearProgram()
    {
      if (meapContext.InCancelationState())
      {
        log.Debug("Cancelation requested");

        Done = true;

        return;
      }

      FindLPSolution();
      Done = true;
    }

    #endregion

    #region private members

    private readonly IReadOnlyKernel configuration;

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType);

    private static readonly Object objectToLock = new Object();
    private TCPEPOptimizer tcpepOptimizer;
    private CheckKZetaGraphs checkKZetaGraphs;
    private readonly LinEquationContext linEquationContext = new LinEquationContext();

    private void CreatePartialTConsistPathEqsSet()
    {
      foreach (long uNodeId in tapeSegContext.PartialTConsistPath)
      {
        long pathVar = linEquationContext.TArbSeqCFGLinProgEqsSet.NodeToVar[uNodeId];

        SortedDictionary<long, RationalNumber> coeffs = new SortedDictionary<long, RationalNumber>
        {
          [pathVar] = RationalNumber.Const_1
        };

        long equation = linEquationContext.TCPELinProgMatrix.AddEquation(
          coeffs, EquationKind.Equal, RationalNumber.Const_1);
        linEquationContext.TCPELinProgEqsSet.Equations.Add(equation);
      }
    }

    private void CreateObjVarsEqsSet()
    {
      long objectiveVar = linEquationContext.TArbSeqCFGLinProgEqsSet.NodeToVar[meapContext.TArbSeqCFG.GetSourceNodeId()];

      SortedDictionary<long, RationalNumber> coeffs =
        new SortedDictionary<long, RationalNumber>
          {
            [objectiveVar] = RationalNumber.Const_1
          };

      long equation = linEquationContext.TCPELinProgMatrix.AddEquation(
        coeffs, EquationKind.Equal, RationalNumber.Const_1);
      linEquationContext.TCPELinProgEqsSet.Equations.Add(equation);

      linEquationContext.TCPELinProgMatrix.ObjFuncVars.Add(objectiveVar);
    }

    private bool GetTCPELinProgSolution()
    {
      linEquationContext.TCPELinProgMatrix = new LinEquationsMatrix();
      ILinEqsAlgorithmProvider linEqsAlgorithmProvider = configuration.Get<ILinEqsAlgorithmProvider>();

      LinEqsSetBuilder tcpeLinProgBuilder =
        linEqsAlgorithmProvider.GetLinEquationsSetBuilder(
          meapContext,
          tapeSegContext,
          linEquationContext);

      if (!tcpeLinProgBuilder.CreateTCPEPLinProgEqsSet())
      {
        return false;
      }

      CreatePartialTConsistPathEqsSet();
      CreateObjVarsEqsSet();

      AppStatistics appStatistics = configuration.Get<AppStatistics>();

      lock (objectToLock)
      {
        appStatistics.RunLinearProgram++;

        String linProgQuery = linEquationContext.TCPELinProgMatrix.GetLinProgQuery();
        tapeSegContext.MathQueryString =
          linearProgramStr + "[" +
          linProgQuery + "," +
          "Method->\"InteriorPoint\"]";

        MathKernelConnector mathKernelConnector = configuration.Get<MathKernelConnector>();

        String linProgOutput = mathKernelConnector.MathKernel.EvaluateToOutputForm(tapeSegContext.MathQueryString, 0);
        if (linProgOutput.Contains(linearProgramStr))
        {
          return false;
        }

        log.Info("LP solution found");

        log.Debug(linProgOutput);

        mathKernelConnector.MathKernel.Evaluate(tapeSegContext.MathQueryString);
        mathKernelConnector.MathKernel.WaitForAnswer();

        tapeSegContext.TCPELinProgSolution = mathKernelConnector.MathKernel.GetSingleArray();

        return true;
      }
    }

    private void FindLPSolution()
    {
      log.Info("Finding LP solution");

      tapeSegContext.TapeSegPathExists = false;
      tapeSegContext.TapeSegPathFound = false;
      tapeSegContext.TapeSegTConsistPath = new List<long>();
      tapeSegContext.TapeSegOutput = Array.Empty<int>();

      StrongConnCommsBuilder strongConnCommsBuilder = new StrongConnCommsBuilder(meapContext, tapeSegContext);
      strongConnCommsBuilder.Run();

      if (tapeSegContext.KSetZetaSubset.Count(v => v.Value.Any()) > 1)
      {
        if (!tapeSegContext.KiToZetaToKjIntSet.Any())
        {
          return;
        }
      }

      if (!GetTCPELinProgSolution())
      {
        return;
      }

      tapeSegContext.TapeSegPathExists = true;

      LPTConsistPathFinder tConsistPathFinder = new LPTConsistPathFinder(meapContext, tapeSegContext, linEquationContext);
      tConsistPathFinder.FindTConsistPath();

      if (tapeSegContext.TapeSegPathFound)
      {
        tConsistPathFinder.ExtractTConsistSeq();

        log.Info("Path, based on LP solution, found");
      }
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
