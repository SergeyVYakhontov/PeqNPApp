////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public abstract class TapeSegContextBase
  {
    #region Ctors

    protected TapeSegContextBase(
      MEAPContext meapContext,
      TapeSegContext tapeSegContext)
    {
      this.meapContext = meapContext;
      this.tapeSegContext = tapeSegContext;
    }

    #endregion

    #region private members

    protected readonly MEAPContext meapContext;
    protected readonly TapeSegContext tapeSegContext;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
