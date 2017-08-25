using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiPatrolState : AiState {

    public AiPatrolState(AiStateMachine _stateMachine) : base(_stateMachine)
    {
        
    }

    public override void Init()
    {
        AiOutPut To_Chase = new AiOutPut(stateMachine.IfFindEnemy, null, stateMachine.ChaseState);
        outputs.Add(To_Chase);
    }

    public override void OnStateEnter()
    {
        stateMachine.SetAnimationTypeIndex_to_0();
        stateMachine.angent.SetDestination(stateMachine.transform.position);
        stateMachine.animationPro.SetMotionType(LEUnitAnimatorPr.AnimationMotionType.IWR_0);
    }

    public override void OnStateUpdate()
    {
        stateMachine.CheckResetPartrol();
        stateMachine.SetAnimationforwarToDesiredVel();
        stateMachine.UpdateDistanceToTarget();
    }

    public override void OnStateExit()
    {
        stateMachine.SetCurrentPosAsReturnPatrolPos();
    }
}
