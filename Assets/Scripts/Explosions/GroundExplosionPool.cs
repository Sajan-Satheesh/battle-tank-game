using System;
using System.Linq;
using UnityEngine;

public class GroundExplosionPool : ObjectPoolSingleton<GameObject,Type, Vector3>
{
    int checkIndex = 0;
    protected override GameObject makeItem(Vector3 spawnPositon)
    {
        return VfxService.Instance.CreateGroundExplosion(spawnPositon);
    }

    public void GetExplosion(Vector3 position)
    {
        GetItem(position);
    }

    protected override GameObject GetLogic(Vector3 position)
    {
        if (availableItems.Count < 1) return makeItem(position);
        availableItems[0].transform.position = position;
        availableItems[0].SetActive(true);
        availableItems[0].GetComponent<ParticleSystem>().Play();
        return availableItems[0];
    }

    protected override void RetreiveLogic(GameObject _retreivedItem)
    {
        _retreivedItem.SetActive(false);
        _retreivedItem.transform.position = Vector3.zero;
    }

    public void CheckStatusAndRetreive()
    {
        if (inUseItems == null || inUseItems.Count < 1) return;
        ++checkIndex;
        if(checkIndex >= inUseItems.Count)
        {
            checkIndex = 0;
        }
        if (inUseItems[checkIndex].GetComponent<ParticleSystem>().isStopped)
        {
            RetrieveItem(inUseItems[checkIndex]);
        }
    }
}
