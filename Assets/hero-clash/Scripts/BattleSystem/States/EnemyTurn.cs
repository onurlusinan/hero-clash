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

        int randomIndex = Random.Range(0, battleSystem.battleHeroes.Count - 1);
        BattleHero battleHero = battleSystem.battleHeroes[randomIndex];
        Hero hero = battleHero.GetHero();
        bool isHeroDead = battleHero.Damage(battleSystem.enemy.attackPower);

        battleSystem.battleUI.PrintMessage("Enemy attacks " + hero.characterName + " with Attack Power: " + battleSystem.enemy.attackPower);
        battleHero.healthBar.SetHealthBar(battleHero.GetCurrentHealth(), hero.health);
        yield return new WaitForSeconds(2f);

        if (isHeroDead)
            battleSystem.SetState(new Lost(battleSystem));
        else
            battleSystem.SetState(new PlayerTurn(battleSystem));
    }
}