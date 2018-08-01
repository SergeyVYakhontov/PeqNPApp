////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Ninject;
using Core;
using ExistsAcceptingPath;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace VerifyResults
{
  public class OrdinaryVerificator : Verificator
  {
    #region public members

    public override void Run()
    {
      IExampleSetProvider exampleSetProvider = configuration.Get<IExampleSetProvider>();

      foreach (ExampleSet exampleSet in exampleSetProvider.ExampleSets)
      {
        exampleSet.RunExamples();
      }

      AppStatistics appStatistics = configuration.Get<AppStatistics>();

      log.InfoFormat("ReduceCommodities: {0}", appStatistics.ReduceCommodities);
      log.InfoFormat("RunGaussElimination: {0}", appStatistics.RunGaussElimination);
      log.InfoFormat("RunLinearProgram: {0}", appStatistics.RunLinearProgram);

      log.InfoFormat("There were errors: {0}", appStatistics.ThereWereErrors);
    }

    #endregion

    #region private members

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
