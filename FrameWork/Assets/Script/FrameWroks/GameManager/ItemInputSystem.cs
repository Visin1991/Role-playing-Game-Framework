using UnityEngine;

public abstract class ItemInputSystem : MonoBehaviour {

    protected Transform rightHandTF;
    protected LEUnitAnimatorPr animationManager;

    protected virtual void OnEnable()
    {
        rightHandTF = GetComponentInChildren<RightHandHolder>().transform;
        animationManager = GetComponent<LEUnitAnimatorPr>();
    }

    public abstract void GetKey_A();
    public abstract void GetKey_A_Down();
    public abstract void GetKey_A_Up();
    public abstract void GetKey_B_Down();
    public abstract void ShutDown();
}
