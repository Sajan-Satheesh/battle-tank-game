using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(TankView))]
public class StateIdle : TankState
{
    public StateIdle(EnemyTankController _tankController) : base(_tankController)
    {
        tankState = TankStates.idle;
    }

    public override void OnStateEnter()
    {
        base.OnStateEnter();
        
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    /*private async Task<bool> IdleForSeconds()
    {
        bool proceed;
        float time = 0;

        time += Time.deltaTime;
        proceed = CalculateSecs(5f);
        await proceed; 
    }*/

    private bool CalculateSecs( float waitingTime)
    {
        float time = 0;
        time += Time.deltaTime;
        if (time > waitingTime)
        {
            return true;
        }
        return false;
    }

    


    public override void OnTick()
    {
        base.OnTick();
        tankController.ShiftDirectionSlow();
        float reqDistance = tankController.DistanceBtwPlayer();

        if (reqDistance > 10)
        {
            tankController.ChangeState(new StatePatrolling(tankController));
        }
        else if(reqDistance <= 10)
        {
            tankController.ChangeState(new StateChase(tankController));
        }
        
    }
}