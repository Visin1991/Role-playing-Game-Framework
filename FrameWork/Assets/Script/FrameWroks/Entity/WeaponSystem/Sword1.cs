﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///     We split a weapon to several Component. This Sword1 Script will hold the root of the sword1
/// We put the actual mesh, physics, riggid body into it's child class. So we can easily add axtra mesh
/// or do some transform offset to it.
/// </summary>
public class Sword1 : Weapon, IInputActable, ItemOnGUIDoubleClickable {

    public InputActionManager inputActionManager;
    ItemHandleOnObj handle;

    RaycastHit hit = new RaycastHit();
    Vector3 targetPos;

    public void SetUpLayer(int layer)
    {
        gameObject.layer = layer;
        WeaponCollider weaponCollider = GetComponentInChildren<WeaponCollider>(); ;
        weaponCollider.SetUpLayer(layer);
        weaponCollider.Active = true;
    }

    public void Init(InputActionManager actionManager)
    {
        inputActionManager = actionManager;
        transform.position = inputActionManager.RightHandMid1.position;
        transform.rotation = inputActionManager.RightHandMid1.rotation;
        transform.SetParent(inputActionManager.RightHandMid1);
        inputActionManager.ChangeAnimationMotionType(LEUnitAnimatorPr.AnimationMotionType.MELEE_1);
        DisableCollision();
        handle = GetComponentInChildren<ItemHandleOnObj>();
    }

    public void GetKey_A()
    {

    }

    public void GetKey_A_Down()
    {
        Visin1_1.MouseAndCamera.GetScreenPointToRayColliderInfo(out hit, (1 << 10));
        if (hit.transform != null)
        {
            targetPos.x = hit.transform.position.x;
            targetPos.y = transform.root.position.y;
            targetPos.z = hit.transform.position.z;
            transform.root.LookAt(targetPos);
            inputActionManager.AnimationManager.SetKeyStatue(InputIndex.A, true);
        } 
    }

    public void GetKey_A_Up()
    {
        //inputActionManager.AnimationManager.SetKeyStatue(InputIndex.A, false);
    }

    public void GetKey_B_Down()
    {
        inputActionManager.AnimationManager.SetKeyStatue(InputIndex.B, true);
    }

    public void ShutDown()
    {
        LEInventory inventory = transform.root.GetComponentInChildren<LEInventory>();
        inventory.AddItem(handle.item, transform);
        transform.gameObject.SetActive(false);
    }

    public void DisableCollision()
    {
        GetComponentInChildren<Collider>().enabled = false;
    }

    public void EnableCollision()
    {
        GetComponentInChildren<Collider>().enabled = true;
    }

    public void SetIInputActableItemStatu(LEUnitAnimatorPr.AnimationAttackStatue s)
    {
        if (s == LEUnitAnimatorPr.AnimationAttackStatue.OnAttack)
        {
            EnableCollision();
        }
        else
        {
            DisableCollision();
        }
    }

    public void ItemOnGUIDoubleClick(ItemHandleOnGUI obj)
    {
        gameObject.SetActive(true);
        GameCentalPr.Instance.PlayerInputActionManager.ResetClient(this);
        obj.Clean();
        CursorManager.GetInstance().setMouse();
    }
}
