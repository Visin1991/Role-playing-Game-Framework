using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponController : ItemInputSystem {

    public Sword1[] weapons;
    Sword1 currentWeapon;

    protected override void OnEnable()
    {
        base.OnEnable();
        EquiWeaponIndex(0);
    }

    // Update is called once per frame
    void Update () {
		
	}

    void InitItems()
    {
        rightHandTF = GetComponentInChildren<RightHandHolder>().transform;
        if(weapons.Length >0)
            EquipWeapon(weapons[0]);

    }

    public void EquiWeaponIndex(int index)
    {
        EquipWeapon(weapons[0]);
    }

    void EquipWeapon(Sword1 weapon)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }
        currentWeapon = Instantiate(weapon, rightHandTF.position, rightHandTF.rotation) as Sword1;
        currentWeapon.transform.SetParent(rightHandTF);
        currentWeapon.SetUpLayer(transform.root.gameObject.layer);
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
        animationManager.SetKeyStatue(InputIndex.A,true);
    }

    public override void GetKey_A()
    {
        
    }

    public override void GetKey_A_Up()
    {
        
    }

    public override void GetKey_B_Down()
    {
        animationManager.SetKeyStatue(InputIndex.B, true);
    }
}
