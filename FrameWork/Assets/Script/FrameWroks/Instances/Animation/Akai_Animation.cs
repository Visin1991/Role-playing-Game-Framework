using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Akai_Animation : LEUnitAnimatorPr
{

    protected override void Start()
    {
        base.Start();
    }

    protected override void MailBox_LE_AnimationEvent(LE_Animation_Event e)
    {
        if (e.Type == LE_Animation_EventType.moveInfo)
        {
            ProcessMovementInfo((LE_Animation_Event_moveInfo)e);
        }
        else if (e.Type == LE_Animation_EventType.shootInfo)
        {
            ProcessShootInfo((LE_Animation_Event_shootInfo)e);
        }
        else if (e.Type == LE_Animation_EventType.Stun)
        {
            StunAnimation((LE_Animation_Event_Stun)e);
        }
        else if (e.Type == LE_Animation_EventType.SlowDown)
        {
            SlowDownAnimation((LE_Animation_Event_SlowDown)e);
        }
    }

    void ProcessShootInfo(LE_Animation_Event_shootInfo info)
    { 
        animator.SetBool("Shoot", info.isShoot);
    }

    void ProcessMovementInfo(LE_Animation_Event_moveInfo info)
    {
      
        animator.SetFloat("Forward", info.forward);
        animator.SetFloat("Strafe", info.strafe);

    }

    void StunAnimation(LE_Animation_Event_Stun stun)
    {
        Debug.LogFormat(" Stun Animation For {0}Seconds", stun.stunTime);
        Debug.LogFormat(" Stun Special Effect {0}", stun.stunEffectIndex);
    }

    void SlowDownAnimation(LE_Animation_Event_SlowDown info)
    {
        Debug.LogFormat("SlowDown For {0} second", info.slowTime);
        Debug.LogFormat("SlowDown Percentage for{0}", info.slowValue);
    }

    public override void CallBack1()
    {

    }

    public override void CallBack2()
    {

    }
}
