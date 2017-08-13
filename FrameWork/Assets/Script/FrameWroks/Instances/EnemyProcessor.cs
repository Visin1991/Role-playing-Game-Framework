using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProcessor : LEUnitProcessor {

    ItemInputSystem itemInput;

    private Vector2 inputVH;
    public  Vector2 InputVH { get { return inputVH; } }

    LE_Animation_Event_moveInfo animationMoveInfo;

    private void Start()
    {
        animationMoveInfo.Init();
    }

    private void Update()
    {
        
    }

    public void SetUpAnimationMoveInfo()
    {

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
       

    }

    public override void MailBox_LE_ProcessEvent(LEEvent_StartHoldGunModel e)
    {
        
    }

    public override void MailBox_LE_ProcessEvent(LEEvent_StartNonModel e)
    {
       
    }

    public override void Pause(bool p)
    {
        
    }

    //===============================================
    // Mail Box------Message from Sub-Component
    //===============================================
    public override void MailBox_LE_AnimationManager_CallBack(LE_BasicMovement_Event e)
    {

    }

    public override void MailBox_LE_BasicMoveMentManager_Callback()
    {
        
    }

    public override void MailBox_LE_CameraManager_Callback()
    {
        
    }

    public override void GetKey_A_Down()
    {
        itemInput.GetKey_A_Down();
    }

    public override void GetKey_A()
    {
        itemInput.GetKey_A();
    }

    public override void GetKey_A_Up()
    {
        itemInput.GetKey_A_Up();
    }

    public override void GetKey_B_Down()
    {
        itemInput.GetKey_B_Down();
    }

    public override void GetKey_B_Up()
    {
        
    }

}
