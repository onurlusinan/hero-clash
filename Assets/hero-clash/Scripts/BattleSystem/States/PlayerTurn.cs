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

        yield return new WaitForSeconds(2f);

        battleSystem.battleUI.SetAllInput(true);
    }

    public override IEnumerator Attack(Hero hero)
    {
        battleSystem.battleUI.PrintMessage("The hero " + hero.characterName + " attacked the enemy with Attack Power: " + hero.attackPower);
        bool isDead = battleSystem.enemyBattleCard.Damage(hero.attackPower);
        battleSystem.enemyBattleCard.RefreshCard();

        battleSystem.battleUI.SetAllInput(false);

        yield return new WaitForSeconds(2f);

        if (isDead)
            battleSystem.SetState(new Won(battleSystem));
        else
            battleSystem.SetState(new EnemyTurn(battleSystem));
    }
}