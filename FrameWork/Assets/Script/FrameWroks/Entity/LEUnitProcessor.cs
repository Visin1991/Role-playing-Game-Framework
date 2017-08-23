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
    LEUnitAnimatorPr.AnimationAttackStatue animationAttackStatue = LEUnitAnimatorPr.AnimationAttackStatue.Off;
    protected LEUnitAnimatorPr.AnimationAttackStatue AnimationAttackStatue { get { return animationAttackStatue; } }

    public virtual void SetToRangeWeaponModel() { }

    public virtual void SetToMeleeWeaponModel() { }

    public virtual void SetToDefaultModel() { }

    public virtual void EquipWeapon(IInputActable iinputActable) { GetComponent<InputActionManager>().ResetClient(iinputActable); }

    public abstract void Pause(bool b);

    //======================================================================
    //Recive massage from AnimationManager.
    //======================================================================
    public virtual void AnimationManager_EnableBasicMoveMent(bool isable) { enableBaiscMovement = isable; }
    public virtual void AnimationManager_SetAnimationStatue(LEUnitAnimatorPr.AnimationAttackStatue s) { animationAttackStatue = s; }
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
