using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSceneGUIHelper : MonoBehaviour {

    public float health = 100;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnGUI()
    {
        GUILayout.Label(new GUIContent("Player Health : " + health.ToString()));
    }
}
