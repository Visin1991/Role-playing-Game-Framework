#define DEBUG

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiStateMachine : MonoBehaviour {

    public Transform[] patrolPoints;

    public NavMeshAgent angent;

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

    

    [HideInInspector]
    public Vector3 partrolPos;

    [HideInInspector]
    public Transform targetTF;
    [HideInInspector]
    public Vector3 targetPos;
    [HideInInspector]
    public Vector3 lostTargetPos;

    [HideInInspector]
    public float distanceToEnemyTarget = 100;

    IInputActable wapon1;
    // Use this for initialization


    // Use this for initialization
    void Start () {

        wapon1 = GetComponentInChildren<IInputActable>();
        GetComponent<InputActionManager>().ResetClient(wapon1);


        processor = GetComponent<LEUnitProcessor>();
        animationPro = GetComponent<LEUnitAnimatorPr>();
        animationPro.SetMotionTypeImmediately(LEUnitAnimatorPr.AnimationMotionType.IWR_0);

        angent = GetComponent<NavMeshAgent>();

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

	//This function will be called by the CentralProsessorB.  
	public void UpdateAiStateMachine() {
        currentState.OnStateUpdate();
        currentState.OnTransitionCheck();
        //Debug.Log(currentState.ToString());
    }

    public void StopAiBehaviour()
    {
        angent.isStopped = true;
    }

    public void ResetCurrentState(AiState state)
    {
        currentState = state;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 5);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,20);
    }
}
