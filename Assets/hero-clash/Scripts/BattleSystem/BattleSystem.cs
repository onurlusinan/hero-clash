using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : StateMachine
{
    public BattleUI battleUI;
    public List<BattleHero> battleHeroes;

    private void Awake()
    {
        BattleHero.heroAttack += OnHeroAttack;
    }
    private void OnDestroy()
    {
        BattleHero.heroAttack -= OnHeroAttack;
    }

    private void Start()
    {
        SetState(new Begin(this));
    }

    private void OnHeroAttack(Hero hero)
    {
        StartCoroutine(battleState.Attack(hero));
    }
}
