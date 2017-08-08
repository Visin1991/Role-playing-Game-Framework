using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    GameObject LoadSaveObj;
    GameObject SaveGameObj;

    GameObject backGroundObj;

    SYS_UI_Event_UpdateHealthBar healthBarInfo;

    int lastLoadIndex = 0;

    public GameData gameData;

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
       // Debug.Log("GameCentalPr Start");
        //mainManue Panel
        if (mainManueObj == null)
        {
            mainManueObj = GetComponentInChildren<MainManue>().gameObject;
        }
        //Setting Panel
        if (settingPanelObj == null)
        {
            settingPanelObj = GetComponentInChildren<SettingPanel>().gameObject;
        }
        if (settingPanelObj != null)
        {
            settingPanelObj.SetActive(false);
        }
        //LoadSave Panel
        if (LoadSaveObj == null)
        {
            LoadSaveObj = GetComponentInChildren<LoadSavePanel>().gameObject;
        }
        if (LoadSaveObj != null)
        {
            LoadSaveObj.SetActive(false);
        }
        if (SaveGameObj == null)
        {
            SaveGameObj = GetComponentInChildren<SaveGamePanel>().gameObject;
        }
        if (SaveGameObj != null)
        {
            SaveGameObj.SetActive(false);
        }

        backGroundObj = GetComponentInChildren<StartbackGround>().gameObject;

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

    public void SwitchActivePanel(PanelType target)
    {
        //Enable Target
        if (target == PanelType.MainMenue)
        {
            mainManueObj.SetActive(true);
        }
        else if (target == PanelType.Setting)
        {
            settingPanelObj.SetActive(true);
        }
        else if (target == PanelType.LoadSave)
        {
            LoadSaveObj.SetActive(true);
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

            if (Adapter_Pause!=null)
                Adapter_Pause.Invoke(mainManueObj.activeSelf);
        }
    }

    void ResumeFromeSave()
    {
        SaveGameObj.SetActive(false);
        Time.timeScale = 1;

        if (Adapter_Pause != null)
            Adapter_Pause.Invoke(false);
        
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

    public void StartNewGame()
    {
        mainManueObj.SetActive(false);
        Time.timeScale = 1;
        backGroundObj.SetActive(false);
        //SceneManager.LoadScene("LoadScene", LoadSceneMode.Single);
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }

    public void OpenSavelPanel()
    {
        if (SaveGameObj != null)
        {
            SaveGameObj.SetActive(!SaveGameObj.activeSelf);
            if (SaveGameObj.activeSelf)
            {
                SaveGameObj.transform.SetAsLastSibling();
            }
            Time.timeScale = SaveGameObj.activeSelf ? 0 : 1;

            if (Adapter_Pause != null)
                Adapter_Pause.Invoke(SaveGameObj.activeSelf);
        }
    }

    public void LoadSave(int index)
    {
        
        if (index < 0 && index > gameData.saves.Length) {
            Debug.LogErrorFormat("Sene {0} is out of range", index);
            return;
        }

        if (gameData.saves[index - 1].isEmpty)
        {
            Debug.LogFormat("Save {0} is empty", index);
        }else
        {
            Time.timeScale = 1;
            lastLoadIndex = index;
            backGroundObj.SetActive(false);
            LoadSaveObj.SetActive(false);
            string sceneName = gameData.saves[index - 1].sceneName;
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
        
    }

    public void  LoadLPlayerDataFromLastIndex()
    {
        if (lastLoadIndex > 0 && lastLoadIndex <= gameData.saves.Length)
            FindObjectOfType<LPlayer>().gameObject.transform.position = gameData.saves[lastLoadIndex - 1].playerData.pos;
    }

    public void SaveGame(int index)
    {
        ResumeFromeSave();
        gameData.saves[index - 1].sceneName = SceneManager.GetActiveScene().name;
        gameData.saves[index - 1].playerData.pos = FindObjectOfType<LPlayer>().gameObject.transform.position;
        gameData.saves[index - 1].isEmpty = false;
    }

    public enum PanelType
    {
        MainMenue,
        Setting,
        LoadSave,
        SaveGame
    }
}

