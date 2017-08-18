﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController :  ItemInputSystem {

    Transform GunHolder;
    public Gun[] allGuns;
    Gun currentGun;

    LEUnitCentralPanel cp;
    LE_Animation_Event_GetInput info;

    private void OnEnable()
    {
        InitItems();
    }

    private void Start(){InitItems();}

    void InitItems()
    {
        GunHolder = GetComponentInChildren<RightHandHolder>().transform;
        cp = GetComponent<LEUnitCentralPanel>();
        info.Init();

        EquipGun(allGuns[0]);
    }

    public override void ShutDown()
    {
        if (currentGun != null)
        {
            Destroy(currentGun.gameObject);
        }
        GetKey_A_Up();
    }

    public void EquipGunIndex(int index)
    {
        EquipGun(allGuns[index]);
    }

    void EquipGun(Gun gun)
    {
        if (currentGun != null)
        {
            Destroy(currentGun.gameObject);
        }

        currentGun = Instantiate(gun, GunHolder.position, GunHolder.rotation) as Gun;
        currentGun.transform.SetParent(GunHolder);
    }

    public override void GetKey_A_Down()
    {
        
    }

    public override void GetKey_A()
    {
        //LookAroundMouseDir();
        info.inputIndex = InputIndex.A;
        info.InputValue = true;
        OnTriggerHold();
        cp.Rise_LE_Animation_Event(info);
        
    }

    public override void GetKey_A_Up()
    {
        info.inputIndex = InputIndex.A;
        info.InputValue = false;
        OnTriggerRelease();
        cp.Rise_LE_Animation_Event(info);
    }

    public override void GetKey_B_Down()
    {
        ReLoad();
    }

    void OnTriggerHold()
    {
        if (currentGun != null)
        {
            currentGun.OnTriggerHold();
        }
        else {
            Debug.LogError("Current Gun is Null");
        }
    }

    void OnTriggerRelease()
    {
        if (currentGun != null)
        {
            currentGun.OnTriggerRelease();
        }
    }

    void ReLoad()
    {
        if (currentGun != null)
        {
            currentGun.Reload();
        }
    }

    void LookAroundMouseDir()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Visin1_1.MouseAndCamera.GetMouseGroundIntersectionPoint();
            mousePos.y = cp.Adapter_LE_mainBody.position.y;
            cp.Adapter_LE_mainBody.LookAt(mousePos);
        }
    }
}
