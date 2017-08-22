using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///     We split a weapon to several Component. This Sword1 Script will hold the root of the sword1
/// We put the actual mesh, physics, riggid body into it's child class. So we can easily add axtra mesh
/// or do some transform offset to it.
/// </summary>
public class Sword1 : Weapon, IInputActable, ItemOnGUIDoubleClickable {

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
        inputActionManager.ChangeAnimationMotionType(LEUnitAnimatorPr.AnimationMotionType.MELEE_1);

    }

    public void GetKey_A()
    {
       
    }

    public void GetKey_A_Down()
    {
        inputActionManager.AnimationManager.SetKeyStatue(InputIndex.A, true);
    }

    public void GetKey_A_Up()
    {
        inputActionManager.AnimationManager.SetKeyStatue(InputIndex.A, false);
    }

    public void GetKey_B_Down()
    {
        inputActionManager.AnimationManager.SetKeyStatue(InputIndex.B, true);
    }

    public void ShutDown()
    {

    }

    //This function will be called Even if the gameObject of sword1 already destroyed....
    //I dont know why this happend
    public void ItemOnGUIDoubleClick()
    {
        gameObject.SetActive(true);
        GameCentalPr.Instance.PlayerInfomationProcessor.EquipWeapon(this);
    }
}
