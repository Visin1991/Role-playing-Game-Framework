using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitializer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameUIPr.Instance.CloseLoadingPanel();
        //GameCentalPr.Instance.LoadSave();
        GameCentalPr.Instance.EnablePlayerObj();
        GameCentalPr.Instance.PlayerProcessor.transform.parent = null;
        GameCentalPr.Instance.PlayerProcessor.transform.position = transform.position;
	}
}
