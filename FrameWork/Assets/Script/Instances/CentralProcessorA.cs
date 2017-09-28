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

    public LEData ledata;

    bool pause;
    protected bool die = false;

    protected override void Start() 
    {
        userInput = GetComponent<UserInputPr>();
        cameraManager = GetComponentInChildren<Visin1_1.CameraManager>();
        base.Start();
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
        ledata.currentHealth -= num;
        GameUIPr.Instance.UpdateHealthBar(ledata.currentHealth,ledata.maxHealth);
        if (ledata.currentHealth <= 0.0f){ Die();}
        animationManager.SetTriggerImmediately("Impact");
    }

    public void ConsumeMana(float num)
    {
        ledata.currentMana -= num;
        if (ledata.currentMana < 0) ledata.currentMana = 0;
        GameUIPr.Instance.UpdateManaBar(ledata.currentMana, ledata.maxMana);
    }

    public float GetCurrentMama()
    {
        return ledata.currentMana;
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
        if (ledata.currentHealth < ledata.maxHealth)
        {
            ledata.currentHealth += addHealth;
            GameUIPr.Instance.Adapter_Healthbar(ledata.currentHealth, ledata.maxHealth);
            return true;
        }
        else
        {
            return false;
        }
    }

    public override bool AddMana(float addMana)
    {
        if (ledata.currentMana < ledata.maxMana)
        {
            ledata.currentMana += addMana;
            GameUIPr.Instance.Adapter_Manabar(ledata.currentMana, ledata.maxMana);
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
