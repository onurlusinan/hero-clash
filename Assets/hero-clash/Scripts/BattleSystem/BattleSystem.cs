using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : StateMachine
{
    public BattleUI battleUI;
    public List<BattleHero> battleHeroes;

    private EnemyBaseDataCollection _enemyBaseDataCollection;
    public Enemy enemy;

    private void Awake()
    {
        BattleHero.heroAttack += OnHeroAttack;
        _enemyBaseDataCollection = Resources.Load<EnemyBaseDataCollection>("EnemyBaseData/EnemyBaseDataCollection");

        InstantiateEnemy();
    }
    private void OnDestroy()
    {
        BattleHero.heroAttack -= OnHeroAttack;
    }

    private void Start()
    {
        SetState(new Begin(this));
    }

    private void InstantiateEnemy()
    {
        EnemyBaseData newEnemyBaseData = _enemyBaseDataCollection.GetRandomEnemy();
        enemy.LoadBaseData(newEnemyBaseData);
    }

    private void OnHeroAttack(Hero hero)
    {
        StartCoroutine(battleState.Attack(hero));
    }
}
