using System;
using System.IO;

namespace FunctionalTests
{
  public static class Program
  {
    public static void Main()
    {
      log4net.Repository.ILoggerRepository logRepository = log4net.LogManager.GetRepository(
        System.Reflection.Assembly.GetEntryAssembly());
      log4net.Config.XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

      DateTime startDT = DateTime.Now;

      try
      {
        F_01_Application_Tests.RunTests();
        F_02_MEAPTests.RunTests();
        F_03_IF_NDTM_Comms_Tests.RunTests();
        F_04_IF_NDTM_CPLTM_Tests.RunTests();

        DateTime endDT = DateTime.Now;

        Console.WriteLine("Passed: all the tests; {0}, {1}", startDT, endDT);
      }
      catch
      {
        DateTime endDT = DateTime.Now;

        Console.WriteLine("Running tests error; {0}, {1}", startDT, endDT);
        throw;
      }
    }
  }
}
