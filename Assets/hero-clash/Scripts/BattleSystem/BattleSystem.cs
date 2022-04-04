using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the battle, all turns and attacks as a state machine
/// </summary>
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
        InstantiateEnemy();
    }
    private void Start()
    {
        SetState(new Begin(this));
    }

    private void OnDestroy()
    {
        HeroBattleCard.heroAttack -= OnHeroAttack;
    }

    private void OnHeroAttack(HeroBattleCard heroBattleCard)
    {
        StartCoroutine(battleState.Attack(heroBattleCard));
    }
    
    /// <summary>
    /// Initializes the enemy battle card from Enemy Base Data
    /// </summary>
    private void InstantiateEnemy()
    {
        _enemyBaseDataCollection = Resources.Load<EnemyBaseDataCollection>("EnemyBaseData/EnemyBaseDataCollection");
        EnemyBaseData newEnemyBaseData = _enemyBaseDataCollection.GetRandomEnemy();
        enemyBattleCard.InitCard(newEnemyBaseData);
    }

    /// <summary>
    /// Checks the availability of any alive heroes
    /// </summary>
    /// <returns></returns>
    public bool AnyHeroesAvailable()
    {
        foreach (HeroBattleCard heroBattleCard in heroBattleCards)
        {
            if (!heroBattleCard.IsDead())
                return true;
        }

        return false;
    }

    /// <summary>
    /// Gets the IDs of the heroes still alive
    /// </summary>
    /// <returns></returns>
    public List<int> GetWinnerHeroIDs()
    {
        List<int> winnerHeroes = new List<int>();
        foreach (HeroBattleCard heroBattleCard in heroBattleCards)
        {
            if (!heroBattleCard.IsDead())
                winnerHeroes.Add(heroBattleCard.GetHero().GetID());
        }
        return winnerHeroes;
    }
}
