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

    public override void SetMotionType(AnimationMotionType type)
    {
        animator.SetInteger("StatuType", (int)type);
    }


}
