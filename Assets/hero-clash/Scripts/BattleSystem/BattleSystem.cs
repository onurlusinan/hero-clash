using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : StateMachine
{
    public BattleUI battleUI;
    public List<Hero> heroes;

    private void Start()
    {
        SetState(new Begin(this));
    }

    public void AttackButton()
    {
        StartCoroutine(nameof(battleState.Attack));
    }
}
