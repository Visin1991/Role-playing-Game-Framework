using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralProcessorB : LEUnitProcessor {

    IInputActable wapon1;
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        wapon1 = GetComponentInChildren<IInputActable>();
        EquipWeapon(wapon1);
    }

    public override void Pause(bool b)
    {
        
    }

    public override void GetDamage()
    {
        animationManager.SetTrigger("Impact");
    }

}
