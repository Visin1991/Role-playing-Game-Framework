using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Living Entity 属性数据处理器
//主 CPU。 用于分析和处理各部件的数据，并且发布任务给各个其它部件。

public abstract class LEUnitProcessor : MonoBehaviour {

    public abstract void MailBox_LE_ProcessEvent(LEEvent e);

    public abstract void MailBox_LE_ProcessEvent(LEEvent_GetDamage e);

    public abstract void MailBox_LE_ProcessEvent(LEEvent_StartDrive e);

    public abstract void MailBox_LE_ProcessEvent(LEEvent_StartFlyModel e);

    public abstract void MailBox_LE_ProcessEvent(LEEvent_StartHoldGunModel e);

    public abstract void MailBox_LE_ProcessEvent(LEEvent_StartMeleeModel e);

    public abstract void MailBox_LE_ProcessEvent(LEEvent_StartNonModel e);

    public abstract Vector2 InputVH { get; }
}
