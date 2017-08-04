using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LEUnitRigdbodyController : MonoBehaviour {

    LEUnitCentralPanel cp;
    private Rigidbody rg;

    CapsuleCollider collider;

    private void Start()
    {
        cp = GetComponent<LEUnitCentralPanel>();
        if(cp==null)
            cp = GetComponentInParent<LEUnitCentralPanel>();
        if (cp == null)
            Debug.LogError("Can not find the LEUnitCP");

        rg = transform.GetOrAddComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();
    }

    /*
        private void FixedUpdate()
        {
            rg.MovePosition(rg.position + cp.Adapter_MoveVeclocity3D * Time.fixedDeltaTime);
        }
    */

    private void Update()
    {
        //Debug.DrawRay(transform.position, -Vector3.up * 0.2f, Color.red);
       // if(Physics.Raycast(transform.position,-Vector3.up * 0.2f,))
    }

}
