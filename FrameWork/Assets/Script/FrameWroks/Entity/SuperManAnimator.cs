using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperManAnimator : LEUnitAnimatorPr {

    float RotateAngle;

    private delegate void UpdateDel();
    UpdateDel updateDel;

    protected override void Start()
    {
        base.Start();
    }

    protected override void MailBox_LE_AnimationEvent(LE_Animation_Event e)
    {
        if (e.Type == LE_Animation_EventType.moveInfo)
        {
            ProcessInfo_1( (LE_Animation_Event_moveInfo)e);
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

    public override void CallBack1()
    {
        updateDel -= AnimationLookAtMousePos_Offset;
        updateDel += AnimationLookAtMousePos_Offset;
    }

    public override void CallBack2()
    {
        updateDel -= AnimationLookAtMousePos_Offset;
    }

    void ProcessShootInfo(LE_Animation_Event_shootInfo info)
    {
        animator.SetBool("Shoot", info.isShoot);
    }

    void ProcessInfo_1(LE_Animation_Event_moveInfo info)
    {
        animator.SetFloat("Speed", info.moveSpeed);
        //RotateAngle = animator.GetFloat("RotateAngle");
    }

    void StunAnimation(LE_Animation_Event_Stun stun)
    {
        Debug.LogFormat(" Stun Animation For {0}Seconds", stun.stunTime);
        Debug.LogFormat(" Stun Special Effect {0}", stun.stunEffectIndex);
    }

    void SlowDownAnimation(LE_Animation_Event_SlowDown info)
    {
        Debug.LogFormat("SlowDown For {0}秒", info.slowTime);
        Debug.LogFormat("SlowDown Percentage for{0}", info.slowValue);
    }

    private void Update()
    {
        if (updateDel != null)
        {
            updateDel();
        }
    }

    void AnimationLookAtMousePos_Offset()
    {
        Vector3 mousePos = Visin1_1.MouseAndCamera.GetMouseGroundIntersectionPoint();

        mousePos.y = cp.Adapter_LE_mainBody.position.y;

        Vector3 toMouseVector = mousePos - cp.Adapter_LE_mainBody.position;

        Vector3 destVector = Visin1_1.WeiVector3.RotateVectorAround(toMouseVector, Vector3.up, 40);

        Vector3 destPos = cp.Adapter_LE_mainBody.position + destVector;
        
        cp.Adapter_LE_mainBody.LookAt(destPos);
    }
}
