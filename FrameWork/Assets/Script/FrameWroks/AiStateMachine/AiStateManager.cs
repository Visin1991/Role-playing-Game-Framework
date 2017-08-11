using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiStateManager : MonoBehaviour {

    public Transform[] patrolPoints;

    NavMeshAgent angent;

    Transform LPlayerT;

    List<AiState> aistates = new List<AiState>();

    AiStatePatrol partrol;
    AiStateFight fight;

    [HideInInspector]
    public AiState currentState;

    public bool findTarget;

    LEUnitProcessor processor;

    // Use this for initialization
    void Start () {

        processor = GetComponent<LEUnitProcessor>();

        angent = GetComponent<NavMeshAgent>();
        LPlayerT = FindObjectOfType<LPlayer>().transform;

        currentState = partrol =  new AiStatePatrol(this);
        fight = new AiStateFight(this);

        currentState.OnStateEnter();
    }
	
	// Update is called once per frame
	void Update () {

        currentState.OnState();
    }


    public void SetPatrolPostionByIndex(int index)
    {
        angent.SetDestination(patrolPoints[index].position);
    }

    public bool FindEnemy()
    {
        float dist = (LPlayerT.position - transform.position).sqrMagnitude;
        return dist < 50;
    }

    public void KeepTrackOfTarget()
    {
        float dist = (LPlayerT.position - transform.position).sqrMagnitude;
        if (dist < 100)
            findTarget = true;
        else
            findTarget = false;
    }

    public AiState EnterPartolState()
    {
        return partrol;
    }

    public AiState EnterFightState()
    {
        return fight;
    }

    public void FaceForwarTarget()
    {
        

    }
}
