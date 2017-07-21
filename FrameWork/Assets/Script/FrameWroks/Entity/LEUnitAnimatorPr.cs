using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LEUnitCentralPanel))]
public abstract class LEUnitAnimatorPr : MonoBehaviour {

    protected Animator animator;
    protected LEUnitCentralPanel cp;
    // Use this for initialization
    protected virtual void Start () {

        cp = GetComponent<LEUnitCentralPanel>();
        cp.Bind_LE_Animation_Event_MailBox(MailBox_LE_AnimationEvent);

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
        if (animator == null)
            Debug.LogError("There is no Animator in this Object, or its children");

    }
   
    protected abstract void MailBox_LE_AnimationEvent(LE_Animation_Event e);

    [Visin1_1.AMBCallback()]
    public virtual void CallBack1() { }

    [Visin1_1.AMBCallback()]
    public virtual void CallBack2() { }

}
