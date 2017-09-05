using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour {

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator> ();
    }

    
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            IDamageable damageReciver = other.transform.GetComponent<IDamageable>();
            anim.SetBool("Hit", true);
            anim.SetBool("Idle", false);
            if (damageReciver != null)
                damageReciver.GetDamage(20);
            StartCoroutine("Rearm");
        }
            
    }
    IEnumerator Rearm()
    {
        yield return new WaitForSeconds(2f);
        anim.SetBool("Hit", false);
        anim.SetBool("Idle", true);
        

    }
}
