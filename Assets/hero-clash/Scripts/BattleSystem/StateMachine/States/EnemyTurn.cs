using System.Collections;
using UnityEngine;

/// <summary>
/// The state where the enemy attacks
/// </summary>
internal class EnemyTurn : State
{
    public EnemyTurn(BattleSystem battleSystem) : base(battleSystem)
    {
    }

    /// <summary>
    /// Selects random hero and attacks it
    /// At the end checks if there are any heroes alive, changes battlesystem state accordingly
    /// </summary>
    public override IEnumerator Start()
    {
        battleSystem.battleUI.PrintMessage("Enemy Turn.");
        battleSystem.battleUI.SetBackImages(false);

        yield return new WaitForSeconds(1.5f);

        HeroBattleCard heroBattleCard = SelectRandomHero();
        EnemyBattleCard enemyBattleCard = battleSystem.enemyBattleCard;

        string enemyName = enemyBattleCard.GetBaseData().GetEnemyName();

        float attackPower = enemyBattleCard.GetAttackPower();
        heroBattleCard.Damage(attackPower);

        enemyBattleCard.AnimateBattleCard(InteractionType.attack);

        enemyBattleCard.damageDisplay.ShowText(InteractionType.attack, attackPower);
        heroBattleCard.damageDisplay.ShowText(InteractionType.damage, attackPower);

        battleSystem.battleUI.PrintMessage(enemyName + " attacks " + heroBattleCard.GetHero().GetName() + " with Attack Power: " + battleSystem.enemyBattleCard.GetAttackPower());

        heroBattleCard.RefreshCard();

        yield return new WaitForSeconds(3f);

        bool allHeroesDead = !battleSystem.AnyHeroesAvailable();
        if (allHeroesDead)
            battleSystem.SetState(new Lost(battleSystem));
        else
            battleSystem.SetState(new PlayerTurn(battleSystem));
    }

    /// <summary>
    /// Selects a random hero battle card for the enemy to attack to
    /// </summary>
    private HeroBattleCard SelectRandomHero()
    {
        int randomIndex = Random.Range(0, battleSystem.heroBattleCards.Count);
        HeroBattleCard heroBattleCard = battleSystem.heroBattleCards[randomIndex];

        if (heroBattleCard.IsDead())
            return SelectRandomHero();
        else
            return heroBattleCard;
    }
}