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
        tapeSegContext.TapeSegTConsistPath.ConvertAll(uId =>
          new KeyValuePair<long, ComputationStep>(uId, meapContext.TArbSeqCFG.IdToNodeInfoMap[uId].CompStep));

      pathCompSteps.ForEach(s =>
        {
          log.InfoFormat(
            "id = {0}, comp.step = {1}",
            s.Key.ToString(), s.Value);
        });

      TMInstance tmInstance = new
        (
          meapContext.MEAPSharedContext.MNP,
          meapContext.MEAPSharedContext.Input
        );

      meapContext.MEAPSharedContext.MNP.PrepareTapeFwd(
        meapContext.MEAPSharedContext.Input,
        tmInstance);

      foreach (KeyValuePair<long, ComputationStep> compStepPair in pathCompSteps)
      {
        ComputationStep compStep = compStepPair.Value;

         TMInstance.MoveToNextConfiguration(
           new StateSymbolDirectionTriple
             (
               state: compStep.qNext,
               symbol: compStep.sNext,
               direction: compStep.m,
               shift: compStep.Shift
             ),
           tmInstance);

        if (tmInstance.IsInFinalState())
        {
          break;
        }
      }

      tapeSegContext.TapeSegOutput = meapContext.MEAPSharedContext.MNP.GetOutput(
        tmInstance,
        meapContext.mu,
        (uint)meapContext.MEAPSharedContext.Input.Length);
    }

    #endregion

    #region private members

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType);

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
