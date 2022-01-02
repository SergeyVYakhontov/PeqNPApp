////////////////////////////////////////////////////////////////////////////////////////////////////

namespace OrdinaryExamplesAppSingleThread
{
  public class TPLOptions : ITPLOptions
  {
    public byte DeterminePathRunnersCount { get => 2; }
    public byte TapeSegRunnersCount { get => 1; }
    public byte SlotsMThRDAProcessCount { get => 1; }

    public byte LinEqSetRunnersCount { get => 1; }
    public byte GaussElimRunnersCount { get => 1; }
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
