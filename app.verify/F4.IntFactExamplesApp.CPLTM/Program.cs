////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Core;
using Ninject;
using ExistsAcceptingPath;
using VerifyResults;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace IntegerFactExamplesAppCPLTM
{
  public static class Program
  {
    #region public members

    public static void Main(string[] args)
    {
      if (!args.Contains("test"))
      {
        Console.BufferWidth = Console.LargestWindowWidth;
        Console.BufferHeight = Console.LargestWindowHeight;

        Console.SetWindowPosition(0, 0);
        Console.SetWindowSize(150, 35);
      }

      log4net.Repository.ILoggerRepository logRepository = log4net.LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
      log4net.Config.XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

      Setup();

      IApplication application = configuration.Get<IntegerFactApplication>();
      application.Run(args);

      if (!args.Contains("test"))
      {
        Console.ReadKey();
      }
    }

    #endregion

    #region private members

    private static readonly IKernel configuration = Core.AppContext.Configuration;
    private static readonly log4net.ILog log =
      log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private static void Setup()
    {
      log.Info("Setup");

      configuration.Load<AppNinjectModule>();

      IExampleSetProvider exampleSetProvider = configuration.Get<IExampleSetProvider>();
      exampleSetProvider.ExampleSets.Add(exampleSetProvider.IF_ExampleSetA);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
