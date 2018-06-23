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

    public static void AddPairToSortedDictionary<KeyType, ItemType>(
      SortedDictionary<KeyType, ItemType> dictionary,
      KeyType key,
      Func<ItemType> itemFunc)
    {
      if (dictionary.TryGetValue(key, out ItemType item))
      {
        return;
      }

      ItemType newItem = itemFunc();
      dictionary.Add(key, newItem);
    }

    public static ItemType TakeValueByKey<KeyType, ItemType>(
      IDictionary<KeyType, ItemType> dictionary,
      KeyType key,
      Func<ItemType> itemFunc)
    {
      if (dictionary.TryGetValue(key, out ItemType item))
      {
        return item;
      }

      ItemType newItem = itemFunc();
      dictionary.Add(key, newItem);

      return newItem;
    }

    public static TValue[] SortedDictionaryToArray<TValue>(
      IDictionary<long, TValue> d,
      long size)
    {
      TValue[] array = new TValue[size];
      foreach (KeyValuePair<long, TValue> p in d)
      {
        array[p.Key] = p.Value;
      }

      return array;
    }

    public static void MergeDictionaryWith<T1, T2>(
      IDictionary<T1, List<T2>> mergeTo,
      IReadOnlyDictionary<T1, List<T2>> with)
    {
      IDictionary<T1, List<T2>> d = mergeTo;

      with.ToList().ForEach(e =>
        {
          if (d.TryGetValue(e.Key, out List<T2> value))
          {
            d[e.Key].AddRange(e.Value);

            return;
          }

          d.Add(e.Key, e.Value);
        });
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
