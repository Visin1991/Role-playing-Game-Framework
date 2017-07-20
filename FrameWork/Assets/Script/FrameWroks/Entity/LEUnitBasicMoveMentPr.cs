using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LEUnitCentralPanel))]
public class LEUnitBasicMoveMentPr : MonoBehaviour {
    bool initalSucced;

    LEUnitCentralPanel cp;

    public bool drivenByInput;

    public float moveSpeed;
    public float speedScales;
    float maxSpeed;

    public float turningSpeed = 10;

    private float yaw;  //Rotation around Y Axis
    public float turnSmoothVelocity;

    Transform cameraT;
    CameraType cameraType;
  

    Vector3 velocityNor = Vector3.zero;

    [SerializeField]private Vector3 velocity3D = Vector3.zero;
    float postTargetMoveSpeed = 0.0f;

    float currentSpeed = 0.0f;
    float speedSmoothVelocity;
    public float reactionSpped;

    LE_Animation_Event_moveInfo info = new LE_Animation_Event_moveInfo();

    void Start () {

        maxSpeed = moveSpeed * speedScales;

        cp = GetComponent<LEUnitCentralPanel>();
        if (cp == null) { Debug.LogError("Can not get LEUnitCP"); initalSucced = false; }
        initialComponents();

        info.Init();
    }

	void Update () {
        if (!initalSucced) return;

        if (drivenByInput)
        {
            // First Person Camera
            if (cameraType == CameraType.ThridPerson)
            { 
                MoveDirection();
                CalculateMoveSpeed_();
                TurnAroundBasedOn_CameraDir();
            }

            // First Person Camera
            if (cameraType == CameraType.FirstPerson)
            {
                MoveDirection();
                CalculateMoveSpeed_();

                reactionSpped = 0.0f;
                yaw += Input.GetAxis("Mouse X") * turningSpeed;
                TurnAround();
            }

          
            info.moveSpeed = currentSpeed / maxSpeed;
            cp.Rise_LE_Animation_Event(info);
        }
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

    void MoveDirection()
    {
        velocityNor = cp.Adapter_LE_mainBody.forward;
    }

    void CalculateMoveSpeed_()
    {
        postTargetMoveSpeed = maxSpeed * cp.Adapter_LE_InputVH.normalized.magnitude;

        currentSpeed = Mathf.SmoothDamp(currentSpeed, postTargetMoveSpeed, ref speedSmoothVelocity, reactionSpped);

        velocity3D = currentSpeed * velocityNor;

        cp.Adapter_MoveVeclocity3D = velocity3D;
    }

    void TurnAroundBasedOn_CameraDir()
    {
        float targetDegree = Mathf.Atan2(cp.Adapter_LE_InputVH.x,cp.Adapter_LE_InputVH.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
        cp.Adapter_LE_mainBody.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(cp.Adapter_LE_mainBody.eulerAngles.y, targetDegree, ref turnSmoothVelocity, reactionSpped);
    }

    void TurnAround()
    {
        float targetDegree = Mathf.Atan2(cp.Adapter_LE_InputVH.x, cp.Adapter_LE_InputVH.y) * Mathf.Rad2Deg + yaw;
        cp.Adapter_LE_mainBody.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(cp.Adapter_LE_mainBody.eulerAngles.y, targetDegree, ref turnSmoothVelocity, reactionSpped);
    }
}
