using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GameUIPr : Singleton<GameUIPr> {
	protected GameUIPr(){}

	bool isOpeMainMenu = false;

	[HideInInspector] public Transform mainMenu;
	[HideInInspector] public Transform soudMenu;
		
	void ToggleSettingUI()
	{
		if(!isOpeMainMenu)
		{
			isOpeMainMenu = !isOpeMainMenu;
			OpenMainMenu();
		}else
		{
			CloseMainMenu();
			isOpeMainMenu = !isOpeMainMenu;
		}
	}

	void OpenMainMenu()
	{
		if(mainMenu != null)
		{
			mainMenu.gameObject.SetActive(true);
		}
	}

	void CloseMainMenu()
	{
		if(mainMenu != null)
		{
			mainMenu.gameObject.SetActive(false);
		}
	}

    /// <summary>
    /// 接收有关于系统方面的UI 事件
    /// </summary>
    /// <param name="sysUIEvent"> 有关于整个游戏系统的UI 比如说退出游戏，开始游戏，音量等等 </param>
	public void MailBox_SYS_UI_Event(SYS_UI_Event sysUIEvent)
	{
		if(sysUIEvent.Type == SYS_UI_EventType.PressSettingButton)
		{
			ToggleSettingUI();
		}
		else
		{
			GameCentalPr.Instance.MailBox_SYS_UIEvent(sysUIEvent);
		}
	}

    /// <summary>
    /// 接收有关于LE单位的UI事件
    /// </summary>
    /// <param name="leUIEvent">比如说：玩家／怪物　血条，技能使用，技能冷却等等。。。。</param>
    public void MailBox_LE_UI_Event(LE_UI_Event leUIEvent)
    {
        if (leUIEvent.Type == LE_UI_EventType.UpdateHealthBar)
        {
            Debug.Log("执行主界面UI 更主界面中人物UI血条");
            LE_UI_Event_UpdateHealthBar healthBarInfo = (LE_UI_Event_UpdateHealthBar)leUIEvent;

            Debug.LogFormat("执行主界面UI 当前生命值是：{0}，总生命值是{1}，剩下百分之{2}的血量", 
                            healthBarInfo.currentHealth, 
                            healthBarInfo.maxHealth, 
                            healthBarInfo.currentHealth / healthBarInfo.maxHealth);
        }
        else if (leUIEvent.Type == LE_UI_EventType.GetStun)
        {
            LE_UI_Event_GetStun stun = (LE_UI_Event_GetStun)leUIEvent;
            Debug.Log("执行主界面UI 主界面人物UI出现眩晕图标 等等。。。。");
            Debug.LogFormat("执行主界面UI 眩晕事事件{0}秒", stun.stunTime);
        }
    }
}


