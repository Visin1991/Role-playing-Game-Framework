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

    void Start () {
        cp = GetComponent<LEUnitCentralPanel>();
        if (cp == null) { Debug.LogError("Can not get LEUnitCP"); initalSucced = false; }
        initialComponents();
	}

	void Update () {
        if (!initalSucced) return;

        if (drivenByInput)
        {
            // First Person Camera
            if (cameraType == CameraType.ThridPerson)
            {
                
                if (cp.Adapter_LE_InputVH != Vector2.zero)
                {
                    MoveDirection();
                    CalculateMoveSpeed_();
                    TurnAroundBasedOn_CameraDir();
                }
                else
                {
                    cp.Adapter_MoveVeclocity3D = Vector3.zero;
                }
            }
            // First Person Camera
            if (cameraType == CameraType.FirstPerson)
            {
                if (cp.Adapter_LE_InputVH != Vector2.zero)
                {
                    MoveDirection();
                    CalculateMoveSpeed_();
                }
                else
                {
                    cp.Adapter_MoveVeclocity3D = Vector3.zero;
                }

                reactionSpped = 0.0f;
                yaw += Input.GetAxis("Mouse X") * turningSpeed;
                TurnAround();
            }
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
        velocityNor = cp.Adapter_LE_mainBody.forward;//按W 按键物体将会背对着摄像头 向前方移动
    }

    void CalculateMoveSpeed_()
    {
        postTargetMoveSpeed = moveSpeed * speedScales * cp.Adapter_LE_InputVH.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, postTargetMoveSpeed, ref speedSmoothVelocity, reactionSpped);
        velocity3D = currentSpeed * velocityNor;

        cp.Adapter_MoveVeclocity3D = velocity3D;//把移动速度告诉 Living Entity Central Processor.
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
