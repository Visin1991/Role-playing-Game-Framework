using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStateFight : AiState
{

    System.Random random = new System.Random();

    float timer;

    public AiStateFight(AiStateManager _manager) : base(_manager)
    {
       
    }

    public override void OnStateEnter()
    {
       
    }

    public override void OnState()
    {
        manager.KeepTrackOfTarget();
        if (!manager.findTarget)
        {
            OnStateExit();
        }

        manager.FaceForwarTarget();



    }

    public override void OnStateExit()
    {
        manager.currentState = manager.EnterPartolState();
        manager.currentState.OnStateEnter();
    }
}
