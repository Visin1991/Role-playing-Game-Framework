using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProcessor : LEUnitProcessor {

    private Vector2 inputVH;
    public override Vector2 InputVH { get { return inputVH; } }

    LEUnitCentralPanel centralPanel;
    LE_Animation_Event_moveInfo animationMoveInfo;

    private void Start()
    {
        centralPanel = GetComponent<LEUnitCentralPanel>();
        animationMoveInfo.Init();
    }

    private void Update()
    {
        centralPanel.Rise_LE_Animation_Event(animationMoveInfo);
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

}
