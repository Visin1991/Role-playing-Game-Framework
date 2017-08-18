using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LEUnitBasicMoveMent : MonoBehaviour {

    protected bool initalSucced;
    protected LEUnitCentralPanel cp;

    


    public float moveSpeed;
    public float speedScales;
    protected float maxSpeed;

    protected LE_Animation_Event_moveInfo info = new LE_Animation_Event_moveInfo();

    protected Transform cameraT;
    protected CameraType cameraType;


    // Use this for initialization
    void Start () {
        maxSpeed = moveSpeed * speedScales;

        cp = GetComponent<LEUnitCentralPanel>();

        if (cp == null) { Debug.LogError("Can not get LEUnitCP"); initalSucced = false; }
        else
        {
            cp.Bind_LE_BasicMovement_Event_MailBox(MailBox_LE_BasicMovementEvent);
        }

        initialComponents();

        info.Init();
    }

    void initialComponents()
    {
        if (cp.Adapter_playerCamera == null)
        {
            initalSucced = false;
        }
        else
        {
            cameraT = cp.Adapter_playerCamera;
            Visin1_1.CameraManager cameraManager = cp.Adapter_playerCamera.GetComponent<Visin1_1.CameraManager>();
            if (cameraManager == null)
            {
                initalSucced = false;
                Debug.LogErrorFormat("Can not define Camera Type, The GameObject of {0} a Monobehavior impliment the interface of PlayerCamera", cameraT.name);
            }
            else
            {
                cameraType = cameraManager.cameraType;
            }
        }
        initalSucced = true;
    }

    protected abstract void MailBox_LE_BasicMovementEvent(LE_BasicMovement_Event e);
}
