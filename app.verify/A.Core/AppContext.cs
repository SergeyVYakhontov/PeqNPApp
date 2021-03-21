////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Ninject;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public static class AppContext
  {
    #region public members

    public static IReadOnlyKernel GetConfiguration() => configuration;

    public static void LoadConfigurationModule<T>()
      where T : Ninject.Modules.INinjectModule, new()
    {
      kernelConfiguration = new KernelConfiguration(new T());
      configuration = kernelConfiguration.BuildReadonlyKernel();
    }

    public static void UnloadConfigurationModule()
    {
      kernelConfiguration.Dispose();

      kernelConfiguration = default!;
      configuration = default!;
    }

    #endregion

    #region private members

    private static KernelConfiguration kernelConfiguration = default!;

    private static IReadOnlyKernel configuration = default!;

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
