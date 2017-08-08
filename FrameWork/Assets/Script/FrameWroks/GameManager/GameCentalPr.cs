using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

//
// Set Up the Tutorial System..........
//
// The Singleton patern make sure all of the publc variables of the GameCentalPr
// can be access by  for example GameCentalPr.Instance.pause
public class GameCentalPr : Singleton<GameCentalPr> {
	protected GameCentalPr(){} // guarantee this will be always a singleton only - can't use the constructor!

	public System.Action<bool> Adapter_Pause;
	public System.Action Adapter_GameOver;
    
    GameObject settingPanelObj;

    public void MailBox_SYS_UIEvent(SYS_UI_Event uiEvent)
	{
        if (uiEvent.Type == SYS_UI_EventType.StartGame)
        {
            Debug.Log("StartGame");
        }
        else if (uiEvent.Type == SYS_UI_EventType.PauseEvent)
        {
            PauseResumeEvent();
        }
        else if (uiEvent.Type == SYS_UI_EventType.PrintSomeInfo)
        {
            SYS_UI_Event_PrintSomeInfo printInfo = (SYS_UI_Event_PrintSomeInfo)uiEvent;
            Debug.LogFormat("The mouse Position is {0}, {1}", printInfo.mousePosition, printInfo.whatYouWantToSay);
        }
	}

    void Start()
    {
        if (settingPanelObj == null)
        {
            settingPanelObj = GetComponentInChildren<SettingPanel>().gameObject;
        }
        if (settingPanelObj != null)
        {
            settingPanelObj.SetActive(false);
        }
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseResumeEvent();
        }
        
    }

    void OnApplicationPause(bool pause)
    {

    }

    public void PauseResumeEvent()
    {
        if (settingPanelObj != null)
        {
            settingPanelObj.SetActive(!settingPanelObj.activeSelf);
            if (settingPanelObj.activeSelf)
            {
                settingPanelObj.transform.SetAsLastSibling();
            }
            Time.timeScale = settingPanelObj.activeSelf ? 0 : 1;
            Adapter_Pause.Invoke(settingPanelObj.activeSelf);
        }
    }

    void OnApplicationQuit()
    {

    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

