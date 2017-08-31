using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///     Physcis Detector is used to trigger the Ai to Update Ai behavior.
/// When the Ai LEEntity get in certain range. The Ai start run it's state
/// Machine.......When the Ai out of range, it should stop runing state machine
/// </summary>
public class PhysicDetector : MonoBehaviour {

    List<AiInfomationReciver> allRecivers = new List<AiInfomationReciver>();

    private void Start()
    {
        SphereCollider c = GetComponent<SphereCollider>();
        float range = c.radius;
        AiUtility.AiFind.FindCollidersWithTypeOf<AiInfomationReciver>(ref allRecivers, transform.position,range, 1 >> 9);
    }

    private void OnTriggerEnter(Collider other)
    {
        AiInfomationReciver reciver = other.GetComponent<AiInfomationReciver>();
        if (allRecivers.Contains(reciver)) return;
        else {
            reciver.StartAiBehavior();
            allRecivers.Add(reciver);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        AiInfomationReciver reciver = other.GetComponent<AiInfomationReciver>();
        if (allRecivers.Contains(reciver))
        {
            reciver.StopAiBehavior();
        }
    }
}
