using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponController : ItemInputSystem {

    LEUnitCentralPanel cp;
    LE_Animation_Event_GetInput info;

    private void OnEnable()
    {
        InitItems();
    }
    // Update is called once per frame
    void Update () {
		
	}

    void InitItems()
    {
        cp = GetComponent<LEUnitCentralPanel>();
        info.Init();
    }

    public override void ShutDown()
    {
        
    }

    public override void GetKey_A_Down()
    {
        info.inputIndex = InputIndex.A;
        info.InputValue = true;
        cp.Rise_LE_Animation_Event(info);
    }
    public override void GetKey_A()
    {
        
    }

    public override void GetKey_A_Up()
    {
        
    }

    public override void GetKey_B_Down()
    {
        info.inputIndex = InputIndex.B;
        info.InputValue = true;
        cp.Rise_LE_Animation_Event(info);
    }
}
