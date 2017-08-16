using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController :  ItemInputSystem {

    
    public Gun[] allGuns;
    Gun currentGun;

    protected override void OnEnable()
    {
        base.OnEnable();
        EquipGunIndex(0);
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
        currentGun = Instantiate(gun, rightHandTF.position, rightHandTF.rotation) as Gun;
        currentGun.transform.SetParent(rightHandTF);
    }

    public override void GetKey_A_Down()
    {
        
    }


    public override void GetKey_A()
    {
        //LookAroundMouseDir();
        OnTriggerHold();
        animationManager.SetKeyStatue(InputIndex.A,true);
        
    }

    public override void GetKey_A_Up()
    {
        OnTriggerRelease();
        animationManager.SetKeyStatue(InputIndex.A,false);
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
            EquipGunIndex(0);
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
            //mousePos.y = cp.Adapter_LE_mainBody.position.y;
            //cp.Adapter_LE_mainBody.LookAt(mousePos);
        }
    }
}
