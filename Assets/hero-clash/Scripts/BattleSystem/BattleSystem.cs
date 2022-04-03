using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : StateMachine
{
    [Header("Battle System Config")]
    public BattleUI battleUI;
    public List<HeroBattleCard> heroBattleCards;
    public EnemyBattleCard enemyBattleCard;

    private EnemyBaseDataCollection _enemyBaseDataCollection;

    private void Awake()
    {
        HeroBattleCard.heroAttack += OnHeroAttack;
        _enemyBaseDataCollection = Resources.Load<EnemyBaseDataCollection>("EnemyBaseData/EnemyBaseDataCollection");

        InstantiateEnemy();
    }
    private void OnDestroy()
    {
        HeroBattleCard.heroAttack -= OnHeroAttack;
    }

    private void Start()
    {
        SetState(new Begin(this));
    }

    private void InstantiateEnemy()
    {
        EnemyBaseData newEnemyBaseData = _enemyBaseDataCollection.GetRandomEnemy();
        enemyBattleCard.InitCard(newEnemyBaseData);
    }

    private void OnHeroAttack(Hero hero)
    {
        StartCoroutine(battleState.Attack(hero));
    }
}
