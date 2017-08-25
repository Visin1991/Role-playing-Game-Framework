using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Obsolete]
public class WeaponPhysics : WeaponComponent {

    Vector3 localPosOffset;
    Vector3 localEulOffset;

    private void OnEnable()
    {
        localPosOffset = transform.localPosition;
        localEulOffset = transform.localEulerAngles;
    }

    private void FixedUpdate()
    {
        transform.localPosition = localPosOffset;
        transform.localEulerAngles = localEulOffset;
    }
    public void SetUpLayer(int layer)
    {
        gameObject.layer = layer;
    }

    private void OnCollisionEnter(Collision collision)
    {
        EnemyHealth ehealth = collision.transform.GetComponent<EnemyHealth>();
        if (ehealth!= null)
        {
            ehealth.TakeDamage(10);
        }
    }
}
