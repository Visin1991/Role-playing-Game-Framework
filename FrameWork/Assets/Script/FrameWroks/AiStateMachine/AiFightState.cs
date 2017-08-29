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
        AiOutPut To_Search = new AiOutPut(LostChaseTarget, null, stateMachine.SearchState);
        AiOutPut To_Chase = new AiOutPut(TargetOutOfFightRange, null, stateMachine.ChaseState);
        outputs.Add(To_Search);
        outputs.Add(To_Chase);
    }

    public override void OnStateEnter()
    { 

    }

    public override void OnStateUpdate()
    {
        UpdateDistanceToTarget();
        OnFight();
    }

    public override void OnStateExit()
    {
        
    }

    bool LostChaseTarget()
    {
        if (stateMachine.distanceToEnemyTarget > 50)
        {
            stateMachine.targetTF = null;
            return true;
        }
        return false;
    }

    bool TargetOutOfFightRange()
    {
        return stateMachine.distanceToEnemyTarget > 5;
    }

    void UpdateDistanceToTarget()
    {
        if (stateMachine.targetTF == null) { stateMachine.distanceToEnemyTarget = float.MaxValue; }
        else
        {
            stateMachine.distanceToEnemyTarget = (stateMachine.transform.position - stateMachine.targetTF.position).magnitude;
        }
    }

    float nextAttackTime = 2.0f;
    bool shouldAttack = true;

    void OnFight()
    {
        if (AiUtility.PathFindingHelper.ArriveDestination_NotPathPending(stateMachine.angent))
        {
            stateMachine.animationPro.SetMovementForwardSmoothDamp(0);

            nextAttackTime -= Time.deltaTime;
            if (nextAttackTime < 0.0f)
            {
                nextAttackTime = UnityEngine.Random.Range(1.5f, 2.5f);
                shouldAttack = nextAttackTime > 2.0f;
            }
            if (shouldAttack)
            {
                stateMachine.animationPro.SetMotionTypeImmediately(LEUnitAnimatorPr.AnimationMotionType.MELEE_1);
                stateMachine.animationPro.SetMotionIndex_Random_From_To(0, 4);
            }
            else
            {
                stateMachine.animationPro.SetMotionTypeImmediately(LEUnitAnimatorPr.AnimationMotionType.IWR_0);
                stateMachine.animationPro.SetMotionIndex_Random_From_To(0, 3);
            }
        }
        else
        {
            stateMachine.animationPro.SetMovementForwardSmoothDamp(1);
        }
    }

}
