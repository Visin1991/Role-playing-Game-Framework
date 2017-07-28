using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    Transform spawnPoint;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.NavMeshAgent nav;
    bool col = false;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform;
        
        Debug.Log(spawnPoint);
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <UnityEngine.NavMeshAgent> ();
    }
    
   void OnTriggerEnter (Collider collider)
    {
        if (collider.gameObject.tag == "Player")
            col = true;
    }
    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
            col = false;
    }



    void Update ()
    {
        if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            if (col)
            {
                nav.SetDestination(player.position);
            }
            else
            {
                //nav.enabled = false;
                nav.SetDestination(spawnPoint.position); 
            }
        }
        
        
    
    }
}
