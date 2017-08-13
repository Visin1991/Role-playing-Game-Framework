using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    Transform spawnPoint;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;
    bool col = false;
    Animator anim;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        //spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").transform;
        
        //Debug.Log(spawnPoint);
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
        anim = GetComponent<Animator>();
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
        //anim.SetBool("Walking", true);
        if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            //nav.SetDestination(player.position);
            /*if (col)
            {
                nav.SetDestination(player.position);
            }
            else
            {
                nav.enabled = false; 
                nav.SetDestination(spawnPoint.position); 
            }*/
        }
        
        
    
    }
}
