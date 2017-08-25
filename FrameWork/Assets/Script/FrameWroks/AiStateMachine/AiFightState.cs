using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiFightState : AiState {

    public AiFightState(AiStateMachine _stateMachine) : base(_stateMachine)
    {
       
    }

    public override void Init()
    {
        AiOutPut To_Search = new AiOutPut(stateMachine.LostChaseTarget, null, stateMachine.SearchState);
        AiOutPut To_Chase = new AiOutPut(stateMachine.TargetOutOfFightRange, null, stateMachine.ChaseState);
        outputs.Add(To_Search);
        outputs.Add(To_Chase);
    }

    public override void OnStateEnter()
    { 

    }

    public override void OnStateUpdate()
    {
        stateMachine.UpdateDistanceToTarget();
        stateMachine.OnFight();
    }

    public override void OnStateExit()
    {
        
    }


}
