using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GameUIPr : Singleton<GameUIPr> {
	protected GameUIPr(){}

    public System.Action<float, float> Adapter_Healthbar;
    public System.Action<float, float> Adapter_Manabar;

    [HideInInspector]
    public GameObject mainManueObj;    //This is the main Manue Object----Press Icon Botton or press Esc key
    GameObject settingPanelObj; //Panel contains the Music sound ..... Button
    GameObject LoadSaveObj;     //Load the save data Panel
    GameObject SaveGameObj;
    GameObject loadingPanelObj;
    GameObject LevelPanelObj;
    GameObject inventoryPanelObj;

    SYS_UI_Event_UpdateHealthBar healthBarInfo;
    List<GameObject> subMainPanelObjs = new List<GameObject>();
    
    private void Start()
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
            if (settingPanelObj != null)
            {
                subMainPanelObjs.Add(settingPanelObj);
                settingPanelObj.SetActive(false);
            }
        }    

        //LoadSave Panel
        if (LoadSaveObj == null)
        {
            LoadSaveObj = GetComponentInChildren<LoadSavePanel>().gameObject;
            if (LoadSaveObj != null)
            {
                subMainPanelObjs.Add(LoadSaveObj);
                LoadSaveObj.SetActive(false);
            }   
        }
       
        //SaveGamePanel;
        if (SaveGameObj == null)
        {
            SaveGameObj = GetComponentInChildren<SaveGamePanel>().gameObject;
            if (SaveGameObj != null)
            {
                subMainPanelObjs.Add(SaveGameObj);
                SaveGameObj.SetActive(false);
            }
            
        }
        
        //LevelPanel
        if (LevelPanelObj == null)
        {
            LevelPanelObj = GetComponentInChildren<LevelPanel>().gameObject;
            if (LevelPanelObj != null)
            {
                
                subMainPanelObjs.Add(LevelPanelObj);
                LevelPanelObj.SetActive(false);
            }
        }

        if (inventoryPanelObj == null)
        {
            inventoryPanelObj = GetComponentInChildren<InventoryPanel>().gameObject;
            if (inventoryPanelObj != null)
            {
                inventoryPanelObj.SetActive(false);
            }
        }

        loadingPanelObj = GetComponentInChildren<StartbackGround>().gameObject;
    }

	public void MailBox_SYS_UI_Event(SYS_UI_Event sysUIEvent)
	{
		
	}

    public void SwitchActivePanel(PanelType target)
    {
        //Enable Target
        if (target == PanelType.MainMenue)
        {
            if(mainManueObj!= null)
                mainManueObj.SetActive(true);
        }
        else if (target == PanelType.Setting)
        {
            if (settingPanelObj != null)
                settingPanelObj.SetActive(true);
        }
        else if (target == PanelType.LoadSave)
        {
            if (LoadSaveObj != null)
                LoadSaveObj.SetActive(true);
        }

    }

    public void LoadLevel(int index = 1)
    {
        GameCentalPr.Instance.LoadLevel(index);
    }

    public void OpenLevelPanel()
    {
        if (LevelPanelObj != null)
        {
            LevelPanelObj.SetActive(true);
        }
    }

    public void LoadLPlayerDataFromLastIndex()
    {
        if (loadingPanelObj != null)
            loadingPanelObj.SetActive(false);     
    }

    public void MainUIMenueEvent()
    {
        if (mainManueObj != null)
        {
            //----------------------------------------------------
            //Make sure all of the sub-Mainpanel are not active
            //----------------------------------------------------
            bool subActive = false;
            foreach (GameObject obj in subMainPanelObjs)
            {
                if (obj.activeSelf) {
                    subActive = true;
                    break;
                }
            }
            if (subActive) return;
            //----------------------------------------------------
            mainManueObj.SetActive(!mainManueObj.activeSelf);
            if (mainManueObj.activeSelf)
            {
                mainManueObj.transform.SetAsLastSibling();
            }
            GameCentalPr.Instance.PauseResumeEvent(mainManueObj.activeSelf);
        }
    }

    public void InventoryPanelEvent()
    {
        if (inventoryPanelObj != null)
        {
            inventoryPanelObj.SetActive(!inventoryPanelObj.activeSelf);
            if (inventoryPanelObj.activeSelf)
            {
                inventoryPanelObj.transform.localPosition = Vector3.zero;
            }
        }
    }

    public void AddItemToInventory(Item item)
    {
        if (inventoryPanelObj != null)
        {
            if (!inventoryPanelObj.activeSelf)
            {
                inventoryPanelObj.SetActive(true);
                inventoryPanelObj.transform.localPosition = new Vector3(999, 999, 0);
                inventoryPanelObj.GetComponent<InventoryPanel>().AddItem(item);
                inventoryPanelObj.SetActive(false);
            }
            else
            {
                inventoryPanelObj.GetComponent<InventoryPanel>().AddItem(item);
            }
        }
    }

    public void ResumeFromeSave()
    {
        SaveGameObj.SetActive(false);
        GameCentalPr.Instance.PauseResumeEvent(false);
    }

    public void StartNewGame()
    {
        mainManueObj.SetActive(false);
        Time.timeScale = 1;
        GameCentalPr.Instance.StartNewGame();
    }

    public void OpenSavePanel()
    {
        if (SaveGameObj != null)
        {
            SaveGameObj.SetActive(!SaveGameObj.activeSelf);
            if (SaveGameObj.activeSelf)
            {
                SaveGameObj.transform.SetAsLastSibling();
            }
            GameCentalPr.Instance.PauseResumeEvent(SaveGameObj.activeSelf);
        }
    }

    public void LoadSave(int index)
    {
        Time.timeScale = 1;
        

        loadingPanelObj.SetActive(true);
        LoadSaveObj.SetActive(false);

        GameCentalPr.Instance.LoadSave(index);

    }

    public void SaveGame(int index)
    {
        ResumeFromeSave();
        GameCentalPr.Instance.SaveGame(index);
    }

    public void CloseLoadingPanel()
    {
        if (loadingPanelObj != null)
            loadingPanelObj.SetActive(false);
    }

    public void NextLevel()
    {
        LevelPanelObj.SetActive(false);
        loadingPanelObj.SetActive(true);
        GameCentalPr.Instance.NextLevel();
    }

    public enum PanelType
    {
        MainMenue,
        Setting,
        LoadSave,
        SaveGame
    }
}


