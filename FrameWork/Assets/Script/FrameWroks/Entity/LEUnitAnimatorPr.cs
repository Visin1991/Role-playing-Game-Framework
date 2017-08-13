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
   
    public abstract void MailBox_LE_AnimationEvent(LE_Animation_Event e);

    [Visin1_1.AMBCallback()]
    public virtual void EnableBasicMoveMent() {
        if (processor == null) return;
        LE_BasicMovement_Event_Enable enable = new LE_BasicMovement_Event_Enable();
        enable.Init();
        processor.MailBox_LE_AnimationManager_CallBack(enable);
    }

    [Visin1_1.AMBCallback()]
    public virtual void DisableBasicMovement() {
        if (processor == null) return;
        LE_BasicMovement_Event_Disable basicDisable = new LE_BasicMovement_Event_Disable();
        basicDisable.Init();
        processor.MailBox_LE_AnimationManager_CallBack(basicDisable);
    }

    [Visin1_1.AMBCallback()]
    public virtual void Attack_Statue_True()
    {

    }

    [Visin1_1.AMBCallback()]
    public virtual void Attack_Statue_False()
    {

    }
}
