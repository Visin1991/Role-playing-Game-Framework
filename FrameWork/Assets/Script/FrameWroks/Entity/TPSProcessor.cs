using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class TPSProcessor : LEUnitProcessor {

    private Vector2 inputVH;
    public string fireButton;

    public KeyCode Key_A;
    public KeyCode Key_B;

    LEUnitCentralPanel centralPanel;

    ItemInputSystem itemInput;

    private void Start() 
    {

        centralPanel = GetComponent<LEUnitCentralPanel>();

        itemInput = GetComponent<GunController>();

        if (centralPanel == null) { Debug.LogError("There is no CentralPanle Please add It"); }

        if (itemInput == null) { Debug.LogError("Losing Game System, Please Add Gun System"); }
    }

    private void Update()
    {
        UpdateInput();
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
/*
 * Log: 在原本我将此处理器 与 Input 以及 枪械控制器绑定在了一块儿。 
 *      后来决定将 枪械控制器 单独抽象出来， 并且创建 ItemInputSystem Interface 接口。
 *      
 *      其目的在于面对不同控制体系。
 *      例如：
 *          当玩家拿着枪械时， Input 直接对 手中的枪 发布指令
 *          当玩家开车时， Input 直接作用于 玩家所开的车
 *          当玩家空着手时， Input 。。。。。。。。
 *     
 
 */
