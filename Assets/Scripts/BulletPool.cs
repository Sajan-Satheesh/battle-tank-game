using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class BulletPool : ObjectPoolSingleton<BulletController>
{
    BulletType bulletType;
    TransformSet bulletTransform;
    [SerializeField] private List<BulletController> bulletControllers = new List<BulletController>();

    public override BulletController getItem()
    {
        foreach(BulletController bullet in availableItems)
        {
            if(bullet.bulletModel.bulletType == bulletType)
            {
                bullet.bulletView.gameObject.SetActive(true);
                return bullet;
            }
        }
        return makeItem();
    }

    public BulletController createBullet(BulletType bulletType, TransformSet bulletTransform)
    {
        this.bulletType = bulletType;
        this.bulletTransform = bulletTransform;
        return getItem();
    }

    protected override BulletController makeItem()
    {
        return BulletService.Instance.createBullet(bulletType, bulletTransform);
    }

    // Update is called once per frame
    void Update()
    {
        bulletControllers = inUseItems;
    }
}
