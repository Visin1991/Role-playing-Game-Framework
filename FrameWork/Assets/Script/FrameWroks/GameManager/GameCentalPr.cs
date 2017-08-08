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
    public System.Action<float, float> Adapter_Healthbar;
    
    GameObject settingPanelObj;

    GameObject mainManueObj;

    SYS_UI_Event_UpdateHealthBar healthBarInfo;

    public void MailBox_SYS_UIEvent(SYS_UI_Event uiEvent)
	{
        if (uiEvent.Type == SYS_UI_EventType.UpdateHealthBar)
        {
            if (Adapter_Healthbar != null)
            {
                healthBarInfo = (SYS_UI_Event_UpdateHealthBar)uiEvent;
                Adapter_Healthbar.Invoke(healthBarInfo.currentHealth, healthBarInfo.maxHealth);
            }
        }
        else if (uiEvent.Type == SYS_UI_EventType.StartGame)
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

        if (mainManueObj == null)
        {
            mainManueObj = GetComponentInChildren<MainManue>().gameObject;
        }
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

    public void SwitchActivePanel(PanelType current,PanelType target)
    {
        //Disable current
        if (current == PanelType.MainMenue)
        {
            mainManueObj.SetActive(false);
        }
        else if (current == PanelType.Setting)
        {
            settingPanelObj.SetActive(false);
        }

        //Enable Target
        if (target == PanelType.MainMenue)
        {
            mainManueObj.SetActive(true);
        }
        else if (target == PanelType.Setting)
        {
            settingPanelObj.SetActive(true);
        }
        
    }

    public void PauseResumeEvent()
    {
        if (mainManueObj != null)
        {
            mainManueObj.SetActive(!mainManueObj.activeSelf);
            if (mainManueObj.activeSelf)
            {
                mainManueObj.transform.SetAsLastSibling();
            }
            Time.timeScale = mainManueObj.activeSelf ? 0 : 1;
            Adapter_Pause.Invoke(mainManueObj.activeSelf);
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

    public enum PanelType
    {
        MainMenue,
        Setting
    }
}

