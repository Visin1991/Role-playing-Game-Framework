using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour {

    Animator anim;
    bool active = false;
    public bool Active { set { active = value; } }

    public void SetUpLayer(int layer)
    {
        gameObject.layer = layer;
    }

    private void Awake()
    {
        anim = GetComponent<Animator> ();
    }

    
    private void OnTriggerEnter(Collider collision)
    {
        if (!active) return;
            IDamageable damageReciver = collision.transform.GetComponent<IDamageable>();
            
            if (damageReciver != null)
                damageReciver.GetDamage(100);
            
    }
   
}
