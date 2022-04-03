using System.Collections;
using UnityEngine;

public class PlayerTurn : State
{
    public PlayerTurn(BattleSystem battleSystem) : base(battleSystem)
    {
    }

    public override IEnumerator Start()
    {
        battleSystem.battleUI.PrintMessage("Tap on a hero to attack with.");
        battleSystem.battleUI.SetAllInput(true);
        yield break;
    }

    public override IEnumerator Attack(HeroBattleCard heroBattleCard)
    {
        string heroName = heroBattleCard.GetHero().characterName;
        float attackPower = heroBattleCard.GetAttackPower();
        EnemyBattleCard enemyBattleCard = battleSystem.enemyBattleCard;

        bool isDead = enemyBattleCard.Damage(heroBattleCard.GetAttackPower());

        battleSystem.battleUI.PrintMessage("The hero " + heroName + " attacked " + enemyBattleCard.GetBaseData().GetEnemyName() + " with Attack Power: " + heroBattleCard.GetAttackPower());
        enemyBattleCard.damageDisplay.ShowText(InteractionType.damage, attackPower);
        heroBattleCard.damageDisplay.ShowText(InteractionType.attack, attackPower);

        battleSystem.enemyBattleCard.RefreshCard();

        battleSystem.battleUI.SetAllInput(false);

        yield return new WaitForSeconds(2f);

        if (isDead)
            battleSystem.SetState(new Won(battleSystem));
        else
            battleSystem.SetState(new EnemyTurn(battleSystem));
    }
}