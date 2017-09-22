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
        AiOutPut To_Search = new AiOutPut(LostChaseTarget, null, stateMachine.SearchState);
        AiOutPut To_Fight = new AiOutPut(TargetInFightRange, null, stateMachine.FightState);
        outputs.Add(To_Search);
        outputs.Add(To_Fight);
    }

    public override void OnStateEnter()
    {
        stateMachine.animationPro.SetMotionTypeImmediately(LEUnitAnimatorManager.AnimationMotionType.IWR_0);
        stateMachine.animationPro.SetMotionIndexImmediately(0);
        ResetNavAgentTargetPos();
    }

    public override void OnStateUpdate()
    {
        UpdateDistanceToTarget();
        UpdateTargetPos();
        ResetNavAgentTargetPos();
        SetAnimationforwarToDesiredVel();
    }

    public override void OnStateExit()
    {
        //Debug.Log("AiChaseState OnStateExit");
    }

    bool LostChaseTarget()
    {
        if (stateMachine.distanceToEnemyTarget > stateMachine.findEnemyRange)
        {
            stateMachine.targetTF = null;
            return true;
        }
        return false;
    }

    bool TargetInFightRange()
    {
        return stateMachine.distanceToEnemyTarget <= 5;
    }

    void ResetNavAgentTargetPos()
    {
        stateMachine.angent.SetDestination(stateMachine.targetPos);
    }

    void UpdateDistanceToTarget()
    {
        if (stateMachine.targetTF == null) { stateMachine.distanceToEnemyTarget = float.MaxValue; }
        else
        {
            stateMachine.distanceToEnemyTarget = (stateMachine.transform.position - stateMachine.targetTF.position).magnitude;
        }
    }

    void UpdateTargetPos()
    {
        if (stateMachine.targetTF != null)
        {
            stateMachine.targetPos = stateMachine.targetTF.position;
        }
        else
        {
            Debug.LogError("There is no Target.....");
        }
    }

    void SetAnimationforwarToDesiredVel()
    {
        stateMachine.animationPro.SetMovementForwardSmoothDamp(1);
    }


}
