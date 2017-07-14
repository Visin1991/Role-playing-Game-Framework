using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManue : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameUIPr.Instance.mainMenu = transform;
		gameObject.SetActive(false);
	}
		
}
