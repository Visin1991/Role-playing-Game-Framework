using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This Represent the local Player....
public class LPlayer : MonoBehaviour {
    object processor;
    string typeID = "CentralProcessorA";
    System.Type type = typeof(CentralProcessorA);

    void Start()
    {
        processor = gameObject.GetComponent(typeID);
        Debug.Log(processor);
        System.Convert.ChangeType(processor, type);
        Debug.Log(processor);
    }
    
}
