using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody),typeof(Collider))]
public class BulletView : MonoBehaviour, IgetController
{
    private BulletController bulletController;
    public Rigidbody bulletRb = new Rigidbody();

    private void Update()
    {
        transform.forward= bulletRb.velocity.normalized;
    }
    private void Start()
    {
        gameObject.GetComponent<MeshCollider>().convex = true;
        bulletRb = gameObject.GetComponent<Rigidbody>();
    }
    public void getBulletController(BulletController _bulletController)
    {
        bulletController = _bulletController;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!bulletController.bulletModel.isFired) return;
        if(collision.gameObject.TryGetComponent(out TankView tankView))
        {
            VfxService.Instance.tankExplosionPool.GetExplosion(transform.position);
        }
        else VfxService.Instance.groundExplosionPool.GetExplosion(transform.position);
        bulletController.onHit();
    }


}
