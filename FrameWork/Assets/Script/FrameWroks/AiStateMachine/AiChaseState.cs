using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiChaseState : AiState {

    public AiChaseState(AiStateMachine _stateMachine) : base(_stateMachine)
    {
        
    }

    public override void Init()
    {
        AiOutPut To_Search = new AiOutPut(stateMachine.LostChaseTarget, null, stateMachine.SearchState);
        AiOutPut To_Fight = new AiOutPut(stateMachine.TargetInRange, null, stateMachine.FightState);
        outputs.Add(To_Search);
        outputs.Add(To_Fight);
    }

    public override void OnStateEnter()
    {
        stateMachine.animationPro.SetMotionType(LEUnitAnimatorPr.AnimationMotionType.IWR_0);
        stateMachine.ResetNavAgentTargetPos();
    }

    public override void OnStateUpdate()
    {
        stateMachine.UpdateDistanceToTarget();
        stateMachine.UpdateTargetPos();
        stateMachine.ResetNavAgentTargetPos();
        stateMachine.SetAnimationforwarToDesiredVel();
    }

    public override void OnStateExit()
    {
        
    }



}
