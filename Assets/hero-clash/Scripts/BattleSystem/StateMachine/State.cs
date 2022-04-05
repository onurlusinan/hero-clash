using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The state class for the state machine
/// </summary>
public abstract class State
{
    protected BattleSystem battleSystem;

    protected State(BattleSystem battleSystem)
    {
        this.battleSystem = battleSystem;
    }

    /// <summary>
    /// Optional start method that the statte machine calls when state is set
    /// </summary>
    public virtual IEnumerator Start()
    {
        yield break;
    }

    /// <summary>
    /// Optional Attack method
    /// </summary>
    public virtual IEnumerator Attack(HeroBattleCard heroBattleCard)
    {
        yield break;
    }
}
