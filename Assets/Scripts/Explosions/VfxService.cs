using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxService : GenericSingleton<VfxService> 
{
    [SerializeField] private GameObject explosionBase;
    [SerializeField] private GameObject tankExplosionBase;
    public GroundExplosionPool groundExplosionPool;
    public TankExplosionPool tankExplosionPool;

    protected override void Awake()
    {
        base.Awake();
        groundExplosionPool = new GroundExplosionPool();
        tankExplosionPool= new TankExplosionPool();
    }
    public GameObject CreateGroundExplosion(Vector3 positon)
    {
        return Instantiate(explosionBase, positon, Quaternion.identity, transform);
    }
    public GameObject CreateTankExplosion(Vector3 positon)
    {
        return Instantiate(tankExplosionBase, positon, Quaternion.identity, transform);
    }
    private void Update()
    {
        groundExplosionPool.CheckStatusAndRetreive();
        tankExplosionPool.CheckStatusAndRetreive();
    }
}
