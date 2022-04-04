using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    protected State battleState;

    /// <summary>
    /// Sets and starts the state
    /// </summary>
    public void SetState(State state)
    {
        battleState = state;
        StartCoroutine(battleState.Start());
    }
}  
