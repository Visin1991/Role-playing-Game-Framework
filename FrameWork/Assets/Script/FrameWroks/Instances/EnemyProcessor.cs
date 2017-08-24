using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProcessor : LEUnitProcessor {

    InputActionManager itemInput;

    private Vector2 inputVH;
    public  Vector2 InputVH { get { return inputVH; } }

    private void Start()
    {

    }

    private void Update()
    {
        
    }

    public void SetUpAnimationMoveInfo()
    {

    }

    //===============================================
    // Mail Box------Event Comb
    //===============================================

    public override void SetToRangeWeaponModel()
    {
        
    }

    public override void SetToDefaultModel()
    {
       
    }

    public override void Pause(bool p)
    {
        
    }

    public override void GetDamage()
    {
        
    }

    //===============================================
    // Mail Box------Message from Sub-Component
    //===============================================

    public override void GetKey_A_Down()
    {
        itemInput.GetKey_A_Down();
    }

    public override void GetKey_A()
    {
        itemInput.GetKey_A();
    }

    public override void GetKey_A_Up()
    {
        itemInput.GetKey_A_Up();
    }

    public override void GetKey_B_Down()
    {
        itemInput.GetKey_B_Down();
    }

    public override void GetKey_B_Up()
    {
        
    }
    
}
