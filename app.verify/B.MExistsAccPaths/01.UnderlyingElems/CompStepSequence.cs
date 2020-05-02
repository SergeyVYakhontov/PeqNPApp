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
  public static class CompStepSequence
  {
    #region public methods

    public static ComputationStep GetSequentialCompStep(
      ComputationStep compStep)
    {
      ComputationStep seqCompStep =
        new ComputationStep
          {
            kappaTape = compStep.kappaTape,
            kappaStep = compStep.kappaStep + 1
          };

      switch (compStep.m)
      {
        case TMDirection.L:
          {
            seqCompStep.kappaTape -= compStep.Shift;
            break;
          }
        case TMDirection.R:
          {
            seqCompStep.kappaTape += compStep.Shift;
            break;
          }
      }

      return seqCompStep;
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
