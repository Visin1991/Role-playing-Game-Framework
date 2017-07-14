using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LEUnitRigdbodyController : MonoBehaviour {

    LEUnitCentralPanel cp;
    private Rigidbody rg;

    private void Start()
    {
        cp = GetComponent<LEUnitCentralPanel>();
        if(cp==null)
            cp = GetComponentInParent<LEUnitCentralPanel>();
        if (cp == null)
            Debug.LogError("Can not find the LEUnitCP");

        rg = transform.GetOrAddComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rg.MovePosition(rg.position + cp.Adapter_MoveVeclocity3D * Time.fixedDeltaTime);
    }


}
