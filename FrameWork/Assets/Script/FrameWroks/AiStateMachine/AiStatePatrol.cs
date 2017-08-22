using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStatePatrol : AiState {

    System.Random random = new System.Random();

    float timer;

    public AiStatePatrol(AiStateManager _manager) : base(_manager)
    {

    }
    public override void OnStateEnter()
    {
        manager.SetPatrolPostionByIndex(random.Next(0, manager.patrolPoints.Length - 1));

        //Initial Value Here
        timer = 0.0f;
        //
    }

    public override void OnState()
    {
        //OnState.....Logical
        timer += Time.deltaTime;
        if (timer > 10.0f)
            manager.SetPatrolPostionByIndex(random.Next(0, manager.patrolPoints.Length - 1));

        //manager.UpdateMoveInfo();

        if (manager.FindEnemy())
        {
            OnStateExit();
        }

    }

    public override void OnStateExit()
    {
        manager.currentState = manager.EnterFightState();
        manager.currentState.OnStateEnter();
    }
}
