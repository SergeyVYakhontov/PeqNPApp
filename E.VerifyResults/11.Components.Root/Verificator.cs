////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Ninject;
using Core;
using ExistsAcceptingPath;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace VerifyResults
{
  public abstract class Verificator : IVerificator
  {
    #region public members

    public abstract void Run();
    
    #endregion
    
    #region private members

    protected static readonly IKernel configuration = Core.AppContext.Configuration;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
