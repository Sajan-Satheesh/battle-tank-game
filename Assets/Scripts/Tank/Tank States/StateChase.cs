using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StateChase : TankState
{
    public StateChase(EnemyTankController _tankController) : base(_tankController)
    {
        tankState = TankStates.chase;
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
    }

    public override void OnTick()
    {
        base.OnTick();
        tankController.LookAtPlayer();
        tankController.MoveForward();
        float reqDistance = tankController.DistanceBtwPlayer();
        if (reqDistance <= tankController.GetRange())
        {
            tankController.ChangeState(new StateAttack(tankController));
        }
        if (reqDistance > 10)
        {
            tankController.ChangeState(new StatePatrolling(tankController));
        }
    }

    public override void OnCollision()
    {

    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

}
