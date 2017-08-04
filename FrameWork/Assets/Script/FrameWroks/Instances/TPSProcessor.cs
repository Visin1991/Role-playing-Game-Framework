using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class TPSProcessor : LEUnitProcessor {

    private Vector2 inputVH;
    private Vector2 currentVH;
    private Vector2 currentVelocity;
    public float smoothTime;

    private float mouseScroll;
    private float mouseVertical;
    private float mouseHorizontal;

    LE_Camera_Event_UpdateVlaue cameraDelta;
    LE_Animation_Event_moveInfo animationMoveInfo;
    LE_Animation_Event_ChangeStatu changeAnimationStatu;
    LE_BasicMovement_Event_Strafe basicMovement_Strafe;

    public string fireButton;

    public KeyCode Key_A;
    public KeyCode Key_B;

    LEUnitCentralPanel centralPanel;

    ItemInputSystem itemInput;

    int statu = 0;

    private void Start() 
    {
        centralPanel = GetComponent<LEUnitCentralPanel>();
        Initial_All_Component();
        if (centralPanel == null) { Debug.LogError("There is no CentralPanle Please add It"); }
    }

    private void Update()
    {
        TestForSwitchPlayModel();

        UpdateInput();

        //Input for Camera
        if (mouseScroll != 0 || mouseHorizontal != 0 || mouseVertical != 0)
        {
            cameraDelta.delta_pitch = mouseVertical;
            cameraDelta.delta_yaw = mouseHorizontal;
            cameraDelta.delta_dstToTarget = mouseScroll;

            centralPanel.Rise_LE_CameraManager_Event(cameraDelta);

            mouseVertical = 0;
            mouseHorizontal = 0;
        }

        //Movement Information for Animation
        currentVH = Vector2.SmoothDamp(currentVH, inputVH, ref currentVelocity, smoothTime,float.MaxValue,Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            animationMoveInfo.forward = currentVH.y;
            animationMoveInfo.strafe = currentVH.x;
            basicMovement_Strafe.strafe = true;
            centralPanel.Rise_LE_BasicMovement_Event(basicMovement_Strafe);
        }
        else
        {
            animationMoveInfo.forward = currentVH.magnitude;
            animationMoveInfo.strafe = 0.0f;
        }

        centralPanel.Rise_LE_Animation_Event(animationMoveInfo);
    }

    public void Initial_All_Component()
    {
        cameraDelta.Init();
        animationMoveInfo.Init();
        changeAnimationStatu.Init();
        basicMovement_Strafe.Init();
    }

    public override Vector2 InputVH { get { return inputVH; } }

    //===============================================
    //Input System
    //===============================================

    protected void UpdateInput()
    {
#if UNITY_IOS || UNITY_ANDROID
		Vector2 input = TouchLib.GetSwipe2D();
		InputVH.x = input.x;
		InputVH.y = input.y;
		CrossPlatformButtonInput();
#else
        inputVH.x = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        inputVH.y = CrossPlatformInputManager.GetAxisRaw("Vertical");

        mouseScroll = Input.GetAxis("Mouse ScrollWheel");

        if (Input.GetMouseButton(1))
        {
            mouseVertical = -Input.GetAxis("Mouse Y");
            mouseHorizontal = Input.GetAxis("Mouse X");
        }

        StandaredKeyInput();
#endif
    }

    protected  void CrossPlatformButtonInput()
    {
        if (CrossPlatformInputManager.GetButton(fireButton))
        {
            itemInput.GetKey_A();
        }

        if (CrossPlatformInputManager.GetButtonUp(fireButton))
        {
            itemInput.GetKey_A_Up();
        }
    }

    protected  void StandaredKeyInput()
    {
        if (itemInput == null) { return; }

        if (Input.GetMouseButtonDown(0))
        {
            itemInput.GetKey_A_Down();
        }

        if (Input.GetMouseButton(0))
        {
            itemInput.GetKey_A();
        }

        

        if (Input.GetMouseButtonUp(0))
        {
            itemInput.GetKey_A_Up();
        }

        if (Input.GetKeyDown(Key_B))
        {
            itemInput.GetKey_B_Down();
        }


    }

    //===============================================

    void ChangeItemInputSystem(ItemInputSystem _itemInput)
    {
        if (itemInput != null) {
            itemInput.ShutDown();
            itemInput.enabled = false;
        }
        itemInput = _itemInput;

        //Non Item
        if(itemInput != null)
            itemInput.enabled = true;
    }

    //===============================================
    // Mail Box------Event Comb
    //===============================================
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
        centralPanel.Rise_LE_Animation_Event(changeAnimationStatu);

    }

    public override void MailBox_LE_ProcessEvent(LEEvent_StartHoldGunModel e)
    {
        //1. Change The the item Input System
        ItemInputSystem gunInputSys = transform.GetOrAddComponent<GunController>();
        ChangeItemInputSystem(gunInputSys);

        //2. Change the Animation Status
        changeAnimationStatu.statu = LE_AnimationStatuType.holdGun;
        centralPanel.Rise_LE_Animation_Event(changeAnimationStatu);
    }

    public override void MailBox_LE_ProcessEvent(LEEvent_StartNonModel e)
    {
        ChangeItemInputSystem(null);

        //2. Change the Animation Status
        changeAnimationStatu.statu = LE_AnimationStatuType.normal;
        centralPanel.Rise_LE_Animation_Event(changeAnimationStatu);
    }

    //===============================================
    //Test for Switch Play Model
    void TestForSwitchPlayModel()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           
            if (statu == 1)
            {
                LEEvent_StartHoldGunModel e = new LEEvent_StartHoldGunModel();
                e.Init();
                MailBox_LE_ProcessEvent(e);
            }
            else if (statu == 2)
            {
                LEEvent_StartMeleeModel e = new LEEvent_StartMeleeModel();
                e.Init();
                MailBox_LE_ProcessEvent(e);
            }
            else
            {
                LEEvent_StartNonModel e = new LEEvent_StartNonModel();
                e.Init();
                MailBox_LE_ProcessEvent(e);
            }
            statu += 1;
            statu %= 3;
        }


    }
}
