
using UnityEngine;

public class BulletModel : IgetController
{
    private BulletController bulletController;
    public int speed;
    public BulletType bulletType;
    public TransformSet bulletTransform;
    public bool isFired = false;
    public BulletModel(BulletType _bulletType, TransformSet _bulletTransform)
    {
        bulletType = _bulletType;
        speed = bulletType.speed;
        bulletTransform = _bulletTransform;
    }

    public void getBulletController(BulletController _bulletController)
    {
        bulletController = _bulletController;
    }
}
