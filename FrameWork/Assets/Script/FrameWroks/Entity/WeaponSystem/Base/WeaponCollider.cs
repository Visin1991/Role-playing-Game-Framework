using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour {

    public void SetUpLayer(int layer)
    {
        gameObject.layer = layer;
    }

    private void OnCollisionEnter(Collision collision)
    {

        LEUnitProcessor processor = collision.transform.GetComponent<LEUnitProcessor>();
        if(processor!=null)
            processor.GetDamage();
        /*EnemyHealth ehealth = collision.transform.GetComponent<EnemyHealth>();
        if (ehealth != null)
        {
            ehealth.TakeDamage(50);
        }*/


    }
}
