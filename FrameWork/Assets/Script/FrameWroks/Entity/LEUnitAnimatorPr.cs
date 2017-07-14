using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LEUnitCentralPanel))]
public abstract class LEUnitAnimatorPr : MonoBehaviour {
	// Use this for initialization
	protected virtual void Start () {
        LEUnitCentralPanel cp = GetComponent<LEUnitCentralPanel>();
        cp.Bind_LE_Animation_Event_MailBox(MailBox_LE_AnimationEvent);
	}
   
    protected abstract void MailBox_LE_AnimationEvent(LE_Animation_Event e);
     
}
