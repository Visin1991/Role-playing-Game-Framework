using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This Represent the local Player....
public class LPlayer : MonoBehaviour {

    private void Start()
    {
        LoadData();
    }

    void LoadData()
    {
       GameCentalPr.Instance.LoadLPlayerDataFromLastIndex();
    }

}
