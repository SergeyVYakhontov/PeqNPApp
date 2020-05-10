////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ninject;
using EnsureThat;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  using NCGraphType = TypedDAG<NestedCommsGraphNodeInfo, StdEdgeInfo>;
  using NCGCommonPathGraphType = TypedDAG<NCGCommonPathGraphNodeInfo, NCGCommonPathGraphEdgeInfo>;

  public class NCGCommonPathGraphBuilder
  {
    #region Ctors

    public NCGCommonPathGraphBuilder(MEAPContext meapContext)
    {
      this.meapContext = meapContext;
      this.CPLTMInfo = meapContext.MEAPSharedContext.CPLTMInfo;
    }

    #endregion

    #region public members

    public void Setup()
    {
      meapContext.NCGCommonPathGraph = new NCGCommonPathGraphType("NCGCommonPathGraphType");
    }

    public void Run()
    {
      log.Info("Compute NCGCommonPathGraph");

      KeyValuePair<long, FwdBkwdNCommsGraphPair>[] nestedCommsGraphPair =
        meapContext.muToNestedCommsGraphPair.ToArray();

      for (int i = 0; i <= (nestedCommsGraphPair.Length - 2); i++)
      {
        long mu = nestedCommsGraphPair[i].Key;

        FwdBkwdNCommsGraphPair leftFwdBkwdNCommsGraphPair = nestedCommsGraphPair[i].Value;
        FwdBkwdNCommsGraphPair rightFwdBkwdNCommsGraphPair = nestedCommsGraphPair[i + 1].Value;
      }
    }

    #endregion

    #region private members

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod()!.DeclaringType);

    private readonly MEAPContext meapContext;
    private readonly ICPLTMInfo CPLTMInfo;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
