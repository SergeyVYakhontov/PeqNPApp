////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Core
{
  public class TPLCollectionRunner<ItemType>
    where ItemType : ITPLCollectionItem
  {
    #region Ctors

    public TPLCollectionRunner(
      List<ItemType> collectionToRun,
      uint packetItemsCount,
      WaitMethod waitMethod,
      Func<ItemType[], ItemType> packetProcessProc)
    {
      this.collectionToRun = collectionToRun;
      this.packetItemsCount = packetItemsCount;
      this.waitMethod = waitMethod;
      this.packetProcessProc = packetProcessProc;
    }

    #endregion

    #region public members

    public bool Done { get; private set; }
    public ItemType CurrentItem { get; private set; }

    public void Run()
    {
      while (true)
      {
        ItemType[] itemsToRun = SelectItemsToRun();
        uint itemsToRunCount = (uint)itemsToRun.Length;

        if (itemsToRunCount == 0)
        {
          break;
        }

        Task[] tasks = new Task[itemsToRunCount];

        for (uint i = 0; i < itemsToRunCount; i++)
        {
          uint j = i;

          tasks[i] = Task.Factory.StartNew(
            () => { itemsToRun[j].Run(); },
            CancellationToken.None,
            TaskCreationOptions.DenyChildAttach,
            TaskScheduler.Default);
        }

        switch (waitMethod)
        {
          case WaitMethod.WaitAny:
            {
              Task.WaitAny(tasks);
              break;
            }
          case WaitMethod.WaitAll:
            {
              Task.WaitAll(tasks);
              break;
            }
        }

        CurrentItem = packetProcessProc(itemsToRun);

        if (!EqualityComparer<ItemType>.Default.Equals(CurrentItem, default))
        {
          Done = true;

          return;
        }
      }
    }

    #endregion

    #region private members

    private readonly List<ItemType> collectionToRun;
    private readonly uint packetItemsCount;
    private readonly WaitMethod waitMethod;
    private readonly Func<ItemType[], ItemType> packetProcessProc;

    private ItemType[] SelectItemsToRun()
    {
      if (collectionToRun.Any())
      {
        uint countToTake = Math.Min((uint)collectionToRun.Count, packetItemsCount);
        ItemType[] selectedItems = collectionToRun.Take((int)countToTake).ToArray();

        collectionToRun.RemoveRange(0, (int)countToTake);

        return selectedItems;
      }
      else
      {
        return Array.Empty<ItemType>();
      }
    }

    #endregion
  }
}

////////////////////////////////////////////////////////////////////////////////////////////////////
