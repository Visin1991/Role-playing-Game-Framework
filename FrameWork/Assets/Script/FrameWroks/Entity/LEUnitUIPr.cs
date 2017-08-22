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
        cp.Bind_LE_UI_Event_MailBox(MailBox_LE_UIEvent);
	}

    void MailBox_LE_UIEvent(LE_UI_Event uiEvent)
    {
        if (uiEvent.Type == LE_UI_EventType.UpdateHealthBar)
        {
            //detecte if the this main game UI need update
            if (attachMainWindow)
            {
                GameUIPr.Instance.MailBox_LE_UI_Event(uiEvent);
            }
           
            //check if the unit in side the camera
            if (inRenderRange)
            {
                Debug.Log("excute the health bar on the head of the unit");
                
                LE_UI_Event_UpdateHealthBar healthBarInfo = (LE_UI_Event_UpdateHealthBar)uiEvent;
                Debug.LogFormat("excute UI current health：{0}，total health{1}，percentage of {2}health", healthBarInfo.currentHealth,healthBarInfo.maxHealth,healthBarInfo.currentHealth/healthBarInfo.maxHealth);
            }
        }
        else if (uiEvent.Type == LE_UI_EventType.GetStun)
        {
          
            if (attachMainWindow)
            {   
                GameUIPr.Instance.MailBox_LE_UI_Event(uiEvent);
            }

            if (inRenderRange)
            {
                LE_UI_Event_GetStun stun = (LE_UI_Event_GetStun)uiEvent;
                Debug.Log("excute UI....。。。。");
                Debug.LogFormat("excute UI stun time for{0}秒", stun.stunTime);
            }
        }

    }
}
