using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LEUnitBasicMoveMentPr : LEUnitBasicMoveMent
{
    bool strafe = false;

    bool enable = true;

    public override void UpdateBasicMoveMent()
    {
        if (drivenByInput)
        {
            
            // First Person Camera
            if (cameraType == CameraType.ThridPerson)
            {
                if (strafe == false)
                {          
                    if (InputVH != Vector2.zero) { TurnAroundBasedOn_CameraDir(); }
                }
                strafe = false; //we set the strafe to false every frame.....
            }

            // First Person Camera
            else if (cameraType == CameraType.FirstPerson)
            {
                MoveTransformBasedOnForwardDirection_2D();
                CalculateMoveSpeed_();
                TransformLookAtCameraForward();
            }
        }
    } 

    public override void MailBox_LE_BasicMovementEvent(LE_BasicMovement_Event e)
    {
        if (e is LE_BasicMovement_Event_Strafe)
        {
            ProcessStrafe((LE_BasicMovement_Event_Strafe)e);
        }
        else if (e is LE_BasicMovement_Event_Info)
        {
            ProcessBasicInfo((LE_BasicMovement_Event_Info)e);
        }
        else if (e is LE_BasicMovement_Event_Enable)
        {
            enable = true;
        }
        else if (e is LE_BasicMovement_Event_Disable)
        {
            enable = false;
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
