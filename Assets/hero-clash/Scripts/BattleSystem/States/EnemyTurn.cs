using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class EnemyTurn : State
{
    public EnemyTurn(BattleSystem battleSystem) : base(battleSystem)
    {
    }

    public override IEnumerator Start()
    {
        battleSystem.battleUI.PrintMessage("Enemy Turn.");

        yield return new WaitForSeconds(2f);

        HeroBattleCard heroBattleCard = SelectRandomHero();
        float attackPower = battleSystem.enemyBattleCard.GetAttackPower();
        string enemyName = battleSystem.enemyBattleCard.GetBaseData().GetEnemyName();
        heroBattleCard.Damage(attackPower);

        battleSystem.enemyBattleCard.AnimateBattleCard(InteractionType.attack);
        battleSystem.enemyBattleCard.damageDisplay.ShowText(InteractionType.attack, attackPower);
        heroBattleCard.damageDisplay.ShowText(InteractionType.damage, attackPower);

        battleSystem.battleUI.PrintMessage(enemyName + " attacks " + heroBattleCard.GetHero().characterName + " with Attack Power: " + battleSystem.enemyBattleCard.GetAttackPower());

        heroBattleCard.RefreshCard();

        yield return new WaitForSeconds(3f);

        bool allHeroesDead = !battleSystem.AnyHeroesAvailable();
        if (allHeroesDead)
            battleSystem.SetState(new Lost(battleSystem));
        else
            battleSystem.SetState(new PlayerTurn(battleSystem));
    }

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