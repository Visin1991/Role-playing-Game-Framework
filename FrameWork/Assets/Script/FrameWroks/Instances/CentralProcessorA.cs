using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CentralProcessorA : LEUnitProcessor {

    protected UserInputPr userInputManager;
    protected Visin1_1.CameraManager cameraManager;

    Visin1_1.CameraManager.CameraDelta cameraDelta;

    bool pause;
    
    private void Start() 
    {
        userInputManager = GetComponent<UserInputPr>();
        basicMovementManager = GetComponent<LEUnitBasicMoveMent>();
        animationManager = GetComponent<LEUnitAnimatorPr>();
        cameraManager = GetComponentInChildren<Visin1_1.CameraManager>();
    }

    private void Update()
    {
        if (pause) return;
        UpdateInput();
        UpdateBasicMovement();
        UpdateAnimation();
        UpdateCamera();
    }

    //======================================================
    //Override Function
    //======================================================
    public override void SetToMeleeWeaponModel()
    {
        //1. Change The the item Input System
        //InputActionManager meleeInputSys = transform.GetComponent<MeleeWeaponController>();
        //ChangeItemInputSystem(meleeInputSys);

        //2. Change the Animation Status
        if (animationManager != null)
            animationManager.SetMotionType(LEUnitAnimatorPr.AnimationMotionType.melee);
    }

    public override void SetToRangeWeaponModel()
    {
        //1. Change The the item Input System
        //InputActionManager gunInputSys = transform.GetOrAddComponent<GunController>();
        //ChangeItemInputSystem(gunInputSys);

        //2. Change the Animation Status
        if (animationManager != null)
            animationManager.SetMotionType(LEUnitAnimatorPr.AnimationMotionType.holdGun);
    }

    public override void SetToDefaultModel()
    {
        ChangeItemInputSystem(null);

        //2. Change the Animation Status
        if (animationManager != null)
            animationManager.SetMotionType(LEUnitAnimatorPr.AnimationMotionType.normal);
    }

    public override void Pause(bool p)
    {
        pause = p;
    }
    //======================================================
    //Sub-component Update
    //======================================================
    void UpdateInput()
    {
        userInputManager.UpdateInput(); //We Check the User Input
    }

    void UpdateBasicMovement()
    {
        if (!enableBaiscMovement) return;
        if (userInputManager.Shift)
        {
            basicMovementManager.SetStrafe(true);
        }
        basicMovementManager.SetInputVH(userInputManager.currentVH);
        basicMovementManager.UpdateBasicMoveMent();
    }

    void UpdateAnimation()
    {
        if (userInputManager.Shift)
        {
            animationManager.SetMovementForward(userInputManager.currentVH.y);
            animationManager.SetMovementStrafe(userInputManager.currentVH.x);
        }
        else
        {
            animationManager.SetMovementForward(userInputManager.currentVH.magnitude);
            animationManager.SetMovementStrafe(0);
        }
    }

    void UpdateCamera()
    {
        //Input for Camera
        if (userInputManager.mouseScroll != 0 || userInputManager.mouseHorizontal != 0 || userInputManager.mouseVertical != 0)
        {
            cameraDelta.delta_pitch = userInputManager.mouseVertical;
            cameraDelta.delta_yaw = userInputManager.mouseHorizontal;
            cameraDelta.delta_dstToTarget = userInputManager.mouseScroll;

            cameraManager.SetCameraDelta(cameraDelta);

            userInputManager.mouseVertical = 0;
            userInputManager.mouseHorizontal = 0;
        }
        cameraManager.UpdateCameraManager();
    }
    //======================================================

    public void ChangeItemInputSystem(InputActionManager _itemInput)
    {
        if (inputActionManager != null)
        {
            inputActionManager.ShutDown();
            inputActionManager.enabled = false;
        }
        inputActionManager = _itemInput;

        //Non Item
        if (inputActionManager != null)
            inputActionManager.enabled = true;
    }

}
