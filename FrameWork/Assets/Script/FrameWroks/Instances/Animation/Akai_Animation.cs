using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Akai_Animation : LEUnitAnimatorPr
{

    protected override void Start()
    {
        base.Start();
    }

    public override void UpdateAnimation()
    {
       
    }

    public override void SetKeyStatue(InputIndex index, bool state)
    {
        animator.SetBool(index.ToString(), state);
    }

    public override void SetMovementForward(float forward)
    {
        animator.SetFloat("Forward", forward);
    }

    public override void SetMovementStrafe(float strafe)
    {
        animator.SetFloat("Strafe", strafe);
    }

    public override void SetMotionType(AnimationMotionType type)
    {
        animator.SetInteger("StatuType", (int)type);
    }


}
