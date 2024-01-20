class StateAttack : TankState
{
    public StateAttack(EnemyTankController _tankController) : base(_tankController)
    {
        tankState = TankStates.attack;
    }

    public override void OnStateEnter()
    {
        tankController.FireAfterCooldown();
        base.OnStateEnter();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        tankController.StopFiring();
    }

    public override void OnTick()
    {
        base.OnTick();
        tankController.LookAtPlayer();
        
        if (tankController.DistanceBtwPlayer() > tankController.GetRange())
        {
            tankController.ChangeState(new StateChase(tankController));
        }
    }
}