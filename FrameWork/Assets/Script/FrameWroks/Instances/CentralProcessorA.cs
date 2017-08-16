using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CentralProcessorA : LEUnitProcessor {

    ItemInputSystem itemInput;
    Visin1_1.CameraManager.CameraDelta cameraDelta;

    bool pause;
    bool enableBaiscMovement = true;
    LEUnitAnimatorPr.AnimationAttackStatue animationAttackStatue = LEUnitAnimatorPr.AnimationAttackStatue.Off;
    public LEUnitAnimatorPr.AnimationAttackStatue AnimationAttackStatue { get { return animationAttackStatue; } }

    /// <summary>
    /// Controll all component's update inside the CentralProcessor
    /// </summary>
    private void Start() 
    {

        userInputManager = GetComponent<UserInputPr>();
        if (userInputManager != null)
        {
            tpspUpdateDel -= UpdateInput;
            tpspUpdateDel += UpdateInput;
            userInputManager.ResetProcessor(this);
        }

        basicMovementManager = GetComponent<LEUnitBasicMoveMent>();
        if (basicMovementManager != null)
        {
            tpspUpdateDel -= UpdateBasicMovement;
            tpspUpdateDel += UpdateBasicMovement;
        }

        animationManager = GetComponent<LEUnitAnimatorPr>();
        if (animationManager != null)
        {
            tpspUpdateDel -= UpdateAnimation;
            tpspUpdateDel += UpdateAnimation;
        }

        cameraManager = GetComponentInChildren<Visin1_1.CameraManager>();
        if (cameraManager != null)
        {
            tpspUpdateDel -= UpdateCamera;
            tpspUpdateDel += UpdateCamera;
        }

    }
    //==================================================
    // Update the Processor and Update all sub-Component
    //==================================================
    private void Update()
    {
        if (pause) return;
        if (tpspUpdateDel != null) tpspUpdateDel.Invoke(); //Now the TPS can add or delete function anytime without check null reference error.
    }

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

    //===============================================
    // Mail Box------Event Comb
    //===============================================
    #region MailBox


    public override void SetToMeleeWeaponModel()
    {
        //1. Change The the item Input System
        ItemInputSystem meleeInputSys = transform.GetComponent<MeleeWeaponController>();
        ChangeItemInputSystem(meleeInputSys);

        //2. Change the Animation Status
        if (animationManager != null)
            animationManager.SetMotionType( LEUnitAnimatorPr.AnimationMotionType.melee);
    }

    public override void SetToRangeWeaponModel()
    {
        //1. Change The the item Input System
        ItemInputSystem gunInputSys = transform.GetOrAddComponent<GunController>();
        ChangeItemInputSystem(gunInputSys);

        //2. Change the Animation Status
        if(animationManager != null)
            animationManager.SetMotionType(LEUnitAnimatorPr.AnimationMotionType.holdGun);
    }

    public override void SetToDefaultModel()
    {
        ChangeItemInputSystem(null);

        //2. Change the Animation Status
        if (animationManager != null)
            animationManager.SetMotionType(LEUnitAnimatorPr.AnimationMotionType.normal);
    }

    #endregion

    public override void AnimationManager_EnableBasicMoveMent(bool isable)
    {
        enableBaiscMovement = isable;
    }

    public override void AnimationManager_SetAnimationStatue(LEUnitAnimatorPr.AnimationAttackStatue s)
    {
        animationAttackStatue = s;
    }


    public override void Pause(bool p)
    {
        pause = p;
    }

    public override void GetKey_A_Down()
    {
        if(itemInput != null)
            itemInput.GetKey_A_Down();
    }

    public override void GetKey_A()
    {
        if (itemInput != null)
            itemInput.GetKey_A();
    }

    public override void GetKey_A_Up()
    {
        if (itemInput != null)
            itemInput.GetKey_A_Up();
    }

    public override void GetKey_B_Down()
    {
        if (itemInput != null)
            itemInput.GetKey_B_Down();
    }

    public override void GetKey_B_Up()
    {
        throw new NotImplementedException();
    }

    public void ChangeItemInputSystem(ItemInputSystem _itemInput)
    {
        if (itemInput != null)
        {
            itemInput.ShutDown();
            itemInput.enabled = false;
        }
        itemInput = _itemInput;

        //Non Item
        if (itemInput != null)
            itemInput.enabled = true;
    }

}
