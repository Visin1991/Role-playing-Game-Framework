using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour {

    bool active = false;
    public bool Active { set { active = value; } }
    public void SetUpLayer(int layer)
    {
        Debug.LogFormat("{0} SetUpLayer {1}",transform.root.name,layer);
        gameObject.layer = layer;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!active) return;
        IDamageable damageReciver = collision.transform.GetComponent<IDamageable>();
        if(damageReciver != null)
            damageReciver.GetDamage(20);
    }
}
