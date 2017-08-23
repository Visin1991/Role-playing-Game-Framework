using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Living Entity Unit UI Processor
/// </summary>
[RequireComponent(typeof(LEUnitCentralPanel))]
public class LEUnitUIPr : MonoBehaviour {

    LEUnitCentralPanel cp;
    public bool attachMainWindow = true;
    public bool inRenderRange=true; 

	void Start () {
        cp = GetComponent<LEUnitCentralPanel>();
        //cp.Bind_LE_UI_Event_MailBox(MailBox_LE_UIEvent);
	}

}
