using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LEUnitBasicMoveMentPr : LEUnitBasicMoveMent
{
   

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
                VelocityDirBasedOnForwardDirection_2D();
                CalculateCurrentVelocity3D();
                TransformLookAtCameraForward();
            }
        }

    } 


}
