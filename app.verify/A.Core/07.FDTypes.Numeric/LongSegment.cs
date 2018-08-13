////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public class LongSegment
  {
    #region Ctors

    public LongSegment(long left, long right)
    {
      this.Left = left;
      this.Right = right;
    }

    public long Left { get; }
    public long Right { get; }

    public LongSegment(LongSegment s)
    {
      this.Left = s.Left;
      this.Right = s.Right;
    }

    #endregion

    #region public members

    public IEnumerable<long> EnumerateElems()
    {
      for (long i = Left; i <= Right; i++)
      {
        yield return i;
      }
    }

    public void ForEach(Action<long> action)
    {
      for (long i = Left; i <= Right; i++)
      {
        action(i);
      }
    }

    public void ForEach(Action action)
    {
      for (long i = Left; i <= Right; i++)
      {
        action();
      }
    }

    public bool Contains(long elem)
    {
      return ((Left <= elem) && (elem <= Right));
    }

    public ulong Count
    {
      get
      {
        return (ulong)(Right - Left + 1);
      }
    }

    public override string ToString()
    {
      return "[" + Left + ".." + Right + "]";
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
