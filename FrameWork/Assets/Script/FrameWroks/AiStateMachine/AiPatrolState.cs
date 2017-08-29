using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiPatrolState : AiState {

    float nextCheckTime;
    public float findEnemyCheckFrequency = 1.0f;

    public AiPatrolState(AiStateMachine _stateMachine) : base(_stateMachine)
    {
        
    }

    public override void Init()
    {
        AiOutPut To_Chase = new AiOutPut(IfFindEnemy, null, stateMachine.ChaseState);
        outputs.Add(To_Chase);
    }

    public override void OnStateEnter()
    {
        stateMachine.animationPro.SetMotionIndex(1);
        stateMachine.angent.SetDestination(stateMachine.transform.position);
        stateMachine.animationPro.SetMotionType(LEUnitAnimatorPr.AnimationMotionType.IWR_0);
    }

    public override void OnStateUpdate()
    {
        CheckResetPartrol();
        stateMachine.animationPro.SetMovementForwardSmoothDamp(1);
        UpdateDistanceToTarget();
    }

    public override void OnStateExit()
    {
        stateMachine.partrolPos = stateMachine.transform.position;
    }

   

    bool IfFindEnemy()
    {
        nextCheckTime -= Time.deltaTime;
        if (nextCheckTime <= 0.0f)
        {
            nextCheckTime = findEnemyCheckFrequency;
            Collider c = AiUtility.AiFind.FindNearestColliderOverlapSphere(stateMachine.transform.position, 50, 1 << 8);
            if (c == null) return false;
            else
            {
                stateMachine.targetTF = c.transform;
                return true;
            }
        }
        return false;
    }

    void CheckResetPartrol()
    {
        if (AiUtility.PathFindingHelper.ArriveDestination_NotPathPending(stateMachine.angent))
        {
            //1. Set New Destination
            stateMachine.angent.SetDestination(stateMachine.patrolPoints[UnityEngine.Random.Range(0, stateMachine.patrolPoints.Length - 1)].position);
        }
    }

    void UpdateDistanceToTarget()
    {
        if (stateMachine.targetTF == null) { stateMachine.distanceToEnemyTarget = float.MaxValue; }
        else
        {
            stateMachine.distanceToEnemyTarget = (stateMachine.transform.position - stateMachine.targetTF.position).magnitude;
        }
    }
}
