

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public abstract class ObjectPoolSingleton<C,T,P> where C: class
{
    [field : SerializeField] protected List<C> availableItems = new List<C>();
    [field : SerializeField] protected List<C> inUseItems = new List<C>();

    #region public functions
    public C GetItem(T type, P position)
    {
        if (availableItems == null) return null;
        C item =  GetLogic(type ,position);
        return GetItem(ref item);
    }
    public C GetItem(P position)
    {
        if (availableItems == null) return null;
        C item = GetLogic(position);
        return GetItem(ref item);
    }
        
    private C GetItem(ref C item)
    {
        availableItems.Remove(item);
        inUseItems.Add(item);
        return item;
    }


    public void RetrieveItem( C _retreivedItem)
    {
        Debug.Log("reteriving bullet");
        foreach (C item in inUseItems)
        {
            Debug.Log("count in used itesm: " + inUseItems.Count);
            if (item.Equals(_retreivedItem))
            {
                Debug.Log("bullet returned");
                RetreiveLogic(item);
                availableItems.Add(item);
                inUseItems.Remove(item);
                break;
            }
        }
    }

    #endregion

    #region protected functions
    protected virtual C makeItem(T type, P position) { return null; }
    protected virtual C makeItem(P position) { return null; }

    protected abstract void RetreiveLogic(C _retreivedItem);
    protected virtual C GetLogic(T type, P position) { return null; }
    protected virtual C GetLogic(P position) { return null; }
    #endregion

}
