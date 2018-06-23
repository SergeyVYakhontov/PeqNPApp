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
  public class TapeSegRunnerStateTable
  {
    #region Ctors

    public TapeSegRunnerStateTable(IReadOnlyCollection<TapeSegRunnerState> allowedStates)
    {
      this.allowedStates = allowedStates;

      CurrentState = allowedStates.First();
    }

    #endregion

    #region public members

    public TapeSegRunnerState CurrentState { get; private set; }

    public void MoveToNextState()
    {
      (TapeSegRunnerState from, TapeSegRunnerState to) =
        stateTable.Where(t => (allowedStates.Contains(t.from) && allowedStates.Contains(t.to)))
          .First(t => (t.from == CurrentState));

      CurrentState = to;
    }

    public void MoveToDoneState()
    {
      CurrentState = TapeSegRunnerState.Done;
    }

    #endregion

    #region private members

    private readonly IReadOnlyCollection<TapeSegRunnerState> allowedStates;

    private readonly List<(TapeSegRunnerState from, TapeSegRunnerState to)> stateTable =
      new List<(TapeSegRunnerState from, TapeSegRunnerState to)>
        {
          (TapeSegRunnerState.CheckKZetaGraphs, TapeSegRunnerState.ReduceCommodities),
          (TapeSegRunnerState.ReduceCommodities, TapeSegRunnerState.RunGaussElimination),
          (TapeSegRunnerState.RunGaussElimination, TapeSegRunnerState.RunLinearProgram),
          (TapeSegRunnerState.ReduceCommodities, TapeSegRunnerState.RunLinearProgram),
          (TapeSegRunnerState.RunLinearProgram, TapeSegRunnerState.Done)
        };

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
