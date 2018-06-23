////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public class IntSegment
  {
    #region Ctors

    public IntSegment(int left, int right)
    {
      this.Left = left;
      this.Right = right;
    }

    public IntSegment(IntSegment s)
    {
      this.Left = s.Left;
      this.Right = s.Right;
    }

    #endregion

    #region public members

    public int Left { get; }
    public int Right { get; }

    public IEnumerable<int> EnumerateElems()
    {
      for (int i = Left; i <= Right; i++)
      {
        yield return i;
      }
    }

    public void ForEach(Action<int> action)
    {
      for (int i = Left; i <= Right; i++)
      {
        action(i);
      }
    }

    public bool Contains(int elem)
    {
      return ((Left <= elem) && (elem <= Right));
    }

    public int Count
    {
      get
      {
        return (Right - Left + 1);
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
