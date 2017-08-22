using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AiState {

    protected List<AiOutPut> outputs = new List<AiOutPut>();
    protected AiStateMachine stateMachine;
    
    public AiState(AiStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
    }

    public abstract void Init();

    public abstract void OnStateEnter();

    public abstract void OnStateUpdate();

    public void OnTransitionCheck()
    {
        foreach (AiOutPut output in outputs)
        {
            if (output.condition.Invoke())
            {
                if (output.onStateAdditionExit != null)
                    output.onStateAdditionExit.Invoke();
                output.outputState.OnStateEnter();
                stateMachine.ResetCurrentState(output.outputState);
                OnStateExit();
                break;
            }
        }
    }

    public abstract void OnStateExit();
}

public class AiOutPut
{
    public System.Func<bool> condition;
    public System.Action onStateAdditionExit;
    public AiState outputState;

    public AiOutPut(System.Func<bool> _condition, System.Action _additionEexit, AiState output)
    {
        condition = _condition;
        onStateAdditionExit = _additionEexit;
        outputState = output;

    }
}