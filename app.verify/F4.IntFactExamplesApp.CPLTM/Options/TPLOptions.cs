////////////////////////////////////////////////////////////////////////////////////////////////////

namespace IntegerFactExamplesAppCPLTM
{
  public class TPLOptions : ITPLOptions
  {
    public byte DeterminePathRunnersCount { get => 1; }
    public byte TapeSegRunnersCount { get => 1; }
    public byte SlotsMThRDAProcessCount { get => 1; }

    public byte LinEqSetRunnersCount { get =>
      throw new InvalidOperationException("The option cannot be used in this context"); }

    public byte GaussElimRunnersCount { get =>
      throw new InvalidOperationException("The option cannot be used in this context"); }
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
