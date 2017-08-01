using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LEUnitCentralPanel))]
public class LEUnitBasicMoveMentPr : LEUnitBasicMoveMent
{
    private Vector2 InputVH;
    bool strafe = false;
    private float yaw;  //Rotation around Y Axis
    public float turnSmoothVelocity;

    public bool drivenByInput;

    Vector3 velocityNor = Vector3.zero;

    [SerializeField]private Vector3 velocity3D = Vector3.zero;
    float postTargetMoveSpeed = 0.0f;

    float currentSpeed = 0.0f;
    float speedSmoothVelocity;
    public float reactionSpped;

	void Update () {
        if (!initalSucced) return;

        if (drivenByInput)
        {
            // First Person Camera
            if (cameraType == CameraType.ThridPerson)
            { 
                MoveDirection();
                //CalculateMoveSpeed_(); used for Riggid body movement
                if(strafe==false)
                    if (cp.Adapter_LE_InputVH != Vector2.zero){TurnAroundBasedOn_CameraDir();}
                //LookAroundMouseDir();

                strafe = false; //we set the strafe to false every frame.....
            }

            // First Person Camera
            if (cameraType == CameraType.FirstPerson)
            {
                MoveDirection();
                CalculateMoveSpeed_();

                // reactionSpped = 0.0f;
                // yaw += Input.GetAxis("Mouse X") * turningSpeed;
                TurnAround();
            }

            info.forward = currentSpeed / maxSpeed;
            cp.Rise_LE_Animation_Event(info);
        }
	}

    void MoveDirection()
    {
        velocityNor = cp.Adapter_LE_mainBody.forward *  Mathf.Sign(cp.Adapter_LE_InputVH.x);
    }

    void CalculateMoveSpeed_()
    {
        postTargetMoveSpeed = maxSpeed * cp.Adapter_LE_InputVH.normalized.magnitude;

        currentSpeed = Mathf.SmoothDamp(currentSpeed, postTargetMoveSpeed, ref speedSmoothVelocity, reactionSpped);

        velocity3D = currentSpeed * velocityNor;

        cp.Adapter_MoveVeclocity3D = velocity3D;
    }

    void TurnAroundBasedOn_CameraDir()
    {
        float targetDegree = Mathf.Atan2(cp.Adapter_LE_InputVH.x,cp.Adapter_LE_InputVH.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
        cp.Adapter_LE_mainBody.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(cp.Adapter_LE_mainBody.eulerAngles.y, targetDegree, ref turnSmoothVelocity, reactionSpped);
    }

    void LookAroundMouseDir()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Visin1_1.MouseAndCamera.GetMouseGroundIntersectionPoint();
            mousePos.y = cp.Adapter_LE_mainBody.position.y;
            cp.Adapter_LE_mainBody.LookAt(mousePos);
        }
    }

    void TurnAround()
    {
        float targetDegree = Mathf.Atan2(cp.Adapter_LE_InputVH.x, cp.Adapter_LE_InputVH.y) * Mathf.Rad2Deg + yaw;
        cp.Adapter_LE_mainBody.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(cp.Adapter_LE_mainBody.eulerAngles.y, targetDegree, ref turnSmoothVelocity, reactionSpped);
    }

    protected override void MailBox_LE_BasicMovementEvent(LE_BasicMovement_Event e)
    {
        if (e.Type == LE_BasicMovement_EventType.Strafe)
        {
            ProcessStrafe((LE_BasicMovement_Event_Strafe)e);
        }
        else if (e.Type == LE_BasicMovement_EventType.UpdateBasicInfo)
        {
            ProcessBasicInfo((LE_BasicMovement_Event_Info)e);
        }
    }

    void ProcessBasicInfo(LE_BasicMovement_Event_Info e)
    {
        InputVH = e.InputVH;
    }

    void ProcessStrafe(LE_BasicMovement_Event_Strafe e)
    {
        strafe = e.strafe;
    }
}
