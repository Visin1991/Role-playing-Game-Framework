using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AiState {

    protected AiStateManager manager;
    public AiState(AiStateManager _manager)
    {
        manager = _manager;
    }
    public abstract void OnStateEnter();
    public abstract void OnState();
    public abstract void OnStateExit();

}
