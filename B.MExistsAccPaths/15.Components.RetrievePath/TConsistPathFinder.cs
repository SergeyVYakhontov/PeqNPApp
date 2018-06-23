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
  public abstract class TConsistPathFinder : LinEquationsContextBase
  {
    #region Ctors

    protected TConsistPathFinder(
      MEAPContext meapContext,
      TapeSegContext tapeSegContext,
      LinEquationContext linEquationContext)
      : base(meapContext, tapeSegContext, linEquationContext)
    { }

    #endregion

    #region public members

    public abstract void FindTConsistPath();

    public void ExtractTConsistSeq()
    {
      log.Info("MExistsAcceptingPath.Compute: path");

      tapeSegContext.TapeSegTConsistPath.Remove(tapeSegContext.TapeSegTConsistPath.Last());
      tapeSegContext.TapeSegTConsistPath.ForEach(s =>
        {
          log.InfoFormat("node = {0}", s.ToString());
        });

      List<KeyValuePair<long, ComputationStep>> pathCompSteps =
        tapeSegContext.TapeSegTConsistPath.Select(uId =>
        new KeyValuePair<long, ComputationStep>(uId, meapContext.TArbSeqCFG.IdToInfoMap[uId].CompStep))
          .ToList();

      pathCompSteps.ForEach(s =>
        {
          log.InfoFormat(
            "id = {0}, comp.step = {1}",
            s.Key.ToString(), s.Value.ToString());
        });

      TMInstance tmInstance = new TMInstance(
        meapContext.MEAPSharedContext.MNP,
        meapContext.MEAPSharedContext.Input);

      foreach (KeyValuePair<long, ComputationStep> compStepP in pathCompSteps)
      {
        ComputationStep compStep = compStepP.Value;

         TMInstance.MoveToNextConfiguration(
          new StateSymbolDirectionTriple()
          {
            State = compStep.qNext,
            Symbol = compStep.sNext,
            Direction = compStep.m,
            Shift = compStep.Shift
          },
          tmInstance);

        if (tmInstance.IsInFinalState())
        {
          break;
        }
      }

      tapeSegContext.TapeSegOutput = meapContext.MEAPSharedContext.MNP.GetOutput(
        tmInstance, meapContext.mu, meapContext.MEAPSharedContext.Input.Length);
    }

    #endregion

    #region private members

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
