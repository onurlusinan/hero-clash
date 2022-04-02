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

        battleSystem.battleUI.PrintMessage("Enemy attacks a hero!");
        yield return new WaitForSeconds(2f);

        bool isHeroDead = false; 

        if (isHeroDead)
            battleSystem.SetState(new Lost(battleSystem));
        else
            battleSystem.SetState(new PlayerTurn(battleSystem));
    }
}