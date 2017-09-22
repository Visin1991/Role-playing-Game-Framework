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
        AiOutPut To_Partol = new AiOutPut(UpdateLEAlive, null, stateMachine.PartrolState);
        AiOutPut To_Search = new AiOutPut(LostChaseTarget, null, stateMachine.SearchState);
        AiOutPut To_Chase = new AiOutPut(TargetOutOfFightRange, null, stateMachine.ChaseState);
        outputs.Add(To_Partol);
        outputs.Add(To_Search);
        outputs.Add(To_Chase);
    }

    public override void OnStateEnter()
    {
        
    }

    public override void OnStateUpdate()
    {
        UpdateForwardTargetAngle();
        UpdateDistanceToTarget();
        
        OnFight();
    }

    public override void OnStateExit()
    {
        
    }

    /// <summary>
    /// When we lost target. we set the lost target Position as the Destination
    /// </summary>
    bool LostChaseTarget()
    {
        if (stateMachine.distanceToEnemyTarget > stateMachine.findEnemyRange)
        {
            stateMachine.lostTargetPos = stateMachine.targetTF.position;
            stateMachine.targetTF = null;
            return true;
        }
        return false;
    }

    bool TargetOutOfFightRange()
    {
        return stateMachine.distanceToEnemyTarget > 3;
    }

    void UpdateForwardTargetAngle()
    {
        stateMachine.animationPro.animator.enabled = false;
        stateMachine.transform.LookAt(stateMachine.targetTF);
        stateMachine.animationPro.animator.enabled = true;
    }

    void UpdateDistanceToTarget()
    {
        if (stateMachine.targetTF == null) { stateMachine.distanceToEnemyTarget = float.MaxValue; }
        else
        {
            stateMachine.distanceToEnemyTarget = (stateMachine.transform.position - stateMachine.targetTF.position).magnitude;
        }
    }

    bool UpdateLEAlive()
    {
      return !stateMachine.targetTF.GetComponent<LEUnitProcessorBase>().Alive;
    }

    float nextAttackTime = 3.5f;
    bool shouldAttack = true;

    void OnFight()
    {
        if (AiUtility.PathFindingHelper.ArriveDestination_NotPathPending(stateMachine.angent))
        {
            stateMachine.animationPro.SetMovementForwardSmoothDamp(0); //Now we stopped move

            //Update the next Attack time
            //---------------------------------
            nextAttackTime -= Time.deltaTime; 
            if (nextAttackTime < 0.0f)
            {
                nextAttackTime = UnityEngine.Random.Range(3.5f, 4.5f);
                shouldAttack = !shouldAttack;
            }

            //Attack Motion
            //---------------------------------
            if (shouldAttack)
            {
                //Attack motion
                stateMachine.animationPro.SetMotionTypeImmediately(LEUnitAnimatorManager.AnimationMotionType.MELEE_1);
                stateMachine.animationPro.SetMotionIndex_Random_From_To(0, 4);
            }
            else
            {
                //Random Idle
                stateMachine.animationPro.SetMotionTypeImmediately(LEUnitAnimatorManager.AnimationMotionType.IWR_0);
                stateMachine.animationPro.SetMotionIndex_Random_From_To(0, 3);
            }
        }
        else
        {
            stateMachine.animationPro.SetMovementForwardSmoothDamp(1);
        }
    }

}
