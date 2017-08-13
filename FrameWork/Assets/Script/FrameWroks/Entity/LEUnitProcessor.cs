using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Living Entity 属性数据处理器
//主 CPU。 用于分析和处理各部件的数据，并且发布任务给各个其它部件。

public abstract class LEUnitProcessor : MonoBehaviour {

    protected delegate void CentralPanelUpdateDel();
    protected CentralPanelUpdateDel tpspUpdateDel;

    protected UserInputPr userInputManager;
    protected LEUnitBasicMoveMent basicMovementManager;
    protected LEUnitAnimatorPr animationManager;
    protected Visin1_1.CameraManager cameraManager;

    public abstract void MailBox_LE_ProcessEvent(LEEvent e);

    public abstract void MailBox_LE_ProcessEvent(LEEvent_GetDamage e);

    public abstract void MailBox_LE_ProcessEvent(LEEvent_StartDrive e);

    public abstract void MailBox_LE_ProcessEvent(LEEvent_StartFlyModel e);

    public abstract void MailBox_LE_ProcessEvent(LEEvent_StartHoldGunModel e);

    public abstract void MailBox_LE_ProcessEvent(LEEvent_StartMeleeModel e);

    public abstract void MailBox_LE_ProcessEvent(LEEvent_StartNonModel e);

    public abstract void Pause(bool b);

    public virtual void MailBox_LE_AnimationManager_CallBack(LE_BasicMovement_Event e) { }

    public virtual void MailBox_LE_BasicMoveMentManager_Callback() { }

    public virtual void MailBox_LE_CameraManager_Callback() { }

    public abstract void GetKey_A_Down();

    public abstract void GetKey_A();

    public abstract void GetKey_A_Up();

    public abstract void GetKey_B_Down();

    public abstract void GetKey_B_Up();
}
