using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CentralProcessorA : LEUnitProcessor,IDamageable {

    //bool test = true;

    protected UserInputPr userInputManager;
    protected Visin1_1.CameraManager cameraManager;

    Visin1_1.CameraManager.CameraDelta cameraDelta;

    public LEData ledata;

    bool pause;
    protected bool die = false;

    ExternalMassageSender exMassageSender;

    protected override void Start() 
    {
        userInputManager = GetComponent<UserInputPr>();
        cameraManager = GetComponentInChildren<Visin1_1.CameraManager>();
        exMassageSender = GetComponent<ExternalMassageSender>();
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
        userInputManager.UpdateInput(); //We Check the User Input
    }

    void UpdateBasicMovement()
    {
        if (!enableBaiscMovement) return;
        if (userInputManager.Shift)
        {
            basicMovementManager.SetStrafe(true);
        }
        basicMovementManager.SetInputVH(userInputManager.currentVH);
        basicMovementManager.UpdateBasicMoveMent();
    }

    void UpdateAnimation()
    {
        if (userInputManager.Shift)
        {
            animationManager.SetMovementForward(userInputManager.currentVH.y);
            animationManager.SetMovementStrafe(userInputManager.currentVH.x);
        }
        else
        {
            animationManager.SetMovementForward(userInputManager.currentVH.magnitude);
            animationManager.SetMovementStrafe(0);
        }
    }

    void UpdateCamera()
    {
        //Input for Camera
        if (userInputManager.mouseScroll != 0 || userInputManager.mouseHorizontal != 0 || userInputManager.mouseVertical != 0)
        {
            cameraDelta.delta_pitch = userInputManager.mouseVertical;
            cameraDelta.delta_yaw = userInputManager.mouseHorizontal;
            cameraDelta.delta_dstToTarget = userInputManager.mouseScroll;

            cameraManager.SetCameraDelta(cameraDelta);

            userInputManager.mouseVertical = 0;
            userInputManager.mouseHorizontal = 0;
        }
        cameraManager.UpdateCameraManager();
    }
    //======================================================
    public void ChangeItemInputSystem(InputActionManager _itemInput)
    {
        if (inputActionManager != null)
        {
            inputActionManager.ShutDown();
            inputActionManager.enabled = false;
        }
        inputActionManager = _itemInput;

        //Non Item
        if (inputActionManager != null)
            inputActionManager.enabled = true;
    }

    //========================================================
    //IDamageable procosser
    //========================================================
    public void GetDamage(float num)
    {

        ledata.currentHealth -= num;

        //if(!test)
        GameUIPr.Instance.UpdateHealthBar(ledata.currentHealth,ledata.maxHealth);

        if (ledata.currentHealth <= 0.0f)
        {
            Die();
        }
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
}
