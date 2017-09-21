using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stuff1 : Weapon,IInputActable, ItemOnGUIDoubleClickable {

    public InputActionManager inputActionManager;
    ItemHandleOnObj handle;
    public Transform muzz; 

    public FireBallParticleScript fireBallPrafeb;

    public void SetUpLayer(int layer)
    {
        //gameObject.layer = layer;
    }

    public void Init(InputActionManager actionManager)
    {
        inputActionManager = actionManager;
        transform.position = inputActionManager.RightHandMid1.position;
        transform.rotation = inputActionManager.RightHandMid1.rotation;
        transform.SetParent(inputActionManager.RightHandMid1);
        inputActionManager.ChangeAnimationMotionType(LEUnitAnimatorPr.AnimationMotionType.STUFF_3);
        handle = GetComponentInChildren<ItemHandleOnObj>();
    }

    public void GetKey_A()
    {

    }

    public void GetKey_A_Down()
    {
        FireBall();
    }

    public void GetKey_A_Up()
    {
        //inputActionManager.AnimationManager.SetKeyStatue(InputIndex.A, false);
    }

    public void GetKey_B_Down()
    {
        inputActionManager.AnimationManager.SetKeyStatue(InputIndex.B, true);
    }

    public void ShutDown()
    {
        LEInventory inventory = transform.root.GetComponentInChildren<LEInventory>();
        inventory.AddItem(handle.item, transform);
        transform.gameObject.SetActive(false);
    }

    public void SetIInputActableItemStatu(LEUnitAnimatorPr.AnimationAttackStatue s)
    {
        
    }

    public void DisableCollision()
    {
        GetComponentInChildren<Collider>().enabled = false;
    }

    public void EnableCollision()
    {
        GetComponentInChildren<Collider>().enabled = true;
    }

    public void ItemOnGUIDoubleClick(ItemHandleOnGUI obj)
    {
        gameObject.SetActive(true);
        GameCentalPr.Instance.PlayerInputActionManager.ResetClient(this);
        obj.Clean();
        CursorManager.GetInstance().setMouse();
    }

    RaycastHit hit = new RaycastHit();
    Vector3 targetPos;

    void FireBall()
    {
        Visin1_1.MouseAndCamera.GetScreenPointToRayColliderInfo(out hit, (1 << 10));
        if (hit.transform != null)
        {
            CentralProcessorA cpa = transform.root.GetComponent<CentralProcessorA>();
            if (cpa.GetCurrentMama() <= 10) return;
            targetPos.x = hit.transform.position.x;
            targetPos.y = transform.root.position.y;
            targetPos.z = hit.transform.position.z;
            transform.root.LookAt(targetPos);
            FireBallParticleScript fireBall_ = Instantiate(fireBallPrafeb) as FireBallParticleScript;
            fireBall_.transform.position = muzz.position;
            fireBall_.SetUp(9);
            fireBall_.target = hit.transform;
            inputActionManager.AnimationManager.SetKeyStatue(InputIndex.A, true);
            cpa.ConsumeMana(10);
        }
    }

}