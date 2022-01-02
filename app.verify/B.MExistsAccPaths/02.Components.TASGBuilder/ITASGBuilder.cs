////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public interface ITASGBuilder
  {
    MEAPContext meapContext { get; set; }
    MEAPSharedContext MEAPSharedContext { get; set; }

    long TapeLBound { get; }
    long TapeRBound { get; }

    void Init();
    void CreateTArbitrarySeqGraph();
    void CreateTArbSeqCFG(uint[] states);
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
