using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TankState
{
    protected EnemyTankController tankController;
    protected TankStates tankState;
    protected bool isUpdated = false;
    public TankState(EnemyTankController _tankController)
    {
        tankController= _tankController;
    }
    public virtual void OnStateEnter()
    {
        isUpdated= true;
    }

    public virtual void OnTick()
    {
        if(!isUpdated) { return; }
    }

    public virtual void OnCollision()
    {

    }

    public virtual void OnStateExit()
    {
        isUpdated= false;
    }

    ~TankState()
    {

    }
}

public enum TankStates { idle, patrolling, chase, attack }

