using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperManAnimator : LEUnitAnimatorPr
{

    float RotateAngle;

    private delegate void UpdateDel();
    UpdateDel updateDel;

    protected override void Start()
    {
        base.Start();
    }

    public override void UpdateAnimation()
    {

    }
}
