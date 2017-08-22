using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISearchState : AiState {

    public AISearchState(AiStateMachine _stateMachine) : base(_stateMachine)
    {
        
    }

    public override void Init()
    {
        AiOutPut To_Partorl = new AiOutPut(stateMachine.IfNotAlert, null, stateMachine.PartrolState);
        AiOutPut TO_Chase = new AiOutPut(stateMachine.IfFindEnemy, null, stateMachine.ChaseState);
        outputs.Add(To_Partorl);
        outputs.Add(TO_Chase);
    }

    public override void OnStateEnter()
    {
        stateMachine.SetAlertToTrue();
        stateMachine.ResetNavAgentTargetPos();
    }

    public override void OnStateUpdate()
    {
        stateMachine.OnSearch();
    }

    public override void OnStateExit()
    {
        stateMachine.ReSetSearchTime();

    }

}
