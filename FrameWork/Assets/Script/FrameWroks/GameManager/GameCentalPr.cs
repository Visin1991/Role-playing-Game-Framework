using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// Set Up the Tutorial System..........
//
// The Singleton patern make sure all of the publc variables of the GameCentalPr
// can be access by  for example GameCentalPr.Instance.pause
public class GameCentalPr : Singleton<GameCentalPr> {
	protected GameCentalPr(){} // guarantee this will be always a singleton only - can't use the constructor!

    /// <summary>
    /// 相当于插座，用来给外界传递信息，当对的应事件发生时，发送信息给那些把插头放在对应插座上的人
    /// </summary>
	public System.Action Adapter_Pause;
	public System.Action Adapter_GameOver;
	
    /// <summary>
    /// 相当于信箱， 接收来自于外界信息的接口
    /// </summary>
    /// <param name="uiEvent"></param>
	public void MailBox_SYS_UIEvent(SYS_UI_Event uiEvent)
	{
		if(uiEvent.Type == SYS_UI_EventType.StartGame)
		{
			Debug.Log("StartGame");
		}

		if(uiEvent.Type == SYS_UI_EventType.PrintSomeInfo)
		{
			SYS_UI_Event_PrintSomeInfo printInfo = (SYS_UI_Event_PrintSomeInfo)uiEvent;
			Debug.LogFormat("The mouse Position is {0}, {1}",printInfo.mousePosition,printInfo.whatYouWantToSay);
		}
	}

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    void Update()
    {
    }

}

