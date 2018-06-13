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
  public class EliminateOverlappingCommsFactTRS
  {
    #region Ctors

    public EliminateOverlappingCommsFactTRS(MEAPContext meapContext)
    {
      this.meapContext = meapContext;
    }

    #endregion

    #region public members

    public void Run()
    {
      log.Info("Computing commodities connectivity");

      foreach (KeyValuePair<long, Commodity> fromIdCommPair in meapContext.Commodities)
      {
        long fromCommId = fromIdCommPair.Key;
        Commodity fromComm = fromIdCommPair.Value;
      }
    }

    #endregion

    #region private members

    private static readonly IKernel configuration = Core.AppContext.Configuration;
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private readonly MEAPContext meapContext;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
