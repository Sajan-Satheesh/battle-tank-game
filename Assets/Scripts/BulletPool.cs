using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class BulletPool : ObjectPoolSingleton<BulletController,BulletType,TransformSet>
{
    public BulletController createBullet(BulletType bulletType, TransformSet bulletTransform)
    {
        return GetItem(bulletType, bulletTransform);
    }

    protected override BulletController GetLogic(BulletType bulletType, TransformSet position)
    {
        foreach (BulletController bullet in availableItems)
        {
            if (bullet.bulletModel.bulletType == bulletType)
            {
                return bullet;
            }
        }
        return makeItem(bulletType, position);
    }
    protected override void RetreiveLogic(BulletController _retreivedItem)
    {
        _retreivedItem.bulletView.gameObject.SetActive(false);
        _retreivedItem.bulletView.bulletRb.velocity = Vector3.zero;
        _retreivedItem.bulletView.gameObject.transform.position = Vector3.zero;
    }

    protected override BulletController makeItem(BulletType bulletType, TransformSet bulletTransform)
    {
        return BulletService.Instance.createBullet(bulletType, bulletTransform);
    }

}
