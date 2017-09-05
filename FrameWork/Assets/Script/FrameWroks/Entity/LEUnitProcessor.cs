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

    protected bool alive = true;
    public bool Alive { get { return alive; } }
    public bool IsAlive() { return alive; }

    private void OnEnable()
    {
        inputActionManager = GetComponent<InputActionManager>();
        basicMovementManager = GetComponent<LEUnitBasicMoveMent>();
        animationManager = GetComponent<LEUnitAnimatorPr>();
    }

    protected virtual void Start()
    {
        
    }

    public abstract void Pause(bool b);

    //======================================================================
    //Recive massage from AnimationManager.
    //======================================================================
    public virtual void AnimationManager_EnableBasicMoveMent(bool isable) { enableBaiscMovement = isable; }

    //We shold Have a statue Enume---and the animation can call back and set current animation state.
    //for example--- On walking, On Runing, On Attack, On Sky what ever.....
    //And also have a combied animation state......use bit-wise oporation....
    public virtual void AnimationManager_SetAnimationAttackStatue(LEUnitAnimatorPr.AnimationAttackStatue s) {
        if (inputActionManager == null) return;
        inputActionManager.SetIInputActableItemStatu(s);
    }

    public virtual bool AddHealth(float addHealth) { return false; }

    //=========================================================================
    //Sometimes When we want to process information about something, we need
    //a target
    //=========================================================================
    protected Transform target;
    protected Vector3 targetPos;

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

    public virtual void LostTarget()
    {
        target = null;
    }

}
