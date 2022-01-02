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
