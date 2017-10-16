using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CentralProcessorA : LEUnitProcessorBase,IDamageable {

    //bool test = true;

    protected UserInputPr userInput;
    protected Visin1_1.CameraManager cameraManager;
    Visin1_1.CameraManager.CameraDelta cameraDelta;

    [HideInInspector]
    public AkaiBasicData basicData;

    bool pause;
    protected bool die = false;

    protected override void Start() 
    {
        userInput = GetComponent<UserInputPr>();
        cameraManager = GetComponentInChildren<Visin1_1.CameraManager>();
        base.Start();

        basicData = GetComponent<AkaiBasicData>();
    }

    private void Update()
    {
        if (pause) return;
        if (die) return;
        UpdateInput();
        UpdateCamera();
        UpdateBasicMovement();
        UpdateAnimation();
        
    }

    //======================================================
    //Override Function
    //======================================================
    public override void Pause(bool p)
    {
        pause = p;
    }
    //======================================================
    //Sub-component Update
    //======================================================
    void UpdateInput()
    {
        userInput.UpdateInput(); //We Check the User Input
    }

    void UpdateBasicMovement()
    {
        if (!enableBaiscMovement) return;
        if (userInput.Shift)
        {
            basicMovementManager.SetStrafe(true);
        }
        basicMovementManager.SetInputVH(userInput.currentVH);
        basicMovementManager.UpdateBasicMoveMent();
    }

    void UpdateAnimation()
    {
        if (userInput.Shift)
        {
            animationManager.SetMovementForward(userInput.currentVH.y);
            animationManager.SetMovementStrafe(userInput.currentVH.x);
        }
        else
        {
            animationManager.SetMovementForward(userInput.currentVH.magnitude);
            animationManager.SetMovementStrafe(0);
        }
    }

    void UpdateCamera()
    {
        //Input for Camera
        if (userInput.mouseScroll != 0 || userInput.mouseHorizontal != 0 || userInput.mouseVertical != 0)
        {
            cameraDelta.delta_pitch = userInput.mouseVertical;
            cameraDelta.delta_yaw = userInput.mouseHorizontal;
            cameraDelta.delta_dstToTarget = userInput.mouseScroll;

            cameraManager.SetCameraDelta(cameraDelta);

            userInput.mouseVertical = 0;
            userInput.mouseHorizontal = 0;
        }
        cameraManager.UpdateCameraManager();
    }

    //========================================================
    //IDamageable procosser
    //========================================================
    public void GetDamage(float num)
    {
        basicData.currentHealth -= num;
        GameUIPr.Instance.UpdateHealthBar(basicData.currentHealth, basicData.maxHealth);
        if (basicData.currentHealth <= 0.0f){ Die();}
        animationManager.SetTriggerImmediately("Impact");
    }

    public void ConsumeMana(float num)
    {
        basicData.currentMana -= num;
        if (basicData.currentMana < 0) basicData.currentMana = 0;
        GameUIPr.Instance.UpdateManaBar(basicData.currentMana, basicData.maxMana);
    }

    public float GetCurrentMama()
    {
        return basicData.currentMana;
    }

    void Die()
    {
        animationManager.SetBool("Die", true);
        die = true;
        alive = false;
        //exMassageSender
    }

    public override bool AddHealth(float addHealth)
    {
        if (basicData.currentHealth < basicData.maxHealth)
        {
            basicData.currentHealth += addHealth;
            GameUIPr.Instance.Adapter_Healthbar_CA(basicData.currentHealth, basicData.maxHealth);
            return true;
        }
        else
        {
            return false;
        }
    }

    public override bool AddMana(float addMana)
    {
        if (basicData.currentMana < basicData.maxMana)
        {
            basicData.currentMana += addMana;
            GameUIPr.Instance.Adapter_Manabar_CA(basicData.currentMana, basicData.maxMana);
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void Dispatch_Animation_Message(AnimationMessageType messageType,object messageValue)
    {
        
    }

    

}
