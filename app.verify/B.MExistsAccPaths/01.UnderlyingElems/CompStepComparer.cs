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
  public class CompStepComparer : IComparer<ComputationStep>
  {
    #region public methods

    public int Compare(ComputationStep x, ComputationStep y)
    {
      if (x.q < y.q)
      {
        return -1;
      }
      else if (x.q == y.q)
      {
        if (x.s < y.s)
        {
          return -1;
        }
        else if (x.s == y.s)
        {
          if (x.qNext < y.qNext)
          {
            return -1;
          }
          else if (x.qNext == y.qNext)
          {
            if (x.sNext < y.sNext)
            {
              return -1;
            }
            else if (x.sNext == y.sNext)
            {
              if (x.m < y.m)
              {
                return -1;
              }
              else if (x.m == y.m)
              {
                if (x.Shift < y.Shift)
                {
                  return -1;
                }
                else if (x.Shift == y.Shift)
                {
                  if (x.kappaTape < y.kappaTape)
                  {
                    return -1;
                  }
                  else if (x.kappaTape == y.kappaTape)
                  {
                    if (x.kappaStep < y.kappaStep)
                    {
                      return -1;
                    }
                    else if (x.kappaStep == y.kappaStep)
                    {
                      return 0;
                    }
                    else
                    {
                      return 1;
                    }
                  }
                  else
                  {
                    return 1;
                  }
                }
                else
                {
                  return 1;
                }
              }
              else
              {
                return 1;
              }
            }
            else
            {
              return 1;
            }
          }
          else
          {
            return 1;
          }
        }
        else
        {
          return 1;
        }
      }
      else
      {
        return 1;
      }
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
