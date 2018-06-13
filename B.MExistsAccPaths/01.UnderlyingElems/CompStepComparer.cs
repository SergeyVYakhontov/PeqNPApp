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

    public int Compare(ComputationStep s1, ComputationStep s2)
    {
      if (s1.q < s2.q)
      {
        return -1;
      }
      else if (s1.q == s2.q)
      {
        if (s1.s < s2.s)
        {
          return -1;
        }
        else if (s1.s == s2.s)
        {
          if (s1.qNext < s2.qNext)
          {
            return -1;
          }
          else if (s1.qNext == s2.qNext)
          {
            if (s1.sNext < s2.sNext)
            {
              return -1;
            }
            else if (s1.sNext == s2.sNext)
            {
              if (s1.m < s2.m)
              {
                return -1;
              }
              else if (s1.m == s2.m)
              {
                if (s1.Shift < s2.Shift)
                {
                  return -1;
                }
                else if (s1.Shift == s2.Shift)
                {
                  if (s1.kappaTape < s2.kappaTape)
                  {
                    return -1;
                  }
                  else if (s1.kappaTape == s2.kappaTape)
                  {
                    if (s1.kappaStep < s2.kappaStep)
                    {
                      return -1;
                    }
                    else if (s1.kappaStep == s2.kappaStep)
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
