////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public static partial class AppHelper
  {
    #region public members

    public static T2[] CreateArrayCopy<T1, T2>(T1[] data, Func<T1, T2> convertor)
    {
      T2[] result = new T2[data.Length];

      for (long i = 0; i < data.Length; i++)
      {
        result[i] = convertor(data[i]);
      }

      return result;
    }

    public static T[] CreateArrayCopy<T>(T[] data)
    {
      T[] result = new T[data.Length];

      for (long i = 0; i < data.Length; i++)
      {
        result[i] = data[i];
      }

      return result;
    }

    public static T[] CreateSubArray<T>(T[] data, long index, long length)
    {
      T[] result = new T[length];
      Array.Copy(data, index, result, 0, length);

      return result;
    }

    public static void FillArray<T>(T[] data, T item)
    {
      for (ulong i = 0; (long)i < data.Length; i++)
      {
        data[i] = item;
      }
    }

    public static void ReplaceInArray<T>(
      T[] data,
      T item1,
      T item2,
      Func<T, T, bool> compare)
    {
      for (ulong i = 0; (long)i < data.Length; i++)
      {
        if (compare(data[i], item1))
        {
          data[i] = item2;
        }
      }
    }

    public static string ArrayToString<T>(T[] objectArray)
      where T: notnull
    {
      StringBuilder repr = new();
      const ulong maxLength = 128;

      repr.Append('(');

      for (ulong i = 0; (i < maxLength) && ((long)i < objectArray.Length); i++)
      {
        repr.Append(objectArray[i].ToString());
        repr.Append(' ');
      }

      repr.Append(')');

      return repr.ToString();
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
