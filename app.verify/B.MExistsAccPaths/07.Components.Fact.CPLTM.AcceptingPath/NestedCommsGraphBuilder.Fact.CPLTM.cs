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
  public class NestedCommsGraphBuilderFactCPLTM
  {
    #region Ctors

    public NestedCommsGraphBuilderFactCPLTM(MEAPContext meapContext)
    {
      this.CPLTMInfo = meapContext.MEAPSharedContext.CPLTMInfo;
      this.meapContext = meapContext;
    }

    #endregion

    #region public members

    public void Setup()
    {
      meapContext.muToNestedCommsGraphPair = new SortedDictionary<long, FwdBkwdNCommsGraphPair>();
    }

    public void Run()
    {
      log.Info("Creating nested commodities graphs");

      FilloutNodeToCommoditiesMap();

      foreach(long kStep in CPLTMInfo.KTapeLRSubseq())
      {
        FwdBkwdNCommsGraphPair fwdBkwdNCommsGraphPair = AppHelper.TakeValueByKey(
          meapContext.muToNestedCommsGraphPair,
          kStep,
          () => new FwdBkwdNCommsGraphPair());
      }
    }

    #endregion

    #region private members

    private static readonly IKernel configuration = Core.AppContext.Configuration;
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private readonly ICPLTMInfo CPLTMInfo;
    private readonly MEAPContext meapContext;

    private readonly SortedDictionary<long, LinkedList<long>> sNodeToCommoditiesMap =
      new SortedDictionary<long, LinkedList<long>>();
    private readonly SortedDictionary<long, LinkedList<long>> tNodeToCommoditiesMap =
      new SortedDictionary<long, LinkedList<long>>();

    private void FilloutNodeToCommoditiesMap()
    {
      foreach(KeyValuePair<long, Commodity> idCommPair in meapContext.Commodities)
      {
        long commId = idCommPair.Key;
        Commodity commodity = idCommPair.Value;

        LinkedList<long> commsAtSNode = AppHelper.TakeValueByKey(
          sNodeToCommoditiesMap,
          commodity.sNodeId,
          () => new LinkedList<long>());

        commsAtSNode.AddLast(commId);

        LinkedList<long> commsAtTNode = AppHelper.TakeValueByKey(
          tNodeToCommoditiesMap,
          commodity.tNodeId,
          () => new LinkedList<long>());

        commsAtTNode.AddLast(commId);
      }
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
