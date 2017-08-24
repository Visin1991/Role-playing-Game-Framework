using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralProcessorB : LEUnitProcessor, IDamageable {

    public LEData data;
    
    private void OnEnable()
    {
        
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void Pause(bool b)
    {
        
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
        animationManager.SetBool("Die", true);
        AiStateMachine stateMachine = GetComponent<AiStateMachine>();
        stateMachine.enabled = false;
    }
}
