using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDamage : MonoBehaviour {

    public int damagePerHit = 100;
    Collider col;
    public GameObject player;
    Animator playerAnimator;
    


    

    /*private void Awake()
    {
        col = gameObject.GetComponent<Collider>();
        playerAnimator = player.GetComponent<Animator>();
        
        //col.isTrigger = false;
    
    }*/

    /*private void Update()
    {
        //if(Input.GetMouseButtonDown(0))
        if (playerAnimator.GetCurrentAnimatorStateInfo(1).IsName("Statu_1_MeleeAnimationA") ||
            playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("MeleeA_Index_1") ||
            playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("MeleeA_Index_2") ||
            playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("MeleeA_Index_3"))
        {
            Debug.Log("state");
            col.isTrigger = true;
        }
        else 
            col.isTrigger = false;
    }*/

    void OnTriggerEnter(Collider collision)
    {
         
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(damagePerHit);

        }
    }
}
