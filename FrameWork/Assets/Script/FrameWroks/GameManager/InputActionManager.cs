using UnityEngine;

public class InputActionManager : MonoBehaviour {

    protected Transform rightHandTF;
    protected LEUnitAnimatorPr animationManager;
    IInputActable inputActionClient;

    protected virtual void OnEnable()
    {
        rightHandTF = GetComponentInChildren<RightHandHolder>().transform;
        animationManager = GetComponent<LEUnitAnimatorPr>();
    }

    public void GetKey_A() { if (inputActionClient != null) inputActionClient.GetKey_A(); }
    public void GetKey_A_Down() { if (inputActionClient != null) inputActionClient.GetKey_A_Down(); }
    public void GetKey_A_Up() { if (inputActionClient != null) inputActionClient.GetKey_A_Up(); }
    public void GetKey_B_Down() { if (inputActionClient != null) inputActionClient.GetKey_B_Down(); }
    public void ShutDown() { if (inputActionClient != null) inputActionClient.ShutDown();}

    public void ResetClient(IInputActable client)
    {
        inputActionClient = client;
        inputActionClient.Init(this);
    }

    public void ChangeAnimationMotionType(LEUnitAnimatorPr.AnimationMotionType type)
    {
        animationManager.SetMotionType(type);
    }

    public Transform RightHandMid1 { get { return rightHandTF; } }
}
