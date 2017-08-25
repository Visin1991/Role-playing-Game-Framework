using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This Represent the local Player....
public class LPlayer : MonoBehaviour {

    Collider[] enemyColliders;
    public float checkFrequency = 1.0f;
    float nextCheckTime;

    private void Start()
    {
        nextCheckTime = checkFrequency;
    }


    private void FixedUpdate()
    {
        nextCheckTime -= Time.deltaTime;
        if (nextCheckTime < 0.0f)
        {
            enemyColliders = AiUtility.AiFind.FindCollidersOverlapSphere(transform.position, 20, 1 << 9);
            foreach (Collider c in enemyColliders)
            {
                
            }
            nextCheckTime = checkFrequency;
        }
    }

}
