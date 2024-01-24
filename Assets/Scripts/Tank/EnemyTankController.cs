using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyTankController : TankController
{
    public EnemyTankController(TankModel _tankModel, TankView _tankview) : base(_tankModel, _tankview) 
    {

        tankModel.currentState = new StatePatrolling(this);
    }
   
    public override void UpdateTank()
    {
        tankModel.currentState.OnTick();
    }

    public override void UpdateCollisionControls()
    {
        tankModel.currentState.OnCollision();
    }

    public override void DestroyTank()
    {
        if (TankService.Instance.playerTankController != null && !tankModel.died)
        {
            TankService.Instance.killIncrementer();
            TankService.Instance.EnemyTanksControllers.Remove(this);
            if (TankService.Instance.EnemyTanksControllers.Count == 0)
            {
                TankService.Instance.onLevelCleared?.Invoke();
            }
        }
        base.DestroyTank(); 
    }

    public void ChangeState(TankState _tanksState)
    {
        tankModel.currentState.OnStateExit();
        tankModel.currentState = _tanksState;
        tankModel.currentState.OnStateEnter();
    }
    public void MoveForward()
    {
        tankView.gameObject.transform.position += tankView.gameObject.transform.forward  * tankModel.speed * Time.deltaTime;
    }
    public void ShiftDirectionSlow ()
    {
        Quaternion newQ = Quaternion.Euler(tankView.gameObject.transform.rotation.eulerAngles+new Vector3(0,10,0));
        tankView.gameObject.transform.rotation = Quaternion.Lerp(tankView.gameObject.transform.rotation, newQ, 0.1f);
    }

    public float DistanceBtwPlayer()
    {
        TankController player = TankService.Instance.playerTankController;
        if (player != null)
        {
            Vector3 selfPosition = tankView.gameObject.transform.position;
            Vector3 targetPosition = player.tankView.gameObject.transform.position;
            return Vector3.Distance(selfPosition, targetPosition);
        }
        return 100f;
        
    }

    public void ThrowRay()
    {
        Ray ememyRay = new Ray(tankView.gameObject.transform.position,tankView.gameObject.transform.forward);
        RaycastHit raycastHit = new RaycastHit();
        if(Physics.Raycast(ememyRay,out raycastHit)){
            if (raycastHit.distance < 3f)
            {
                tankModel.speed = 0;
                ShiftDirectionSlow();
            }
            else tankModel.speed = tankModel.defaultSpeed;
        }
    }

    public void LookAtPlayer()
    {
        PlayerTankController player = TankService.Instance.playerTankController;
        if (player != null)
        {
            tankView.gameObject.transform.LookAt(player.GetPositionAtElevation(tankView.transform.position.y));
        }
    }

    public void FireAfterCooldown()
    {
        ToggleAutoFiring(true);
        tankView.FireCoroutine(1f);
    }

    public void ToggleAutoFiring(bool _switch)
    {
        tankModel.firing = _switch;
    }

    public void StopFiring()
    {
        ToggleAutoFiring(false);
        tankView.StopFiring();
    }
    public float GetRange()
    {
        if (TankService.Instance.playerTankController != null)
        {
            float shootAngle = Vector3.Angle(tankModel.shootertransform.forward, (TankService.Instance.playerTankController.tankView.transform.position - tankView.transform.position).normalized);
            float range = (tankModel.bulletType.speed * tankModel.shootertransform.forward).magnitude + 2 * MathF.Sin(shootAngle) / Physics.gravity.magnitude;
            return range;
        }
        return default;

    }

}