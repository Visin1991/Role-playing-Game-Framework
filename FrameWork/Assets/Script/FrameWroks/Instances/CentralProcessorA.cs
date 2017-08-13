using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CentralProcessorA : LEUnitProcessor {

    ItemInputSystem itemInput;
    LE_Camera_Event_UpdateVlaue cameraDelta;
    LE_Animation_Event_moveInfo animationMoveInfo;
    LE_Animation_Event_ChangeStatu changeAnimationStatu;
    LE_BasicMovement_Event_Strafe basicMovement_Strafe;
    LE_BasicMovement_Event_Info basicMovement_info;

  
    bool pause;

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

        cameraDelta.Init();
        animationMoveInfo.Init();
        changeAnimationStatu.Init();
        basicMovement_Strafe.Init();

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
        if (userInputManager.Shift)
        {
            basicMovement_Strafe.strafe = true;
            basicMovementManager.MailBox_LE_BasicMovementEvent(basicMovement_Strafe);
        }
        basicMovement_info.InputVH = userInputManager.currentVH;

        basicMovementManager.MailBox_LE_BasicMovementEvent(basicMovement_info);
        basicMovementManager.UpdateBasicMoveMent();
    }

    void UpdateAnimation()
    {
        if (userInputManager.Shift)
        {
            animationMoveInfo.forward = userInputManager.currentVH.y;
            animationMoveInfo.strafe = userInputManager.currentVH.x;
        }
        else
        {
            animationMoveInfo.forward = userInputManager.currentVH.magnitude;
            animationMoveInfo.strafe = 0.0f;
        }
        animationManager.MailBox_LE_AnimationEvent(animationMoveInfo);
    }

    void UpdateCamera()
    {
        //Input for Camera
        if (userInputManager.mouseScroll != 0 || userInputManager.mouseHorizontal != 0 || userInputManager.mouseVertical != 0)
        {
            cameraDelta.delta_pitch = userInputManager.mouseVertical;
            cameraDelta.delta_yaw = userInputManager.mouseHorizontal;
            cameraDelta.delta_dstToTarget = userInputManager.mouseScroll;

            cameraManager.MailBox_LE_CameraManager_Event(cameraDelta);

            userInputManager.mouseVertical = 0;
            userInputManager.mouseHorizontal = 0;
        }
        cameraManager.UpdateCameraManager();
    }

    //===============================================
    // Mail Box------Event Comb
    //===============================================
    #region MailBox

    public override void MailBox_LE_ProcessEvent(LEEvent e)
    {
    }

    public override void MailBox_LE_ProcessEvent(LEEvent_GetDamage e)
    {

    }

    public override void MailBox_LE_ProcessEvent(LEEvent_StartDrive e)
    {

    }

    public override void MailBox_LE_ProcessEvent(LEEvent_StartFlyModel e)
    {

    }

    public override void MailBox_LE_ProcessEvent(LEEvent_StartMeleeModel e)
    {
        //1. Change The the item Input System
        ItemInputSystem meleeInputSys = transform.GetComponent<MeleeWeaponController>();
        ChangeItemInputSystem(meleeInputSys);

        //2. Change the Animation Status
        changeAnimationStatu.statu = LE_AnimationStatuType.melee;
        if (animationManager != null)
            animationManager.MailBox_LE_AnimationEvent(changeAnimationStatu);

    }

    public override void MailBox_LE_ProcessEvent(LEEvent_StartHoldGunModel e)
    {
        //1. Change The the item Input System
        ItemInputSystem gunInputSys = transform.GetOrAddComponent<GunController>();
        ChangeItemInputSystem(gunInputSys);

        //2. Change the Animation Status
        changeAnimationStatu.statu = LE_AnimationStatuType.holdGun;
        if(animationManager != null)
            animationManager.MailBox_LE_AnimationEvent(changeAnimationStatu);
    }

    public override void MailBox_LE_ProcessEvent(LEEvent_StartNonModel e)
    {
        ChangeItemInputSystem(null);

        //2. Change the Animation Status
        changeAnimationStatu.statu = LE_AnimationStatuType.normal;
        if (animationManager != null)
            animationManager.MailBox_LE_AnimationEvent(changeAnimationStatu);
    }

    #endregion

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
