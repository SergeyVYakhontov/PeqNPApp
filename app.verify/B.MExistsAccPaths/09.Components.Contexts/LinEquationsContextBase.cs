////////////////////////////////////////////////////////////////////////////////////////////////////

namespace ExistsAcceptingPath
{
  public  class LinEquationsContextBase : TapeSegContextBase
  {
    #region Ctors

    public LinEquationsContextBase(
      MEAPContext meapContext,
      TapeSegContext tapeSegContext,
      LinEquationContext linEquationContext)
      : base(meapContext, tapeSegContext)
    {
      this.linEquationContext = linEquationContext;
    }

    #endregion

    #region private members

    protected readonly LinEquationContext linEquationContext;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
