////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Core;

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
