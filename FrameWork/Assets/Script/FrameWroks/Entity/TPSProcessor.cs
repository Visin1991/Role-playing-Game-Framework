using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class TPSProcessor : LEUnitProcessor {

    private Vector2 inputVH;

    private float delta_dstToTarget;

    LE_Camera_Event_UpdateVlaue cameraDelta;

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

        if (centralPanel == null) { Debug.LogError("There is no CentralPanle Please add It"); }

        if (itemInput == null) { Debug.LogError("Losing Game System, Please Add Gun System"); }
    }

    private void Update()
    {
        UpdateInput();
        if (inputVH != Vector2.zero && delta_dstToTarget != 0)
        {
            cameraDelta.delta_pitch = inputVH.x;
            cameraDelta.delta_yaw = inputVH.y;
            cameraDelta.delta_dstToTarget = delta_dstToTarget;
            centralPanel.Rise_LE_CameraManager_Event(cameraDelta);
        }
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
        delta_dstToTarget = Input.GetAxis("Mouse ScrollWheel");

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

        if (Input.GetKey(Key_A))
        {
           
            itemInput.GetKey_A();
        }

        if (Input.GetKeyUp(Key_A))
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
