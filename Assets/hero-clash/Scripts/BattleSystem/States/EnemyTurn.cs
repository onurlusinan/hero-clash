using System.Collections;
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

        int randomIndex = Random.Range(0, battleSystem.heroBattleCards.Count - 1);
        HeroBattleCard heroBattleCard = battleSystem.heroBattleCards[randomIndex];
        Hero hero = heroBattleCard.GetHero();
        bool isHeroDead = heroBattleCard.Damage(battleSystem.enemyBattleCard.GetAttackPower());

        battleSystem.battleUI.PrintMessage("Enemy attacks " + hero.characterName + " with Attack Power: " + battleSystem.enemyBattleCard.GetAttackPower());
        heroBattleCard.RefreshCard();
        yield return new WaitForSeconds(2f);

        if (isHeroDead)
            battleSystem.SetState(new Lost(battleSystem));
        else
            battleSystem.SetState(new PlayerTurn(battleSystem));
    }
}