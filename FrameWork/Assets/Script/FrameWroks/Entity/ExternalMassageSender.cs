using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///     Physcis Detector is used to trigger the Ai to Update Ai behavior.
/// When the Ai LEEntity get in certain range. The Ai start run it's state
/// Machine.......When the Ai out of range, it should stop runing state machine
/// </summary>
public class ExternalMassageSender : MonoBehaviour {

    List<AiInfomationReciver> allRecivers = new List<AiInfomationReciver>();

    private void Start()
    {
        SphereCollider c = GetComponent<SphereCollider>();
        float range = c.radius;
        AiUtility.AiFind.FindCollidersWithTypeOf<AiInfomationReciver>(ref allRecivers, transform.position,range, 1 >> 9);
    }

    
    
    //This Object is touch to a player Component. it's layer is Player Component. Player Component only collider with Enemy. it's not Collide with EnemyComponent
    private void OnTriggerEnter(Collider other)
    {
        AiInfomationReciver reciver = other.GetComponent<AiInfomationReciver>();
        if (allRecivers.Contains(reciver)) return;
        else {
            Debug.Log("Start Ai");
            reciver.StartAiBehavior();
            allRecivers.Add(reciver);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        AiInfomationReciver reciver = other.GetComponent<AiInfomationReciver>();
        if (allRecivers.Contains(reciver))
        {
            Debug.Log("Stop Ai");
            reciver.StopAiBehavior();
            allRecivers.Remove(reciver);
        }
    }
}
