using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiStateMachine : MonoBehaviour {

    public Transform[] patrolPoints;

    public NavMeshAgent angent;

    [HideInInspector]
    public Transform LPlayerT;

    List<AiState> aistates = new List<AiState>();

    [HideInInspector]
    public AiState currentState;

    public bool findTarget;

    LEUnitProcessor processor;
    public LEUnitAnimatorPr animationPro;

    AiPatrolState partrolState;
    public AiPatrolState PartrolState { get { return partrolState; } }
    AISearchState searchState;
    public AISearchState SearchState { get { return searchState; } }
    AiChaseState chaseState;
    public AiChaseState ChaseState { get { return chaseState; } }
    AiFightState fightState;
    public AiFightState FightState{ get { return fightState; } }

    public bool onAlert = false;
    float searchTime = 0;
    Vector3 partrolPos;
    Vector3 targetPos;
    float distanceToEnemyTarget = 100;

    IInputActable wapon1;
    // Use this for initialization


    // Use this for initialization
    void Start () {

        wapon1 = GetComponentInChildren<IInputActable>();
        GetComponent<CentralProcessorB>().EquipWeapon(wapon1);

        processor = GetComponent<LEUnitProcessor>();
        animationPro = GetComponent<LEUnitAnimatorPr>();

        angent = GetComponent<NavMeshAgent>();
        LPlayerT = FindObjectOfType<LPlayer>().transform;

        processor.SetTarget(ref LPlayerT);

        partrolState = new AiPatrolState(this);
        searchState = new AISearchState(this);
        chaseState = new AiChaseState(this);
        fightState = new AiFightState(this);

        partrolState.Init();
        searchState.Init();
        chaseState.Init();
        fightState.Init();

        currentState = partrolState;
        currentState.OnStateEnter();
    }

	// Update is called once per frame
	void Update () {
        currentState.OnStateUpdate();
        currentState.OnTransitionCheck();
    }
    

    //Condition Func

    public bool IfFindEnemy()
    {
        bool findTarget =  distanceToEnemyTarget < 50;
        if (findTarget) { targetPos = LPlayerT.position; }
        return findTarget;
    }

    public bool IfNotAlert()
    {
        return !onAlert;
    }

    public bool IfAlert()
    {
        return onAlert;
    }

    public void SetAlertToTrue()
    {
        onAlert = true;
    }

    //===============================================
    // Patrol
    public void CheckResetPartrol()
    {
        if (AiUtility.PathFindingHelper.ArriveDestination_NotPathPending(angent))
        {
            //1. Set New Destination
            angent.SetDestination(patrolPoints[Random.Range(0, patrolPoints.Length -1)].position);
        }
    }

    public void CheckLeftTheAlertTime()
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

    public void SetCurrentPosAsReturnPatrolPos()
    {
        partrolPos = transform.position;
    }

    //-------------------------------------------------
    //Search
    public void ReSetSearchTime()
    {
        searchTime = 0.0f;
        onAlert = false;
    }

    public void OnSearch()
    {
        if (AiUtility.PathFindingHelper.ArriveDestination_NotPathPending(angent))
        {
            angent.enabled = false;
            //When We Arrived the destination, We Start To Search
            animationPro.SetMovementForward(0);
            animationPro.SetMotionIndex(2);
            CheckLeftTheAlertTime();
        }
        angent.enabled = true;
    }

    public void ResetNavAgentTargetPos()
    {
        angent.SetDestination(targetPos);
    }

    //-------------------------------------------------
    // Chase and Fight

    public bool LostChaseTarget()
    {
        return distanceToEnemyTarget > 50;
    }

    public bool TargetInRange()
    {
        return distanceToEnemyTarget < 10;
    }

    public bool TargetOutOfFightRange()
    {
        return distanceToEnemyTarget > 10;
    }

    float nextAttackTime = 2.0f;
    bool shouldAttack = true;
    public void OnFight()
    {
        if (AiUtility.PathFindingHelper.ArriveDestination_NotPathPending(angent))
        {
            animationPro.SetMovementForwardSmoothDamp(0);

            nextAttackTime -= Time.deltaTime;
            if (nextAttackTime < 0.0f)
            {
                 nextAttackTime = Random.Range(1.5f, 2.5f);
                 shouldAttack = nextAttackTime > 2.0f;
            }
            if (shouldAttack)
            {
                animationPro.SetMotionTypeImmediately(LEUnitAnimatorPr.AnimationMotionType.MELEE_1);
                animationPro.SetMotionIndex_Random_From_To(0, 4);
            }
            else
            {
                animationPro.SetMotionTypeImmediately(LEUnitAnimatorPr.AnimationMotionType.IWR_0);
                animationPro.SetMotionIndex_Random_From_To(0, 3);
            }



        }
        else
        {
            animationPro.SetMovementForwardSmoothDamp(1);
        }
    }


    //-----------------------------------


    public void UpdateDistanceToTarget()
    {
        distanceToEnemyTarget = (LPlayerT.position - transform.position).sqrMagnitude;
        //Debug.LogFormat("Target To Distance is {0}", distanceToEnemyTarget);
    }

    public void UpdateTargetPos()
    {
        targetPos = LPlayerT.position;
    }

    public void ResetCurrentState(AiState state)
    {
        currentState = state;
    }

    public void SetAnimationInputAtoTrue()
    {
        animationPro.SetKeyStatue(InputIndex.A, true);
    }

    float lastVelocity;
    float currentVelocity;

    public void SetAnimationforwarToDesiredVel()
    {
        animationPro.SetMovementForwardSmoothDamp(1);
    }

    public void SetAnimationTypeIndex_to_1()
    {
        animationPro.SetMotionIndex(1);
    }

    public void SetAnimationTypeIndex_to_0()
    {
        animationPro.SetMotionIndex(0);
    }
}
