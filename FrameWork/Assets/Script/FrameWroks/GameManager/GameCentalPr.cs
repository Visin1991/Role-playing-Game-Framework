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

    public GameData gameData;

    public System.Action<bool> Adapter_Pause;
	public System.Action Adapter_GameOver;

    int currentLevel = 0;
    int lastLoadIndex = 0;
    bool loadSaveData;

    GameObject LplayerObj;
    LEUnitProcessorBase leUnitProcessor;
    LEUnitAnimatorManager leUnitAnimationManager;
    LEUnitBasicMoveMentManager leUnitBasicMovementManager;
    InputClientManager inputActionManager;
    
    public LEUnitProcessorBase PlayerProcessor {get { if (leUnitProcessor == null) { InitalLPlayer(); } return leUnitProcessor; }}
    public LEUnitAnimatorManager PlayerAnimationManager { get { if (leUnitAnimationManager == null) { InitalLPlayer(); } return leUnitAnimationManager; } }
    public LEUnitBasicMoveMentManager PlayerBasicMovementManager { get { if (leUnitBasicMovementManager == null) { InitalLPlayer(); } return leUnitBasicMovementManager; } }
    public InputClientManager PlayerInputActionManager { get { if (leUnitBasicMovementManager == null) { InitalLPlayer(); } return inputActionManager; } }

    void Start()
    {
        PlayerProcessor.transform.parent = transform;
        LplayerObj = PlayerProcessor.gameObject;
        LplayerObj.SetActive(false);
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameUIPr.Instance.MainUIMenueEvent();
        }
        
    }

    void InitalLPlayer()
    {
        LplayerObj = FindObjectOfType<LPlayer>().gameObject;
        leUnitProcessor = LplayerObj.GetComponent<LEUnitProcessorBase>();
        leUnitAnimationManager = LplayerObj.GetComponent<LEUnitAnimatorManager>();
        leUnitBasicMovementManager = LplayerObj.GetComponent<LEUnitBasicMoveMentManager>();
        inputActionManager = LplayerObj.GetComponent<InputClientManager>();
    }

    void OnApplicationPause(bool pause)
    {

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
        currentLevel = 1;
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void LoadSave(int index)
    {
        lastLoadIndex = index;
        if (index < 0 && index > gameData.saves.Length) {
            Debug.LogErrorFormat("Sene {0} is out of range", index);
            return;
        }

        if (gameData.saves[index - 1].isEmpty)
        {
            Debug.LogFormat("Save {0} is empty", index);
        }else
        {
            loadSaveData = true;
            string sceneName = gameData.saves[index - 1].sceneName;
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
        
    }

    public void EnablePlayerObj()
    {
        LplayerObj.SetActive(true);
    }

    public void SaveGame(int index)
    {
        gameData.saves[index - 1].sceneName = SceneManager.GetActiveScene().name;
        gameData.saves[index - 1].playerData.pos = FindObjectOfType<LPlayer>().gameObject.transform.position;
        gameData.saves[index - 1].isEmpty = false;
    }

    public void PauseResumeEvent(bool pause)
    {
        Time.timeScale = pause ? 0 : 1;
        if (Adapter_Pause != null)
            Adapter_Pause.Invoke(pause);
    }

    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index, LoadSceneMode.Single);
    }

    public void NextLevel()
    {
        currentLevel++;
        Debug.Log(currentLevel);
        SceneManager.LoadScene(currentLevel, LoadSceneMode.Single);
    }

    public Transform GetPlayerTransform()
    {
        if (LplayerObj == null)
        {
            LplayerObj = PlayerProcessor.gameObject;
        }
        if (LplayerObj != null)
            return LplayerObj.transform;
        else
            return null;
    }
    /// <summary>
    /// This function will be called when the SceneInitializer Script Start function get auto called by Unity.
    /// </summary>
    public void LoadSave()
    {
        if (loadSaveData)
        {
            if (lastLoadIndex > 0 && lastLoadIndex <= gameData.saves.Length)
                FindObjectOfType<LPlayer>().gameObject.transform.position = gameData.saves[lastLoadIndex - 1].playerData.pos;
            loadSaveData = false;
        }
    }
}

