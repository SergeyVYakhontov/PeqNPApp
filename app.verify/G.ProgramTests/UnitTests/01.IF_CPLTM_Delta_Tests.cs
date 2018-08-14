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

namespace ProgramTests
{
  [TestCaseOrderer("ProgramTests.AlphabeticalTestOrderer", "G.ProgramTests")]
  public sealed class IF_CPLTM_Delta_05CompareResults_Tests : IDisposable
  {
    #region Ctors

    static IF_CPLTM_Delta_05CompareResults_Tests()
    {
      log4net.Repository.ILoggerRepository logRepository = log4net.LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
      log4net.Config.XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
    }

    #endregion

    #region public members

    public void Dispose()
    {
      ResetNinjectKernel();
    }

    [Fact]
    public void T05_CompareResults1_Test()
    {
      configuration.Load<IntegerFactExamplesAppCPLTM.AppNinjectModule>();

      int[] input = new int[] { 1, 0, 1, 0 }.Reverse().ToArray();

      MTExtDefinitions.v2.IF_NDTM tm = new MTExtDefinitions.v2.IF_NDTM(input.Length);
      tm.Setup();

      TMInstance tmInstance = new TMInstance(tm, input);
      tm.PrepareTapeFwd(input, tmInstance);

      Setup(input.Length);

      tmInstance.SetTapeSymbol(1 + frameLength + 1, 0);
      tmInstance.SetTapeSymbol(1 + frameLength + 2, 1);
      tmInstance.SetTapeSymbol(1 + frameLength + 3, MTExtDefinitions.v2.IF_NDTM.markB0);
      tmInstance.SetTapeSymbol(1 + frameLength + 4, MTExtDefinitions.v2.IF_NDTM.markB1);
      tmInstance.SetTapeSymbol(1 + frameLength + 5, OneTapeTuringMachine.blankSymbol);

      DetermStepsEmulator dse = new DetermStepsEmulator(tm.Delta, tmInstance);
      dse.SetupConfiguration(
        1 + frameLength + 5,
        (uint)MTExtDefinitions.v2.IF_NDTM.CompareStates.StartComparing);

      dse.DoStepN(12);

      Assert.True(tmInstance.CellIndex() == 0);
      Assert.True(tmInstance.TapeSymbol(0) == MTExtDefinitions.v2.IF_NDTM.delimiter0);

      Assert.True(tmInstance.TapeSymbol(frameStart1) == MTExtDefinitions.v2.IF_NDTM.delimiter1);
      Assert.True(tmInstance.TapeSymbol(frameStart2) == MTExtDefinitions.v2.IF_NDTM.delimiter2);
      Assert.True(tmInstance.TapeSymbol(frameStart3) == MTExtDefinitions.v2.IF_NDTM.delimiter3);
      Assert.True(tmInstance.TapeSymbol(frameEnd4) == MTExtDefinitions.v2.IF_NDTM.delimiter4);
    }

    #endregion

    #region private members

    private readonly IKernel configuration = Core.AppContext.Configuration;

    private int frameLength;
    private int frameStart1;
    private int frameStart2;
    private int frameStart3;
    private int frameEnd4;

    private void Setup(int inputLength)
    {
      frameLength = MTExtDefinitions.v2.IF_NDTM.FrameLength(inputLength);
      frameStart1 = MTExtDefinitions.v2.IF_NDTM.FrameStart1(inputLength);
      frameStart2 = MTExtDefinitions.v2.IF_NDTM.FrameStart2(inputLength);
      frameStart3 = MTExtDefinitions.v2.IF_NDTM.FrameStart3(inputLength);
      frameEnd4 = MTExtDefinitions.v2.IF_NDTM.FrameEnd4(inputLength);
    }

    private void ResetNinjectKernel()
    {
      List<Ninject.Modules.INinjectModule> modules = configuration.GetModules().ToList();
      modules.ForEach(m => configuration.Unload(m.Name));
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
