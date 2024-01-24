using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController 
{
    public BulletModel bulletModel { get; }
    public BulletView bulletView { get; }
    public BulletController(BulletModel _bulletModel, BulletView _bulletView) 
    {
        bulletModel= _bulletModel;
        bulletModel.getBulletController(this);
        bulletView = GameObject.Instantiate<BulletView>(_bulletView,_bulletModel.bulletTransform.position, bulletModel.bulletTransform.rotation,BulletService.Instance.transform);
        bulletView.getBulletController(this);
        onFire(_bulletModel.bulletTransform);
    }
    public void onFire(TransformSet _bulletTransform)
    {
        prepareBullet(_bulletTransform);
        exertForce(_bulletTransform.velocity);
    }
    public void onHit()
    {
        bulletModel.isFired = false;
        BulletService.Instance.returnBullet( this);
    }
    private void prepareBullet(TransformSet _bulletTransform)
    {

        bulletModel.bulletTransform = _bulletTransform;
        bulletView.transform.position = bulletModel.bulletTransform.position;
        bulletView.transform.rotation = bulletModel.bulletTransform.rotation;
        bulletModel.isFired = true;
        bulletView.gameObject.SetActive(true);
    }
    private void exertForce(Vector3 shooterVelocity)
    {
        bulletView.bulletRb.velocity = bulletView.gameObject.transform.forward  * (bulletModel.speed + shooterVelocity.magnitude);
    }
}


public interface IgetController
{
    public void getBulletController(BulletController bulletController);
}

