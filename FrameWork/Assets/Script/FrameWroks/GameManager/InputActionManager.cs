using UnityEngine;

//  This Script is touch to A LE Entity.  
//  For different Weapon or in different game Play model. Input may raise different 
//Action.
//  For example: If the inputActionClient is a Gun, Then GetKey_A(), the LEEntity
//Will shoot the gun, and use the shot animation....When the inputActionClient is a 
//sword, the LEEntity will play sword animation
//
public class InputActionManager : MonoBehaviour {

    protected Transform rightHandTF;
    public LEUnitAnimatorPr animationManager;
    IInputActable inputActionClient;
    public int actionItemPhycislayer = 0;

    protected virtual void OnEnable()
    {
        rightHandTF = GetComponentInChildren<RightHandHolder>().transform;
        animationManager = GetComponent<LEUnitAnimatorPr>();
    }

    public void GetKey_A() {if (inputActionClient != null) inputActionClient.GetKey_A(); }
    public void GetKey_A_Down() { if (inputActionClient != null) inputActionClient.GetKey_A_Down(); }
    public void GetKey_A_Up() { if (inputActionClient != null) inputActionClient.GetKey_A_Up(); }
    public void GetKey_B_Down() { if (inputActionClient != null) inputActionClient.GetKey_B_Down(); }
    public void ShutDown() { if (inputActionClient != null) inputActionClient.ShutDown();}

    //Reset InputAction Client and Init the Client.
    public void ResetClient(IInputActable client)
    {
        if(inputActionClient!=null)
            inputActionClient.ShutDown();
        inputActionClient = client;
        inputActionClient.Init(this);
        inputActionClient.SetUpLayer(gameObject.layer + 1);
        
    }

    public void ChangeAnimationMotionType(LEUnitAnimatorPr.AnimationMotionType type)
    {
        animationManager.SetMotionType(type);
    }

    public Transform RightHandMid1 { get { return rightHandTF; } }

    public LEUnitAnimatorPr AnimationManager { get { return animationManager; } }

    public void SetIInputActableItemStatu(LEUnitAnimatorPr.AnimationAttackStatue s)
    {
        inputActionClient.SetIInputActableItemStatu(s);
    }
}
