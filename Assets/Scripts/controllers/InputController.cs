using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Joystick joystick;
    [SerializeField] private Button Accelerator;
    [SerializeField] private Button Reverse;
    [SerializeField] private Button Shoot;
    bool accelerated;
    bool reversed;

    // Update is called once per frame
    private void Start()
    {
        Shoot.onClick.AddListener(shootBullet);
    }
    void Update()
    {
        if(RequestPlayerTank() != null) {
            accelerated = getButtonState(Accelerator);
            reversed = getButtonState(Reverse);
            if (accelerated)
            {
                RequestPlayerTank()?.moveWithVelocity(Direction.front);
            }
            else if (reversed)
            {
                RequestPlayerTank()?.moveWithVelocity(Direction.back);
            }
            if (joystick.Direction.magnitude > 0)
            {
                float rotationAngle = Vector3.Angle(Vector3.forward, Camera.main.transform.forward);
                Vector3 rotatedVector = Quaternion.AngleAxis(rotationAngle, Vector3.up) * new Vector3(joystick.Direction.x, 0, joystick.Direction.y);
                RequestPlayerTank()?.RotateToDirection(rotatedVector);
            }
        }
       
    }

    private void shootBullet()
    {
        //Debug.Log("bullet launched");
        if(RequestPlayerTank() != null)
        {
            RequestPlayerTank()?.Fire();
        }
    }

    private PlayerTankController RequestPlayerTank()
    {
        return TankService.Instance.playerTankController;
    }

    private bool getButtonState(Button button)
    {
        return button.GetComponent<ButtonPressed>().buttonPressed;
    }
}
