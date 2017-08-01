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
    LE_BasicMovement_Event_Strafe basicMovement_Strafe;

    public string fireButton;

    public KeyCode Key_A;
    public KeyCode Key_B;

    LEUnitCentralPanel centralPanel;

    ItemInputSystem itemInput;

    private void Start() 
    {

        centralPanel = GetComponent<LEUnitCentralPanel>();

        itemInput = GetComponent<GunController>();

        cameraDelta = new LE_Camera_Event_UpdateVlaue();
        cameraDelta.Init();

        animationMoveInfo = new LE_Animation_Event_moveInfo();
        animationMoveInfo.Init();

        basicMovement_Strafe = new LE_BasicMovement_Event_Strafe();
        basicMovement_Strafe.Init();

        if (centralPanel == null) { Debug.LogError("There is no CentralPanle Please add It"); }

        if (itemInput == null) { Debug.LogError("Losing Game System, Please Add Gun System"); }
    }

    private void Update()
    {
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

    }

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
        
    }

    public override void MailBox_LE_ProcessEvent(LEEvent_StartShootGunModel e)
    {

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

    public void ChangeItemInputSystem(ItemInputSystem _itemInput)
    {
        itemInput = _itemInput;
    }

}
