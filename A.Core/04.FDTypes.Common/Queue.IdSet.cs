////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public class QueueWithIdSet<T> where T : IObjectWithId
  {
    #region public members

    public void Enqueue(T item)
    {
      queue.Enqueue(item);
      itemsId.Add(item.Id);
    }

    public T Dequeue()
    {
      T item = queue.Dequeue();
      itemsId.Remove(item.Id);

      return item;
    }

    public bool Any()
    {
      return queue.Any();
    }

    public bool Contains(T item)
    {
      return itemsId.Contains(item.Id);
    }

    #endregion

    #region private members

    private readonly Queue<T> queue = new Queue<T>();
    private readonly SortedSet<long> itemsId = new SortedSet<long>();

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
