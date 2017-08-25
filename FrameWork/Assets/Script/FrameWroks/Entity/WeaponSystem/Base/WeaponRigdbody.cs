using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRigdbody : MonoBehaviour {

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

}
