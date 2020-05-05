////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using EnsureThat;
using Core;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public interface ICheckDataStructures
  {
    #region public members

    void CheckTASGHasNoBackAndCrossEdges(DAG dag);
    void CheckCommoditiesHaveNoSingleNodes(MEAPContext meapContext);

    void CheckTASGNodesHaveTheSameSymbol(MEAPContext meapContext);
    void CheckNCGNodesHaveTheSameSymbol(MEAPContext meapContext);

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
