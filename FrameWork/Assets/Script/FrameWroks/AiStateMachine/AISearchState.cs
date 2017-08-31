using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISearchState : AiState {

    float nextCheckTime;
    public float findEnemyCheckFrequency = 1.0f;

    public AISearchState(AiStateMachine _stateMachine) : base(_stateMachine)
    {
        
    }

    public override void Init()
    {
        AiOutPut To_Partorl = new AiOutPut(IfNotAlert, null, stateMachine.PartrolState);
        AiOutPut TO_Chase = new AiOutPut(IfFindEnemy, null, stateMachine.ChaseState);
        outputs.Add(To_Partorl);
        outputs.Add(TO_Chase);
    }

    public override void OnStateEnter()
    {
        SetAlertToTrue();
        ResetNavAgentTargetPos();
    }

    public override void OnStateUpdate()
    {
        OnSearch();
    }

    public override void OnStateExit()
    {
        ReSetSearchTime();
    }


    public bool onAlert = false;
    float searchTime = 0;

    void CheckLeftTheAlertTime()
    {
        searchTime += Time.deltaTime;

        if (searchTime < 5.0f)
        {
            onAlert = true;
        }
        else
        {   //Release Alert
            onAlert = false;
        }
    }

    void OnSearch()
    {
        if (AiUtility.PathFindingHelper.ArriveDestination_NotPathPending(stateMachine.angent))
        {
            stateMachine.angent.enabled = false;
            //When We Arrived the destination, We Start To Search
            stateMachine.animationPro.SetMovementForward(0);
            stateMachine.animationPro.SetMotionIndex(2);
            CheckLeftTheAlertTime();
        }
        stateMachine.angent.enabled = true;
    }

    void ReSetSearchTime()
    {
        searchTime = 0.0f;
        onAlert = false;
    }

    void SetAlertToTrue()
    {
#if DEBUG
        Debug.Log("SetAlertToTrue");
#endif
        onAlert = true;
    }

    void ResetNavAgentTargetPos()
    {
        stateMachine.angent.SetDestination(stateMachine.targetPos);
    }

    bool IfFindEnemy()
    {
#if DEBUG
        Debug.Log("IfFindEnemy");
#endif
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

    bool IfNotAlert()
    {
#if DEBUG
        Debug.Log("IfNotAlert");
#endif
        return !onAlert;
    }

    bool IfAlert()
    {
#if DEBUG
        Debug.Log("IfAlert");
#endif
        return onAlert;
    }
}
