using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
