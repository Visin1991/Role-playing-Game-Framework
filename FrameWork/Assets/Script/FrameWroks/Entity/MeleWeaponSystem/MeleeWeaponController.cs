using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponController : MonoBehaviour {

    protected Transform rightHandTF;
    public Sword1[] weapons;
    Sword1 currentWeapon;

    protected void OnEnable()
    {
        EquiWeaponIndex(0);
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

    public void ShutDown()
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }
    }

    public void GetKey_A_Down()
    {
        
    }

    public void GetKey_A()
    {
        
    }

    public void GetKey_A_Up()
    {
        
    }

    public void GetKey_B_Down()
    {
        
    }
}
