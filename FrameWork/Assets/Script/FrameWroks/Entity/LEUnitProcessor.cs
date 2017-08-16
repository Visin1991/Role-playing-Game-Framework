using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Living Entity 属性数据处理器
//主 CPU。 用于分析和处理各部件的数据，并且发布任务给各个其它部件。

public abstract class LEUnitProcessor : MonoBehaviour {

    protected delegate void CentralPanelUpdateDel();
    protected CentralPanelUpdateDel tpspUpdateDel;

    protected UserInputPr userInputManager;
    protected LEUnitBasicMoveMent basicMovementManager;
    protected LEUnitAnimatorPr animationManager;
    protected Visin1_1.CameraManager cameraManager;


    public virtual void SetToRangeWeaponModel() { }

    public virtual void SetToMeleeWeaponModel() { }

    public virtual void SetToDefaultModel() { }

    public abstract void Pause(bool b);

    //======================================================================
    //Recive massage from AnimationManager.
    //======================================================================
    public virtual void AnimationManager_EnableBasicMoveMent(bool isable) { }
    public virtual void AnimationManager_SetAnimationStatue(LEUnitAnimatorPr.AnimationAttackStatue s) { }
    //======================================================================

    public abstract void GetKey_A_Down();

    public abstract void GetKey_A();

    public abstract void GetKey_A_Up();

    public abstract void GetKey_B_Down();

    public abstract void GetKey_B_Up();
}
