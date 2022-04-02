using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected BattleSystem battleSystem;

    protected State(BattleSystem battleSystem)
    {
        this.battleSystem = battleSystem;
    }

    public virtual IEnumerator Start()
    {
        yield break;
    }

    public virtual IEnumerator Attack(Hero hero)
    {
        yield break;
    }
}
