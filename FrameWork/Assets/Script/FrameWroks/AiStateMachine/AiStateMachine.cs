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
    public float distanceToEnemyTarget = 100;

    IInputActable wapon1;
    // Use this for initialization


    // Use this for initialization

    public StateMachineCanvas stateMachineCanvas;

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

	// Update is called once per frame
	public void UpdateAiStateMachine() {
        currentState.OnStateUpdate();
        currentState.OnTransitionCheck();
        //Debug.Log(currentState.ToString());
    }

    public void ResetCurrentState(AiState state)
    {
        currentState = state;
    }

    [SMCondition]
    public bool ShouldGoBack()
    {
        return false;
    }

    [SMCondition]
    public bool IfFindEnemy()
    {
        return false;
    }
    [SMCondition]
    public bool IfLostTarget()
    {
        return false;
    }
    [SMCondition]
    public bool ShouldRunaway()
    {
        return false;
    }

    [SMCondition]
    public bool ThisisWei()
    {
        return false;
    }

    [SMCondition]
    public bool WhatUp()
    {
        return false;
    }





    [ContextMenu("InitialEditor")]
    public void InitConditionFuncs()
    {
        if (stateMachineCanvas != null) {
            stateMachineCanvas.conditionFuncs.Clear();
            stateMachineCanvas.conditionFuncs.Add("Null");
            List<string> functionNames = StateMachineEditorHelper.GetAllMethodNameWithSMSCondition_In_T<AiStateMachine>();
            foreach (string s in functionNames)
            {
                stateMachineCanvas.conditionFuncs.Add(s);
            }
        }
    }
}
