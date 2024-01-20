using System;
using UnityEngine;

[Serializable]
public class StatePatrolling : TankState
{
    public StatePatrolling(EnemyTankController _tankController) : base(_tankController)
    {
        tankState = TankStates.patrolling;
    }

    public override void OnCollision()
    {
        tankController.ShiftDirectionSlow();
    }
    public override void OnStateEnter()
    {
        base.OnStateEnter();    
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public override void OnTick()
    {
        base.OnTick();
        tankController.MoveForward();
        tankController.ThrowRay();
        if (tankController.DistanceBtwPlayer() <= 10)
        {
            tankController.ChangeState(new StateChase(tankController));
        }
    }
}