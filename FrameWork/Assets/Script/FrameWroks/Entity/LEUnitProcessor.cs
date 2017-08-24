using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Living Entity 属性数据处理器
//主 CPU。 用于分析和处理各部件的数据，并且发布任务给各个其它部件。

public abstract class LEUnitProcessor : MonoBehaviour {

    protected InputActionManager inputActionManager;
    protected LEUnitBasicMoveMent basicMovementManager;
    protected LEUnitAnimatorPr animationManager;

    protected bool enableBaiscMovement = true;
    

    [HideInInspector]
    Transform target;
    Vector3 targetPos;
    

    protected virtual void Start()
    {
        inputActionManager = GetComponent<InputActionManager>();
        basicMovementManager = GetComponent<LEUnitBasicMoveMent>();
        animationManager = GetComponent<LEUnitAnimatorPr>();


    }

    public virtual void SetToRangeWeaponModel() { }

    public virtual void SetToMeleeWeaponModel() { }

    public virtual void SetToDefaultModel() { }

    public virtual void EquipWeapon(IInputActable iinputActable) { GetComponent<InputActionManager>().ResetClient(iinputActable); }

    public abstract void Pause(bool b);

    //======================================================================
    //Recive massage from AnimationManager.
    //======================================================================
    public virtual void AnimationManager_EnableBasicMoveMent(bool isable) { enableBaiscMovement = isable; }
    public virtual void AnimationManager_SetAnimationAttackStatue(LEUnitAnimatorPr.AnimationAttackStatue s) {
        inputActionManager.SetIInputActableItemStatu(s);
    }

    public virtual void LookAtTarget()
    {
        //Need to re editor the animation system. Because..... the animation Enter state only call once
        //Or we need to use Animation Event.
        if (target == null) return;
        targetPos.x = target.position.x;
        targetPos.y = transform.position.y;
        targetPos.z = target.position.z;
        transform.LookAt(targetPos);
    }

    public void SetTarget(ref Transform _target)
    {
        Debug.Log(_target.name);
        target = _target;
    }

    public virtual bool AddHealth(float addHealth) { return false; }
    //======================================================================

    //------------------------------------------------
    //   Those normally called by User input or Ai ......
    // Hold different weapon and use different animation statue.....  do different behavior
    //------------------------------------------------
    public virtual void GetKey_A_Down()
    {
        if (inputActionManager != null)
            inputActionManager.GetKey_A_Down();
    }

    public virtual void GetKey_A()
    {
        if (inputActionManager != null)
            inputActionManager.GetKey_A();
    }

    public virtual void GetKey_A_Up()
    {
        if (inputActionManager != null)
            inputActionManager.GetKey_A_Up();
    }

    public virtual void GetKey_B_Down()
    {
        if (inputActionManager != null)
            inputActionManager.GetKey_B_Down();
    }

    public virtual void GetKey_B_Up()
    {
        
    }


}
