////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using EnsureThat;
using Ninject;
using Core;
using ExistsAcceptingPath;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace VerifyResults
{
  public class OrdinaryApplication : IApplication
  {
    #region public members

    public OrdinaryApplication()
    {
      this.configuration = Core.AppContext.GetConfiguration();
    }

    #endregion

    #region public members

    public void Run(string[] args)
    {
      AppStatistics appStatistics = configuration.Get<AppStatistics>();
      ICommonOptions commonOptions = configuration.Get<ICommonOptions>();

      try
      {
        MathKernelConnector mathKernelConnector = configuration.Get<MathKernelConnector>();

        mathKernelConnector.LoadMathKernel();
        Ensure.That(ThreadPool.SetMinThreads(32, 2)).IsTrue();

        IVerificator verificator = configuration.Get<OrdinaryVerificator>();
        verificator.Run();

        mathKernelConnector.UnloadMathKernel();
      }
      catch (Exception exception)
      {
        appStatistics.ThereWereErrors = true;
        log.Error(exception.ToString());
      }
    }

    #endregion

    #region private members

    private readonly IReadOnlyKernel configuration;

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////

