using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LEUnitAnimatorPr : MonoBehaviour {

    LEUnitProcessor processor;

    protected Animator animator;
    // Use this for initialization
    protected virtual void Start () {
        processor = GetComponent<LEUnitProcessor>();
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
        if (animator == null)
            Debug.LogError("There is no Animator in this Object, or its children");
    }

    public abstract void UpdateAnimation();

    public virtual void SetKeyStatue(InputIndex index,bool state) { }

    public virtual void SetMovementForward(float forward) { }

    public virtual void SetMovementStrafe(float strafe) { }

    public virtual void SetMotionType(AnimationMotionType type) { }

    [Visin1_1.AMBCallback()]
    public virtual void EnableBasicMoveMent() {
        if (processor == null) return;
        processor.AnimationManager_EnableBasicMoveMent(true);
    }

    [Visin1_1.AMBCallback()]
    public virtual void DisableBasicMovement() {
        if (processor == null) return;
        processor.AnimationManager_EnableBasicMoveMent(false);
    }

    [Visin1_1.AMBCallback()]
    public virtual void Attack_Statue_On()
    {
        if (processor == null) return;
        processor.AnimationManager_SetAnimationStatue(AnimationAttackStatue.OnAttack);
    }

    [Visin1_1.AMBCallback()]
    public virtual void Attack_Statue_Off()
    {
        if (processor == null) return;
        processor.AnimationManager_SetAnimationStatue(AnimationAttackStatue.Off);
    }

    public enum AnimationAttackStatue
    {
        OnAttack,
        Off
    }

    public enum AnimationMotionType
    {
        normal,
        melee,
        holdGun
    }
}
