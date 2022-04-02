using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    protected State battleState;

    public void SetState(State state)
    {
        battleState = state;
        StartCoroutine(battleState.Start());
    }
}  
