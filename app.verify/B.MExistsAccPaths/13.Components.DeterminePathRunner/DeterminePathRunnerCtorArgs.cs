////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public sealed class DeterminePathRunnerCtorArgs
  {
    #region public members

    public MEAPSharedContext MEAPSharedContext { get; set; } = default!;

    public OneTapeTuringMachine tMachine { get; set; } = default!;
    public int[] input { get; set; } = Array.Empty<int>();
    public ulong currentMu { get; set; }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
