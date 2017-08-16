using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
