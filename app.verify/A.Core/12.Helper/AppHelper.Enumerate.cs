////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using EnsureThat;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public static partial class AppHelper
  {
    #region public members

    public static void ForEach<ItemType>(
      this ItemType[] array,
      Action<ItemType> action)
    {
      Array.ForEach(array, action);
    }

    public static void ForEach<ItemType>(
      this IReadOnlyCollection<ItemType> set,
      Action<ItemType> action)
    {
      IEnumerator<ItemType> enumerator = set.GetEnumerator();

      while (enumerator.MoveNext())
      {
        action(enumerator.Current);
      }
    }

    public static void ForEach<KeyType, ItemType>(
      this IReadOnlyDictionary<KeyType, ItemType> dictionary,
      Action<KeyValuePair<KeyType, ItemType>> action)
    {
      IEnumerator<KeyValuePair<KeyType, ItemType>> enumerator = dictionary.GetEnumerator();

      while (enumerator.MoveNext())
      {
        action(enumerator.Current);
      }
    }

    public static void ForEach<KeyType, ItemType>(
      this SortedDictionary<KeyType, ItemType>.ValueCollection valueCollection,
      Action<ItemType> action)
    {
      SortedDictionary<KeyType, ItemType>.ValueCollection.Enumerator enumerator =
        valueCollection.GetEnumerator();

      while (enumerator.MoveNext())
      {
        action(enumerator.Current);
      }
    }

    public static List<KeyValuePair<T,T>> MakeSequencePairs<T>(IEnumerable<T> sequence)
    {
      Ensure.That(sequence.Count()).IsGte(2);

      List<KeyValuePair<T, T>> sequencePairs = new();
      T item = sequence.First();

      foreach (T nextItem in sequence.Skip(1))
      {
        sequencePairs.Add(new KeyValuePair<T, T>(item, nextItem));
        item = nextItem;
      }

      return sequencePairs;
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
