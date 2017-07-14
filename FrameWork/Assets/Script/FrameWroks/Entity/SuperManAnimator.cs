using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperManAnimator : LEUnitAnimatorPr {

    protected override void Start()
    {
        base.Start();
    }

    protected override void MailBox_LE_AnimationEvent(LE_Animation_Event e)
    {
        if (e.Type == LE_Animation_EventType.Stun)
        {
            LE_Animation_Event_Stun stun = (LE_Animation_Event_Stun)e;
            执行眩晕动画(stun);
        }
        if (e.Type == LE_Animation_EventType.SlowDown)
        {
            LE_Animation_Event_SlowDown slowDown = (LE_Animation_Event_SlowDown)e;
            执行减速动画(slowDown);
        }
    }



    void 执行眩晕动画(LE_Animation_Event_Stun stun)
    {
        Debug.LogFormat("执行动画 被眩晕{0}秒", stun.stunTime);
        Debug.LogFormat("执行动画 被眩晕粒子特效{0}", stun.stunEffectIndex);
    }

    void 执行减速动画(LE_Animation_Event_SlowDown info)
    {
        Debug.LogFormat("执行动画 被减速{0}秒", info.减速时间);
        Debug.LogFormat("执行动画 被减速百分之{0}", info.减速值);
    }
}
