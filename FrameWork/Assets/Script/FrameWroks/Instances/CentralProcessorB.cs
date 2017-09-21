using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralProcessorB : LEUnitProcessor, IDamageable, AiInfomationReciver {

    public LEData data;
    bool upStateMachine;
    AiStateMachine aistateMachine;

    public GameObject potionPrafbs;
    public GameObject manaPrafbs;

    bool die = false;

    protected override void Start()
    {
        base.Start();
        aistateMachine = GetComponent<AiStateMachine>();
        //aistateMachine.StartAiStateMachine();
    }

    private void Update()
    {
        if (upStateMachine)
        {
            if(aistateMachine!=null)
                aistateMachine.UpdateAiStateMachine();
        }
    }

    public override void Pause(bool b)
    {
        
    }

    public void StartAiBehavior()
    {
        upStateMachine = true;
    }

    public void StopAiBehavior()
    {
        upStateMachine = false;
        animationManager.SetMotionTypeImmediately(LEUnitAnimatorPr.AnimationMotionType.IWR_0);
        animationManager.SetMovementForward(0.0f);
        aistateMachine.StopAiBehaviour();
    }

    public void GetDamage(float num)
    {
        data.currentHealth -= num;
        if (data.currentHealth <= 0) {
            Die();
        }
        animationManager.SetTrigger("Impact");
    }

    void Die()
    {
        if (die == true) return;
        animationManager.SetBool("Die", true);
        AiStateMachine stateMachine = GetComponent<AiStateMachine>();
        stateMachine.enabled = false;

        StopAiBehavior();

        Instantiate(potionPrafbs, transform.position + new Vector3(0,1,0), Quaternion.identity);
        Instantiate(manaPrafbs, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        die = true;
    }


    
}
