using System.ComponentModel.Design;
using UnityEngine;

public class PlayerTankController: TankController
{
    private int distanceChecker = 0;
    public PlayerTankController(TankModel _tankModel, TankView _prefabTankView) : base(_tankModel,  _prefabTankView)
    {
        TankService.Instance.playerTankFollower.AddFollower(tankView);
    }

    public void moveWithVelocity(Direction direction)
    {
        tankView.tankRb.velocity = tankView.gameObject.transform.forward * (int)direction * tankModel.speed;
    }

    public void RotateToDirection(Vector3 direction)
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        tankView.gameObject.transform.rotation = Quaternion.Slerp(tankView.gameObject.transform.rotation, lookRotation, 0.1f * direction.magnitude);
    }

    public override void DestroyTank()
    {
        TankService.Instance.gameOverProcess();
        TankService.Instance.playerTankController = null;
        tankModel.died = true;
        tankView.StartDestroyCoroutine(0.2f);
        TankService.Instance.destroyAllTanks();
    }

    public override void UpdateTank()
    {
        if(!determineBlock())
        tankModel.distanceCovered += tankView.tankRb.velocity.magnitude * Time.deltaTime;
        tankView.checker = distanceChecker;
        messageAtDistanceMilestones();
    }

    public Vector3 GetPositionAtElevation(float y)
    {
        return new Vector3(tankView.transform.position.x, y, tankView.transform.position.z);
    }

    private void messageAtDistanceMilestones()
    {
        if(tankModel.distanceCovered > distanceChecker)
        {
            TankService.Instance.checkDistanceAcheivement((int)tankModel.distanceCovered);
            distanceChecker += 5;
        }
    }

    private bool determineBlock()
    {
        Ray ray = new Ray(tankModel.shootertransform.position, tankView.transform.forward);
        RaycastHit hitInfo;
        Physics.Raycast(ray, out hitInfo, 1f);
        if (hitInfo.collider!=null)
        {
            return true;
        }
        return false;
    }

}
