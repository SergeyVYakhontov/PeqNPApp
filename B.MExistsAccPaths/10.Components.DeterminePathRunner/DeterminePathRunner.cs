////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ninject;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public abstract class DeterminePathRunner : ITPLCollectionItem, IDeterminePathRunner
  {
    #region Ctors

    protected DeterminePathRunner(DeterminePathRunnerCtorArgs determinePathRunnerCtorArgs)
    {
      this.MEAPSharedContext = determinePathRunnerCtorArgs.MEAPSharedContext;

      this.tMachine = determinePathRunnerCtorArgs.tMachine;
      this.input = determinePathRunnerCtorArgs.input;
      this.currentMu = determinePathRunnerCtorArgs.currentMu;
    }

    #endregion

    #region public members

    public bool Result { get; set; }
    public int[] Output {  get; set; }
    public bool Done { get; set; }

    public abstract void Run();

    #endregion

    #region private members

    protected static readonly IKernel configuration = Core.AppContext.Configuration;
    protected static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected readonly MEAPSharedContext MEAPSharedContext;
    protected readonly OneTapeTuringMachine tMachine;
    protected readonly int[] input;
    protected readonly long currentMu;

    protected MEAPContext meapContext;
    protected long baseMu;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
