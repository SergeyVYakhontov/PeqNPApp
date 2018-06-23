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
  public class TapeSegRunner : TapeSegContextBase, IObjectWithId, ITPLCollectionItem
  {
    #region Ctors

    public TapeSegRunner(
      long id,
      MEAPContext meapContext,
      TapeSegContext tapeSegContext,
      List<TapeSegRunnerState> allowedStates
      )
      :base(meapContext, tapeSegContext)
    {
      this.Id = id;
      tapeSegRunnerStateTable = new TapeSegRunnerStateTable(allowedStates);
    }

    #endregion

    #region public members

    public long Id { get; set; }

    public TapeSegRunnerState TapeSegRunnerState =>
      tapeSegRunnerStateTable.CurrentState;

    public TapeSegContext TapeSegContext => tapeSegContext;
    public bool Done { get; set; }

    public void Init()
    {
      tapeSegContext.TapeSegPathExists = false;
      tapeSegContext.TapeSegPathFound = false;
      tapeSegContext.TapeSegTConsistPath = new List<long>();
      tapeSegContext.TapeSegOutput = Array.Empty<int>();
      tapeSegContext.TCPELinProgSolution = null;

      ILinEqsAlgorithmProvider linEqsAlgorithmProvider = configuration.Get<ILinEqsAlgorithmProvider>();

      tcpepSolver = linEqsAlgorithmProvider.GetTCPEPSolver(meapContext, tapeSegContext);
      tcpepSolver.Init(tapeSegContext.TapeSeg);
    }

    public void Run()
    {
      log.InfoFormat(
        "TapeSeg = {0} : {1} {2}",
        tapeSegContext.TapeSeg.ToString(),
        Id, TapeSegRunnerState);

      lock (meapContext.MEAPSharedContext)
      {
        if (meapContext.InCancelationState())
        {
          log.Debug("Cancelation requested");

          Done = true;

          return;
        }
      }

      AppStatistics appStatistics = configuration.Get<AppStatistics>();

      switch (tapeSegRunnerStateTable.CurrentState)
      {
        case TapeSegRunnerState.CheckKZetaGraphs:
          tcpepSolver.CheckKZetaGraphs();
          break;

        case TapeSegRunnerState.ReduceCommodities:
          tcpepSolver.ReduceCommodities();
          appStatistics.ReduceCommodities++;
          break;

        case TapeSegRunnerState.RunGaussElimination:
          tcpepSolver.RunGaussElimination();
          appStatistics.RunGaussElimination++;
          break;

        case TapeSegRunnerState.RunLinearProgram:
          tcpepSolver.RunLinearProgram();
          Done = true;
          break;
      }

      tapeSegRunnerStateTable.MoveToNextState();

      if (tcpepSolver.Done)
      {
        tapeSegRunnerStateTable.MoveToDoneState();
        Done = true;
      }
    }

    #endregion

    #region private members

    private static readonly IKernel configuration = Core.AppContext.Configuration;
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private readonly TapeSegRunnerStateTable tapeSegRunnerStateTable;
    private TCPEPSolver tcpepSolver;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
