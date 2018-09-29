////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public abstract class MEAPCurrentStep : IMeapCurrentStep
  {
    #region ctors

    protected MEAPCurrentStep(MEAPContext meapContext)
    {
      this.meapContext = meapContext;
    }

    #endregion

    #region public members

    public abstract void Run(uint[] states);

    public void RetrievePath()
    {
      log.Info("Start retrieve path");

      while (true)
      {
        TapeSegContext currentTapeSegContext = meapContext.TapeSegContext;

        foreach (long forwardNodeId in currentTapeSegContext.TConsistPathNodes)
        {
          TapeSegContext forwardTapeSegContext = new TapeSegContext(currentTapeSegContext);
          forwardTapeSegContext.PartialTConsistPath.Add(forwardNodeId);

          meapContext.TapeSegContext = forwardTapeSegContext;

          DetermineIfExistsTCPath determineIfExistsTCSeq = new DetermineIfExistsTCPath(meapContext);
          determineIfExistsTCSeq.RunToRetrievePath();

          if (meapContext.TapeSegContext.TapeSegPathFound)
          {
            CopyResultFromTapeSegContext();

            return;
          }

          if (meapContext.TapeSegContext.TapeSegPathExists)
          {
            break;
          }
        }
      }
    }

    #endregion

    #region private members

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(
      System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected readonly MEAPContext meapContext;

    protected void ComputeNodeVLevels(DAG dag)
    {
      log.Info("Compute nodeVLevels");

      meapContext.NodeVLevels = new SortedDictionary<long, SortedSet<long>>();
      meapContext.NodeToLevel = new SortedDictionary<long, long>();
      meapContext.EdgeToLevel = new SortedDictionary<long, long>();

      DAG.DFS(
        dag.s,
        GraphDirection.Forward,
        (u, level) =>
        {
          AppHelper.TakeValueByKey(meapContext.NodeVLevels, level,
            () => new SortedSet<long>()).Add(u.Id);

          meapContext.NodeToLevel[u.Id] = level;
        },
        (e, level) =>
        {
          meapContext.EdgeToLevel[e.Id] = level;
        }
        );
    }

    protected void CopyResultFromTapeSegContext()
    {
      meapContext.PathExists = meapContext.TapeSegContext.TapeSegPathExists;
      meapContext.PathFound = meapContext.TapeSegContext.TapeSegPathFound;
      meapContext.TConsistPath = new List<long>(meapContext.TapeSegContext.TapeSegTConsistPath);
      meapContext.Output = AppHelper.CreateArrayCopy(meapContext.TapeSegContext.TapeSegOutput);
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
