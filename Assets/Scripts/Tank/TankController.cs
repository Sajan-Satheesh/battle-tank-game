using System;
using UnityEngine;
using Random = UnityEngine.Random;


public class TankController
{
    public TankModel tankModel { get; private set; }
    public TankView tankView { get; }

    public TankController(TankModel _tankModel, TankView _prefabTankView)
    {
        tankModel = _tankModel;
        tankView = GameObject.Instantiate<TankView>(_prefabTankView,GetRandPosInWorld(),Quaternion.identity, TankService.Instance.transform);
        tankModel.getTankController(this);
        tankView.getTankController(this);
    }

    public TankModel GetTankModel()
    {
        return tankModel;
    }

    private Vector3 GetRandPosInWorld()
    {
        return new Vector3(Random.Range(0, 50), 1f, Random.Range(0, 50));
    }

    public virtual void UpdateTank()
    {

    }
    public virtual void UpdateCollisionControls()
    {

    }
    public virtual void OnBulletHit()
    {
        tankModel.health -= 20;
        if (tankModel.health < 0 && !tankModel.died)
        {
            tankView.GetComponent<Collider>().isTrigger = true;
            DestroyTank();
        }
    }

    public virtual void DestroyTank()
    {
        tankModel.died = true;
        if(tankView!= null)
        {
            tankView.StartDestroyCoroutine(0.2f);
        }
        
    }

    public void DestroyTankDatas()
    {
        tankModel = null;
    }

    public void Fire()
    {
        Vector3 bulletPosition = tankModel.shootertransform.position;
        Quaternion bulletRotation = tankModel.shootertransform.rotation;
        TransformSet reqBulletTransform = new TransformSet(bulletPosition, bulletRotation, tankView.tankRb.velocity);
        TankService.Instance.ServiceLaunchBullet(tankModel.bulletType, reqBulletTransform);
    }
    
}


public enum Direction { front = 1 , back = -1};
