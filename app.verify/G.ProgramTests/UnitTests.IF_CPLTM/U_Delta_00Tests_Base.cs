////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Ninject;
using Xunit;
using Core;
using ExistsAcceptingPath;
using MTExtDefinitions;
using VerifyResults;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace UnitTests
{
  public class U_CPLTM_Delta_Tests_Base
  {
    #region Ctors

    static U_CPLTM_Delta_Tests_Base()
    {
      log4net.Repository.ILoggerRepository logRepository = log4net.LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
      log4net.Config.XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
    }

    #endregion

    #region private members

    private readonly IKernel configuration = Core.AppContext.Configuration;

    protected int frameLength;
    protected int frameStart1;
    protected int frameStart2;
    protected int frameStart3;
    protected int frameEnd4;

    protected void Setup(int inputLength)
    {
      configuration.Load<IntegerFactExamplesAppCPLTM.AppNinjectModule>();

      frameLength = MTExtDefinitions.v2.IF_NDTM.FrameLength(inputLength);
      frameStart1 = MTExtDefinitions.v2.IF_NDTM.FrameStart1(inputLength);
      frameStart2 = MTExtDefinitions.v2.IF_NDTM.FrameStart2(inputLength);
      frameStart3 = MTExtDefinitions.v2.IF_NDTM.FrameStart3(inputLength);
      frameEnd4 = MTExtDefinitions.v2.IF_NDTM.FrameEnd4(inputLength);
    }

    protected void ResetNinjectKernel()
    {
      List<Ninject.Modules.INinjectModule> modules = configuration.GetModules().ToList();
      modules.ForEach(m => configuration.Unload(m.Name));
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
