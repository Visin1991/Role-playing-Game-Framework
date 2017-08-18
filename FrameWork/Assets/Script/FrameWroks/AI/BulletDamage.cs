using UnityEngine;
using System.Collections;

public class BulletDamage : MonoBehaviour {
    public int damagePerShot = 20;
    float timer;

    void OnCollisionEnter (Collision col)
    {
       
        if (col.gameObject.tag == "Player")
        {
            PlayerHealth playerHealth = col.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damagePerShot);
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Enemy")
        {
           
            EnemyHealth enemyHealth = col.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(damagePerShot);
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Wall")
        {
            
            Destroy(gameObject);
            
        }
        if (col.gameObject.tag == "Door")
        {
            Destroy(gameObject);
        }
    }
    
    void Update()
    {
        timer += 1.0f * Time.deltaTime;

        if (timer >= 2)
        {
            GameObject.Destroy(gameObject);
        }       

    }

  
    
}
