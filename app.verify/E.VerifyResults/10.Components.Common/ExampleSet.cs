////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;
using Ninject;
using ExistsAcceptingPath;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace VerifyResults
{
  public abstract class ExampleSet
  {
    #region public members

    public bool RunSmallExamples()
    {
      bool result = RunExamples(GetSmallExamples());

      log.InfoFormat(
        "Small examples {0}, errors: {1}",
          Name, !result);

      IReadOnlyKernel configuration = Core.AppContext.GetConfiguration();
      AppStatistics appStatistics = configuration.Get<AppStatistics>();

      appStatistics.ThereWereErrors = (appStatistics.ThereWereErrors || !result);

      return result;
    }

    public bool RunLargeExamples()
    {
      bool result = RunExamples(GetLargeExamples());

      log.InfoFormat(
        "Large examples {0}, errors: {1}",
          Name, !result);

      IReadOnlyKernel configuration = Core.AppContext.GetConfiguration();
      AppStatistics appStatistics = configuration.Get<AppStatistics>();

      appStatistics.ThereWereErrors = (appStatistics.ThereWereErrors || !result);

      return result;
    }

    public bool RunExamples()
    {
      bool smallExamplesResult = RunSmallExamples();
      bool largeExamplesResult = RunLargeExamples();

      return (smallExamplesResult && largeExamplesResult);
    }

    public bool RunRandomExamples()
    {
      return RunExamples(GetRandomExamples(1, 20));
    }

    private bool RunExamples(List<IExample> examples)
    {
      log.InfoFormat("Run example set: {0}", Name);

      bool errorFound = false;

      foreach (IExample currentExample in examples)
      {
        log.Info(currentExample.Name);

        IList<IRunnable> runnables = currentExample.GetRunnables();
        List<bool> decideOutputs = new List<bool>();
        List<int[]> computeOutputs = new List<int[]>();

        while (runnables.Any())
        {
          GC.Collect();

          IRunnable currentRunnable = runnables[0];
          runnables.RemoveAt(0);

          log.InfoFormat("Runnable decide start: {0}", currentRunnable.Kind);

          int[] currentInput = currentExample.Input;
          bool currDecideOutput = currentRunnable.Decide(currentInput);

          log.InfoFormat("Runnable decide end: {0}", currentRunnable.Kind);

          log.Info("Runnable");
          log.InfoFormat("Input: {0}", Core.AppHelper.ArrayToString(currentInput));

          if (!currentRunnable.ComputationFinished)
          {
            log.Info("Computation not finished");

            continue;
          }

          log.InfoFormat("Output: {0}", currDecideOutput);
          decideOutputs.Add(currDecideOutput);

          GC.Collect();

          int[] currComputeOutput = Array.Empty<int>();

          if (currDecideOutput)
          {
            log.InfoFormat("Runnable compute start: {0}", currentRunnable.Kind);
            currComputeOutput = currentRunnable.Compute(currentInput);
            log.InfoFormat("Runnable decide end: {0}", currentRunnable.Kind);

            log.InfoFormat("Input: {0}", Core.AppHelper.ArrayToString(currentInput));
            log.InfoFormat("Output: {0}", Core.AppHelper.ArrayToString(currComputeOutput));
          }

          if (!GetCheckAlgorithm().CheckDecide(currentInput, currDecideOutput))
          {
            errorFound = true;
            log.Info("[ERROR]: Output is incorrect");
          }

          if (currDecideOutput != decideOutputs[0])
          {
            errorFound = true;
            log.Info("[ERROR]: Outputs are different");
          }

          if (!currDecideOutput)
          {
            continue;
          }

          if (currentRunnable.RunCheckAlgorithm)
          {
            if (!GetCheckAlgorithm().CheckCompute(currentInput, currComputeOutput))
            {
              errorFound = true;
              log.Info("[ERROR]: Output is incorrect");

              continue;
            }
          }

          if (!currentRunnable.CompareOutputs)
          {
            continue;
          }

          computeOutputs.Add(currComputeOutput);

          if (!currComputeOutput.SequenceEqual(computeOutputs[0]))
          {
            errorFound = true;

            log.Info("[ERROR]: Outputs are different");
          }
        }

        GC.Collect();
      }

      return !errorFound;
    }

    public abstract string Name { get; set; }

    public abstract List<IExample> GetSmallExamples();
    public abstract List<IExample> GetLargeExamples();

    public abstract List<IExample> GetRandomExamples(
      int count,
      int inputLength);

    public abstract ICheckAlgorithm GetCheckAlgorithm();

    #endregion

    #region private members

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType);

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
