
using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshCollider),typeof(Rigidbody))]
public class TankView : MonoBehaviour
{
    public TankController tankController;
    [SerializeField] private GameObject shooter;
    public Rigidbody tankRb;
    public Coroutine destroyThis;
    public Coroutine firing;
    public int checker = 0;
    public float distanceCovered = 0;

    private void Awake()
    {
        tankRb= GetComponent<Rigidbody>();
    }

    private void Start()
    {
        gameObject.GetComponent<MeshCollider>().convex= true;
        tankController.tankModel.shootertransform = shooter.transform;
    }
    public void getTankController(TankController _tankController)
    {
        tankController = _tankController;
    }

    void changeMaterial(Material _material)
    {
        gameObject.GetComponent<MeshRenderer>().material = _material;
    }

    private void Update()
    {
        tankController.UpdateTank();
        distanceCovered = tankController.tankModel.distanceCovered;
    }

    private void OnCollisionEnter(Collision collision)
    {
        tankController.UpdateCollisionControls();
        if(collision.gameObject.TryGetComponent<BulletView>(out BulletView bullet))
        {
            tankController.OnBulletHit();
        }
    }

    public void StartDestroyCoroutine(float seconds)
    {
        if(destroyThis!=null)
        {
            StopCoroutine(destroyThis);
            destroyThis = null;
        }
        destroyThis = StartCoroutine(Destroy(seconds));
    }

    private IEnumerator Destroy(float seconds)
    {
        //gameObject.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(seconds);
        tankController.DestroyTankDatas();
        tankController = null;
        Destroy(gameObject);

    }

    public void FireCoroutine(float seconds)
    {
        firing = StartCoroutine(FireEnumerator(seconds));
    }
    private IEnumerator FireEnumerator(float seconds)
    {
        while (tankController.tankModel.firing && !tankController.tankModel.died)
        {
            tankController?.Fire();
            yield return new WaitForSeconds(seconds);
        }
    }
    public void StopFiring()
    {
        if (firing != null)
        {
            StopCoroutine(firing);
            firing = null;
        }
    }

    
};
