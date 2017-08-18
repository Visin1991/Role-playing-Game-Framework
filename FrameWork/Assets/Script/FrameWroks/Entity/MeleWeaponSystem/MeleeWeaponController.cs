using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponController : ItemInputSystem {

    LEUnitCentralPanel cp;
    LE_Animation_Event_GetInput info;

    Transform weaponHolder;

    public GameObject[] weapons;
    GameObject currentWeapon;

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

        weaponHolder = GetComponentInChildren<RightHandHolder>().transform;
        EquipWeapon(weapons[0]);

    }

    public void EquiWeaponIndex(int index)
    {

    }

    void EquipWeapon(GameObject weapon)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }
        currentWeapon = Instantiate(weapon, weaponHolder.position, weaponHolder.rotation) as GameObject;
        currentWeapon.transform.SetParent(weaponHolder);
    }

    public void enableWeaponCollider()
    {
        currentWeapon.GetComponentInChildren<Collider>().enabled = true;
    }

    public void disableWeaponCollider()
    {
        currentWeapon.GetComponentInChildren<Collider>().enabled = false;
    }
    public override void ShutDown()
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }
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
