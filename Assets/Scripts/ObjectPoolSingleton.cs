

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ObjectPoolSingleton<T> : GenericSingleton<ObjectPoolSingleton<T>> where T: class
{
    [SerializeField] protected List<T> availableItems = new List<T>();
    [SerializeField] protected List<T> inUseItems = new List<T>();

    #region public functions
    public virtual T getItem()
    {
        T item = (T)null;
        if (availableItems.Count < 1)
        {
            item = makeItem();
        }
        else
        {
            item = availableItems.Last();
            availableItems.Remove(item);
        }
        inUseItems.Add(item);
        return item;
    }


    public void retrieveItem( T _retreivedItem)
    {
        Debug.Log("reteriving bullet");
        foreach (T item in inUseItems)
        {
            Debug.Log("count in used itesm: " + inUseItems.Count);
            if (item.Equals(_retreivedItem))
            {
                Debug.Log("bullet returned");
                availableItems.Add(item);
                inUseItems.Remove(item);
                break;
            }
        }
    }

    #endregion

    #region protected functions
    protected virtual T makeItem()
    {
        T availableItem = (T)null;
        return availableItem;
    } 
    #endregion


    protected class ObjectInfo 
    {
        public T item;
        public bool isUsed;
    }
}
