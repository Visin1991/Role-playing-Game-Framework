//	Most of time, Each Device will only have one player. So this component is actually not 
//necessary in the GameManager Level. Except for multi-player-single device game. like fighting game
//bomb man.....
//  But we cal also use it to manage player acount in single game. and save the custom key input setting, customer UI setting...

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UserCentalInfoPr : Singleton<UserInputPr> {
	protected UserCentalInfoPr(){} // guarantee this will be always a singleton only - can't use the constructor!

    public void LoadUserInfo()
	{
		
	}
}