using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///     We split a weapon to several Component. This Sword1 Script will hold the root of the sword1
/// We put the actual mesh, physics, riggid body into it's child class. So we can easily add axtra mesh
/// or do some transform offset to it.
/// </summary>
public class Sword1 : Weapon, IInputActable {

    public InputActionManager inputActionManager;

    public void SetUpLayer(int layer)
    {
        GetComponentInChildren<WeaponPhysics>().SetUpLayer(layer);
    }

    public void Init(InputActionManager actionManager)
    {
        inputActionManager = actionManager;
        transform.position = inputActionManager.RightHandMid1.position;
        transform.rotation = inputActionManager.RightHandMid1.rotation;
        transform.SetParent(inputActionManager.RightHandMid1);
    }

    public void GetKey_A()
    {

    }

    public void GetKey_A_Down()
    {

    }

    public void GetKey_A_Up()
    {

    }

    public void GetKey_B_Down()
    {

    }

    public void ShutDown()
    {

    }
    
}
