using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LEUnitAnimatorPr : MonoBehaviour {

    LEUnitProcessor processor;

    protected Animator animator;

    float currentVelocity;
    bool enableMotionInput = true;
    float nextInputTime;

    System.Random rand = new System.Random();
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

    public virtual void SetKeyStatue(InputIndex index,bool state) { animator.SetBool(index.ToString(), state); }

    public virtual void SetMovementForward(float forward) { animator.SetFloat("Forward", forward); }

    public void SetMovementForwardSmoothDamp(float forward)
    {
        float current = animator.GetFloat("Forward");
        float newCurrent = Mathf.SmoothDamp(current, forward,ref currentVelocity, 0.15f);
        animator.SetFloat("Forward", newCurrent);
    }

    public virtual void SetMovementStrafe(float strafe) { animator.SetFloat("Strafe", strafe); }

    public virtual void SetMotionType(AnimationMotionType type) {if(enableMotionInput) animator.SetInteger("MotionType", (int)type); }

    public virtual void SetMotionTypeImmediately(AnimationMotionType type)
    {
        animator.SetInteger("MotionType", (int)type);
    }

    public virtual void SetMotionIndex(int motionIndex) {if(enableMotionInput)animator.SetInteger("MotionIndex", motionIndex); }

    public virtual void SetMotionIndexImmediately(int motionIndex) { animator.SetInteger("MotionIndex", motionIndex); }

    public void SetMotionIndex_Random_From_To(int from, int to)
    {
        nextInputTime += Time.deltaTime;
        if (nextInputTime > 1.0f)
        {
            int index = rand.Next(from, to);
            animator.SetInteger("MotionIndex", index);
            nextInputTime = 0.0f;
        }
    }

    public Animator GetAnimator()
    {
        return animator;
    }

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

    [Visin1_1.AMBCallback()]
    public virtual void DisableMotionInput()
    {
        enableMotionInput = true;
    }

    [Visin1_1.AMBCallback()]
    public virtual void OnableMotionInput()
    {
        enableMotionInput = false;
    }

    public enum AnimationAttackStatue
    {
        OnAttack,
        Off
    }

    public enum AnimationMotionType
    {
        IWR_0,
        MELEE_1,
        HoldGun_2
    }
}
