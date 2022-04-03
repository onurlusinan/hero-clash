using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Begin : State
{
    public Begin(BattleSystem battleSystem) : base(battleSystem)
    {
    }

    public override IEnumerator Start()
    {
        battleSystem.battleUI.SetAllInput(false);
        battleSystem.battleUI.PrintMessage("Another epic battle begins!");

        yield return new WaitForSeconds(3f);
        battleSystem.SetState(new PlayerTurn(battleSystem));
    }
}
