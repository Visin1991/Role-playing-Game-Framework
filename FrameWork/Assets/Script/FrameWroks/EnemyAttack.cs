using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;


    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    bool playerInRange = false;
    float timer;


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
        anim.SetBool("Walking", true);
    }

    
    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = true;
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
            anim.SetBool("Attacking", false);
        }
    }


    void Update ()
    {
        timer += Time.deltaTime;

        

        if(timer >= timeBetweenAttacks && playerInRange/* && enemyHealth.currentHealth > 0*/)
        {
            Attack ();
        }

        if (playerHealth.currentHealth <= 0)
        {
            anim.SetBool("PlayerDead", false);
        }
        

       

        
    }


    void Attack ()
    {
        timer = 0f;


        if(playerHealth.currentHealth > 0)
        {
            anim.SetBool("Attacking", true);
            anim.SetBool("PlayerDead", false);
            playerHealth.TakeDamage (attackDamage);
        }
    }
}
